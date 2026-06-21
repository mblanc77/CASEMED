using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("rptVale_Tmp")]
public partial class rptVale_Tmp
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 6)]
    public float? ImporteTotal { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? LetraImporte { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 50)]
    public string? DescMoneda { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 50)]
    public string? DescMonedaLargo { get; set; }

    [SgpaColumn(Order = 10)]
    public short? Cuotas { get; set; }

    [SgpaColumn(Order = 11)]
    public float? ImporteCuota { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FechaVencimiento { get; set; }

    [SgpaColumn(Order = 13)]
    public float? Tasa { get; set; }

}
