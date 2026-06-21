using System;
using System.Threading.Tasks;
using Sgpa.Business.Certificaciones;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2 (read-only): validación de superposición de certificaciones
/// (port del bloque de DatosOk de AbmCerti que usa 310_CertificacionAnterior).
/// </summary>
public class CertificacionServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private sealed record CertRow(int NroLlamado, long CI, DateTime FechaIni, DateTime FechaFin);

    private static (CertificacionService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new CertificacionService(db), db);
    }

    private static Task<CertRow?> SampleAsync(IDbExecutor db)
        => db.QuerySingleOrDefaultAsync<CertRow>(
            @"SELECT TOP 1 NroLlamado, CI, FechaIni, FechaFin FROM dbo.Certificacion
              WHERE Efectiva = 1 AND CI IS NOT NULL AND FechaIni IS NOT NULL AND FechaFin IS NOT NULL
              ORDER BY NroLlamado DESC");

    [Fact]
    public async Task Detecta_una_certificacion_efectiva_que_se_superpone()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        // Mismo período, excluyendo un llamado inexistente (-1): debe encontrar al menos la propia.
        var sup = await svc.BuscarSuperposicionAsync(c!.CI, c.FechaIni, c.FechaFin, nroLlamado: -1);
        Assert.NotNull(sup);
        // El período devuelto efectivamente se superpone con el consultado.
        Assert.True(sup!.FechaIni <= c.FechaFin && sup.FechaFin >= c.FechaIni);
    }

    [Fact]
    public async Task Excluye_la_propia_certificacion_del_chequeo()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        // Excluyendo su propio NroLlamado, nunca debe devolverse a sí misma.
        var sup = await svc.BuscarSuperposicionAsync(c!.CI, c.FechaIni, c.FechaFin, nroLlamado: c.NroLlamado);
        Assert.True(sup is null || sup.NroLlamado != c.NroLlamado);
    }

    [Fact]
    public async Task Validar_devuelve_mensaje_cuando_hay_superposicion()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        var nueva = new Certificacion { NroLlamado = -1, CI = c!.CI, FechaIni = c.FechaIni, FechaFin = c.FechaFin, Efectiva = true };
        var error = await svc.ValidarSuperposicionAsync(nueva);
        Assert.NotNull(error);
        Assert.Contains("se superpone", error);
    }

    [Fact]
    public async Task Validar_no_aplica_si_no_es_efectiva()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        var noEfectiva = new Certificacion { NroLlamado = -1, CI = c!.CI, FechaIni = c.FechaIni, FechaFin = c.FechaFin, Efectiva = false };
        Assert.Null(await svc.ValidarSuperposicionAsync(noEfectiva));
    }

    [Fact]
    public async Task Validar_sin_superposicion_devuelve_null()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        // Período muy lejano: no debería superponerse con nada del afiliado.
        var futura = new Certificacion
        {
            NroLlamado = -1, CI = c!.CI, Efectiva = true,
            FechaIni = new DateTime(2099, 1, 1), FechaFin = new DateTime(2099, 1, 15),
        };
        Assert.Null(await svc.ValidarSuperposicionAsync(futura));
    }

    [Fact]
    public async Task Validar_rechaza_fecha_inicio_mayor_que_fin()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        var invertida = new Certificacion
        {
            NroLlamado = -1, CI = c!.CI, Efectiva = true,
            FechaIni = new DateTime(2099, 2, 1), FechaFin = new DateTime(2099, 1, 1),
        };
        var error = await svc.ValidarSuperposicionAsync(invertida);
        Assert.NotNull(error);
        Assert.Contains("inicio", error);
    }

    // --- Avisos por días acumulados (lógica pura: umbrales, prioridad y fecha estimada) ---

    private static readonly DateTime Fin = new(2026, 6, 30);

    [Fact]
    public void Avisos_sin_prorroga_y_pocos_dias_no_avisa()
        => Assert.Empty(CertificacionService.ConstruirAvisosDias(300, null, Fin));

    [Fact]
    public void Avisos_cerca_de_365_avisa_un_anio_con_fecha_estimada()
    {
        var avisos = CertificacionService.ConstruirAvisosDias(400, null, Fin);
        Assert.Single(avisos);
        Assert.Contains("365 días", avisos[0]);
        // fecha = fin + (365 - 400) = fin - 35 días.
        Assert.Contains(Fin.AddDays(365 - 400).ToString("dd/MM/yyyy"), avisos[0]);
    }

    [Fact]
    public void Avisos_cerca_de_720_avisa_dos_anios_y_tiene_prioridad_sobre_365()
    {
        var avisos = CertificacionService.ConstruirAvisosDias(700, null, Fin);
        Assert.Single(avisos);
        Assert.Contains("720 días", avisos[0]);
        Assert.DoesNotContain("365", avisos[0]);
        Assert.Contains(Fin.AddDays(720 - 700).ToString("dd/MM/yyyy"), avisos[0]); // fin + 20
    }

    [Fact]
    public void Avisos_con_prorroga_cercana_avisa_fin_de_prorroga_y_suprime_los_topes()
    {
        // Aún con días altos, la prórroga (a ≤30 días del fin) es el único aviso.
        var finProrroga = Fin.AddDays(-10);
        var avisos = CertificacionService.ConstruirAvisosDias(700, finProrroga, Fin);
        Assert.Single(avisos);
        Assert.Contains("prórroga", avisos[0]);
        Assert.Contains(finProrroga.ToString("dd/MM/yyyy"), avisos[0]);
    }

    [Fact]
    public void Avisos_con_prorroga_lejana_no_avisa_nada()
    {
        // Hay prórroga pero su fin está a más de 30 días: el VB6 no muestra ningún aviso (ni de topes).
        var avisos = CertificacionService.ConstruirAvisosDias(700, Fin.AddDays(-60), Fin);
        Assert.Empty(avisos);
    }

    // --- Auto-numeración, validación de fechas requeridas y aviso por diferencia de días ---

    [Fact]
    public async Task ProximoNroLlamado_es_max_mas_uno()
    {
        var (svc, db) = Build();
        var max = await db.ExecuteScalarAsync<int?>("SELECT MAX(NroLlamado) FROM dbo.Certificacion") ?? 0;
        Assert.Equal(max + 1, await svc.ProximoNroLlamadoAsync());
    }

    [Fact]
    public async Task GetDiffDias_devuelve_el_default_90_si_no_esta_configurado()
    {
        var (svc, _) = Build();
        Assert.Equal(90, await svc.GetDiffDiasCertificacionAsync());
    }

    [Fact]
    public async Task Validar_efectiva_sin_fechas_exige_las_fechas()
    {
        var (svc, _) = Build();
        var c = new Certificacion { NroLlamado = -1, CI = 12345678, Efectiva = true };
        var error = await svc.ValidarAsync(c);
        Assert.NotNull(error);
        Assert.Contains("fecha de certificación", error);
    }

    [Fact]
    public async Task Validar_no_efectiva_no_exige_fechas()
    {
        var (svc, _) = Build();
        var c = new Certificacion { NroLlamado = -1, CI = 12345678, Efectiva = false };
        Assert.Null(await svc.ValidarAsync(c));
    }

    [Fact]
    public async Task Avisos_incluye_diferencia_de_dias_muy_alta()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        // Período lejano y largo (~151 días > 90): debe incluir el aviso por diferencia alta.
        var cert = new Certificacion
        {
            NroLlamado = -1, CI = c!.CI, Efectiva = true,
            FechaIni = new DateTime(2099, 1, 1), FechaFin = new DateTime(2099, 6, 1),
        };
        var avisos = await svc.GetAvisosDiasAsync(cert);
        Assert.Contains(avisos, a => a.Contains("supera el máximo"));
    }

    [Fact]
    public async Task GetDiasCertificados_coincide_con_la_suma_directa()
    {
        var (svc, db) = Build();
        var c = await SampleAsync(db);
        Assert.NotNull(c);

        var esperado = await db.ExecuteScalarAsync<int?>(
            @"SELECT SUM(DATEDIFF(day, FechaIni, FechaFin)+1) FROM dbo.Certificacion
              WHERE CI=@ci AND Efectiva=1 AND NroLlamado <= @n",
            new { ci = (int)c!.CI, n = c.NroLlamado }) ?? 0;

        Assert.Equal(esperado, await svc.GetDiasCertificadosAsync(c.CI, c.NroLlamado));
    }
}
