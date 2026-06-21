using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_SubsidioFecha_Tmp")]
public partial class _600_SubsidioFecha_Tmp
{
    [SgpaColumn(Order = 1)]
    public int? IDSubsidio { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? DescFecha { get; set; }

}
