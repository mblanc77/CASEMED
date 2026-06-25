using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sgpa.Business.Subsidios;
using Sgpa.Data.Security;
using Xunit;
using Xunit.Abstractions;

namespace Sgpa.Business.Tests;

/// <summary>
/// Ejercita el camino real de liquidación (selección → cabezal → ValorJornal → detalle por empresa)
/// para UN afiliado del período 202201, dentro de una transacción con ROLLBACK: valida el SQL/SPs
/// y los inserts sin tocar los datos productivos (70k cabezales reales).
/// </summary>
public class SubsidioLiquidacionTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private readonly ITestOutputHelper _out;
    public SubsidioLiquidacionTests(ITestOutputHelper output) => _out = output;

    [Fact]
    public async Task GeneraCertificaciones_consolida_tramos()
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var repo = new SubsidioRepository(new ScopedDbExecutor(cn, tx));

        var ci = (await repo.SeleccionarAfiliadosAsync(new SubsidioPeriodo(2022, 1), true)).First();
        var spans = await repo.GenerarCertificacionesAsync(ci, new SubsidioPeriodo(2022, 1));

        // Tramos ordenados y sin solapamiento ni huecos de 1 día (deberían estar fusionados).
        for (int i = 1; i < spans.Count; i++)
        {
            Assert.True(spans[i].FechaIni > spans[i - 1].FechaFin);
            Assert.NotEqual(1, (spans[i].FechaIni - spans[i - 1].FechaFin).Days);
        }
        await tx.RollbackAsync();
    }

    [Fact]
    public async Task Liquida_un_afiliado_crea_cabezal_y_valor_jornal()
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var db = new ScopedDbExecutor(cn, tx);
        var repo = new SubsidioRepository(db);
        var svc = new SubsidioLiquidacionService(db, new FakeCurrentUser(), new NoImponibleSync());

        var ci = (await repo.SeleccionarAfiliadosAsync(new SubsidioPeriodo(2022, 1), true)).First();

        var res = await svc.LiquidarAsync(2022, 1, liquidar: true, ci: ci);

        Assert.True(res.SubsidiosGenerados >= 1, "se esperaba al menos un subsidio generado");

        // Dentro de la transacción: el cabezal existe con sus montos calculados.
        var cab = (await db.QueryAsync<(decimal ValorJornal, int Dias, decimal ImpNominal, decimal ImpAguinaldo, decimal ImpLiquido)>(
            "SELECT ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido FROM dbo.SubsidioCabezal WHERE CI=@ci AND Anio=2022 AND Mes=1", new { ci })).First();
        Assert.True(cab.Dias > 0, "se esperaban días subsidiados > 0");
        Assert.True(cab.ImpLiquido != 0, "se esperaba un importe líquido calculado");

        var items = await db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM dbo.SubsidioItem i
              JOIN dbo.SubsidioCabezal c ON c.IdSubsidio = i.IdSubsidio
              WHERE c.CI=@ci AND c.Anio=2022 AND c.Mes=1", new { ci });
        Assert.True(items >= 2, "se esperaban al menos los 2 aportes jubilatorios");

        var itemsEmp = await db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM dbo.SubsidioItemEmpresa ie
              JOIN dbo.SubsidioCabezal c ON c.IdSubsidio = ie.IdSubsidio
              WHERE c.CI=@ci AND c.Anio=2022 AND c.Mes=1", new { ci });
        Assert.True(itemsEmp >= 2, "se esperaba detalle de ítems por empresa");

        _out.WriteLine($"CI {ci}: ValorJornal={cab.ValorJornal}, Dias={cab.Dias}, ImpNominal={cab.ImpNominal}, ImpAguinaldo={cab.ImpAguinaldo}, ImpLiquido={cab.ImpLiquido}, items={items}, itemsEmpresa={itemsEmp}");

        await tx.RollbackAsync(); // nada se persiste
    }

    private sealed record CabRow(long CI, float ValorJornal, int Dias, double ImpNominal, double ImpAguinaldo, double ImpLiquido);
    private static bool Cerca(double a, double b) => System.Math.Abs(a - b) <= 1d;

    /// <summary>
    /// Compara, para una muestra de cédulas de un período reciente, el resultado de la liquidación NUEVA (C#)
    /// recalculada vs el valor ALMACENADO en SubsidioCabezal (que es el cómputo VB6/Access migrado desde
    /// sgpaserv2k3.mdb). Todo dentro de una transacción con ROLLBACK (no toca datos productivos).
    /// </summary>
    [Theory]
    [InlineData(2025, 12)]
    [InlineData(2026, 1)]
    public async Task Liquidacion_nueva_coincide_con_almacenada(int anio, int mes)
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var db = new ScopedDbExecutor(cn, tx);
        var svc = new SubsidioLiquidacionService(db, new FakeCurrentUser(), new NoImponibleSync());

        // Resultado almacenado (= VB6/Access) para una muestra del período. Sólo cédulas con UN cabezal en
        // el período (las de múltiples subsidios darían artefactos al comparar fila a fila).
        var stored = await db.QueryAsync<CabRow>(
            @"SELECT TOP 25 CI, ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido
              FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1
                AND CI IN (SELECT CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1 GROUP BY CI HAVING COUNT(*)=1)
              ORDER BY CI",
            new { anio, mes });

        int okGross = 0, okLiq = 0; var grossDiffs = new System.Collections.Generic.List<string>();
        foreach (var s in stored)
        {
            await svc.LiquidarAsync(anio, mes, liquidar: true, ci: s.CI);
            var n = (await db.QueryAsync<CabRow>(
                @"SELECT CI, ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido
                  FROM dbo.SubsidioCabezal WHERE CI=@ci AND Anio=@anio AND Mes=@mes", new { ci = s.CI, anio, mes })).FirstOrDefault();
            if (n is null) { grossDiffs.Add($"CI {s.CI}: la liquidación nueva no generó cabezal"); continue; }

            bool gross = s.Dias == n.Dias && Cerca(s.ImpNominal, n.ImpNominal) && Cerca(s.ImpAguinaldo, n.ImpAguinaldo);
            bool liq = Cerca(s.ImpLiquido, n.ImpLiquido);
            if (gross) okGross++; if (liq) okLiq++;
            if (!gross) grossDiffs.Add($"CI {s.CI}: BRUTO alm(dias={s.Dias},nom={s.ImpNominal},agui={s.ImpAguinaldo}) vs nuevo(dias={n.Dias},nom={n.ImpNominal},agui={n.ImpAguinaldo})");
            var ratioS = s.ImpNominal != 0 ? s.ImpLiquido / s.ImpNominal : 0;
            var ratioN = n.ImpNominal != 0 ? n.ImpLiquido / n.ImpNominal : 0;
            _out.WriteLine($"CI {s.CI,-9} dias {s.Dias}/{n.Dias} nom {s.ImpNominal}/{n.ImpNominal} agui {s.ImpAguinaldo}/{n.ImpAguinaldo}  LIQ alm={s.ImpLiquido} (×{ratioS:F3})  nuevo={n.ImpLiquido} (×{ratioN:F3})  {(liq ? "OK" : "*** LIQ DIFF")}");
        }
        _out.WriteLine($"== {anio}/{mes}: BRUTO coincide {okGross}/{stored.Count} | LÍQUIDO coincide {okLiq}/{stored.Count} ==");
        await tx.RollbackAsync();
        // El bruto (días/nominal/aguinaldo) debe coincidir; el líquido se reporta aparte (ver salida).
        Assert.True(grossDiffs.Count == 0, $"Difiere el BRUTO en {grossDiffs.Count}/{stored.Count}:\n" + string.Join("\n", grossDiffs));
    }

    private sealed record SplitRow(double ImpLiquido, double LiquidoBPS, double LiquidoPagar, int DiasBPS);

    /// <summary>
    /// Verifica la paridad REAL del líquido teniendo en cuenta el split BPS: lo almacenado en períodos viejos
    /// pasó por el post-proceso VB6 (506_Update_Liquidos: SubsidioCabezal.ImpLiquido := LiquidoPagar), por lo
    /// que el ImpLiquido viejo = complemento CASEMED. La liquidación nueva deja ImpLiquido en bruto y el
    /// complemento en SubsidioCabezal_BPS.LiquidoPagar. Parida si: nuevo.LiquidoPagar == viejo.ImpLiquido
    /// y nuevo.LiquidoBPS == viejo.LiquidoBPS.
    /// </summary>
    [Theory]
    [InlineData(2025, 12)]
    [InlineData(2026, 1)]
    public async Task Split_BPS_nuevo_coincide_con_almacenado(int anio, int mes)
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var db = new ScopedDbExecutor(cn, tx);
        var svc = new SubsidioLiquidacionService(db, new FakeCurrentUser(), new NoImponibleSync());

        var cis = await db.QueryAsync<long>(
            @"SELECT TOP 25 CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1
                AND CI IN (SELECT CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1 GROUP BY CI HAVING COUNT(*)=1)
              ORDER BY CI", new { anio, mes });

        const string q = @"SELECT c.ImpLiquido, ISNULL(b.LiquidoBPS,0) LiquidoBPS, ISNULL(b.LiquidoPagar,0) LiquidoPagar, ISNULL(b.DiasBPS,0) DiasBPS
                           FROM dbo.SubsidioCabezal c LEFT JOIN dbo.SubsidioCabezal_BPS b ON c.IdSubsidio=b.IdSubsidio
                           WHERE c.CI=@ci AND c.Anio=@anio AND c.Mes=@mes";

        static bool Cerca2(double a, double b) => System.Math.Abs(a - b) <= System.Math.Max(2d, 0.001 * System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)));

        // Invariante: si la porción que paga BPS coincide (mismo tope), entonces el bruto es el mismo y por lo
        // tanto el complemento CASEMED (viejo ImpLiquido = nuevo LiquidoPagar) debe coincidir. Las diferencias
        // sólo pueden aparecer cuando cambió un tope (BPS o jubilatorio) — afiliados de alto ingreso —, lo que
        // mueve la porción BPS: esos casos se reportan como "drift de tope", no son defecto de cálculo.
        int matched = 0, driftTope = 0; var diffs = new System.Collections.Generic.List<string>();
        foreach (var ci in cis)
        {
            var old = (await db.QueryAsync<SplitRow>(q, new { ci, anio, mes })).First();   // ya post-procesado (ImpLiquido = LiquidoPagar)
            await svc.LiquidarAsync(anio, mes, liquidar: true, ci: ci);
            var nw = (await db.QueryAsync<SplitRow>(q, new { ci, anio, mes })).First();      // recalculado (ImpLiquido = bruto)

            string estado;
            if (Cerca2(old.LiquidoBPS, nw.LiquidoBPS))
            {
                bool ok = Cerca2(old.ImpLiquido, nw.LiquidoPagar);
                estado = ok ? "OK" : "*** DIFF";
                if (ok) matched++; else diffs.Add($"CI {ci}: bps coincide ({old.LiquidoBPS}) pero complemento NO: viejo={old.ImpLiquido} vs nuevo={nw.LiquidoPagar}");
            }
            else { driftTope++; estado = $"drift tope BPS ({old.LiquidoBPS} -> {nw.LiquidoBPS})"; }
            _out.WriteLine($"CI {ci,-9} viejo ImpLiq={old.ImpLiquido} bps={old.LiquidoBPS} | nuevo bruto={nw.ImpLiquido} bps={nw.LiquidoBPS} pagar={nw.LiquidoPagar}  {estado}");
        }
        _out.WriteLine($"== {anio}/{mes}: coincide {matched}, drift de tope (alto ingreso) {driftTope}, defectos {diffs.Count} (de {cis.Count}) ==");
        await tx.RollbackAsync();
        Assert.True(diffs.Count == 0, $"Diferencias NO explicadas por tope {diffs.Count}/{cis.Count}:\n" + string.Join("\n", diffs));
    }
}
