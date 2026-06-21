using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Empresa")]
public partial class Empresa
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodEmpresa { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 25)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 25)]
    public string? Fax { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 25)]
    public string? EMail { get; set; }

    [SgpaColumn(Order = 7)]
    public float? AporteCasemed { get; set; }

    [SgpaColumn(Order = 8)]
    public int? AporteAguinaldo { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 50)]
    public string? PersonaContacto { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 255)]
    public string? Autoridades { get; set; }

    [SgpaColumn(Order = 11)]
    public int? CodRegimenAporte { get; set; }

    [SgpaColumn(Order = 12)]
    public int? CodSituacionPago { get; set; }

    [SgpaColumn(Order = 13, Required = true)]
    public bool Liquidar { get; set; }

    [SgpaColumn(Order = 14, Required = true)]
    public bool Ficticia { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
