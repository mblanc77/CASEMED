using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("BROU")]
public partial class BROU
{
    [SgpaColumn(Order = 1)]
    public double? CI { get; set; }

    [SgpaColumn(Column = "NOMBRE Y APELLIDO", Order = 2, MaxLength = 255)]
    public string? NOMBRE_Y_APELLIDO { get; set; }

    [SgpaColumn(Column = "Nº CUENTA", Order = 3)]
    public double? Nº_CUENTA { get; set; }

}
