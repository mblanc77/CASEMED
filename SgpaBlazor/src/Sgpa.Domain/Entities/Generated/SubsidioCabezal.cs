using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SubsidioCabezal")]
public partial class SubsidioCabezal
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSubsidio { get; set; }

    [SgpaColumn(Order = 2)]
    public byte? Mes { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Anio { get; set; }

    [SgpaColumn(Order = 4)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 5, Required = true)]
    public bool Liquidar { get; set; }

    [SgpaColumn(Order = 6)]
    public float? ValorJornal { get; set; }

    [SgpaColumn(Order = 7)]
    public int? Dias { get; set; }

    [SgpaColumn(Order = 8)]
    public double? ImpNominal { get; set; }

    [SgpaColumn(Order = 9)]
    public double? ImpAguinaldo { get; set; }

    [SgpaColumn(Order = 10)]
    public double? ImpLiquido { get; set; }

    [SgpaColumn(Order = 11)]
    public int? NroRecibo { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FechaPago { get; set; }

    [SgpaColumn(Order = 13)]
    public int? CodBanco { get; set; }

    [SgpaColumn(Order = 14, MaxLength = 50)]
    public string? NroCuenta { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
