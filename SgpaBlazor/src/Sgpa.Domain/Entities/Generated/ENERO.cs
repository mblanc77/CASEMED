using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("ENERO")]
public partial class ENERO
{
    [SgpaColumn(Column = "Nro#", Order = 1)]
    public double? Nro_ { get; set; }

    [SgpaColumn(Column = "C#I#", Order = 2)]
    public double? C_I_ { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 5)]
    public double? importe { get; set; }

    [SgpaColumn(Order = 6)]
    public double? F6 { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? F7 { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? F8 { get; set; }

}
