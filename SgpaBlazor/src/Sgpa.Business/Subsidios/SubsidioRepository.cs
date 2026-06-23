using System.Data;
using Sgpa.Data;

namespace Sgpa.Business.Subsidios;

public sealed class SubsidioRepository : ISubsidioRepository
{
    private readonly IDbExecutor _db;

    public SubsidioRepository(IDbExecutor db) => _db = db;

    public Task<IReadOnlyList<long>> SeleccionarAfiliadosAsync(SubsidioPeriodo periodo, bool liquidar, long? ci = null,
        CancellationToken cancellationToken = default)
    {
        // Port del SELECT DISTINCT CI de LiquidarSubsidio: el período cae dentro del rango de la
        // certificación (yyyymm de FechaIni..FechaFin) y el afiliado trabaja en una empresa con
        // Liquidar=@liquidar, con ingreso a Casemed <= período y sin baja anterior al período.
        var sql = @"
SELECT DISTINCT c.CI
FROM dbo.Certificacion c
WHERE @lMes BETWEEN (YEAR(c.FechaIni) * 100 + MONTH(c.FechaIni))
                AND (YEAR(c.FechaFin) * 100 + MONTH(c.FechaFin))
  AND c.CI IN (
        SELECT t.CI
        FROM dbo.Trabaja t
        INNER JOIN dbo.Empresa e ON t.CodEmpresa = e.CodEmpresa
        WHERE e.Liquidar = @liquidar
          -- La baja en el MISMO mes del período aún cuenta: el afiliado trabajó parte del mes y puede tener
          -- certificación vigente (>= y no >). Verificado contra la liquidación VB6 05/2026/04/03: con > se
          -- perdían afiliados con FechaBaja en el mes (p. ej. CI 17535941 baja 13/03, CI 27788467 baja 31/03).
          AND (t.FechaBaja IS NULL OR (YEAR(t.FechaBaja) * 100 + MONTH(t.FechaBaja)) >= @lMes)
          AND (YEAR(t.FechaIngCasemed) * 100 + MONTH(t.FechaIngCasemed)) <= @lMes
  )";
        if (ci.HasValue)
            sql += " AND c.CI = @ci";

        return _db.QueryAsync<long>(sql, new { lMes = periodo.YyyyMm, liquidar, ci }, cancellationToken: cancellationToken);
    }

    public Task<int> BorrarPeriodoAsync(SubsidioPeriodo periodo, bool liquidar, long? ci = null,
        CancellationToken cancellationToken = default)
    {
        var sql = "DELETE FROM dbo.SubsidioCabezal WHERE (Anio * 100 + Mes) = @lMes AND Liquidar = @liquidar";
        if (ci.HasValue)
            sql += " AND CI = @ci";

        return _db.ExecuteAsync(sql, new { lMes = periodo.YyyyMm, liquidar, ci }, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<CertificacionSpan>> GenerarCertificacionesAsync(long ci, SubsidioPeriodo periodo,
        CancellationToken cancellationToken = default)
    {
        var raw = await _db.QueryProcAsync<CertRow>("dbo.acc_sgpa_300_CertificacionesAfiliadoMes",
            new { pCI = ci, pMes = periodo.YyyyMm }, cancellationToken).ConfigureAwait(false);

        var spans = new List<CertificacionSpan>();
        foreach (var r in raw.OrderBy(x => x.FechaIni))
        {
            var last = spans.Count > 0 ? spans[^1] : null;
            if (last is not null && (r.FechaIni - last.FechaFin).Days == 1)
            {
                // Certificación consecutiva: se extiende el tramo y se acumula el deducible.
                last.FechaFin = r.FechaFin;
                last.ImporteDeducible += r.ImporteDeducible;
            }
            else
            {
                spans.Add(new CertificacionSpan
                {
                    CI = ci,
                    FechaIni = r.FechaIni,
                    FechaFin = r.FechaFin,
                    ImporteDeducible = r.ImporteDeducible,
                    CodSalidaTipo = r.CodSalidaTipo
                });
            }
        }
        return spans;
    }

    public async Task<int> ProximoIdSubsidioAsync(CancellationToken cancellationToken = default)
        => await _db.ExecuteScalarAsync<int>(
            "SELECT ISNULL(MAX(IdSubsidio), 0) + 1 FROM dbo.SubsidioCabezal", cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    public async Task<bool> ExistenCabezalesSinEnfermedadAsync(int anio, int mes, CancellationToken cancellationToken = default)
        => await _db.ExecuteScalarAsync<int>(
            @"SELECT CASE WHEN EXISTS (
                  SELECT 1 FROM dbo.SubsidioCabezal sc
                  WHERE sc.Mes = @mes AND sc.Anio = @anio
                    AND NOT EXISTS (SELECT 1 FROM dbo.SubsidioEnfermedad e WHERE e.IdSubsidio = sc.IdSubsidio))
              THEN 1 ELSE 0 END",
            new { anio, mes }, cancellationToken: cancellationToken).ConfigureAwait(false) == 1;

    public Task InsertarCabezalAsync(int idSubsidio, long ci, SubsidioPeriodo periodo, bool liquidar, string usr,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioCabezal (IdSubsidio, Mes, Anio, CI, Liquidar, ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido, Usr, Ts)
              VALUES (@idSubsidio, @mes, @anio, @ci, @liquidar, 0, 0, 0, 0, 0, @usr, SYSDATETIME())",
            new { idSubsidio, mes = periodo.Mes, anio = periodo.Anio, ci, liquidar, usr },
            cancellationToken: cancellationToken);

    public Task GenerarNrosReciboAsync(SubsidioPeriodo periodo, bool liquidar, long? ci,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"DECLARE @base int = ISNULL((SELECT MAX(NroRecibo) FROM dbo.SubsidioCabezal), 0);
              ;WITH c AS (
                  SELECT IdSubsidio, ROW_NUMBER() OVER (ORDER BY CI, IdSubsidio) AS rn
                  FROM dbo.SubsidioCabezal
                  WHERE Anio = @anio AND Mes = @mes AND Liquidar = @liquidar
                    AND (@ci IS NULL OR CI = @ci))
              UPDATE sc SET NroRecibo = @base + c.rn
              FROM dbo.SubsidioCabezal sc INNER JOIN c ON c.IdSubsidio = sc.IdSubsidio",
            new { anio = periodo.Anio, mes = periodo.Mes, liquidar, ci },
            cancellationToken: cancellationToken);

    public Task ActualizarCabezalMontosAsync(int idSubsidio, decimal valorJornal, int dias,
        decimal impNominal, decimal impAguinaldo, decimal impLiquido, string usr,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"UPDATE dbo.SubsidioCabezal
              SET ValorJornal = @valorJornal, Dias = @dias, ImpNominal = @impNominal,
                  ImpAguinaldo = @impAguinaldo, ImpLiquido = @impLiquido, Usr = @usr, Ts = SYSDATETIME()
              WHERE IdSubsidio = @idSubsidio",
            new { idSubsidio, valorJornal, dias, impNominal, impAguinaldo, impLiquido, usr },
            cancellationToken: cancellationToken);

    public Task InsertarCabezalEmpresaAsync(int idSubsidio, int codEmpresa, decimal valorJornal, int dias,
        decimal impNominal, decimal impAguinaldo, decimal impLiquido, string usr,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioCabezalEmpresa (IdSubsidio, CodEmpresa, ValorJornal, Dias, ImpNominal, ImpAguinaldo, ImpLiquido, Usr, Ts)
              VALUES (@idSubsidio, @codEmpresa, @valorJornal, @dias, @impNominal, @impAguinaldo, @impLiquido, @usr, SYSDATETIME())",
            new { idSubsidio, codEmpresa, valorJornal, dias, impNominal, impAguinaldo, impLiquido, usr },
            cancellationToken: cancellationToken);

    public Task<IReadOnlyList<CabezalEmpresa>> GetCabezalEmpresasAsync(int idSubsidio, CancellationToken cancellationToken = default)
        => _db.QueryAsync<CabezalEmpresa>(
            "SELECT CodEmpresa, ValorJornal FROM dbo.SubsidioCabezalEmpresa WHERE IdSubsidio = @idSubsidio",
            new { idSubsidio }, cancellationToken: cancellationToken);

    public Task<IReadOnlyList<CabezalEmpresaMontos>> GetCabezalEmpresasMontosAsync(int idSubsidio, CancellationToken cancellationToken = default)
        => _db.QueryAsync<CabezalEmpresaMontos>(
            "SELECT CodEmpresa, ImpNominal, ImpAguinaldo FROM dbo.SubsidioCabezalEmpresa WHERE IdSubsidio = @idSubsidio",
            new { idSubsidio }, cancellationToken: cancellationToken);

    public Task InsertarItemEmpresaAsync(int idSubsidio, long codSubsidioItemCod, int codEmpresa, decimal importe, string usr,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioItemEmpresa (IdSubsidio, CodSubsidioItemCod, CodEmpresa, Importe, Usr, Ts)
              VALUES (@idSubsidio, @codSubsidioItemCod, @codEmpresa, @importe, @usr, SYSDATETIME())",
            new { idSubsidio, codSubsidioItemCod, codEmpresa, importe, usr }, cancellationToken: cancellationToken);

    public Task ActualizarCabezalEmpresaFinalAsync(int idSubsidio, int codEmpresa, decimal impNominal, decimal impAguinaldo,
        decimal impLiquido, int dias, string usr, CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"UPDATE dbo.SubsidioCabezalEmpresa
              SET ImpNominal = @impNominal, ImpAguinaldo = @impAguinaldo, ImpLiquido = @impLiquido, Dias = @dias, Usr = @usr, Ts = SYSDATETIME()
              WHERE IdSubsidio = @idSubsidio AND CodEmpresa = @codEmpresa",
            new { idSubsidio, codEmpresa, impNominal, impAguinaldo, impLiquido, dias, usr }, cancellationToken: cancellationToken);

    public Task ActualizarCabezalEmpresaMontosAsync(int idSubsidio, int codEmpresa, decimal impNominal, decimal impAguinaldo,
        string usr, CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"UPDATE dbo.SubsidioCabezalEmpresa
              SET ImpNominal = @impNominal, ImpAguinaldo = @impAguinaldo, Usr = @usr, Ts = SYSDATETIME()
              WHERE IdSubsidio = @idSubsidio AND CodEmpresa = @codEmpresa",
            new { idSubsidio, codEmpresa, impNominal, impAguinaldo, usr }, cancellationToken: cancellationToken);

    public Task InsertarEnfermedadAsync(int idSubsidio, DateTime fechaIni, DateTime fechaFin,
        DateTime fechaIniSubsidio, DateTime fechaFinSubsidio, string usr, CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioEnfermedad (IdSubsidio, FechaIni, FechaFin, FechaIniSubsidio, FechaFinSubsidio, Usr, Ts)
              VALUES (@idSubsidio, @fechaIni, @fechaFin, @fechaIniSubsidio, @fechaFinSubsidio, @usr, SYSDATETIME())",
            new { idSubsidio, fechaIni, fechaFin, fechaIniSubsidio, fechaFinSubsidio, usr }, cancellationToken: cancellationToken);

    public async Task<bool> ExisteCertificacionEnFechaAsync(long ci, DateTime fecha, CancellationToken cancellationToken = default)
    {
        // El SP devuelve filas (no un count); basta con que exista alguna (como el "Not rs.EOF" del VB6).
        var rows = await _db.QueryProcAsync<dynamic>("dbo.acc_sgpa_300_CertificacionCIDia",
            new { pCI = ci, pFecha = fecha }, cancellationToken).ConfigureAwait(false);
        return rows.Count > 0;
    }

    public Task<SubsidioParametros> GetParametrosAsync(CancellationToken cancellationToken = default)
        => _db.QuerySingleOrDefaultAsync<SubsidioParametros>(
            @"SELECT TOP 1 CAST(SMN AS decimal(18,4)) AS SMN,
                     CAST(TopeJubilatorio AS decimal(18,4)) AS TopeJubilatorio,
                     CAST(PctBPS AS decimal(18,6)) AS PctBPS,
                     CAST(TopeLiquidoBPS AS decimal(18,4)) AS TopeLiquidoBPS
              FROM dbo.Parametros", cancellationToken: cancellationToken)!;

    public async Task<int> GetCodCasemedAsync(CancellationToken cancellationToken = default)
    {
        var v = await _db.QuerySingleOrDefaultAsync<string?>(
            "SELECT TOP 1 value1 FROM dbo.xUsrParam WHERE clave = 'CodCasemed' AND value1 IS NOT NULL ORDER BY login",
            cancellationToken: cancellationToken).ConfigureAwait(false);
        return int.TryParse(v, out var cod) && cod > 0 ? cod : 900; // 900 = "SUBSIDIO POR ENF." (fallback)
    }

    public Task<IReadOnlyList<ItemCodConfig>> GetItemsCodFullAsync(long ci, SubsidioPeriodo periodo, CancellationToken cancellationToken = default)
        => _db.QueryProcAsync<ItemCodConfig>("dbo.acc_sgpa_300_SubsidioItemCod_Full",
            new { pFecha = new DateTime(periodo.Anio, periodo.Mes, 1), pCI = ci }, cancellationToken);

    public Task InsertarItemAsync(int idSubsidio, long codSubsidioItemCod, decimal importe, string usr, CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioItem (IdSubsidio, CodSubsidioItemCod, Importe, Usr, Ts)
              VALUES (@idSubsidio, @codSubsidioItemCod, @importe, @usr, SYSDATETIME())",
            new { idSubsidio, codSubsidioItemCod, importe, usr }, cancellationToken: cancellationToken);

    public async Task<int> GetRegimenJubilatorioAsync(long ci, CancellationToken cancellationToken = default)
    {
        var row = (await _db.QueryProcAsync<RegimenRow>("dbo.acc_sgpa_300_RegimenJubilatorioAfiliado",
            new { pCI = ci }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        var cod = row?.CodRegimenJubilatorio ?? 0;
        return cod > 0 ? cod : 2; // PC_CODREGIMENJUBILATORIO_NUEVO
    }

    public async Task<decimal> GetSubsidioItemValorAsync(long codSubsidioItemCod, CancellationToken cancellationToken = default)
    {
        var row = (await _db.QueryProcAsync<ValorRow>("dbo.acc_sgpa_300_SubsidioItemId",
            new { pCodSubsidioItemCod = codSubsidioItemCod }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        return (decimal)(row?.Valor ?? 0d);
    }

    public async Task<decimal?> GetImporteFranjaAnteriorAsync(long codSubsidioItemCod, decimal importe, decimal smn, CancellationToken cancellationToken = default)
    {
        var row = (await _db.QueryProcAsync<FranjaRow>("dbo.acc_sgpa_300_SubsidioFranjaAnt",
            new { pCodSubsidioItemCod = codSubsidioItemCod, pImporte = importe, pSMN = smn }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        return row is null ? null : (decimal)row.ImpFrjAnt;
    }

    private sealed class CertRow
    {
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public double ImporteDeducible { get; set; }
        public int? CodSalidaTipo { get; set; }
    }

    public Task<EnfermedadFechas?> GetEnfermedadFechasAsync(int idSubsidio, CancellationToken cancellationToken = default)
        => _db.QuerySingleOrDefaultAsync<EnfermedadFechas>(
            "SELECT TOP 1 FechaIni, FechaIniSubsidio, FechaFinSubsidio FROM dbo.SubsidioEnfermedad WHERE IdSubsidio = @idSubsidio",
            new { idSubsidio }, cancellationToken: cancellationToken);

    public Task<int?> GetCodSalidaTipoAsync(long ci, DateTime fechaIni, CancellationToken cancellationToken = default)
        => _db.QuerySingleOrDefaultAsync<int?>(
            "SELECT TOP 1 CodSalidaTipo FROM dbo.Certificacion WHERE Efectiva = 1 AND CI = @ci AND FechaIni = @fechaIni",
            new { ci, fechaIni }, cancellationToken: cancellationToken);

    public Task<CertFechas?> GetCertificacionPorFechaFinAsync(long ci, DateTime fechaFin, CancellationToken cancellationToken = default)
        => _db.QuerySingleOrDefaultAsync<CertFechas>(
            "SELECT TOP 1 FechaIni, FechaFin FROM dbo.Certificacion WHERE Efectiva = 1 AND CI = @ci AND FechaFin = @fechaFin",
            new { ci, fechaFin }, cancellationToken: cancellationToken);

    public async Task<decimal> GetTotalLiquidoBpsAsync(long ci, int mes, int anio, CancellationToken cancellationToken = default)
    {
        var row = (await _db.QueryProcAsync<BpsTotalRow>("dbo.acc_sgpa_506_TotalLiquidoBPSCIMes",
            new { pCI = ci, pMes = mes, pAnio = anio }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        return (decimal)(row?.LiquidoBPS ?? 0d);
    }

    public Task InsertarCabezalBpsAsync(int idSubsidio, int diasBps, decimal liquidoBps, decimal liquidoPagar, string usr,
        CancellationToken cancellationToken = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SubsidioCabezal_BPS (IdSubsidio, DiasBPS, LiquidoBPS, AguinaldoBPS, LiquidoPagar, Usr, Ts)
              VALUES (@idSubsidio, @diasBps, @liquidoBps, 0, @liquidoPagar, @usr, SYSDATETIME())",
            new { idSubsidio, diasBps, liquidoBps, liquidoPagar, usr }, cancellationToken: cancellationToken);

    private sealed class BpsTotalRow { public double? LiquidoBPS { get; set; } }
    private sealed class RegimenRow { public int? CodRegimenJubilatorio { get; set; } }
    private sealed class ValorRow { public double? Valor { get; set; } }
    private sealed class FranjaRow { public double ImpFrjAnt { get; set; } }
}
