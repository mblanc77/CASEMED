using System;
using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: cálculos a nivel afiliado. Promedio de ingresos (cmdPromedio / Promedio):
/// media de imponibles de la ventana de 6 meses que termina 2 meses atrás (IMS-free).
/// </summary>
public class AfiliadoServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (AfiliadoService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new AfiliadoService(db), db);
    }

    [Fact]
    public async Task Promedio_usa_la_ventana_de_6_meses_que_termina_dos_meses_atras()
    {
        var (svc, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Imponible WHERE Concepto='1' ORDER BY CI");

        // Ventana esperada (replica el cálculo del servicio): mes = aaaamm(hoy−2); ini = mes−5.
        var fecha = DateTime.Today.AddMonths(-2);
        var mes = fecha.Year * 100 + fecha.Month;
        var ini = fecha.AddMonths(-5);
        var mesIni = ini.Year * 100 + ini.Month;

        var esperado = await db.ExecuteScalarAsync<double?>(
            "SELECT Promedio FROM dbo.acc_sgpa_220_AfiliadoPromedio_q(@ci, @mes, @mesIni)",
            new { ci = (int)ci, mes, mesIni });

        Assert.Equal(esperado, await svc.GetPromedioIngresosAsync(ci));
    }

    [Fact]
    public async Task Promedio_de_una_CI_inexistente_es_null()
    {
        var (svc, _) = Build();
        Assert.Null(await svc.GetPromedioIngresosAsync(-1));
    }

    [Fact]
    public async Task Avisos_marca_la_cedula_con_digito_verificador_incorrecto()
    {
        var (svc, _) = Build();
        var avisos = await svc.GetAvisosAsync(new Sgpa.Domain.Entities.Afiliado { CI = 12345673 });
        Assert.Single(avisos);
        Assert.Contains("dígito verificador", avisos[0]);
    }

    [Fact]
    public async Task Avisos_no_marca_una_cedula_valida()
    {
        var (svc, _) = Build();
        Assert.Empty(await svc.GetAvisosAsync(new Sgpa.Domain.Entities.Afiliado { CI = 12345672 }));
    }

    [Fact]
    public async Task NoLlegaA125Smn_es_true_sin_ingresos_en_la_ventana()
    {
        var (svc, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Afiliado WHERE CI BETWEEN 1000000 AND 60000000 ORDER BY CI DESC");
        // Período lejano → sin imponibles → promedio y último mes 0 → no llega a 1,25 SMN.
        Assert.True(await svc.NoLlegaA125SmnAsync(ci, 1, 2099));
    }
}
