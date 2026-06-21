using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("CertificacionesTmp")]
public partial class CertificacionesTmp
{
    [SgpaColumn(Order = 1)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public DateTime? FechaIni { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? FechaFin { get; set; }

    [SgpaColumn(Order = 4)]
    public double? ImporteDeducible { get; set; }

    [SgpaColumn(Order = 5)]
    public int? CodSalidaTipo { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
