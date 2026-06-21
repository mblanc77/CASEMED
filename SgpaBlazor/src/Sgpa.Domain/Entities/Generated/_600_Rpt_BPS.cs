using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_BPS")]
public partial class _600_Rpt_BPS
{
    [SgpaColumn(Order = 1)]
    public short? Mes { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Anio { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Cantidad { get; set; }

    [SgpaColumn(Order = 4)]
    public int? Monto { get; set; }

    [SgpaColumn(Order = 5)]
    public int? TributoMonto { get; set; }

    [SgpaColumn(Order = 6)]
    public int? ImpRetPatronal { get; set; }

    [SgpaColumn(Order = 7)]
    public int? ImpRetObrero { get; set; }

    [SgpaColumn(Order = 8)]
    public int? TotImpRet { get; set; }

    [SgpaColumn(Order = 9)]
    public int? TotImpMut { get; set; }

    [SgpaColumn(Order = 10)]
    public int? TributoTotImpMut { get; set; }

}
