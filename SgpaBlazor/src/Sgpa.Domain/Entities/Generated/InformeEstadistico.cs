using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("InformeEstadistico")]
public partial class InformeEstadistico
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int IdRpt { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Grupo { get; set; }

    [SgpaColumn(Order = 3)]
    public int? Orden { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 255)]
    public string? TituloPantalla { get; set; }

    [SgpaColumn(Order = 5, MaxLength = 255)]
    public string? TituloRpt { get; set; }

    [SgpaColumn(Order = 6, Required = true)]
    public bool MesAnio { get; set; }

    [SgpaColumn(Order = 7, Required = true)]
    public bool Periodo { get; set; }

    [SgpaColumn(Order = 8, Required = true)]
    public bool Empresa { get; set; }

    [SgpaColumn(Order = 9, Required = true)]
    public bool Fecha { get; set; }

    [SgpaColumn(Order = 10, Required = true)]
    public bool GrupoEtario { get; set; }

    [SgpaColumn(Order = 11, Required = true)]
    public bool Patologia { get; set; }

    [SgpaColumn(Order = 12)]
    public string? Comentario { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
