using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SubsidioImponible")]
public partial class SubsidioImponible
{
    [SgpaColumn(Order = 1)]
    public int? IdSubsidio { get; set; }

    [SgpaColumn(Order = 2)]
    public byte? Mes { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Anio { get; set; }

    [SgpaColumn(Order = 4)]
    public int? CodEmpresa { get; set; }

    [SgpaColumn(Order = 5)]
    public int? Dias { get; set; }

    [SgpaColumn(Order = 6)]
    public double? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
