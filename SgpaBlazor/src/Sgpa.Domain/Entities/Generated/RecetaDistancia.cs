using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("RecetaDistancia")]
public partial class RecetaDistancia
{
    [SgpaColumn(Order = 1, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodRecetaDistancia { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
