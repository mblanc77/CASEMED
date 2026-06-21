using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IdSubsidio, CodSubsidioItemCod.
[SgpaTable("SubsidioItem")]
public partial class SubsidioItem
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSubsidio { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int CodSubsidioItemCod { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 4)]
    public bool? AbiEmp { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
