using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, CodEmpresa, Fechaingreso, Mes, Anio.
[SgpaTable("SP_ImpLiquido")]
public partial class SP_ImpLiquido
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public DateTime Fechaingreso { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    [SgpaKey]
    public byte Mes { get; set; }

    [SgpaColumn(Order = 5, Required = true)]
    [SgpaKey]
    public int Anio { get; set; }

    [SgpaColumn(Order = 6)]
    public int? IdTrabaja { get; set; }

    [SgpaColumn(Order = 7)]
    public double? Importe { get; set; }

    [SgpaColumn(Order = 8)]
    public int? AnioMes { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
