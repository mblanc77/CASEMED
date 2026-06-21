using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Cristalin")]
public partial class Cristalin
{
    [SgpaColumn(Order = 1, MaxLength = 50)]
    public string? DOCUMENTO { get; set; }

    [SgpaColumn(Column = "1ER APELLIDO", Order = 2, MaxLength = 255)]
    public string? _1ER_APELLIDO { get; set; }

    [SgpaColumn(Column = "2DO APELLIDO", Order = 3, MaxLength = 255)]
    public string? _2DO_APELLIDO { get; set; }

    [SgpaColumn(Column = "1ER NOMBRE", Order = 4, MaxLength = 255)]
    public string? _1ER_NOMBRE { get; set; }

    [SgpaColumn(Column = "2DO NOMBRE", Order = 5, MaxLength = 255)]
    public string? _2DO_NOMBRE { get; set; }

}
