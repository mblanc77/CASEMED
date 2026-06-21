using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

internal static class SubsidioFmt
{
    public static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public static string Cedula(long ci)
    {
        var s = ci.ToString();
        if (s.Length < 2) return s;
        return $"{long.Parse(s[..^1]).ToString("#,#", EsUy)}-{s[^1]}";
    }
}

/// <summary>Una línea del resumen de liquidación (cabezal por subsidio). Port de ResumenLiquidacion.xps.</summary>
public sealed class SubsidioResumenLinea
{
    public int IdSubsidio { get; set; }
    public long CI { get; set; }
    public string? DescAfiliado { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int Dias { get; set; }
    public double ImpNominal { get; set; }
    public double ImpAguinaldo { get; set; }
    public double ImpLiquido { get; set; }
    public bool Liquidar { get; set; }
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }

    public string CIFormato => SubsidioFmt.Cedula(CI);
    public string NombreTexto => (DescAfiliado ?? "").Trim();
    public string FNacFmt => FechaNacimiento is { } f ? f.ToString("dd/MM/yyyy", SubsidioFmt.EsUy) : "";
    public string PeriodoTexto => FechaIni is { } i && FechaFin is { } f
        ? $"{i:dd/MM/yyyy} - {f:dd/MM/yyyy}" : "";
    public string LiquidarMarca => Liquidar ? "X" : "";
    public string NominalFmt => ((decimal)ImpNominal).ToString("N2", SubsidioFmt.EsUy);
    public string AguinaldoFmt => ((decimal)ImpAguinaldo).ToString("N2", SubsidioFmt.EsUy);
    public string LiquidoFmt => ((decimal)ImpLiquido).ToString("N2", SubsidioFmt.EsUy);
}

/// <summary>Una línea del detalle de liquidación (imponible por empresa/mes). Port de DetalleLiquidacion.xps.</summary>
public sealed class SubsidioDetalleLinea
{
    public int IdSubsidio { get; set; }
    public long CI { get; set; }
    public string? DescAfiliado { get; set; }
    public string? DescEmpresa { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }
    public int Dias { get; set; }
    public double Importe { get; set; }

    public string CIFormato => SubsidioFmt.Cedula(CI);
    public string NombreTexto => (DescAfiliado ?? "").Trim();
    public string EmpresaTexto => (DescEmpresa ?? "").Trim();
    public string ImporteFmt => ((decimal)Importe).ToString("N2", SubsidioFmt.EsUy);
    // Encabezado de grupo por afiliado: "1.241.469-3  ESTRELLA MOUMDJIAN — CASMU IAMPP".
    public string AfiliadoEncabezado => $"{CIFormato}   {NombreTexto}".Trim();
}

/// <summary>Datos del recibo de subsidio (uno por cabezal). Port de Recibos.xps.</summary>
public sealed class SubsidioReciboData
{
    public int IdSubsidio { get; set; }
    public int? NroRecibo { get; set; }
    public long CI { get; set; }
    public string? DescAfiliado { get; set; }
    public double ImpLiquido { get; set; }
    public DateTime? FechaPago { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }

    public string CIFormato => SubsidioFmt.Cedula(CI);
    public string NombreTexto => (DescAfiliado ?? "").Trim();
    public string NroReciboFmt => (NroRecibo ?? 0).ToString("0000000");
    public string MesTexto => new DateTime(Anio, Mes >= 1 && Mes <= 12 ? Mes : 1, 1).ToString("MMM' - 'yyyy", SubsidioFmt.EsUy);
    public string PeriodoTexto => FechaIni is { } i && FechaFin is { } f
        ? $"{i:dd/MM/yyyy} - {f:dd/MM/yyyy}" : "";
    public string ImporteFmt => ((decimal)ImpLiquido).ToString("N2", SubsidioFmt.EsUy);
    public string FechaPagoFmt => FechaPago is { } f ? f.ToString("dd/MM/yyyy", SubsidioFmt.EsUy) : "";
}

/// <summary>Una línea de la obligación BPS: un código de aporte (jubilatorio/FRL/mutual) con su total. Port de InformeBPS.xps.</summary>
public sealed class SubsidioBpsLinea
{
    public int Cod { get; set; }
    public string? Descrip { get; set; }
    public string? Tipo { get; set; }       // "O" = obrero, "P" = patronal
    public double Importe { get; set; }

    public string DescripTexto => (Descrip ?? $"Código {Cod}").Trim();
    public string TipoTexto => Tipo?.Trim().ToUpperInvariant() switch { "O" => "Obrero", "P" => "Patronal", _ => "—" };
    public string ImporteFmt => ((decimal)Importe).ToString("N2", SubsidioFmt.EsUy);
}

/// <summary>
/// Obligación mensual BPS: los casilleros del informe (port de InformeBPS.xps). Valores agregados de los
/// SubsidioItem de los subsidios seleccionados. Código mutual (CASEMED) = 710; obrero/patronal por Tipo (O/P).
/// </summary>
public sealed class SubsidioBpsData
{
    private const int CodMutual = 710;   // CASEMED (3,00%) — el aporte mutual

    public string Periodo { get; init; } = "";
    public int Personas { get; init; }
    public decimal Gravado { get; init; }
    public decimal AporteObrero { get; init; }     // Tipo O excepto mutual (jubilatorio 15% + FRL)
    public decimal AportePatronal { get; init; }   // Tipo P (jubilatorio 7,5% + FRL)
    public decimal AporteMutual { get; init; }     // CASEMED (cod 710)

    public decimal ApObPat => AporteObrero + AportePatronal;
    public decimal TribMutual => Math.Round(Gravado * 0.005m, 2);    // 0,5% sobre el gravado
    public decimal ImpTributos => AporteObrero;                      // tributo (montepío) obrero retenido
    public decimal TotTributos => AporteObrero + AporteMutual + TribMutual;
    public decimal TotalGeneral => ApObPat + AporteMutual + TribMutual;

    private static string F(decimal v) => v.ToString("N2", SubsidioFmt.EsUy);
    public string GravadoFmt => F(Gravado);
    public string ImpTributosFmt => F(ImpTributos);
    public string AporteObreroFmt => F(AporteObrero);
    public string AportePatronalFmt => F(AportePatronal);
    public string ApObPatFmt => F(ApObPat);
    public string AporteMutualFmt => F(AporteMutual);
    public string TribMutualFmt => F(TribMutual);
    public string TotTributosFmt => F(TotTributos);
    public string TotalGeneralFmt => F(TotalGeneral);

    public static SubsidioBpsData Desde(IReadOnlyList<SubsidioBpsLinea> lineas, string periodo, int personas, decimal gravado)
        => new()
        {
            Periodo = periodo,
            Personas = personas,
            Gravado = gravado,
            AporteObrero = (decimal)lineas.Where(l => string.Equals(l.Tipo?.Trim(), "O", StringComparison.OrdinalIgnoreCase) && l.Cod != CodMutual).Sum(l => l.Importe),
            AportePatronal = (decimal)lineas.Where(l => string.Equals(l.Tipo?.Trim(), "P", StringComparison.OrdinalIgnoreCase)).Sum(l => l.Importe),
            AporteMutual = (decimal)lineas.Where(l => l.Cod == CodMutual).Sum(l => l.Importe),
        };
}

/// <summary>Provee los datos de los reportes predefinidos de subsidios (por selección de IdSubsidio) desde NewSgpa2.</summary>
public interface ISubsidioReporteData
{
    Task<IReadOnlyList<SubsidioResumenLinea>> GetResumenAsync(IReadOnlyList<long> ids, CancellationToken ct = default);
    Task<IReadOnlyList<SubsidioDetalleLinea>> GetDetalleAsync(IReadOnlyList<long> ids, CancellationToken ct = default);
    Task<IReadOnlyList<SubsidioReciboData>> GetRecibosAsync(IReadOnlyList<long> ids, CancellationToken ct = default);
    Task<SubsidioBpsData> GetBpsAsync(IReadOnlyList<long> ids, CancellationToken ct = default);
}

public sealed class SubsidioReporteData(IDbExecutor db) : ISubsidioReporteData
{
    private static bool Vacio(IReadOnlyList<long>? ids) => ids is null || ids.Count == 0;

    public Task<IReadOnlyList<SubsidioResumenLinea>> GetResumenAsync(IReadOnlyList<long> ids, CancellationToken ct = default)
        => Vacio(ids)
            ? Task.FromResult<IReadOnlyList<SubsidioResumenLinea>>(System.Array.Empty<SubsidioResumenLinea>())
            : db.QueryAsync<SubsidioResumenLinea>(
                @"SELECT c.IdSubsidio, c.CI, a.DescAfiliado, a.FechaNacimiento, c.Dias,
                         c.ImpNominal, c.ImpAguinaldo, c.ImpLiquido, c.Liquidar,
                         (SELECT MIN(e.FechaIni) FROM dbo.SubsidioEnfermedad e WHERE e.IdSubsidio = c.IdSubsidio) AS FechaIni,
                         (SELECT MAX(e.FechaFin) FROM dbo.SubsidioEnfermedad e WHERE e.IdSubsidio = c.IdSubsidio) AS FechaFin
                  FROM dbo.SubsidioCabezal c
                  LEFT JOIN dbo.Rpt_Afiliado a ON a.CI = c.CI
                  WHERE c.IdSubsidio IN @ids
                  ORDER BY a.DescAfiliado",
                new { ids }, cancellationToken: ct);

    public Task<IReadOnlyList<SubsidioDetalleLinea>> GetDetalleAsync(IReadOnlyList<long> ids, CancellationToken ct = default)
        => Vacio(ids)
            ? Task.FromResult<IReadOnlyList<SubsidioDetalleLinea>>(System.Array.Empty<SubsidioDetalleLinea>())
            : db.QueryAsync<SubsidioDetalleLinea>(
                @"SELECT IdSubsidio, CI, DescAfiliado, DescEmpresa, Mes, Anio, Dias, Importe
                  FROM dbo.[500_Rpt_DetalleSubsidio]
                  WHERE IdSubsidio IN @ids
                  ORDER BY DescAfiliado, Anio DESC, Mes DESC",
                new { ids }, cancellationToken: ct);

    public Task<IReadOnlyList<SubsidioReciboData>> GetRecibosAsync(IReadOnlyList<long> ids, CancellationToken ct = default)
        => Vacio(ids)
            ? Task.FromResult<IReadOnlyList<SubsidioReciboData>>(System.Array.Empty<SubsidioReciboData>())
            : db.QueryAsync<SubsidioReciboData>(
                @"SELECT c.IdSubsidio, c.NroRecibo, c.CI, a.DescAfiliado, c.ImpLiquido, c.FechaPago, c.Mes, c.Anio,
                         (SELECT MIN(e.FechaIni) FROM dbo.SubsidioEnfermedad e WHERE e.IdSubsidio = c.IdSubsidio) AS FechaIni,
                         (SELECT MAX(e.FechaFin) FROM dbo.SubsidioEnfermedad e WHERE e.IdSubsidio = c.IdSubsidio) AS FechaFin
                  FROM dbo.SubsidioCabezal c
                  LEFT JOIN dbo.Rpt_Afiliado a ON a.CI = c.CI
                  WHERE c.IdSubsidio IN @ids
                  ORDER BY c.NroRecibo",
                new { ids }, cancellationToken: ct);

    public async Task<SubsidioBpsData> GetBpsAsync(IReadOnlyList<long> ids, CancellationToken ct = default)
    {
        if (Vacio(ids)) return new SubsidioBpsData();

        // Aportes (jubilatorio, FRL, mutual) agregados por código de ítem para los subsidios seleccionados.
        var lineas = await db.QueryAsync<SubsidioBpsLinea>(
            @"SELECT i.CodSubsidioItemCod AS Cod, MAX(d.Descrip) AS Descrip, MAX(d.Tipo) AS Tipo,
                     SUM(CAST(i.Importe AS decimal(18,2))) AS Importe
              FROM dbo.SubsidioItem i
              JOIN dbo.SubsidioCabezal c ON c.IdSubsidio = i.IdSubsidio
              LEFT JOIN dbo.SubsidioItemCod d ON d.CodSubsidioItemCod = i.CodSubsidioItemCod
              WHERE c.IdSubsidio IN @ids
              GROUP BY i.CodSubsidioItemCod
              ORDER BY MAX(d.Tipo), i.CodSubsidioItemCod",
            new { ids }, cancellationToken: ct);

        // Cantidad de personas + monto gravado (nominal) + período del conjunto.
        var h = await db.QuerySingleOrDefaultAsync<(int Personas, decimal Gravado, int Mes, int Anio)>(
            @"SELECT COUNT(DISTINCT CI) AS Personas, ISNULL(SUM(CAST(ImpNominal AS decimal(18,2))), 0) AS Gravado,
                     MIN(Mes) AS Mes, MIN(Anio) AS Anio
              FROM dbo.SubsidioCabezal WHERE IdSubsidio IN @ids",
            new { ids }, cancellationToken: ct);

        var mes = h.Mes >= 1 && h.Mes <= 12 ? h.Mes : 1;
        var periodo = h.Anio > 0
            ? char.ToUpper(new DateTime(h.Anio, mes, 1).ToString("MMMM yyyy", SubsidioFmt.EsUy)[0]) +
              new DateTime(h.Anio, mes, 1).ToString("MMMM yyyy", SubsidioFmt.EsUy)[1..]
            : "";
        return SubsidioBpsData.Desde(lineas, periodo, h.Personas, h.Gravado);
    }
}
