using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SP_CuadroAmortizacion_Tmp")]
public partial class SP_CuadroAmortizacion_Tmp
{
    [SgpaColumn(Order = 1)]
    public short? NroCuota { get; set; }

    [SgpaColumn(Order = 2)]
    public float? Monto { get; set; }

    [SgpaColumn(Order = 3)]
    public float? ImporteCuota { get; set; }

    [SgpaColumn(Order = 4)]
    public float? Interes { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Amortizacion { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Saldo { get; set; }

}
