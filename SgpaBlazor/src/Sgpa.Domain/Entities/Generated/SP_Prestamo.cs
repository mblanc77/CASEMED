using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_Prestamo")]
public partial class SP_Prestamo
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 4)]
    public int? CodEmpresa { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 3)]
    public string? CodMoneda { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 7)]
    public int? Cuotas { get; set; }

    [SgpaColumn(Order = 8)]
    public float? ImporteCuota { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 3)]
    public string? CodPrestamoEstado { get; set; }

    [SgpaColumn(Order = 10)]
    public string? Observaciones { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 3)]
    public string? CodPrestamoTipo { get; set; }

    [SgpaColumn(Order = 12)]
    public float? Tasa { get; set; }

    [SgpaColumn(Order = 13)]
    public float? Saldo { get; set; }

    [SgpaColumn(Order = 14)]
    public int? CuotasPagas { get; set; }

    [SgpaColumn(Order = 15)]
    public DateTime? FechaCobro { get; set; }

    [SgpaColumn(Order = 16)]
    public float? TasaCambio { get; set; }

    [SgpaColumn(Order = 17)]
    public float? Promedio { get; set; }

    [SgpaColumn(Order = 18, MaxLength = 50)]
    public string? Banco { get; set; }

    [SgpaColumn(Order = 19, MaxLength = 50)]
    public string? Sucursal { get; set; }

    [SgpaColumn(Order = 20, MaxLength = 50)]
    public string? NroCta { get; set; }

    [SgpaColumn(Order = 21)]
    public int? NroSerieCheque { get; set; }

    [SgpaColumn(Order = 22)]
    public int? NroCheque { get; set; }

    [SgpaColumn(Order = 23)]
    public int? IDPrestamoRef { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
