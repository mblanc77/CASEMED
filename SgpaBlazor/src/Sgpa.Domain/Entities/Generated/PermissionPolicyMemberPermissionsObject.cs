using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyMemberPermissionsObject")]
public partial class PermissionPolicyMemberPermissionsObject
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? Members { get; set; }

    [SgpaColumn(Order = 3)]
    public string? Criteria { get; set; }

    [SgpaColumn(Order = 4)]
    public int? ReadState { get; set; }

    [SgpaColumn(Order = 5)]
    public int? WriteState { get; set; }

    [SgpaColumn(Order = 6)]
    public Guid? TypePermissionObjectID { get; set; }

}
