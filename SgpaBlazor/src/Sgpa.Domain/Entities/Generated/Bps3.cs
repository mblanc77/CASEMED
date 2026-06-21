using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("Bps3")]
public partial class Bps3
{
    [SgpaColumn(Order = 1)]
    public int? TipoReg { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? AcumulacionLaboral { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? FechaIngreso { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? SeguroSalud { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 255)]
    public string? RemuneracionTipo { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? HorasSemanales { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? VinculoFuncional { get; set; }

    [SgpaColumn(Order = 9, MaxLength = 255)]
    public string? CodigoExoneracion { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 255)]
    public string? ComputosEspeciales { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 255)]
    public string? CausalBaja { get; set; }

    [SgpaColumn(Order = 12)]
    public DateTime? FechaBaja { get; set; }

    [SgpaColumn(Order = 13, MaxLength = 255)]
    public string? LocalEmpresa { get; set; }

    [SgpaColumn(Order = 14)]
    public int? DiasTrabajados { get; set; }

    [SgpaColumn(Order = 15, MaxLength = 255)]
    public string? HorasTrabajadas { get; set; }

    [SgpaColumn(Order = 16, MaxLength = 255)]
    public string? Reservado1 { get; set; }

    [SgpaColumn(Order = 17, MaxLength = 255)]
    public string? Reservado2 { get; set; }

    [SgpaColumn(Order = 18, MaxLength = 255)]
    public string? Reservado3 { get; set; }

    [SgpaColumn(Order = 19, MaxLength = 255)]
    public string? Reservado4 { get; set; }

    [SgpaColumn(Order = 20, MaxLength = 255)]
    public string? Reservado5 { get; set; }

}
