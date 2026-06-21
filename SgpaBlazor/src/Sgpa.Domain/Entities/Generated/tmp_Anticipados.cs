using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmp_Anticipados")]
public partial class tmp_Anticipados
{
    [SgpaColumn(Order = 1)]
    public int? IDPrestamo { get; set; }

}
