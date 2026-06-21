using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: IdSubsidio, FechaIni.
[SgpaTable("SubsidioEnfermedad")]
public partial class SubsidioEnfermedad
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSubsidio { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public DateTime FechaIni { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? FechaFin { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaIniSubsidio { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaFinSubsidio { get; set; }

    [SgpaColumn(Order = 6)]
    public int? NroLlamado { get; set; }

    [SgpaColumn(Order = 7)]
    public byte? Dias { get; set; }

    [SgpaColumn(Order = 8)]
    public double? Importe { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
