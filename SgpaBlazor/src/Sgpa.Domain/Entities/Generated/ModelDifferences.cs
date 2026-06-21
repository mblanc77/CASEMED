using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("ModelDifferences")]
public partial class ModelDifferences
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? UserId { get; set; }

    [SgpaColumn(Order = 3)]
    public string? ContextId { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public int Version { get; set; }

}
