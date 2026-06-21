using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: PrestamoEstadoAnt, PrestamoEstadoSig.
[SgpaTable("SP_CtrlPrestamoEstado")]
public partial class SP_CtrlPrestamoEstado
{
    [SgpaColumn(Order = 1, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? PrestamoEstadoAnt { get; set; }

    [SgpaColumn(Order = 2, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? PrestamoEstadoSig { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
