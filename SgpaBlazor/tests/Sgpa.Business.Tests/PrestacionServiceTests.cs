using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Business.Prestaciones;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: avisos de prestaciones (port de AbmPrest). Verifica el período de
/// renovación con el PeriodoRenovacion REAL (la query migrada lo hardcodea a 0) y la elegibilidad SMN.
/// </summary>
public class PrestacionServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PrestacionService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new PrestacionService(db, new AfiliadoService(db)), db);
    }

    [Theory]
    [InlineData("2024-01-15", "2024-03-10", 2)]
    [InlineData("2024-12-01", "2025-02-01", 2)]
    [InlineData("2026-04-28", "2026-04-01", 0)]
    public void MesesEntre_cuenta_meses_calendario(string desde, string hasta, int esperado)
        => Assert.Equal(esperado, PrestacionService.MesesEntre(DateTime.Parse(desde), DateTime.Parse(hasta)));

    private sealed record RenovCase(long CI, int CodPrestacionTipo, DateTime PrevFecha, int PeriodoRenovacion);

    private static Task<RenovCase?> CasoRenovacionAsync(IDbExecutor db)
        => db.QuerySingleOrDefaultAsync<RenovCase>(
            @"SELECT TOP 1 p.CI, p.CodPrestacionTipo, MAX(p.Fecha) AS PrevFecha, pt.PeriodoRenovacion
              FROM dbo.Prestacion p JOIN dbo.PrestacionTipo pt ON p.CodPrestacionTipo = pt.CodPrestacionTipo
              WHERE pt.PeriodoRenovacion > 0
              GROUP BY p.CI, p.CodPrestacionTipo, pt.PeriodoRenovacion
              ORDER BY MAX(p.Fecha) DESC");

    [Fact]
    public async Task Avisa_renovacion_cuando_hay_una_prestacion_reciente_del_mismo_tipo()
    {
        var (svc, db) = Build();
        var c = await CasoRenovacionAsync(db);
        Assert.NotNull(c);

        // Nueva prestación un mes después de la anterior → dentro del período de renovación → avisa.
        var nueva = new Prestacion { CI = c!.CI, CodPrestacionTipo = c.CodPrestacionTipo, Fecha = c.PrevFecha.AddMonths(1) };
        var avisos = await svc.GetAvisosAsync(nueva);
        Assert.Contains(avisos, a => a.Contains("renovación"));
    }

    [Fact]
    public async Task No_avisa_renovacion_cuando_paso_el_periodo()
    {
        var (svc, db) = Build();
        var c = await CasoRenovacionAsync(db);
        Assert.NotNull(c);

        // Nueva prestación pasado el período de renovación → no avisa por renovación.
        var nueva = new Prestacion { CI = c!.CI, CodPrestacionTipo = c.CodPrestacionTipo, Fecha = c.PrevFecha.AddMonths(c.PeriodoRenovacion + 2) };
        var avisos = await svc.GetAvisosAsync(nueva);
        Assert.DoesNotContain(avisos, a => a.Contains("renovación"));
    }
}
