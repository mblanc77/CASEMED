using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("acc_sp_1030_FacturaFlujo_q")]
public partial class acc_sp_1030_FacturaFlujo_q
{
    [SgpaColumn(Order = 1)]
    public int? IdPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Mes { get; set; }

    [SgpaColumn(Order = 3)]
    public double? Importe { get; set; }

}
