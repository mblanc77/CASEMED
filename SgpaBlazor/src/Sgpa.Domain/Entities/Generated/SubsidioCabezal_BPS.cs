using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SubsidioCabezal_BPS")]
public partial class SubsidioCabezal_BPS
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSubsidio { get; set; }

    [SgpaColumn(Order = 2)]
    public int? DiasBPS { get; set; }

    [SgpaColumn(Order = 3)]
    public double? LiquidoBPS { get; set; }

    [SgpaColumn(Order = 4)]
    public double? AguinaldoBPS { get; set; }

    [SgpaColumn(Order = 5)]
    public double? LiquidoPagar { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
