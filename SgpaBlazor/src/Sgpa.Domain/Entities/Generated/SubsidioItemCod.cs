using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("SubsidioItemCod")]
public partial class SubsidioItemCod
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public int CodSubsidioItemCod { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 50)]
    public string? Descrip { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 1)]
    public string? Tipo { get; set; }

    [SgpaColumn(Order = 4, MaxLength = 50)]
    public string? ValorTipo { get; set; }

    [SgpaColumn(Order = 5)]
    public bool? Comparar { get; set; }

    [SgpaColumn(Order = 6)]
    public byte? CompararContra { get; set; }

    [SgpaColumn(Order = 7)]
    public double? Valor { get; set; }

    [SgpaColumn(Order = 8, MaxLength = 1)]
    public string? TipoComp { get; set; }

    [SgpaColumn(Order = 9)]
    public int? Orden { get; set; }

    [SgpaColumn(Order = 10)]
    public int? Signo { get; set; }

    [SgpaColumn(Order = 11, MaxLength = 5)]
    public string? Operador { get; set; }

    [SgpaColumn(Order = 12)]
    public double? ValorMin { get; set; }

    [SgpaColumn(Order = 13)]
    public double? ValorMax { get; set; }

    [SgpaColumn(Order = 14, Required = true)]
    public bool Procesar { get; set; }

    [SgpaColumn(Order = 15)]
    public DateTime? FechaVigencia { get; set; }

    [SgpaColumn(Order = 16)]
    public DateTime? FechaBaja { get; set; }

    [SgpaColumn(Order = 17, Required = true)]
    public bool AperturaXEmpresa { get; set; }

    [SgpaColumn(Order = 18, Required = true)]
    public bool ModificaNominal { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }

}
