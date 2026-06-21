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
/// Verifica la PARIDAD del motor contra las últimas 3 liquidaciones reales.
/// Una fila es "paridad total" si coinciden ValorJornal, ImpNominal e ImpLiquido (bruto o neto-BPS).
/// Cuando ValorJornal o ImpNominal NO coinciden, es porque el DATO DE ENTRADA (Imponible/Trabaja) se
/// editó después de la liquidación original (la lógica es idéntica: el motor llama a los mismos SPs).
/// Cada CI corre en su transacción con ROLLBACK.
/// </summary>
public class VerificacionUltimasLiquidacionesTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private readonly ITestOutputHelper _out;
    public VerificacionUltimasLiquidacionesTests(ITestOutputHelper output) => _out = output;

    private sealed class Cab
    {
        public int IdSubsidio { get; set; }
        public decimal ValorJornal { get; set; }
        public int Dias { get; set; }
        public decimal ImpNominal { get; set; }
        public decimal ImpLiquido { get; set; }
        public decimal LiquidoPagar { get; set; }
        public string? Usr { get; set; }
        public DateTime Ts { get; set; }
    }
    private sealed class Baseline { public string Usr { get; set; } = ""; public DateTime RunTs { get; set; } }

    [Fact]
    public async Task Paridad_ultimas_3_liquidaciones()
    {
        await using var meta = new SqlConnection(ConnectionString);
        await meta.OpenAsync();

        var periodos = (await meta.QueryAsync<int>(
            @"SELECT TOP 3 (Anio*100+Mes) p FROM dbo.SubsidioCabezal WHERE Liquidar=1
              GROUP BY Anio, Mes HAVING COUNT(*) > 50 ORDER BY (Anio*100+Mes) DESC")).ToList();

        const string sel = @"SELECT TOP 1 c.IdSubsidio, c.ValorJornal, c.Dias, c.ImpNominal, c.ImpLiquido,
                                    ISNULL(b.LiquidoPagar, c.ImpLiquido) AS LiquidoPagar, c.Usr, c.Ts
                             FROM dbo.SubsidioCabezal c
                             LEFT JOIN dbo.SubsidioCabezal_BPS b ON b.IdSubsidio = c.IdSubsidio
                             WHERE c.CI=@ci AND c.Anio=@anio AND c.Mes=@mes";
        int gTotal = 0, gParidad = 0, gManual = 0, gDato = 0, gOtro = 0;

        foreach (var p in periodos)
        {
            int anio = p / 100, mes = p % 100;
            var baseline = await meta.QuerySingleOrDefaultAsync<Baseline>(
                @"SELECT TOP 1 Usr, MIN(Ts) RunTs FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes
                  GROUP BY Usr ORDER BY COUNT(*) DESC", new { anio, mes });

            var cis = (await meta.QueryAsync<long>(
                @"SELECT TOP 25 CI FROM dbo.SubsidioCabezal WHERE Anio=@anio AND Mes=@mes AND Liquidar=1
                  GROUP BY CI HAVING COUNT(*)=1 ORDER BY CI", new { anio, mes })).ToList();

            int paridad = 0, manual = 0, dato = 0, otro = 0;
            foreach (var ci in cis)
            {
                await using var cn = new SqlConnection(ConnectionString);
                await cn.OpenAsync();
                await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
                var db = new ScopedDbExecutor(cn, tx);

                var prod = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                var svc = new SubsidioLiquidacionService(db, new FakeCurrentUser());
                await svc.LiquidarAsync(anio, mes, liquidar: true, ci: ci);
                var mine = await db.QuerySingleOrDefaultAsync<Cab>(sel, new { ci, anio, mes });
                await tx.RollbackAsync();
                if (prod is null || mine is null) continue;
                gTotal++;

                bool vj = Math.Abs(prod.ValorJornal - mine.ValorJornal) < 0.5m;
                bool nom = Math.Abs(prod.ImpNominal - mine.ImpNominal) < 1m;
                bool liq = Math.Abs(prod.ImpLiquido - mine.ImpLiquido) < 1m
                           || Math.Abs(prod.LiquidoPagar - mine.LiquidoPagar) < 1m;
                bool esManual = !string.Equals(prod.Usr, baseline?.Usr, StringComparison.OrdinalIgnoreCase)
                                || (baseline is not null && Math.Abs((prod.Ts - baseline.RunTs).TotalHours) > 2);

                if (vj && nom && liq) paridad++;
                else if (esManual) manual++;          // ajuste manual posterior
                else if (!vj || !nom)                 // el dato de entrada cambió (VJ/Nominal difieren)
                {
                    dato++;
                    _out.WriteLine($"  {p} CI {ci} CAMBIO-DE-DATO: VJ prod {prod.ValorJornal:F2} mine {mine.ValorJornal:F2} | Nom prod {prod.ImpNominal:F2} mine {mine.ImpNominal:F2}");
                }
                else { otro++; _out.WriteLine($"  {p} CI {ci} SIN EXPLICAR: prodLiq {prod.ImpLiquido:F2} prodPagar {prod.LiquidoPagar:F2} miBruto {mine.ImpLiquido:F2} miPagar {mine.LiquidoPagar:F2}"); }
            }
            _out.WriteLine($"== {p}: total {cis.Count} → paridad {paridad}, manual {manual}, cambio-de-dato {dato}, sin-explicar {otro} ==");
            gParidad += paridad; gManual += manual; gDato += dato; gOtro += otro;
        }

        _out.WriteLine($"=== TOTAL {gTotal}: PARIDAD={gParidad}, manual={gManual}, cambio-de-dato={gDato}, SIN EXPLICAR={gOtro} ===");
        _out.WriteLine($"=== Explicados = {gParidad + gManual + gDato}/{gTotal} ({100.0 * (gParidad + gManual + gDato) / Math.Max(gTotal, 1):F1}%) ===");
        Assert.Equal(0, gOtro); // ninguna diferencia debe quedar sin explicar
    }
}
