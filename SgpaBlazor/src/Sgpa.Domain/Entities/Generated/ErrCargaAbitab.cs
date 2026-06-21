using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: Fecha, NroReng.
[SgpaTable("ErrCargaAbitab")]
public partial class ErrCargaAbitab
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public DateTime Fecha { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public int NroReng { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 100)]
    public string? Descrip { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
