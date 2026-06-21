using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("Afiliado")]
public partial class Afiliado
{
    [SgpaColumn(Order = 1)]
    [SgpaKey]
    public long CI { get; set; }

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

    [SgpaColumn(Order = 7, MaxLength = 100)]
    public string? Direccion { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 25)]
    public string? Telefono { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 100)]
    public string? EMail { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 50)]
    public string? Movil { get; set; }

    [SgpaColumn(Order = 11)]
    public int? CodMutualista { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FechaIngMutualista { get; set; }

    [SgpaColumn(Order = 13)]
    public DateTime? FechaBajaMutualista { get; set; }

    [SgpaColumn(Order = 14, MaxLength = 12)]
    public string? NroSocioMutualista { get; set; }

    [SgpaColumn(Order = 15)]
    public byte? CodRegimenJubilatorio { get; set; }

    [SgpaColumn(Order = 16, MaxLength = 3)]
    public string? CodDepartamento { get; set; }

    [SgpaColumn(Order = 17, Required = true)]
    public bool PagaMutualista { get; set; }

    [SgpaColumn(Order = 18, MaxLength = 2)]
    public string? CodSituacionMutual { get; set; }

    [SgpaColumn(Order = 19)]
    public int? CodBanco { get; set; }

    [SgpaColumn(Order = 20, MaxLength = 50)]
    public string? NroCuenta { get; set; }

    [SgpaColumn(Order = 21, MaxLength = 50)]
    public string? NroFunCuenta { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
