using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmp_ReporteBPS_Full")]
public partial class tmp_ReporteBPS_Full
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public short? Dias { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 30)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 30)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 6)]
    public DateTime? FechaNacimiento { get; set; }

    [SgpaColumn(Order = 7)]
    public int? IdSubsidio { get; set; }

    [SgpaColumn(Order = 8)]
    public int? NroRecibo { get; set; }

    [SgpaColumn(Order = 9)]
    public DateTime? FechaIni { get; set; }

    [SgpaColumn(Order = 10)]
    public DateTime? FechaFin { get; set; }

    [SgpaColumn(Order = 11)]
    public DateTime? FechaIniSubsidio { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FechaFinSubsidio { get; set; }

    [SgpaColumn(Order = 13)]
    public double? ImpNominal { get; set; }

    [SgpaColumn(Order = 14)]
    public double? ImpAguinaldo { get; set; }

    [SgpaColumn(Order = 15)]
    public double? ImpLiquido { get; set; }

    [SgpaColumn(Order = 16)]
    public double? Jornal70 { get; set; }

    [SgpaColumn(Order = 17)]
    public double? Aguinaldo70 { get; set; }

    [SgpaColumn(Order = 18)]
    public int? DiasBPS { get; set; }

    [SgpaColumn(Order = 19)]
    public double? LiquidoBPS { get; set; }

    [SgpaColumn(Order = 20)]
    public double? LiquidoPagar { get; set; }

    [SgpaColumn(Order = 21, MaxLength = 50)]
    public string? Banco { get; set; }

    [SgpaColumn(Order = 22, MaxLength = 50)]
    public string? NroCuenta { get; set; }

    [SgpaColumn(Order = 23)]
    public double? MONTO_TOTAL { get; set; }

    [SgpaColumn(Order = 24)]
    public int? MES_DE_CARGO { get; set; }

    [SgpaColumn(Order = 25, MaxLength = 255)]
    public string? NOM_EMPRESA { get; set; }

    [SgpaColumn(Order = 26)]
    public double? PCT_POR_EMPRESA { get; set; }

    [SgpaColumn(Order = 27)]
    public DateTime? FECHA_PER_DESDE { get; set; }

    [SgpaColumn(Order = 28)]
    public DateTime? FECHA_PER_HASTA { get; set; }

    [SgpaColumn(Column = "N_ ENTREGA", Order = 29)]
    public int? N__ENTREGA { get; set; }

    [SgpaColumn(Order = 30)]
    public DateTime? FECHA_DE_ENTREGA { get; set; }

}
