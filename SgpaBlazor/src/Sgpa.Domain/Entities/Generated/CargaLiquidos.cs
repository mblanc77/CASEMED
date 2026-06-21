using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).
[SgpaTable("CargaLiquidos")]
public partial class CargaLiquidos
{
    [SgpaColumn(Order = 1)]
    public double? vlbidn { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 255)]
    public string? apellido { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 255)]
    public string? nombre { get; set; }

    [SgpaColumn(Order = 4)]
    public double? cedula { get; set; }

    [SgpaColumn(Order = 5)]
    public double? chkdig { get; set; }

    [SgpaColumn(Order = 6)]
    public double? dd_egre { get; set; }

    [SgpaColumn(Order = 7)]
    public double? mm_egre { get; set; }

    [SgpaColumn(Order = 8)]
    public double? aa_egre { get; set; }

    [SgpaColumn(Order = 9)]
    public double? imphaberes { get; set; }

    [SgpaColumn(Order = 10)]
    public double? impdescuen { get; set; }

    [SgpaColumn(Order = 11)]
    public double? liquido { get; set; }

    [SgpaColumn(Order = 12)]
    public double? cargo { get; set; }

    [SgpaColumn(Order = 13)]
    public double? cantmov { get; set; }

}
