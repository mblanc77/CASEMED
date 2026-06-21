using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("ExportedQueries")]
public partial class ExportedQueries
{
    [SgpaColumn(Order = 1, MaxLength = 255)]
    public string? QueryName { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? QueryType { get; set; }

    [SgpaColumn(Order = 3)]
    public string? Parameters { get; set; }

    [SgpaColumn(Order = 4)]
    public string? SqlText { get; set; }

}
