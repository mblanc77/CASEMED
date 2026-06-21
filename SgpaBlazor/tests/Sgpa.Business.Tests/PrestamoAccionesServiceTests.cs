using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Prestamos;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: acciones de toolbar de préstamos (anular / cancelación anticipada).
/// Cada test crea un préstamo, ejecuta la acción, verifica y limpia (try/finally).
/// </summary>
[Collection("PrestamosDb")]
public class PrestamoAccionesServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PrestamoGrabadoService Grabado, PrestamoAccionesService Acciones, PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var user = new DefaultCurrentUser();
        return (new PrestamoGrabadoService(db, user), new PrestamoAccionesService(db, user), new PrestamoRepository(db), db);
    }

    private static async Task<GrabarPrestamoRequest> BuildRequestAsync(IDbExecutor db, PrestamoRepository repo,
        int cuotas, double importe, DateTime? fechaCobro)
    {
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var m = (await repo.GetMonedasAsync()).First(x => x.CodMoneda == "$");
        var tipo = await db.ExecuteScalarAsync<string>("SELECT TOP 1 CodPrestamoTipo FROM dbo.SP_PrestamoTipo ORDER BY CodPrestamoTipo");
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(cuotas, m.Tasa, importe);
        return new GrabarPrestamoRequest
        {
            CI = ci, CodMoneda = m.CodMoneda, CodPrestamoTipo = tipo!, Importe = importe, Cuotas = cuotas,
            ImporteCuota = cuadro[0].Importe, Tasa = m.Tasa, Fecha = DateTime.Today, FechaCobro = fechaCobro, Cuadro = cuadro,
        };
    }

    private static async Task LimpiarAsync(IDbExecutor db, int id)
    {
        await db.ExecuteAsync("DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id });
    }

    [Fact]
    public async Task Anular_marca_prestamo_facturas_y_cuotas_como_anulados()
    {
        var (grabado, acciones, repo, db) = Build();
        var alta = await grabado.GrabarAsync(await BuildRequestAsync(db, repo, cuotas: 4, importe: 20000, fechaCobro: DateTime.Today));
        try
        {
            await acciones.AnularAsync(alta.IDPrestamo);

            var estado = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("anu", estado);
            var facturasNoAnu = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado<>'anu'", new { id = alta.IDPrestamo });
            Assert.Equal(0, facturasNoAnu);
            var cuotasNoAnu = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND CodCuotaEstado<>'anu'", new { id = alta.IDPrestamo });
            Assert.Equal(0, cuotasNoAnu);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Cancelar_anula_facturas_emitidas_y_genera_factura_de_cancelacion()
    {
        var (grabado, acciones, repo, db) = Build();
        var alta = await grabado.GrabarAsync(await BuildRequestAsync(db, repo, cuotas: 3, importe: 18000, fechaCobro: DateTime.Today));
        try
        {
            var r = await acciones.CancelarAsync(alta.IDPrestamo);

            Assert.True(r.Importe >= 18000d * 0.99); // ≈ saldo (≈ importe) + interés; al menos el saldo
            Assert.True(r.NroFactura > 0);

            // Las 3 facturas de cuota quedaron anuladas; la de cancelación es nueva, emitida y tipo "can".
            var anuladas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado='anu'", new { id = alta.IDPrestamo });
            Assert.Equal(3, anuladas);
            var cancelacion = await db.QuerySingleOrDefaultAsync<int>(
                "SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaTipo='can' AND CodFacturaEstado='emi'", new { id = alta.IDPrestamo });
            Assert.Equal(1, cancelacion);
            var detalle = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM dbo.SP_FacturaDetalle WHERE IdFactura=@idf AND CodItemPago='can'", new { idf = r.IDFactura });
            Assert.Equal(1, detalle);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }
}
