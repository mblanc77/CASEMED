using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_SubsidioResumen_Tmp")]
public partial class _600_SubsidioResumen_Tmp
{
    [SgpaColumn(Order = 1)]
    public short? Anio { get; set; }

    [SgpaColumn(Order = 2)]
    public byte? Mes { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? DescAfiliado { get; set; }

    [SgpaColumn(Order = 5)]
    public short? Dias { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 50)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 30)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 30)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 9)]
    public double? ImpNominal { get; set; }

    [SgpaColumn(Order = 10)]
    public double? ImpAguinaldo { get; set; }

    [SgpaColumn(Order = 11)]
    public double? ImpLiquido { get; set; }

    [SgpaColumn(Order = 12)]
    public bool? Liquidar { get; set; }

    [SgpaColumn(Order = 13)]
    public DateTime? FechaNacimiento { get; set; }

    [SgpaColumn(Order = 14, MaxLength = 255)]
    public string? DescFecha { get; set; }

    [SgpaColumn(Order = 15)]
    public int? CIOrig { get; set; }

    [SgpaColumn(Order = 16)]
    public bool? Baja { get; set; }

}
