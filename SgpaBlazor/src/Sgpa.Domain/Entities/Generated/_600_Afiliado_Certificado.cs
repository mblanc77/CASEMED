using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Afiliado_Certificado")]
public partial class _600_Afiliado_Certificado
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

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

    [SgpaColumn(Order = 7)]
    public short? CodMutualista { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 50)]
    public string? DescMutualista { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 255)]
    public string? Especialidad { get; set; }

    [SgpaColumn(Order = 10)]
    public float? Promedio { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 255)]
    public string? Empleos { get; set; }

    [SgpaColumn(Order = 12)]
    public int? DiaProrroga { get; set; }

    [SgpaColumn(Order = 13)]
    public int? DiasUltPro { get; set; }

    [SgpaColumn(Order = 14)]
    public DateTime? F_Ult_Prorroga { get; set; }

    [SgpaColumn(Order = 15)]
    public DateTime? F_Ult_Certificacion { get; set; }

}
