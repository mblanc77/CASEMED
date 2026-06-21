using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IDPrestamo, NroCuota.
[SgpaTable("SP_CuadroAmortizacion")]
public partial class SP_CuadroAmortizacion
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDPrestamo { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int NroCuota { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Monto { get; set; }

    [SgpaColumn(Order = 4)]
    public float? ImporteCuota { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Interes { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Amortizacion { get; set; }

    [SgpaColumn(Order = 7)]
    public float? Saldo { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
