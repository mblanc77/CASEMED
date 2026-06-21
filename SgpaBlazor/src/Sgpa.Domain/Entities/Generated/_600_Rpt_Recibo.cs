using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_Recibo")]
public partial class _600_Rpt_Recibo
{
    [SgpaColumn(Order = 1)]
    public int? IdSubsidio { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? NroRecibo { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 50)]
    public string? Mes { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 50)]
    public string? CI { get; set; }

    [SgpaColumn(Order = 6, MaxLength = 255)]
    public string? Periodo { get; set; }

    [SgpaColumn(Order = 7, MaxLength = 255)]
    public string? Item { get; set; }

    [SgpaColumn(Order = 8)]
    public float? Importe { get; set; }

    [SgpaColumn(Order = 9)]
    public short? Signo { get; set; }

    [SgpaColumn(Order = 10, MaxLength = 255)]
    public string? bANCO { get; set; }

}
