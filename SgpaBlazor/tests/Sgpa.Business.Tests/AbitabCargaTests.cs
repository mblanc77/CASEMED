using System;
using System.Collections.Generic;
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
/// Integración contra NewSgpa2: la carga batch de pagos Abitab (port de cAdmPago.ProcesarPagos). Siembra
/// un mapeo de columnas temporal (MapeoAbitab está vacía en NewSgpa2), procesa un archivo en memoria con
/// líneas válidas e inválidas y verifica los efectos y el log de errores. Restaura el mapeo al terminar.
/// </summary>
[Collection("PrestamosDb")]
public class AbitabCargaTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    // Layout sintético del archivo (coincide con AbitabPagoParserTests).
    private static readonly (string Campo, int Inicio, int Largo)[] Layout =
    {
        ("NroFactura", 1, 10), ("FechaPago", 11, 8), ("Importe", 19, 12),
        ("ImporteCobrado", 31, 12), ("NroAgencia", 43, 4), ("NroSubAgencia", 47, 4),
    };

    private static string Linea(long nroFactura, DateTime fecha, double importe, double cobrado, int ag, int subag)
        => nroFactura.ToString().PadLeft(10, '0')
         + fecha.ToString("ddMMyyyy")
         + ((long)Math.Round(importe * 100)).ToString().PadLeft(12, '0')
         + ((long)Math.Round(cobrado * 100)).ToString().PadLeft(12, '0')
         + ag.ToString().PadLeft(4, '0')
         + subag.ToString().PadLeft(4, '0');

    [Fact]
    public async Task Procesa_lineas_validas_y_registra_las_invalidas()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var user = new DefaultCurrentUser();
        var pago = new PagoService(db, user);
        var grabado = new PrestamoGrabadoService(db, user);
        var repo = new PrestamoRepository(db);

        var previo = await db.QueryAsync<AbitabCampo>("SELECT Campo, Inicio, Largo FROM dbo.MapeoAbitab");
        await SeedMapeoAsync(db);

        var fecha = DateTime.Today;
        GrabarPrestamoResultado? alta = null;
        try
        {
            alta = await EmitirAsync(grabado, repo, db, cuotas: 2, importe: 20000);
            var facturas = (await pago.GetFacturasAsync(alta.IDPrestamo)).OrderBy(f => f.NroFactura).ToList();

            var lineas = facturas.Select(f => Linea(f.NroFactura, fecha, f.Importe, f.Importe, 1, 2)).ToList();
            lineas.Add(Linea(99999999, fecha, 1000, 1000, 1, 2));   // factura inexistente

            var r = await pago.ProcesarPagosAsync(lineas, fecha);

            Assert.Equal(3, r.Procesados);
            Assert.Equal(1, r.ConError);
            Assert.Equal(2, r.Ok);
            Assert.Contains(r.Errores, e => e.Resultado == ResultadoPago.FacturaInexistente);

            foreach (var f in facturas)
            {
                var est = await db.ExecuteScalarAsync<string>("SELECT CodFacturaEstado FROM dbo.SP_Factura WHERE IDFactura=@id", new { id = f.IDFactura });
                Assert.Equal("can", est);
            }
            var estPres = await db.ExecuteScalarAsync<string>("SELECT CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = alta.IDPrestamo });
            Assert.Equal("can", estPres);

            var errLog = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.ErrCargaAbitab WHERE Fecha=@f", new { f = fecha });
            Assert.True(errLog >= 1);
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.ErrCargaAbitab WHERE Fecha=@f", new { f = fecha });
            if (alta is not null) await LimpiarAsync(db, alta.IDPrestamo);
            await RestoreMapeoAsync(db, previo);
        }
    }

    [Fact]
    public async Task Sin_mapeo_configurado_lanza()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var pago = new PagoService(db, new DefaultCurrentUser());

        var previo = await db.QueryAsync<AbitabCampo>("SELECT Campo, Inicio, Largo FROM dbo.MapeoAbitab");
        await db.ExecuteAsync("DELETE FROM dbo.MapeoAbitab");
        try
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => pago.ProcesarPagosAsync(new[] { "linea" }, DateTime.Today));
        }
        finally { await RestoreMapeoAsync(db, previo); }
    }

    private static async Task SeedMapeoAsync(IDbExecutor db)
    {
        await db.ExecuteAsync("DELETE FROM dbo.MapeoAbitab");
        foreach (var (campo, inicio, largo) in Layout)
            await db.ExecuteAsync(
                "INSERT INTO dbo.MapeoAbitab (Campo, Inicio, Largo, CodigoBarra, Usr, Ts) VALUES (@campo, @inicio, @largo, 0, 'qa', @ts)",
                new { campo, inicio, largo, ts = DateTime.Now });
    }

    private static async Task RestoreMapeoAsync(IDbExecutor db, IReadOnlyList<AbitabCampo> previo)
    {
        await db.ExecuteAsync("DELETE FROM dbo.MapeoAbitab");
        foreach (var c in previo)
            await db.ExecuteAsync(
                "INSERT INTO dbo.MapeoAbitab (Campo, Inicio, Largo, CodigoBarra, Usr, Ts) VALUES (@campo, @inicio, @largo, 0, 'qa', @ts)",
                new { campo = c.Campo, inicio = c.Inicio, largo = c.Largo, ts = DateTime.Now });
    }

    private static async Task<GrabarPrestamoResultado> EmitirAsync(
        PrestamoGrabadoService grabado, PrestamoRepository repo, IDbExecutor db, int cuotas, double importe)
    {
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
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
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago_ItemPago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Pago WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_PagoError WHERE IDFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_FacturaDetalle WHERE IdFactura IN (SELECT IDFactura FROM dbo.SP_Factura WHERE IdPrestamo=@id)", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_CuadroAmortizacion WHERE IDPrestamo=@id", new { id = idPrestamo });
        await db.ExecuteAsync("DELETE FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo });
    }
}
