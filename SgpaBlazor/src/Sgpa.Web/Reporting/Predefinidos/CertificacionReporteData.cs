using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Una certificación del afiliado (vista dbo.Rpt_Certificacion, por CI). Port de Certificado(s).rpt.</summary>
public sealed class CertificacionDetalleLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public int NroLlamado { get; set; }
    public int NroRecibo { get; set; }
    public DateTime? FechaRecibido { get; set; }
    public DateTime? FechaCertificacion { get; set; }
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }
    public long CI { get; set; }
    public string? DescAfiliado { get; set; }
    public string? DescCertificador { get; set; }
    public string? DescAfeccionTipo { get; set; }
    public string? DescMutualista { get; set; }
    public string? Indicaciones { get; set; }

    // Cédula uruguaya con dígito verificador: último dígito = verificador, resto agrupado en miles (135.029-0).
    public string CIFormato
    {
        get
        {
            var s = CI.ToString();
            if (s.Length < 2) return s;
            var ver = s[^1];
            var num = long.Parse(s[..^1]).ToString("#,#", EsUy);
            return $"{num}-{ver}";
        }
    }

    public string NombreTexto => (DescAfiliado ?? "").Trim();
    public string CertificadorTexto => (DescCertificador ?? "").Trim();
    public string AfeccionTexto => (DescAfeccionTipo ?? "").Trim();
    public string MutualistaTexto => string.IsNullOrWhiteSpace(DescMutualista) ? "(sin mutualista)" : DescMutualista!.Trim();
    public string IndicacionesTexto => (Indicaciones ?? "").Trim();
    public string FechaRecibidoFmt => FechaRecibido is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string FechaCertFmt => FechaCertificacion is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string FechaIniFmt => FechaIni is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string FechaFinFmt => FechaFin is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
}

/// <summary>
/// Resumen de certificaciones de un afiliado por tipo de afección (agregado de dbo.Rpt_Certificacion).
/// Port de "Fecha de certificacion" / "Dias certificacion por afiliado".
/// </summary>
public sealed class CertificacionAfeccionLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public long CI { get; set; }
    public string? DescAfiliado { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? DescMutualista { get; set; }
    public string? DescAfeccionTipo { get; set; }
    public int Cantidad { get; set; }
    public int Dias { get; set; }

    public string CIFormato
    {
        get
        {
            var s = CI.ToString();
            if (s.Length < 2) return s;
            var ver = s[^1];
            var num = long.Parse(s[..^1]).ToString("#,#", EsUy);
            return $"{num}-{ver}";
        }
    }
    public string NombreTexto => (DescAfiliado ?? "").Trim();
    public string AfeccionTexto => (DescAfeccionTipo ?? "").Trim();
    public string MutualistaTexto => string.IsNullOrWhiteSpace(DescMutualista) ? "(sin mutualista)" : DescMutualista!.Trim();
    public string FNacFmt => FechaNacimiento is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    // Encabezado de grupo por afiliado: "135.029-0  CELI CEDREZ VERGARA".
    public string AfiliadoEncabezado => $"{CIFormato}   {NombreTexto}".Trim();
}

/// <summary>Provee los datos de los reportes predefinidos de certificaciones desde NewSgpa2.</summary>
public interface ICertificacionReporteData
{
    /// <summary>Certificaciones de las filas seleccionadas en la grilla (por NroLlamado), con mutualista del afiliado.</summary>
    Task<IReadOnlyList<CertificacionDetalleLinea>> GetByLlamadosAsync(IReadOnlyList<long> nroLlamados, CancellationToken ct = default);

    /// <summary>Resumen por afiliado + tipo de afección (cantidad + días) de las certificaciones seleccionadas.</summary>
    Task<IReadOnlyList<CertificacionAfeccionLinea>> GetAfeccionesByLlamadosAsync(IReadOnlyList<long> nroLlamados, CancellationToken ct = default);
}

public sealed class CertificacionReporteData(IDbExecutor db) : ICertificacionReporteData
{
    public Task<IReadOnlyList<CertificacionDetalleLinea>> GetByLlamadosAsync(IReadOnlyList<long> nroLlamados, CancellationToken ct = default)
    {
        if (nroLlamados is null || nroLlamados.Count == 0)
            return Task.FromResult<IReadOnlyList<CertificacionDetalleLinea>>(System.Array.Empty<CertificacionDetalleLinea>());
        return db.QueryAsync<CertificacionDetalleLinea>(
            @"SELECT c.NroLlamado, c.NroRecibo, c.FechaRecibido, c.FechaCertificacion, c.FechaIni, c.FechaFin, c.CI,
                     c.DescAfiliado, c.DescCertificador, c.DescAfeccionTipo, a.DescMutualista,
                     CAST(c.Indicaciones AS nvarchar(max)) AS Indicaciones
              FROM dbo.Rpt_Certificacion c
              LEFT JOIN dbo.Rpt_Afiliado a ON a.CI = c.CI
              WHERE c.NroLlamado IN @llamados
              ORDER BY c.DescAfiliado, c.FechaCertificacion DESC, c.NroLlamado DESC",
            new { llamados = nroLlamados }, cancellationToken: ct);
    }

    public Task<IReadOnlyList<CertificacionAfeccionLinea>> GetAfeccionesByLlamadosAsync(IReadOnlyList<long> nroLlamados, CancellationToken ct = default)
    {
        if (nroLlamados is null || nroLlamados.Count == 0)
            return Task.FromResult<IReadOnlyList<CertificacionAfeccionLinea>>(System.Array.Empty<CertificacionAfeccionLinea>());
        return db.QueryAsync<CertificacionAfeccionLinea>(
            @"SELECT c.CI, MAX(c.DescAfiliado) AS DescAfiliado, MAX(a.FechaNacimiento) AS FechaNacimiento,
                     MAX(a.DescMutualista) AS DescMutualista, c.DescAfeccionTipo,
                     COUNT(*) AS Cantidad, SUM(DATEDIFF(DAY, c.FechaIni, c.FechaFin) + 1) AS Dias
              FROM dbo.Rpt_Certificacion c
              LEFT JOIN dbo.Rpt_Afiliado a ON a.CI = c.CI
              WHERE c.NroLlamado IN @llamados
              GROUP BY c.CI, c.DescAfeccionTipo
              ORDER BY c.CI, c.DescAfeccionTipo",
            new { llamados = nroLlamados }, cancellationToken: ct);
    }
}
