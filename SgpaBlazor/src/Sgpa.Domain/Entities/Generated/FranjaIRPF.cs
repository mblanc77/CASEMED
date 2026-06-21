using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("FranjaIRPF")]
public partial class FranjaIRPF
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public double Desde { get; set; }

    [SgpaColumn(Order = 2)]
    public double? Hasta { get; set; }

    [SgpaColumn(Order = 3)]
    public double? Porcentaje { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
