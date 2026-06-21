using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: CI, Fecha, CodPrestacionTipo, CodRecetaDistancia.
[SgpaTable("Receta")]
public partial class Receta
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

    [SgpaColumn(Order = 4, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodRecetaDistancia { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Esf_I { get; set; }

    [SgpaColumn(Order = 6)]
    public float? Esf_D { get; set; }

    [SgpaColumn(Order = 7)]
    public float? Cil_I { get; set; }

    [SgpaColumn(Order = 8)]
    public float? Cil_D { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
