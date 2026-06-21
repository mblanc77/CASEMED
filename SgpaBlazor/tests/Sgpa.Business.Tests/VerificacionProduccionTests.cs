using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Sgpa.Business.Subsidios;
using Xunit;
using Xunit.Abstractions;

namespace Sgpa.Business.Tests;

/// <summary>
/// Compara el cálculo portado contra los SubsidioCabezal reales de producción (202201),
/// para CIs con exactamente un cabezal. Cada CI se procesa en su propia transacción con ROLLBACK.
/// </summary>
public class VerificacionProduccionTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private readonly ITestOutputHelper _out;
    public VerificacionProduccionTests(ITestOutputHelper output) => _out = output;

    private sealed class Cab
    {
        public decimal ValorJornal { get; set; }
        public int Dias { get; set; }
        public decimal ImpNominal { get; set; }
        public decimal ImpAguinaldo { get; set; }
        public decimal ImpLiquido { get; set; }
    }

    [Fact]
    public async Task Comparar_contra_produccion()
    {
        await using var q = new SqlConnection(ConnectionString);
        await q.OpenAsync();
        var cis = (await q.QueryAsync<long>(
            @"SELECT TOP 10 CI FROM dbo.SubsidioCabezal
              WHERE Anio=2022 AND Mes=1 AND Liquidar=1
              GROUP BY CI HAVING COUNT(*)=1 ORDER BY CI")).ToList();

        const string sel = "SELECT TOP 1 ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido FROM dbo.SubsidioCabezal WHERE CI=@ci AND Anio=2022 AND Mes=1";
        _out.WriteLine("CI            | ValorJornal prod / mío / SP-actual | ImpLiquido prod / mío | histórico | fidelidad SP");
        int vjOk = 0, nomOk = 0, liqOk = 0, fidOk = 0;

        foreach (var ci in cis)
        {
            await using var cn = new SqlConnection(ConnectionString);
            await cn.OpenAsync();
            await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
            var db = new ScopedDbExecutor(cn, tx);

            // ValorJornal esperado según los SPs ACTUALES (suma de promedios por empresa + Casemed).
            var spVj = await SumarValorJornalSpAsync(db, ci);

            var prod = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci });
            var svc = new SubsidioLiquidacionService(db, new FakeCurrentUser());
            var res = await svc.LiquidarAsync(2022, 1, liquidar: true, ci: ci);
            var mine = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci });

            bool vj = prod is not null && mine is not null && System.Math.Abs(prod.ValorJornal - mine.ValorJornal) < 0.5m;
            bool nom = prod is not null && mine is not null && System.Math.Abs(prod.ImpNominal - mine.ImpNominal) < 1m;
            bool liq = prod is not null && mine is not null && System.Math.Abs(prod.ImpLiquido - mine.ImpLiquido) < 1m;
            bool fid = mine is not null && System.Math.Abs(spVj - mine.ValorJornal) < 0.5m; // fidelidad al SP
            if (vj) vjOk++; if (nom) nomOk++; if (liq) liqOk++; if (fid) fidOk++;

            _out.WriteLine($"{ci,-13} | prod {prod?.ValorJornal,9:F2} / mío {mine?.ValorJornal,9:F2} / SP {spVj,9:F2} | Liq prod {prod?.ImpLiquido,11:F2} / mío {mine?.ImpLiquido,11:F2} | hist:VJ={vj} Liq={liq} | fidelidadSP={fid}");

            await tx.RollbackAsync();
        }

        _out.WriteLine($"=== Histórico {cis.Count}: ValorJornal={vjOk}, ImpNominal={nomOk}, ImpLiquido={liqOk} | Fidelidad a SPs actuales (sin enganchada): {fidOk}/{cis.Count} ===");
        Assert.True(cis.Count > 0);
    }

    /// <summary>Suma cruda de los SPs de ValorJornal actuales (promedio por empresa ≥3m + Casemed ≥1m).</summary>
    private static async Task<decimal> SumarValorJornalSpAsync(ScopedDbExecutor db, long ci)
    {
        const int lMes = 202201, lMesIni = 202107, mesFin = 202112, lMesIniImp = 202101, cod = 902;
        var emp = await db.QueryProcAsync<Prom>("dbo.acc_sgpa_300_AfiliadoValorJornalxEmpresa",
            new { pCodCasemed = cod, pCI = ci, pMesIni = lMesIni, pMesFin = mesFin, pLiquidar = true, pDias = 3, pMes = lMes, pMesIniImp = lMesIniImp });
        var cas = await db.QueryProcAsync<Prom>("dbo.acc_sgpa_300_AfiliadoValorJornalCasemed",
            new { pCodCasemed = cod, pCI = ci, pMesIni = lMesIni, pMesFin = mesFin, pLiquidar = true, pDias = 1, pMes = lMes, pMesIniImp = lMesIniImp });
        decimal total = 0m;
        foreach (var p in emp) total += System.Math.Round((decimal)p.Promedio, 3);
        foreach (var p in cas) total += System.Math.Round((decimal)p.Promedio, 3);
        return total;
    }

    private sealed class Prom { public double Promedio { get; set; } }
}
