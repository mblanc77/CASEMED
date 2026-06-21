using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("qCorregirFinSubsidio")]
public partial class qCorregirFinSubsidio
{
    [SgpaColumn(Order = 1)]
    public int? Id { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CI { get; set; }

    [SgpaColumn(Column = "Nombre y apellido", Order = 3, MaxLength = 255)]
    public string? Nombre_y_apellido { get; set; }

    [SgpaColumn(Column = "Fecha Inicio", Order = 4)]
    public DateTime? Fecha_Inicio { get; set; }

    [SgpaColumn(Column = "Fecha Fin", Order = 5)]
    public DateTime? Fecha_Fin { get; set; }

}
