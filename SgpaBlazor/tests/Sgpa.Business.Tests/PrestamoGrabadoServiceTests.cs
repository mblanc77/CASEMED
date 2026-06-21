using System;
using System.Threading.Tasks;
using Sgpa.Business.Prestamos;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el grabado de préstamos (alta) — préstamo + cuadro de amortización y,
/// al emitir, cuotas + facturas + detalle. Cada test limpia lo que inserta (try/finally).
/// </summary>
[Collection("PrestamosDb")]
public class PrestamoGrabadoServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PrestamoGrabadoService Svc, PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new PrestamoGrabadoService(db, new DefaultCurrentUser()), new PrestamoRepository(db), db);
    }

    private static async Task<GrabarPrestamoRequest> BuildRequestAsync(IDbExecutor db, PrestamoRepository repo,
        int cuotas, double importe, DateTime? fechaCobro)
    {
        var ci = await db.ExecuteScalarAsync<long>(
            "SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var monedas = await repo.GetMonedasAsync();
        var m = System.Linq.Enumerable.First(monedas, x => x.CodMoneda == "$");
        var tipo = await db.ExecuteScalarAsync<string>("SELECT TOP 1 CodPrestamoTipo FROM dbo.SP_PrestamoTipo ORDER BY CodPrestamoTipo");
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(cuotas, m.Tasa, importe);

        return new GrabarPrestamoRequest
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
            FechaCobro = fechaCobro,
            Cuadro = cuadro,
        };
    }

    private static async Task LimpiarAsync(IDbExecutor db, int idPrestamo)
    {
        await db.ExecuteAsync(
            "DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
    }

    [Fact]
    public async Task Alta_sin_fecha_de_cobro_queda_ingresado_y_graba_el_cuadro()
    {
        var (svc, repo, db) = Build();
        var req = await BuildRequestAsync(db, repo, cuotas: 6, importe: 30000, fechaCobro: null);

        var r = await svc.GrabarAsync(req);
        try
        {
            Assert.Equal("ing", r.CodPrestamoEstado);
            Assert.Equal(0, r.CuotasGeneradas);
            Assert.Equal(0, r.FacturasGeneradas);

            var estado = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal("ing", estado);
            var saldo = await db.ExecuteScalarAsync<double>("SELECT Saldo FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal(30000, saldo, 2);

            var cuadro = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal(6, cuadro);
            var cuotas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal(0, cuotas); // no se emitió: sin cuotas/facturas
        }
        finally { await LimpiarAsync(db, r.IDPrestamo); }
    }

    [Fact]
    public async Task Editar_ingresado_con_fecha_de_cobro_lo_emite_y_genera_cuotas()
    {
        var (svc, repo, db) = Build();
        var alta = await svc.GrabarAsync(await BuildRequestAsync(db, repo, cuotas: 5, importe: 25000, fechaCobro: null));
        try
        {
            Assert.Equal("ing", alta.CodPrestamoEstado);

            var edit = await svc.EditarAsync(new EditarPrestamoRequest
            {
                IDPrestamo = alta.IDPrestamo,
                Fecha = DateTime.Today,
                FechaCobro = DateTime.Today,
                Observaciones = "emitido al editar",
            });

            Assert.Equal("emi", edit.CodPrestamoEstado);
            Assert.Equal(5, edit.CuotasGeneradas);
            Assert.Equal(5, edit.FacturasGeneradas);

            var estado = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("emi", estado);
            var cuotas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal(5, cuotas);
            var obs = await db.ExecuteScalarAsync<string>("SELECT Observaciones FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("emitido al editar", obs);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Editar_emitido_sin_cambiar_fecha_actualiza_datos_sin_regenerar()
    {
        var (svc, repo, db) = Build();
        var alta = await svc.GrabarAsync(await BuildRequestAsync(db, repo, cuotas: 3, importe: 18000, fechaCobro: DateTime.Today));
        try
        {
            Assert.Equal("emi", alta.CodPrestamoEstado);
            var facturasAntes = await db.QueryAsync<int>("SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id ORDER BY IDFactura", new { id = alta.IDPrestamo });

            var edit = await svc.EditarAsync(new EditarPrestamoRequest
            {
                IDPrestamo = alta.IDPrestamo,
                Fecha = DateTime.Today,
                FechaCobro = DateTime.Today,           // misma fecha → no se re-emite
                Banco = "BROU",
                Observaciones = "actualizado",
            });

            Assert.Equal("emi", edit.CodPrestamoEstado);
            Assert.Equal(0, edit.CuotasGeneradas);     // no regeneró
            Assert.Equal(0, edit.FacturasGeneradas);

            var facturasDespues = await db.QueryAsync<int>("SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id ORDER BY IDFactura", new { id = alta.IDPrestamo });
            Assert.Equal(facturasAntes, facturasDespues);   // mismas facturas, no se borraron/recrearon
            var banco = await db.ExecuteScalarAsync<string>("SELECT Banco FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("BROU", banco);
        }
        finally { await LimpiarAsync(db, alta.IDPrestamo); }
    }

    [Fact]
    public async Task Alta_con_fecha_de_cobro_emite_y_genera_cuotas_facturas_y_detalle()
    {
        var (svc, repo, db) = Build();
        var req = await BuildRequestAsync(db, repo, cuotas: 4, importe: 20000, fechaCobro: DateTime.Today);

        var r = await svc.GrabarAsync(req);
        try
        {
            Assert.Equal("emi", r.CodPrestamoEstado);
            Assert.Equal(4, r.CuotasGeneradas);
            Assert.Equal(4, r.FacturasGeneradas);

            var cuotas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal(4, cuotas);
            var facturas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = r.IDPrestamo });
            Assert.Equal(4, facturas);
            var detalle = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = r.IDPrestamo });
            Assert.Equal(4, detalle);

            // Toda factura emitida lleva código de barras y la primera cuota vence a los 30 días del cobro.
            var sinBarra = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM dbo.SP_Factura WHERE IdPrestamo=@id AND (CodigoBarra IS NULL OR CodigoBarra='')", new { id = r.IDPrestamo });
            Assert.Equal(0, sinBarra);
            var primerVenc = await db.ExecuteScalarAsync<DateTime>(
                "SELECT FechaVencimiento FROM dbo.SP_Cuota WHERE IDPrestamo=@id AND Nro=1", new { id = r.IDPrestamo });
            Assert.Equal(DateTime.Today.AddDays(30).Date, primerVenc.Date);
        }
        finally { await LimpiarAsync(db, r.IDPrestamo); }
    }
}
