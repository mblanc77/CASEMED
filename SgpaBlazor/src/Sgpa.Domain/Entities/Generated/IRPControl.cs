using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("IRPControl")]
public partial class IRPControl
{
    [SgpaColumn(Order = 1)]
    public int? CodIRP { get; set; }

    [SgpaColumn(Order = 2)]
    public float? ImpFrjAnt { get; set; }

    [SgpaColumn(Order = 3)]
    public float? FranjaAnt { get; set; }

    [SgpaColumn(Order = 4)]
    public float? SMNAnt { get; set; }

}
