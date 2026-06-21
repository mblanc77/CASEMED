using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Cuenta")]
public partial class Cuenta
{
    [SgpaColumn(Order = 1)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CodBanco { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? NroCuenta { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
