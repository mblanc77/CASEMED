using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Mutualista")]
public partial class Mutualista
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodMutualista { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 25)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 25)]
    public string? Fax { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 25)]
    public string? EMail { get; set; }

    [SgpaColumn(Order = 6)]
    public double? Cuota { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 50)]
    public string? PersonaContacto { get; set; }

    [SgpaColumn(Order = 8)]
    public short? DiaPago { get; set; }

    [SgpaColumn(Order = 9)]
    public int? CodFormaPago { get; set; }

    [SgpaColumn(Order = 10, Required = true)]
    public bool Ficticia { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

    [SgpaColumn(Order = 13, MaxLength = 50)]
    public string? Descrip { get; set; }

}
