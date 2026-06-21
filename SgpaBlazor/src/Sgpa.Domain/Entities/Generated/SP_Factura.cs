using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_Factura")]
public partial class SP_Factura
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDFactura { get; set; }

    [SgpaColumn(Order = 2)]
    public int? NroFactura { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 2)]
    public string? NroEmpresa { get; set; }

    [SgpaColumn(Order = 4)]
    public int? IdPrestamo { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaEmitida { get; set; }

    [SgpaColumn(Order = 6)]
    public DateTime? FechaVencimiento { get; set; }

    [SgpaColumn(Order = 7)]
    public DateTime? FechaPago { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 3)]
    public string? CodMoneda { get; set; }

    [SgpaColumn(Order = 9)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 3)]
    public string? CodFacturaEstado { get; set; }

    [SgpaColumn(Order = 11)]
    public float? TasaCambio { get; set; }

    [SgpaColumn(Order = 12, MaxLength = 50)]
    public string? CodigoBarra { get; set; }

    [SgpaColumn(Order = 13)]
    public int? Impresiones { get; set; }

    [SgpaColumn(Order = 14)]
    public float? ImpAmortizable { get; set; }

    [SgpaColumn(Order = 15)]
    public float? ImpInteres { get; set; }

    [SgpaColumn(Order = 16, MaxLength = 50)]
    public string? CodFacturaTipo { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
