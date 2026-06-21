using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IDPrestamo, Fecha, IDFactura, CodRetencionItemCod.
[SgpaTable("SP_RetencionItem")]
public partial class SP_RetencionItem
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDPrestamo { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public DateTime Fecha { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int IDFactura { get; set; }

    [SgpaColumn(Order = 4, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodRetencionItemCod { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
