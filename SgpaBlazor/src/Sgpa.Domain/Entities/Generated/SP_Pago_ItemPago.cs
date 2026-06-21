using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IDFactura, CodItemPago, NroCuota.
[SgpaTable("SP_Pago_ItemPago")]
public partial class SP_Pago_ItemPago
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDFactura { get; set; }

    [SgpaColumn(Order = 2, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodItemPago { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int NroCuota { get; set; }

    [SgpaColumn(Order = 4)]
    public float? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
