using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PrimaFallecimiento")]
public partial class PrimaFallecimiento
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2)]
    public DateTime? FechaFirma { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? FechaFallecimiento { get; set; }

    [SgpaColumn(Order = 4)]
    public double? Importe { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaPago { get; set; }

    [SgpaColumn(Order = 6)]
    public string? Observaciones { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
