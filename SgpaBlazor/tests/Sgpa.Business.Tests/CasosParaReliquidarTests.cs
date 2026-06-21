using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Sgpa.Business.Subsidios;
using Xunit;
using Xunit.Abstractions;

namespace Sgpa.Business.Tests;

/// <summary>
/// Diagnóstico (off-topic): lista las cédulas de las últimas liquidaciones cuyo ValorJornal/ImpNominal
/// RECALCULADO con los datos actuales difiere de lo liquidado — es decir, candidatos a RE-LIQUIDAR
/// porque sus imponibles/trabaja se editaron después de la liquidación. Sólo imprime; no asserta nada.
/// </summary>
public class CasosParaReliquidarTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private readonly ITestOutputHelper _out;
    public CasosParaReliquidarTests(ITestOutputHelper output) => _out = output;

    private sealed class Cab { public decimal ValorJornal { get; set; } public decimal ImpNominal { get; set; } public string? Usr { get; set; } public DateTime Ts { get; set; } }
    private sealed class Baseline { public string Usr { get; set; } = ""; public DateTime RunTs { get; set; } }

    [Fact]
    public async Task Listar_casos_para_reliquidar()
    {
        await using var meta = new SqlConnection(ConnectionString);
        await meta.OpenAsync();

        var periodos = (await meta.QueryAsync<int>(
            @"SELECT TOP 3 (Anio*100+Mes) p FROM dbo.SubsidioCabezal WHERE Liquidar=1
              GROUP BY Anio, Mes HAVING COUNT(*) > 50 ORDER BY (Anio*100+Mes) DESC")).ToList();

        const string sel = "SELECT TOP 1 ValorJornal, ImpNominal, Usr, Ts FROM dbo.SubsidioCabezal WHERE CI=@ci AND Anio=@anio AND Mes=@mes";

        foreach (var p in periodos)
        {
            int anio = p / 100, mes = p % 100;
            var baseline = await meta.QuerySingleOrDefaultAsync<Baseline>(
                @"SELECT TOP 1 Usr, MIN(Ts) RunTs FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes
                  GROUP BY Usr ORDER BY COUNT(*) DESC", new { anio, mes });

            var cis = (await meta.QueryAsync<long>(
                @"SELECT CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1
                  GROUP BY CI HAVING COUNT(*)=1 ORDER BY CI", new { anio, mes })).ToList();

            var casos = new List<string>();
            foreach (var ci in cis)
            {
                await using var cn = new SqlConnection(ConnectionString);
                await cn.OpenAsync();
                await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
                var db = new ScopedDbExecutor(cn, tx);
                var prod = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                await new SubsidioLiquidacionService(db, new FakeCurrentUser()).LiquidarAsync(anio, mes, true, ci);
                var mine = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                await tx.RollbackAsync();
                if (prod is null || mine is null) continue;

                bool vjDif = Math.Abs(prod.ValorJornal - mine.ValorJornal) >= 0.5m;
                bool nomDif = Math.Abs(prod.ImpNominal - mine.ImpNominal) >= 1m;
                bool esManual = !string.Equals(prod.Usr, baseline?.Usr, StringComparison.OrdinalIgnoreCase)
                                || (baseline is not null && Math.Abs((prod.Ts - baseline.RunTs).TotalHours) > 2);
                if ((vjDif || nomDif) && !esManual)
                    casos.Add($"  CI {ci,-12} VJ liq {prod.ValorJornal,10:F2} → recalc {mine.ValorJornal,10:F2} | Nom liq {prod.ImpNominal,12:F2} → recalc {mine.ImpNominal,12:F2}");
            }

            _out.WriteLine($"===== Período {p}: {casos.Count} de {cis.Count} cédulas a RE-LIQUIDAR (datos cambiaron) =====");
            foreach (var c in casos) _out.WriteLine(c);
        }
    }
}
