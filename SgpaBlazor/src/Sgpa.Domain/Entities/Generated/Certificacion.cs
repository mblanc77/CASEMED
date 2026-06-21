using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Certificacion")]
public partial class Certificacion
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int NroLlamado { get; set; }

    [SgpaColumn(Order = 2)]
    public long? CI { get; set; }

    [SgpaColumn(Order = 3)]
    public int? NroRecibo { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaRecibido { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaCertificacion { get; set; }

    [SgpaColumn(Order = 6)]
    public DateTime? FechaIni { get; set; }

    [SgpaColumn(Order = 7)]
    public DateTime? FechaFin { get; set; }

    [SgpaColumn(Order = 8)]
    public int? CodAfeccionTipo { get; set; }

    [SgpaColumn(Order = 9)]
    public int? CodCertificador { get; set; }

    [SgpaColumn(Order = 10)]
    public int? CodSalidaTipo { get; set; }

    [SgpaColumn(Order = 11, Required = true)]
    public bool Efectiva { get; set; }

    [SgpaColumn(Order = 12)]
    public string? Indicaciones { get; set; }

    [SgpaColumn(Order = 13)]
    public double? ImporteDeducible { get; set; }

    [SgpaColumn(Order = 14, Required = true)]
    public bool Trabaja { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
