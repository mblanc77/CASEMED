using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("AfeccionTipo")]
public partial class AfeccionTipo
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodAfeccionTipo { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 200)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3)]
    public int? CodAfeccionGrupo { get; set; }

    [SgpaColumn(Order = 4)]
    public int? CodDiameg { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
