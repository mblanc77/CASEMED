using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, Mes, Anio.
[SgpaTable("AdPreJubPago")]
public partial class AdPreJubPago
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int Mes { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int Anio { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 6)]
    public string? Observaciones { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
