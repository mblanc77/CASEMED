using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyNavigationPermissionObject")]
public partial class PermissionPolicyNavigationPermissionObject
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public Guid? RoleID { get; set; }

    [SgpaColumn(Order = 3)]
    public string? ItemPath { get; set; }

    [SgpaColumn(Order = 4)]
    public string? TargetTypeFullName { get; set; }

    [SgpaColumn(Order = 5)]
    public int? NavigateState { get; set; }

}
