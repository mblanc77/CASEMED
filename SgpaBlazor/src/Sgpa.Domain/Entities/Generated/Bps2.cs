using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Bps2")]
public partial class Bps2
{
    [SgpaColumn(Order = 1)]
    public int? TipoReg { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? Apellido1 { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? Apellido2 { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? Nombres { get; set; }

    [SgpaColumn(Order = 6)]
    public DateTime? FechaNacimiento { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? Sexo { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? Nacionalidad { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 255)]
    public string? Reservado1 { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 255)]
    public string? Reservado2 { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 255)]
    public string? Reservado3 { get; set; }

    [SgpaColumn(Order = 12, MaxLength = 255)]
    public string? Reservado4 { get; set; }

}
