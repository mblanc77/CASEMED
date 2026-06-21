using System;
using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: prima por fallecimiento (port del cluster Prima de AbmAfili).
/// El test de alta/baja usa una cédula que no esté en PrimaFallecimiento y limpia lo que inserta.
/// </summary>
public class PrimaServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PrimaService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new PrimaService(db, new DefaultCurrentUser()), db);
    }

    [Theory]
    [InlineData(202606, -1, 202605)]
    [InlineData(202601, -1, 202512)]   // cruce de año hacia atrás
    [InlineData(202612, 1, 202701)]    // cruce de año hacia adelante
    [InlineData(202606, -6, 202512)]
    public void AddMonth_suma_meses_con_cruce_de_anio(int anioMes, int n, int esperado)
        => Assert.Equal(esperado, PrimaService.AddMonth(anioMes, n));

    [Fact]
    public async Task Liquidar_es_el_minimo_entre_tope_y_imponibles_de_6_meses()
    {
        var (svc, db) = Build();

        // Cédula con imponibles concepto 1 y su último mes.
        var info = await db.QuerySingleOrDefaultAsync<CiMes>(
            @"SELECT TOP 1 CI, MAX(AnioMes) AS AnioMes FROM dbo.Imponible
              WHERE Concepto='1' GROUP BY CI HAVING COUNT(*) >= 6 ORDER BY CI");
        Assert.NotNull(info);

        // Fallecimiento el mes siguiente al último imponible → la ventana [mes-6, mes-1] cae sobre datos.
        var deathMonth = PrimaService.AddMonth(info!.AnioMes, 1);
        var death = new DateTime(deathMonth / 100, deathMonth % 100, 15);

        var mesFin = PrimaService.AddMonth(deathMonth, -1);
        var mesIni = PrimaService.AddMonth(deathMonth, -6);
        var suma = await db.ExecuteScalarAsync<double?>(
            "SELECT SUM(Importe) FROM dbo.Imponible WHERE CI=@ci AND AnioMes BETWEEN @ini AND @fin AND Concepto='1'",
            new { ci = (int)info.CI, ini = mesIni, fin = mesFin }) ?? 0d;
        var p = await db.QuerySingleOrDefaultAsync<TopeUr>("SELECT TOP 1 CAST(TopePrima AS float) TopePrima, CAST(UR AS float) UR FROM dbo.Parametros");
        var esperado = Math.Min(p!.TopePrima * p.UR, suma);

        Assert.Equal(esperado, await svc.LiquidarAsync(info.CI, death), 2);
    }

    [Fact]
    public async Task Grabar_y_borrar_hacen_alta_modificacion_y_baja()
    {
        var (svc, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>(
            "SELECT TOP 1 CI FROM dbo.Afiliado WHERE CI NOT IN (SELECT CI FROM dbo.PrimaFallecimiento) ORDER BY CI DESC");

        try
        {
            // Alta.
            await svc.GrabarAsync(new PrimaFallecimiento
            {
                CI = ci, FechaFallecimiento = new DateTime(2026, 3, 10), Importe = 12345.67, Observaciones = "alta test",
            });
            var creada = await svc.GetAsync(ci);
            Assert.NotNull(creada);
            Assert.Equal(12345.67, creada!.Importe!.Value, 2);

            // Modificación (mismo CI → update, no segunda fila).
            await svc.GrabarAsync(new PrimaFallecimiento { CI = ci, Importe = 999, Observaciones = "mod" });
            var cuantas = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.PrimaFallecimiento WHERE CI=@ci", new { ci });
            Assert.Equal(1, cuantas);
            var mod = await svc.GetAsync(ci);
            Assert.Equal(999, mod!.Importe!.Value, 2);
            Assert.Equal("mod", mod.Observaciones);

            // Baja.
            await svc.BorrarAsync(ci);
            Assert.Null(await svc.GetAsync(ci));
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.PrimaFallecimiento WHERE CI=@ci", new { ci });
        }
    }

    private sealed record CiMes(long CI, int AnioMes);
    private sealed record TopeUr(double TopePrima, double UR);
}
