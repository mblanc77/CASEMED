using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SP_PrestamoTipo")]
public partial class SP_PrestamoTipo
{
    [SgpaColumn(Order = 1, Required = true, MaxLength = 3)]
    [SgpaKey]
    public string? CodPrestamoTipo { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    public bool TopeaImporte { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
