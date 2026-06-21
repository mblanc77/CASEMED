namespace Sgpa.Business.Subsidios;

/// <summary>
/// Tramo de certificación ya consolidado (merge de certificaciones consecutivas).
/// Reemplaza a la tabla temporal CertificacionesTmp del VB6 (se arma en memoria).
/// </summary>
public sealed class CertificacionSpan
{
    public long CI { get; set; }
    public DateTime FechaIni { get; set; }
    public DateTime FechaFin { get; set; }
    public double ImporteDeducible { get; set; }
    public int? CodSalidaTipo { get; set; }
}
