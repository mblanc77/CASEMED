using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IDPrestamo, Nro.
[SgpaTable("SP_Cuota")]
public partial class SP_Cuota
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IDPrestamo { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int Nro { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? FechaVencimiento { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaPago { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 3)]
    public string? CodItemPago { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 3)]
    public string? CodMoneda { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 3)]
    public string? CodCuotaEstado { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
