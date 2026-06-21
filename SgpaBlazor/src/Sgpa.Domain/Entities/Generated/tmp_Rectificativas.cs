using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmp_Rectificativas")]
public partial class tmp_Rectificativas
{
    [SgpaColumn(Order = 1)]
    public int? Id { get; set; }

    [SgpaColumn(Order = 2)]
    public double? EMPRESA { get; set; }

    [SgpaColumn(Order = 3)]
    public double? CI { get; set; }

    [SgpaColumn(Order = 4)]
    public double? Concepto { get; set; }

    [SgpaColumn(Order = 5)]
    public double? Importe { get; set; }

}
