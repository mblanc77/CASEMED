using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PrestacionTipo")]
public partial class PrestacionTipo
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodPrestacionTipo { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 3)]
    public string? CodMoneda { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaVigencia { get; set; }

    [SgpaColumn(Order = 6)]
    public double? ImporteTopeDISSE { get; set; }

    [SgpaColumn(Order = 7)]
    public double? ImporteTopeCASEMED { get; set; }

    [SgpaColumn(Order = 8)]
    public int? PeriodoRenovacion { get; set; }

    [SgpaColumn(Order = 9)]
    public bool? Receta { get; set; }

    [SgpaColumn(Order = 10)]
    public string? Obs { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
