using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("AfeccionGrupo")]
public partial class AfeccionGrupo
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodAfeccionGrupo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CodPatologia { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 50)]
    public string? Descrip { get; set; }

}
