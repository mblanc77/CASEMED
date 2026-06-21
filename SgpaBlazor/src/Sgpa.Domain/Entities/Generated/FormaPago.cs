using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("FormaPago")]
public partial class FormaPago
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodFormaPago { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? Descrip { get; set; }

}
