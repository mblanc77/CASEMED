using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_CantidadDescrip")]
public partial class _600_Rpt_CantidadDescrip
{
    [SgpaColumn(Order = 1)]
    public int? Codigo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Cantidad { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Descrip { get; set; }

}
