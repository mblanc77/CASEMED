using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("BpsFormat")]
public partial class BpsFormat
{
    [SgpaColumn(Order = 1, MaxLength = 50)]
    public string? Cedula { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Mutualista { get; set; }

}
