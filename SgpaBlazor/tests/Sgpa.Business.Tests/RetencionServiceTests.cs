using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Pagos;
using Sgpa.Business.Prestamos;
using Sgpa.Business.Retenciones;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el ciclo de retenciones (port de cAdmRetencion). Emite un préstamo, retiene
/// una factura, amortiza la cuenta corriente y deshace todo, verificando los efectos y limpiando.
/// </summary>
[Collection("PrestamosDb")]
public class RetencionServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (RetencionService Ret, PagoService Pago, PrestamoGrabadoService Grabado, PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var user = new DefaultCurrentUser();
        var pago = new PagoService(db, user);
        return (new RetencionService(db, user, pago), pago, new PrestamoGrabadoService(db, user), new PrestamoRepository(db), db);
    }

    private static async Task<(GrabarPrestamoResultado Alta, long CI)> EmitirAsync(
        PrestamoGrabadoService grabado, PrestamoRepository repo, IDbExecutor db, int cuotas, double importe)
    {
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var m = (await repo.GetMonedasAsync()).First(x => x.CodMoneda == "$");
        var tipo = await db.ExecuteScalarAsync<string>("SELECT TOP 1 CodPrestamoTipo FROM dbo.SP_PrestamoTipo ORDER BY CodPrestamoTipo");
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(cuotas, m.Tasa, importe);
        var alta = await grabado.GrabarAsync(new GrabarPrestamoRequest
        {
            CI = ci, CodMoneda = m.CodMoneda, CodPrestamoTipo = tipo!, Importe = importe, Cuotas = cuotas,
            ImporteCuota = cuadro[0].Importe, Tasa = m.Tasa, Promedio = 0, Fecha = DateTime.Today,
            FechaCobro = DateTime.Today, Cuadro = cuadro,
        });
        return (alta, ci);
    }

    private static async Task LimpiarAsync(IDbExecutor db, int idPrestamo)
    {
        await db.ExecuteAsync("DELETE FROM dbo.SP_RetencionItem WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_RetencionPago WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_RetencionAviso WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Retencion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_RetencionPrestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago_ItemPago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
    }

    private static async Task<(int Id, DateTime Fecha, double Importe)> RetenerPrimeraFacturaAsync(
        RetencionService ret, IDbExecutor db, GrabarPrestamoResultado alta, long ci)
    {
        var fecha = DateTime.Today;
        var facturas = await ret.GetFacturasParaRetenerAsync(alta.IDPrestamo, fecha, directa: true);
        var f = facturas.OrderBy(x => x.NroFactura).First();
        await ret.IngresarAsync(new IngresarRetencionRequest(
            alta.IDPrestamo, ci, fecha, CodEmpresa: null, CodMoneda: "$", TipoCambio: 1,
            Facturas: new List<FacturaRetencion> { new(f.NroFactura, f.Importe, f.ImpMora) },
            ImpTelegrama: 0, Observaciones: "test", Directa: true));
        return (alta.IDPrestamo, fecha, f.Importe);
    }

    [Fact]
    public async Task Ingresar_retiene_la_factura_y_arma_la_cuenta_corriente()
    {
        var (ret, _, grabado, repo, db) = Build();
        var (alta, ci) = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var (id, _, importe) = await RetenerPrimeraFacturaAsync(ret, db, alta, ci);

            var retenidas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado='ret'", new { id });
            Assert.Equal(1, retenidas);
            var estPres = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id });
            Assert.Equal("ret", estPres);   // queda una cuota pendiente → emi→ret

            var cc = await ret.GetCuentaCorrienteAsync(id);
            Assert.NotNull(cc);
            Assert.Equal(importe, cc!.Importe, 2);
            Assert.Equal(importe, cc.Saldo, 2);
            Assert.Equal(0, cc.ImpPago, 2);

            var items = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_RetencionItem WHERE IDPrestamo=@id", new { id });
            Assert.Equal(1, items);   // 1 factura (el telegrama no genera ítem por la FK)
            var pagos = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Pago p JOIN dbo.SP_Factura f ON f.IDFactura=p.IDFactura WHERE f.IdPrestamo=@id AND p.CodPagoOrigen='ret'", new { id });
            Assert.Equal(1, pagos);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task IngresarPago_y_BorrarPago_ajustan_el_saldo()
    {
        var (ret, _, grabado, repo, db) = Build();
        var (alta, ci) = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var (id, fecha, importe) = await RetenerPrimeraFacturaAsync(ret, db, alta, ci);

            await ret.IngresarPagoAsync(id, fecha, mes: 6, anio: 2026, importe: 5000);
            var cc = await ret.GetCuentaCorrienteAsync(id);
            Assert.Equal(importe - 5000, cc!.Saldo, 2);
            Assert.Equal(5000, cc.ImpPago, 2);
            Assert.Single(await ret.GetPagosAsync(id));

            await ret.BorrarPagoAsync(id, fecha, mes: 6, anio: 2026);
            cc = await ret.GetCuentaCorrienteAsync(id);
            Assert.Equal(importe, cc!.Saldo, 2);
            Assert.Equal(0, cc.ImpPago, 2);
            Assert.Empty(await ret.GetPagosAsync(id));
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Borrar_deshace_la_retencion_y_devuelve_la_factura_a_emitida()
    {
        var (ret, _, grabado, repo, db) = Build();
        var (alta, ci) = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var (id, fecha, _) = await RetenerPrimeraFacturaAsync(ret, db, alta, ci);

            await ret.BorrarAsync(id, fecha);

            var emitidas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado='emi'", new { id });
            Assert.Equal(2, emitidas);   // la retenida volvió a emitida
            var pendientes = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND CodCuotaEstado='pen'", new { id });
            Assert.Equal(2, pendientes);
            var estPres = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id });
            Assert.Equal("emi", estPres);
            var cuotasPagas = await db.ExecuteScalarAsync<int>("SELECT CuotasPagas FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id });
            Assert.Equal(0, cuotasPagas);

            Assert.Empty(await ret.GetRetencionesAsync(id));
            var items = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_RetencionItem WHERE IDPrestamo=@id", new { id });
            Assert.Equal(0, items);
            var pagos = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Pago p JOIN dbo.SP_Factura f ON f.IDFactura=p.IDFactura WHERE f.IdPrestamo=@id", new { id });
            Assert.Equal(0, pagos);
            var cc = await ret.GetCuentaCorrienteAsync(id);
            Assert.Equal(0, cc!.Saldo, 2);
            Assert.Equal(0, cc.Importe, 2);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task IngresarAviso_registra_el_comentario()
    {
        var (ret, _, grabado, repo, db) = Build();
        var (alta, _) = await EmitirAsync(grabado, repo, db, cuotas: 1, importe: 10000);
        try
        {
            await ret.IngresarAvisoAsync(alta.IDPrestamo, DateTime.Today, "revisar con la empresa");
            var avisos = await ret.GetAvisosAsync(alta.IDPrestamo);
            Assert.Single(avisos);
            Assert.Contains("revisar", avisos[0].Comentario);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }
}
