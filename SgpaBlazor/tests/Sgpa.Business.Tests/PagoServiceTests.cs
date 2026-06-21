using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Pagos;
using Sgpa.Business.Prestamos;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el ingreso de pagos (port de cAdmPago.Ingresar). Cada test emite un
/// préstamo real, cobra sus facturas y verifica los efectos (factura/cuota/préstamo/pago), limpiando todo.
/// </summary>
[Collection("PrestamosDb")]
public class PagoServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PagoService Pago, PrestamoGrabadoService Grabado, PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var user = new DefaultCurrentUser();
        return (new PagoService(db, user), new PrestamoGrabadoService(db, user), new PrestamoRepository(db), db);
    }

    private static async Task<GrabarPrestamoResultado> EmitirAsync(
        PrestamoGrabadoService grabado, PrestamoRepository repo, IDbExecutor db, int cuotas, double importe)
    {
        var ci = await db.ExecuteScalarAsync<long>(
            "SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var m = (await repo.GetMonedasAsync()).First(x => x.CodMoneda == "$");
        var tipo = await db.ExecuteScalarAsync<string>("SELECT TOP 1 CodPrestamoTipo FROM dbo.SP_PrestamoTipo ORDER BY CodPrestamoTipo");
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(cuotas, m.Tasa, importe);

        return await grabado.GrabarAsync(new GrabarPrestamoRequest
        {
            CI = ci,
            CodMoneda = m.CodMoneda,
            CodPrestamoTipo = tipo!,
            Importe = importe,
            Cuotas = cuotas,
            ImporteCuota = cuadro[0].Importe,
            Tasa = m.Tasa,
            Promedio = 0,
            Fecha = DateTime.Today,
            FechaCobro = DateTime.Today,
            Cuadro = cuadro,
        });
    }

    private static async Task LimpiarAsync(IDbExecutor db, int idPrestamo)
    {
        await db.ExecuteAsync(
            "DELETE FROM dbo.SP_Pago_ItemPago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync(
            "DELETE FROM dbo.SP_Pago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync(
            "DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
    }

    [Fact]
    public async Task Pagar_primera_cuota_cancela_factura_y_cuota_y_pasa_a_en_proceso()
    {
        var (pago, grabado, repo, db) = Build();
        var alta = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var fac1 = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).First();

            var r = await pago.IngresarPagoAsync(new IngresarPagoRequest(fac1.NroFactura, DateTime.Today, fac1.Importe));
            Assert.Equal(ResultadoPago.Ok, r);

            var estFac = await db.ExecuteScalarAsync<string>("SELECT CodFacturaEstado FROM dbo.SP_Factura WHERE IDFactura=@id", new { id = fac1.IDFactura });
            Assert.Equal("can", estFac);
            var estCuota = await db.ExecuteScalarAsync<string>("SELECT CodCuotaEstado FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND Nro=1", new { id = alta.IDPrestamo });
            Assert.Equal("can", estCuota);

            var pagos = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Pago WHERE IDFactura=@id", new { id = fac1.IDFactura });
            Assert.Equal(1, pagos);

            var cuotasPagas = await db.ExecuteScalarAsync<int>("SELECT CuotasPagas FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal(1, cuotasPagas);
            var estPres = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("pro", estPres);   // quedan cuotas: emi → pro (transición habilitada)
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Pagar_todas_las_cuotas_cancela_el_prestamo_con_saldo_cero()
    {
        var (pago, grabado, repo, db) = Build();
        var alta = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            foreach (var f in (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura))
                Assert.Equal(ResultadoPago.Ok, await pago.IngresarPagoAsync(new IngresarPagoRequest(f.NroFactura, DateTime.Today, f.Importe)));

            var estPres = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("can", estPres);
            var saldo = await db.ExecuteScalarAsync<double>("SELECT Saldo FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal(0, saldo, 2);
            var cuotasPagas = await db.ExecuteScalarAsync<int>("SELECT CuotasPagas FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal(2, cuotasPagas);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Pagar_factura_inexistente_devuelve_FacturaInexistente()
    {
        var (pago, _, _, _) = Build();
        var r = await pago.IngresarPagoAsync(new IngresarPagoRequest(NroFactura: -999999, DateTime.Today, 1000));
        Assert.Equal(ResultadoPago.FacturaInexistente, r);
    }

    [Fact]
    public async Task Repagar_una_factura_ya_cancelada_devuelve_EstadoIncorrecto_y_no_duplica_pago()
    {
        var (pago, grabado, repo, db) = Build();
        var alta = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var fac1 = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).First();
            Assert.Equal(ResultadoPago.Ok, await pago.IngresarPagoAsync(new IngresarPagoRequest(fac1.NroFactura, DateTime.Today, fac1.Importe)));

            var r = await pago.IngresarPagoAsync(new IngresarPagoRequest(fac1.NroFactura, DateTime.Today, fac1.Importe));
            Assert.Equal(ResultadoPago.EstadoIncorrecto, r);

            var pagos = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Pago WHERE IDFactura=@id", new { id = fac1.IDFactura });
            Assert.Equal(1, pagos);   // no se duplicó
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Mora_no_se_aplica_si_la_factura_no_esta_vencida()
    {
        var (pago, grabado, repo, db) = Build();
        var alta = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            var fac1 = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).First();
            // Vence a los 30 días: pagando hoy no hay atraso → mora 0.
            var mora = await pago.GetImporteMoraAsync(fac1.NroFactura, DateTime.Today);
            Assert.Equal(0, mora, 6);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }
}
