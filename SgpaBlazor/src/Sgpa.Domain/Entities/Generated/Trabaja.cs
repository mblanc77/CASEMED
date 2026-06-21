using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, CodEmpresa, FechaIngreso.
[SgpaTable("Trabaja")]
public partial class Trabaja
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public DateTime FechaIngreso { get; set; }

    [SgpaColumn(Order = 4)]
    public int? IdTrabaja { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaBaja { get; set; }

    [SgpaColumn(Order = 6)]
    public int? CodBajaMotivo { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 20)]
    public string? NroFichaEmpresa { get; set; }

    [SgpaColumn(Order = 8)]
    public DateTime? FechaIngCasemed { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
