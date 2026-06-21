using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("600_Rpt_<125_Pct")]
public partial class _600_Rpt__125_Pct
{
    [SgpaColumn(Order = 1, MaxLength = 50)]
    public string? Grupo { get; set; }

    [SgpaColumn(Order = 2)]
    public int? CI { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 100)]
    public string? Nombre { get; set; }

    [SgpaColumn(Order = 4)]
    public int? Importe { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 50)]
    public string? Especialidad { get; set; }

}
