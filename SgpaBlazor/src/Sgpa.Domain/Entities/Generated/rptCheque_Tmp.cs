using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("rptCheque_Tmp")]
public partial class rptCheque_Tmp
{
    [SgpaColumn(Order = 1)]
    public int? IDPrestamo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 4)]
    public DateTime? Fecha { get; set; }

    [SgpaColumn(Order = 5)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 255)]
    public string? Letras { get; set; }

}
