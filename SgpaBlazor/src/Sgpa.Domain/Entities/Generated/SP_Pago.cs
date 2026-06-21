using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_Pago")]
public partial class SP_Pago
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDFactura { get; set; }

    [SgpaColumn(Order = 2)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? CodSucursal { get; set; }

    [SgpaColumn(Order = 5)]
    public float? TasaCambio { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 3)]
    public string? CodPagoOrigen { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
