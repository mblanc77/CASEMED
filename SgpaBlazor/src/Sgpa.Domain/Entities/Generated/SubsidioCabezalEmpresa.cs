using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IdSubsidio, CodEmpresa.
[SgpaTable("SubsidioCabezalEmpresa")]
public partial class SubsidioCabezalEmpresa
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSubsidio { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 3)]
    public float? ValorJornal { get; set; }

    [SgpaColumn(Order = 4)]
    public int? Dias { get; set; }

    [SgpaColumn(Order = 5)]
    public double? ImpNominal { get; set; }

    [SgpaColumn(Order = 6)]
    public double? ImpAguinaldo { get; set; }

    [SgpaColumn(Order = 7)]
    public double? ImpLiquido { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
