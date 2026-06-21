using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IDPrestamo, Fecha.
[SgpaTable("SP_Retencion")]
public partial class SP_Retencion
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDPrestamo { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public DateTime Fecha { get; set; }

    [SgpaColumn(Order = 3)]
    public float? TipoCambio { get; set; }

    [SgpaColumn(Order = 4)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 5)]
    public string? Observaciones { get; set; }

    [SgpaColumn(Order = 6, Required = true)]
    public bool Directa { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
