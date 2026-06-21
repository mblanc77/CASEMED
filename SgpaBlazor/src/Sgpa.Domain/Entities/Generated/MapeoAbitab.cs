using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: Inicio, Largo.
[SgpaTable("MapeoAbitab")]
public partial class MapeoAbitab
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int Inicio { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int Largo { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Campo { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public bool CodigoBarra { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
