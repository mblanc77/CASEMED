using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SP_PrestamoComentario")]
public partial class SP_PrestamoComentario
{
    [SgpaColumn(Order = 1)]
    public int? IDPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 3)]
    public string? Observaciones { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
