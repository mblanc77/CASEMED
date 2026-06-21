using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("RegimenAporte")]
public partial class RegimenAporte
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodRegimenAporte { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Porcentaje { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 50)]
    public string? Descrip { get; set; }

}
