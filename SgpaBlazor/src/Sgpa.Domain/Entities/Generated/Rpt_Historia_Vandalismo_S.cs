using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Rpt_Historia_Vandalismo_S")]
public partial class Rpt_Historia_Vandalismo_S
{
    [SgpaColumn(Order = 1)]
    public int Id { get; set; }

}
