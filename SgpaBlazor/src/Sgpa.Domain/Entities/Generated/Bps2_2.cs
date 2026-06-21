using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Bps2_2")]
public partial class Bps2_2
{
    [SgpaColumn(Order = 1)]
    public int? TipoReg { get; set; }

    [SgpaColumn(Order = 2)]
    public int? Pais { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? TipoDocumento { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? PrimerApellido { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 255)]
    public string? SegundoApellido { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? PrimerNombre { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? SegundoNombre { get; set; }

    [SgpaColumn(Order = 9)]
    public DateTime? FechaNacimiento { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 255)]
    public string? Sexo { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 255)]
    public string? Nacionalidad { get; set; }

}
