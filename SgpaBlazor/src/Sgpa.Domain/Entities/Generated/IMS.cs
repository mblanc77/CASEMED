using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: Mes, Anio.
[SgpaTable("IMS")]
public partial class IMS
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int Mes { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int Anio { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 4)]
    public int? AnioMes { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
