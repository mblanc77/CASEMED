using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("ReportDataV2")]
public partial class ReportDataV2
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? DataTypeName { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    public bool IsInplaceReport { get; set; }

    [SgpaColumn(Order = 4)]
    public string? PredefinedReportTypeName { get; set; }

    [SgpaColumn(Order = 5)]
    public byte[]? Content { get; set; }

    [SgpaColumn(Order = 6)]
    public string? DisplayName { get; set; }

    [SgpaColumn(Order = 7)]
    public string? ParametersObjectTypeName { get; set; }

}
