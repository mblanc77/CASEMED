using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, Fecha, CodPrestacionTipo.
[SgpaTable("Prestacion")]
public partial class Prestacion
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public long CI { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public DateTime Fecha { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    [SgpaKey]
    public int CodPrestacionTipo { get; set; }

    [SgpaColumn(Order = 4)]
    public int? CodFormaPago { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 3)]
    public string? Moneda { get; set; }

    [SgpaColumn(Order = 6)]
    public double? Importe { get; set; }

    [SgpaColumn(Order = 7, Required = true)]
    public bool Boleta { get; set; }

    [SgpaColumn(Order = 8)]
    public int? NroRecibo { get; set; }

    [SgpaColumn(Order = 9)]
    public string? Observaciones { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
