using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Parametros")]
public partial class Parametros
{
    [SgpaColumn(Order = 1)]
    public double? SMN { get; set; }

    [SgpaColumn(Order = 2)]
    public float? TopeJubilatorio { get; set; }

    [SgpaColumn(Order = 3)]
    public float? TopePrima { get; set; }

    [SgpaColumn(Order = 4)]
    public float? UR { get; set; }

    [SgpaColumn(Order = 5)]
    public float? PctAdPreJub { get; set; }

    [SgpaColumn(Order = 6)]
    public double? BCP { get; set; }

    [SgpaColumn(Order = 7)]
    public double? TopeLiquidoBPS { get; set; }

    [SgpaColumn(Order = 8)]
    public double? PctBPS { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
