using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("rptPlanes_Tmp")]
public partial class rptPlanes_Tmp
{
    [SgpaColumn(Order = 1)]
    public short? Cuotas { get; set; }

    [SgpaColumn(Order = 2)]
    public float? ValorCuota { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Monto { get; set; }

}
