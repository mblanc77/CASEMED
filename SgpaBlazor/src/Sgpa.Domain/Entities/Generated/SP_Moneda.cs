using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_Moneda")]
public partial class SP_Moneda
{
    [SgpaColumn(Order = 1, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodMoneda { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Tasa { get; set; }

    [SgpaColumn(Order = 4)]
    public float? TasaMora { get; set; }

    [SgpaColumn(Order = 5)]
    public float? TasaCambio { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 2)]
    public string? CodAbitab { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? DescripLarga { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
