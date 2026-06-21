using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmp_Cantidad_Por_Puesto")]
public partial class tmp_Cantidad_Por_Puesto
{
    [SgpaColumn(Order = 1)]
    public short? CodEmpresa { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Cantidad { get; set; }

    [SgpaColumn(Order = 4)]
    public int? CantidadNo0 { get; set; }

}
