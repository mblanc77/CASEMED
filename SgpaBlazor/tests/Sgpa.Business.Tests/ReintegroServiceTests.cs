using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Reintegros;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: reglas de los reintegros mutuales (port de AbmReint): validación del
/// período, aviso de elegibilidad (1,25 SMN) y actualización de la cuota del mutualista.
/// </summary>
public class ReintegroServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (ReintegroService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new ReintegroService(db), db);
    }

    [Theory]
    [InlineData(0, 2026, false)]    // mes inválido
    [InlineData(13, 2026, false)]   // mes inválido
    [InlineData(6, 0, false)]       // año inválido
    [InlineData(6, 2026, true)]     // ok
    public async Task Validar_chequea_el_periodo(int mes, int anio, bool ok)
    {
        var (svc, _) = Build();
        var error = await svc.ValidarAsync(new ReintegroMutual { CI = 12345672, Mes = mes, Anio = anio });
        Assert.Equal(ok, error is null);
    }

    [Fact]
    public async Task Avisos_marca_no_elegible_cuando_no_hay_ingresos_en_la_ventana()
    {
        var (svc, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Afiliado WHERE CI BETWEEN 1000000 AND 60000000 ORDER BY CI DESC");

        // Período lejano → sin imponibles en la ventana → promedio y último mes 0 → no llega a 1,25 SMN.
        var avisos = await svc.GetAvisosAsync(new ReintegroMutual { CI = ci, Mes = 1, Anio = 2099 });
        Assert.Single(avisos);
        Assert.Contains("1,25 SMN", avisos[0]);
    }

    [Fact]
    public async Task Avisos_coincide_con_el_calculo_directo_de_promedio_y_ultimo_mes()
    {
        var (svc, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.ReintegroMutual ORDER BY Anio DESC, Mes DESC");
        int mes = 6, anio = 2024;

        var smn = await db.ExecuteScalarAsync<double>("SELECT TOP 1 CAST(SMN AS float) FROM dbo.Parametros");
        var fin = ReintegroService.AddMonth(anio * 100 + mes, -2);
        var ini = ReintegroService.AddMonth(fin, -5);
        var promedio = await db.ExecuteScalarAsync<double?>("SELECT Importe FROM dbo.acc_sgpa_320_AfiliadoPromedio_q(@ci,@ini,@fin)", new { ci = (int)ci, ini, fin }) ?? 0d;
        var ultMes = await db.ExecuteScalarAsync<double?>("SELECT Importe FROM dbo.acc_sgpa_320_AfiliadoUltMes_q(@ci,@m)", new { ci = (int)ci, m = fin }) ?? 0d;
        var esperaAviso = promedio < 1.25 * smn && ultMes < 1.25 * smn;

        var avisos = await svc.GetAvisosAsync(new ReintegroMutual { CI = ci, Mes = mes, Anio = anio });
        Assert.Equal(esperaAviso, avisos.Any());
    }

    [Fact]
    public async Task ActualizarCuota_setea_la_cuota_del_mutualista()
    {
        var (svc, db) = Build();
        var m = await db.QuerySingleOrDefaultAsync<MutRow>("SELECT TOP 1 CodMutualista, CAST(ISNULL(Cuota,0) AS float) Cuota FROM dbo.Mutualista ORDER BY CodMutualista");
        Assert.NotNull(m);

        try
        {
            await svc.ActualizarCuotaMutualAsync(new ReintegroMutual { CodMutualista = m!.CodMutualista, Importe = 777.5f });
            var cuota = await db.ExecuteScalarAsync<double>("SELECT CAST(Cuota AS float) FROM dbo.Mutualista WHERE CodMutualista=@c", new { c = m.CodMutualista });
            Assert.Equal(777.5, cuota, 2);
        }
        finally
        {
            await db.ExecuteAsync("UPDATE dbo.Mutualista SET Cuota=@cuota WHERE CodMutualista=@c", new { cuota = m!.Cuota, c = m.CodMutualista });
        }
    }

    private sealed record MutRow(int CodMutualista, double Cuota);
}
