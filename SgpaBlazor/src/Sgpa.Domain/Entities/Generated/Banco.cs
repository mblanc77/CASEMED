using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Banco")]
public partial class Banco
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodBanco { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descripcion { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
