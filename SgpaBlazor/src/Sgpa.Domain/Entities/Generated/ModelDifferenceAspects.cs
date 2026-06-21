using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("ModelDifferenceAspects")]
public partial class ModelDifferenceAspects
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? Name { get; set; }

    [SgpaColumn(Order = 3)]
    public string? Xml { get; set; }

    [SgpaColumn(Order = 4)]
    public Guid? OwnerID { get; set; }

}
