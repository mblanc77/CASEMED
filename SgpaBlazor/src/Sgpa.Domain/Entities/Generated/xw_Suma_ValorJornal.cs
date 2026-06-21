using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("xw_Suma_ValorJornal")]
public partial class xw_Suma_ValorJornal
{
    [SgpaColumn(Order = 1)]
    public int? IdSubsidio { get; set; }

    [SgpaColumn(Order = 2)]
    public double? SumaDeValorJornal { get; set; }

}
