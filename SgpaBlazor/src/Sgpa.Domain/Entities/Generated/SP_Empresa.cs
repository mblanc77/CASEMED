using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("SP_Empresa")]
public partial class SP_Empresa
{
    [SgpaColumn(Order = 1)]
    public short? CodEmpresa { get; set; }

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
    public short? CodRegimenAporte { get; set; }

    [SgpaColumn(Order = 12)]
    public short? CodSituacionPago { get; set; }

    [SgpaColumn(Order = 13)]
    public bool? Liquidar { get; set; }

    [SgpaColumn(Order = 14)]
    public bool? Ficticia { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
