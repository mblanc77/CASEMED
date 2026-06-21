using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("tmp_FacturasAnticipadas")]
public partial class tmp_FacturasAnticipadas
{
    [SgpaColumn(Order = 1)]
    public int? IdPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? IDFactura { get; set; }

}
