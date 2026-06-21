using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IdFactura, NroReng.
[SgpaTable("SP_FacturaDetalle")]
public partial class SP_FacturaDetalle
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdFactura { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int NroReng { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 3)]
    public string? CodItemPago { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 5)]
    public int? NroCuota { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
