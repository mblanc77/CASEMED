using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Grupo_Usuario")]
public partial class Grupo_Usuario
{
    [SgpaColumn(Order = 1, MaxLength = 8)]
    public string? Cod_Grupo { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 8)]
    public string? Login { get; set; }

}
