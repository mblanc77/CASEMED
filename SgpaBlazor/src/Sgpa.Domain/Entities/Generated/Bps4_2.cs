using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Bps4_2")]
public partial class Bps4_2
{
    [SgpaColumn(Order = 1)]
    public int? TipoReg { get; set; }

    [SgpaColumn(Order = 2)]
    public int? MesAño { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Pais { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? TipoDocumento { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 6)]
    public int? AcumulacionLaboral { get; set; }

    [SgpaColumn(Order = 7)]
    public int? Concepto { get; set; }

    [SgpaColumn(Order = 8)]
    public double? Imponible { get; set; }

    [SgpaColumn(Order = 9)]
    public double? Jornal { get; set; }

    [SgpaColumn(Order = 10)]
    public double? OtrosHaberes { get; set; }

}
