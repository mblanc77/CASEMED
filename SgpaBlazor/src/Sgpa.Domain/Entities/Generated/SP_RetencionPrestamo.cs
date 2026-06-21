using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_RetencionPrestamo")]
public partial class SP_RetencionPrestamo
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
    public float? Saldo { get; set; }

    [SgpaColumn(Order = 8)]
    public float? ImpPago { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
