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
/// Diagnóstico (no asserta): identifica las cédulas "cambio-de-dato" de las últimas 3 liquidaciones
/// (mismas 25xCI que Paridad_ultimas_3) y para cada una imprime prod vs recálculo (VJ/Nominal/Días)
/// y el snapshot por empresa del run vs el imponible ACTUAL, para localizar EXACTAMENTE qué dato cambió.
/// </summary>
public class DiagnosticoCambioDatoTests
{
    private const string Cs =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private readonly ITestOutputHelper _out;
    public DiagnosticoCambioDatoTests(ITestOutputHelper output) => _out = output;

    private sealed class Cab
    {
        public int IdSubsidio { get; set; }
        public decimal ValorJornal { get; set; }
        public int Dias { get; set; }
        public decimal ImpNominal { get; set; }
        public decimal ImpLiquido { get; set; }
        public string? Usr { get; set; }
        public DateTime Ts { get; set; }
    }
    private sealed class Baseline { public string Usr { get; set; } = ""; public DateTime RunTs { get; set; } }

    private static int AddMonth(int yyyymm, int delta)
    {
        int y = yyyymm / 100, m = yyyymm % 100;
        int idx = y * 12 + (m - 1) + delta;
        return (idx / 12) * 100 + (idx % 12) + 1;
    }

    [Fact]
    public async Task Diagnostico_cambio_de_dato()
    {
        await using var meta = new SqlConnection(Cs);
        await meta.OpenAsync();

        var periodos = (await meta.QueryAsync<int>(
            @"SELECT TOP 3 (Anio*100+Mes) p FROM dbo.SubsidioCabezal WHERE Liquidar=1
              GROUP BY Anio, Mes HAVING COUNT(*) > 50 ORDER BY (Anio*100+Mes) DESC")).ToList();

        const string sel = @"SELECT TOP 1 c.IdSubsidio, c.ValorJornal, c.Dias, c.ImpNominal, c.ImpLiquido, c.Usr, c.Ts
                             FROM dbo.SubsidioCabezal c WHERE c.CI=@ci AND c.Anio=@anio AND c.Mes=@mes";

        int totalDato = 0;
        foreach (var p in periodos)
        {
            int anio = p / 100, mes = p % 100;
            int mesFin = AddMonth(p, -1), mesIni = AddMonth(p, -6), mesIniImp = AddMonth(p, -12);
            var baseline = await meta.QuerySingleOrDefaultAsync<Baseline>(
                @"SELECT TOP 1 Usr, MIN(Ts) RunTs FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes
                  GROUP BY Usr ORDER BY COUNT(*) DESC", new { anio, mes });

            var cis = (await meta.QueryAsync<long>(
                @"SELECT TOP 25 CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1
                  GROUP BY CI HAVING COUNT(*)=1 ORDER BY CI", new { anio, mes })).ToList();

            foreach (var ci in cis)
            {
                var prod = await meta.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                Cab? mine;
                await using (var cn = new SqlConnection(Cs))
                {
                    await cn.OpenAsync();
                    await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
                    var db = new ScopedDbExecutor(cn, tx);
                    await new SubsidioLiquidacionService(db, new FakeCurrentUser()).LiquidarAsync(anio, mes, true, ci);
                    mine = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                    await tx.RollbackAsync();
                }
                if (prod is null || mine is null) continue;

                bool vj = Math.Abs(prod.ValorJornal - mine.ValorJornal) < 0.5m;
                bool nom = Math.Abs(prod.ImpNominal - mine.ImpNominal) < 1m;
                bool esManual = !string.Equals(prod.Usr, baseline?.Usr, StringComparison.OrdinalIgnoreCase)
                                || (baseline is not null && Math.Abs((prod.Ts - baseline.RunTs).TotalHours) > 2);
                if ((vj && nom) || esManual) continue;

                totalDato++;
                _out.WriteLine($"== {p} CI {ci} (IdSubsidio {prod.IdSubsidio}) ==");
                _out.WriteLine($"   VJ:  prod {prod.ValorJornal,11:F2}  mine {mine.ValorJornal,11:F2}  {(vj ? "OK" : "DIFF")}");
                _out.WriteLine($"   Nom: prod {prod.ImpNominal,11:F2}  mine {mine.ImpNominal,11:F2}  {(nom ? "OK" : "DIFF")}");
                _out.WriteLine($"   Dias:prod {prod.Dias,11}  mine {mine.Dias,11}");

                // Snapshot por empresa del run (lo que se liquidó) vs imponible ACTUAL.
                var emps = (await meta.QueryAsync(
                    @"SELECT CodEmpresa, ValorJornal FROM dbo.SubsidioCabezalEmpresa WHERE IdSubsidio=@id ORDER BY CodEmpresa",
                    new { id = prod.IdSubsidio })).ToList();
                foreach (var e in emps)
                {
                    int cod = (int)e.CodEmpresa;
                    decimal vjRun = (decimal)e.ValorJornal;
                    var imp = await meta.QuerySingleOrDefaultAsync(
                        @"SELECT COUNT(*) Meses, ISNULL(SUM(Importe),0) Imp, ISNULL(SUM(DiasTrabajados),0) Dias
                          FROM dbo.Imponible WHERE CI=@ci AND CodEmpresa=@cod AND Concepto='1'
                            AND AnioMes BETWEEN @ini AND @fin", new { ci, cod, ini = mesIni, fin = mesFin });
                    int aporte = await meta.ExecuteScalarAsync<int>(
                        @"SELECT COUNT(*) FROM dbo.Imponible WHERE CI=@ci AND CodEmpresa=@cod AND Concepto='1'
                            AND Importe>0 AND AnioMes BETWEEN @ini AND @fin", new { ci, cod, ini = mesIniImp, fin = mesFin });
                    double impv = (double)imp!.Imp; int meses = (int)imp.Meses; int diasv = (int)imp.Dias;
                    _out.WriteLine($"     emp{cod,-4}: VJ_run {vjRun,10:F2} | hoy {meses}m dias={diasv} importe={impv,12:F2} /180={impv / 180.0,9:F2} (aporteOk={aporte}m)");
                }
            }
        }
        _out.WriteLine($"=== cambio-de-dato encontrados: {totalDato} ===");
    }
}
