using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("DashboardData")]
public partial class DashboardData
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? Content { get; set; }

    [SgpaColumn(Order = 3)]
    public string? Title { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public bool SynchronizeTitle { get; set; }

}
