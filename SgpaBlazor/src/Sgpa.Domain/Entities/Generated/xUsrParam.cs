using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("xUsrParam")]
public partial class xUsrParam
{
    [SgpaColumn(Order = 1, MaxLength = 8)]
    public string? login { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 100)]
    public string? clave { get; set; }

    [SgpaColumn(Order = 3)]
    public short? orden { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? value1 { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? value2 { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 255)]
    public string? value3 { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? value4 { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 255)]
    public string? value5 { get; set; }

}
