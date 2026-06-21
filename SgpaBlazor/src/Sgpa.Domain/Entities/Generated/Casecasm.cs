using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Casecasm")]
public partial class Casecasm
{
    [SgpaColumn(Order = 1, MaxLength = 255)]
    public string? Campo1 { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Campo2 { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? Campo3 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? Campo4 { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? Campo5 { get; set; }

}
