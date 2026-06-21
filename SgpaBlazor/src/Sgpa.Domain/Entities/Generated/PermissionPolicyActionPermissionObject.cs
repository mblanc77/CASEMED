using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyActionPermissionObject")]
public partial class PermissionPolicyActionPermissionObject
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public Guid? RoleID { get; set; }

    [SgpaColumn(Order = 3)]
    public string? ActionId { get; set; }

}
