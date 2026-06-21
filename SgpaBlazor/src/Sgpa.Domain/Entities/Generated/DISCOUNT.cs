using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("DISCOUNT")]
public partial class DISCOUNT
{
    [SgpaColumn(Order = 1)]
    public double? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public double? FICHA { get; set; }

    [SgpaColumn(Column = "NOMBRE Y APELLIDO", Order = 3, MaxLength = 255)]
    public string? NOMBRE_Y_APELLIDO { get; set; }

    [SgpaColumn(Column = "Nº CUENTA", Order = 4)]
    public double? Nº_CUENTA { get; set; }

}
