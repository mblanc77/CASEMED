using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmpCorrecciones")]
public partial class tmpCorrecciones
{
    [SgpaColumn(Order = 1)]
    public double? CI { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Nombre { get; set; }

    [SgpaColumn(Column = "Fecha Inicio", Order = 3)]
    public DateTime? Fecha_Inicio { get; set; }

    [SgpaColumn(Column = "Fecha Fin", Order = 4)]
    public DateTime? Fecha_Fin { get; set; }

}
