using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Certificador")]
public partial class Certificador
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodCertificador { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 25)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 25)]
    public string? Fax { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 25)]
    public string? Bip { get; set; }

    [SgpaColumn(Order = 7, Required = true)]
    public bool CobraLlamado { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
