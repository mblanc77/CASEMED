using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
// Clave compuesta: RolesID, UsersID.
[SgpaTable("PermissionPolicyRolePermissionPolicyUser")]
public partial class PermissionPolicyRolePermissionPolicyUser
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid RolesID { get; set; }

    [SgpaColumn(Order = 2, Required = true)]
    [SgpaKey]
    public Guid UsersID { get; set; }

}
