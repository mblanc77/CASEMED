using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Bps4")]
public partial class Bps4
{
    [SgpaColumn(Order = 1)]
    public int? TipoReg { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? AcumulacionLaboral { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? Concepto { get; set; }

    [SgpaColumn(Order = 5)]
    public double? Imponible { get; set; }

}
