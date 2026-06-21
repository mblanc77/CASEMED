using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("AdPreJub")]
public partial class AdPreJub
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2)]
    public DateTime? FechaPresentacion { get; set; }

    [SgpaColumn(Order = 3)]
    public int? ImporteMensual { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaJubilacion { get; set; }

    [SgpaColumn(Order = 5)]
    public string? Observaciones { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
