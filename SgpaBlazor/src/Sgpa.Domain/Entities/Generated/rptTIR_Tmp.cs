using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("rptTIR_Tmp")]
public partial class rptTIR_Tmp
{
    [SgpaColumn(Order = 1)]
    public int? IDPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Mes { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Importe { get; set; }

}
