using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Afiliado_Certificado_Afeccion")]
public partial class _600_Afiliado_Certificado_Afeccion
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2)]
    public short? CodAfeccionTipo { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? DescAfeccionTipo { get; set; }

    [SgpaColumn(Order = 4)]
    public int? Cantidad { get; set; }

    [SgpaColumn(Order = 5)]
    public short? Dias { get; set; }

}
