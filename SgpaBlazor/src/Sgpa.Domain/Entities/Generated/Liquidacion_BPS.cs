using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Liquidacion_BPS")]
public partial class Liquidacion_BPS
{
    [SgpaColumn(Order = 1)]
    public int? Id { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    public double CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? NOMBRE { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? APELLIDO { get; set; }

    [SgpaColumn(Order = 5)]
    public double? MONTO_TOTAL { get; set; }

    [SgpaColumn(Order = 6, Required = true)]
    public int MES_DE_CARGO { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? NOM_EMPRESA { get; set; }

    [SgpaColumn(Order = 8, Required = true)]
    public double PCT_POR_EMPRESA { get; set; }

    [SgpaColumn(Order = 9)]
    public DateTime? FECHA_PER_DESDE { get; set; }

    [SgpaColumn(Order = 10)]
    public DateTime? FECHA_PER_HASTA { get; set; }

    [SgpaColumn(Column = "N_ ENTREGA", Order = 11)]
    public int? N__ENTREGA { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FECHA_DE_ENTREGA { get; set; }

    [SgpaColumn(Order = 13)]
    public short? MES { get; set; }

    [SgpaColumn(Order = 14)]
    public short? ANIO { get; set; }

    [SgpaColumn(Order = 15)]
    public double? LIQUIDO { get; set; }

}
