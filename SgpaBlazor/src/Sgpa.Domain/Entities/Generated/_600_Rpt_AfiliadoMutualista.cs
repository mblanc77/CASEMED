using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_AfiliadoMutualista")]
public partial class _600_Rpt_AfiliadoMutualista
{
    [SgpaColumn(Order = 1, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 30)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 30)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 5)]
    public DateTime? FechaNacimiento { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 1)]
    public string? Sexo { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 25)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 25)]
    public string? EMail { get; set; }

    [SgpaColumn(Order = 9)]
    public short? CodMutualista { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 50)]
    public string? DescMutualista { get; set; }

    [SgpaColumn(Order = 11)]
    public DateTime? FechaIngMutualista { get; set; }

    [SgpaColumn(Order = 12, MaxLength = 12)]
    public string? NroSocioMutualista { get; set; }

    [SgpaColumn(Order = 13)]
    public byte? CodRegimenJubilatorio { get; set; }

    [SgpaColumn(Order = 14, MaxLength = 50)]
    public string? DescRegimenJubilatorio { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

    [SgpaColumn(Order = 17, MaxLength = 255)]
    public string? DescAfiliado { get; set; }

    [SgpaColumn(Order = 18)]
    public double? Cuota { get; set; }

    [SgpaColumn(Order = 19, MaxLength = 100)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 20)]
    public bool? PagaMutualista { get; set; }

    [SgpaColumn(Order = 21, MaxLength = 3)]
    public string? CodDepartamento { get; set; }

    [SgpaColumn(Order = 22)]
    public short? CodEmpresa { get; set; }

    [SgpaColumn(Order = 23, MaxLength = 50)]
    public string? DescEmpresa { get; set; }

    [SgpaColumn(Order = 24, MaxLength = 50)]
    public string? DescSituacionMutual { get; set; }

}
