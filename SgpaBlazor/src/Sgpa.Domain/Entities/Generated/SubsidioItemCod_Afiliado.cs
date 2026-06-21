using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SubsidioItemCod_Afiliado")]
public partial class SubsidioItemCod_Afiliado
{
    [SgpaColumn(Order = 1)]
    [SgpaKey(IsIdentity = true)]
    public int SubItmCodAfiId { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CodSubsidioItemCod { get; set; }

    [SgpaColumn(Order = 3)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 4)]
    public double? Valor { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? Vigencia { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
