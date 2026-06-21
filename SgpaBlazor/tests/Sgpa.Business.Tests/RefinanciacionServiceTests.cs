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
/// Integración contra NewSgpa2: la refinanciación (port de cAdmPrestamo.Refinanciar). Emite un préstamo,
/// lo lleva a "en proceso" (pagando una factura), lo refinancia y verifica el saldado del viejo y el
/// préstamo nuevo. Limpia ambos préstamos.
/// </summary>
[Collection("PrestamosDb")]
public class RefinanciacionServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (RefinanciacionService Refi, PagoService Pago, PrestamoGrabadoService Grabado, PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var user = new DefaultCurrentUser();
        var pago = new PagoService(db, user);
        return (new RefinanciacionService(db, user, pago), pago, new PrestamoGrabadoService(db, user), new PrestamoRepository(db), db);
    }

    private static async Task<(GrabarPrestamoResultado Alta, double Tasa)> EmitirAsync(
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
        return (alta, m.Tasa);
    }

    private static async Task LimpiarAsync(IDbExecutor db, int idPrestamo)
    {
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago_ItemPago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
    }

    /// <summary>Emite y paga la primera factura → el préstamo queda "en proceso" (refinanciable).</summary>
    private static async Task<(GrabarPrestamoResultado Alta, double Tasa)> EmitirYEnProcesoAsync(
        PagoService pago, PrestamoGrabadoService grabado, PrestamoRepository repo, IDbExecutor db, int cuotas, double importe)
    {
        var (alta, tasa) = await EmitirAsync(grabado, repo, db, cuotas, importe);
        var f1 = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).First();
        Assert.Equal(ResultadoPago.Ok, await pago.IngresarPagoAsync(new IngresarPagoRequest(f1.NroFactura, DateTime.Today, f1.Importe)));
        return (alta, tasa);
    }

    [Fact]
    public async Task Puede_refinanciar_solo_en_proceso_o_retencion()
    {
        var (refi, pago, grabado, repo, db) = Build();
        var (alta, _) = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
        try
        {
            Assert.False(await refi.PuedeRefinanciarAsync(alta.IDPrestamo));   // emitido
            var f1 = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).First();
            await pago.IngresarPagoAsync(new IngresarPagoRequest(f1.NroFactura, DateTime.Today, f1.Importe));
            Assert.True(await refi.PuedeRefinanciarAsync(alta.IDPrestamo));    // en proceso
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Refinanciar_salda_el_viejo_y_crea_el_nuevo_ingresado()
    {
        var (refi, pago, grabado, repo, db) = Build();
        var (alta, tasa) = await EmitirYEnProcesoAsync(pago, grabado, repo, db, cuotas: 3, importe: 30000);
        var nuevoId = 0;
        try
        {
            // Importe esperado: amortización de las 2 facturas que aún no vencen (cuotas 2 y 3).
            var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(3, tasa, 30000);
            var esperado = cuadro[1].Amortizacion + cuadro[2].Amortizacion;
            Assert.Equal(esperado, await refi.CalcularImporteRefinanciadoAsync(alta.IDPrestamo, DateTime.Today), 2);

            var r = await refi.RefinanciarAsync(alta.IDPrestamo, DateTime.Today, cuotas: 4, mismaTasa: true);
            nuevoId = r.NuevoIDPrestamo;
            Assert.Equal(esperado, r.Importe, 2);

            // Viejo: refinanciado, saldo 0, facturas emitidas pasaron a "car", cuotas pendientes a "car".
            var estViejo = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("car", estViejo);
            var saldoViejo = await db.ExecuteScalarAsync<double>("SELECT Saldo FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal(0, saldoViejo, 2);
            var emitidas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado='emi'", new { id = alta.IDPrestamo });
            Assert.Equal(0, emitidas);
            var refinFacturas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado='car'", new { id = alta.IDPrestamo });
            Assert.Equal(2, refinFacturas);
            var pagosRef = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Pago p JOIN dbo.SP_Factura f ON f.IDFactura=p.IDFactura WHERE f.IdPrestamo=@id AND p.CodPagoOrigen='ref'", new { id = alta.IDPrestamo });
            Assert.Equal(2, pagosRef);

            // Nuevo: ingresado, referencia al viejo, importe refinanciado, 4 cuotas con su cuadro.
            var estNuevo = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = nuevoId });
            Assert.Equal("ing", estNuevo);
            var refId = await db.ExecuteScalarAsync<int>("SELECT IDPrestamoRef FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = nuevoId });
            Assert.Equal(alta.IDPrestamo, refId);
            var impNuevo = await db.ExecuteScalarAsync<double>("SELECT Importe FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = nuevoId });
            Assert.Equal(esperado, impNuevo, 2);
            var cuotasNuevo = await db.ExecuteScalarAsync<int>("SELECT Cuotas FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = nuevoId });
            Assert.Equal(4, cuotasNuevo);
            var cuadroNuevo = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = nuevoId });
            Assert.Equal(4, cuadroNuevo);
        }
        finally
        {
            if (nuevoId != 0) await LimpiarAsync(db, nuevoId);
            await LimpiarAsync(db, alta.IDPrestamo);
        }
    }
}
