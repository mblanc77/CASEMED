using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("AuditEFCoreWeakReferences")]
public partial class AuditEFCoreWeakReferences
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 450)]
    public string? TypeName { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 450)]
    public string? Key { get; set; }

    [SgpaColumn(Order = 4)]
    public string? DefaultString { get; set; }

    [SgpaColumn(Order = 5, Required = true)]
    public DateTime LastModifiedDate { get; set; }

}
