using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("AuditData")]
public partial class AuditData
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    public DateTime ModifiedOn { get; set; }

    [SgpaColumn(Order = 3)]
    public string? OperationType { get; set; }

    [SgpaColumn(Order = 4)]
    public string? PropertyName { get; set; }

    [SgpaColumn(Order = 5)]
    public string? OldValue { get; set; }

    [SgpaColumn(Order = 6)]
    public string? NewValue { get; set; }

    [SgpaColumn(Order = 7)]
    public string? Description { get; set; }

    [SgpaColumn(Order = 8)]
    public Guid? AuditedObjectID { get; set; }

    [SgpaColumn(Order = 9)]
    public Guid? OldObjectID { get; set; }

    [SgpaColumn(Order = 10)]
    public Guid? NewObjectID { get; set; }

    [SgpaColumn(Order = 11)]
    public Guid? UserObjectID { get; set; }

}
