using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SP_Trabaja")]
public partial class SP_Trabaja
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public short? CodEmpresa { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? FechaIngreso { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaBaja { get; set; }

    [SgpaColumn(Order = 5)]
    public int? CodBajaMotivo { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 20)]
    public string? NroFichaEmpresa { get; set; }

    [SgpaColumn(Order = 7)]
    public int? IdTrabaja { get; set; }

    [SgpaColumn(Order = 8)]
    public DateTime? FechaIngCasemed { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
