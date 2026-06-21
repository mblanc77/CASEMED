using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Seleccion")]
public partial class Seleccion
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdSeleccion { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Form { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 4)]
    public string? Txt { get; set; }

    [SgpaColumn(Order = 5, Required = true)]
    public bool System { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
