using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_Cheque_Tmp")]
public partial class _600_Rpt_Cheque_Tmp
{
    [SgpaColumn(Order = 1)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 3)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 4)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? Letras { get; set; }

}
