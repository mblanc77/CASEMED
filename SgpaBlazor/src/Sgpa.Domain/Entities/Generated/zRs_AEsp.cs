using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("zRs_AEsp")]
public partial class zRs_AEsp
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? EspNom1 { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? EspNom2 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? EspNom3 { get; set; }

}
