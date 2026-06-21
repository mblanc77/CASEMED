using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyTypePermissionObject")]
public partial class PermissionPolicyTypePermissionObject
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? TargetTypeFullName { get; set; }

    [SgpaColumn(Order = 3)]
    public Guid? RoleID { get; set; }

    [SgpaColumn(Order = 4)]
    public int? ReadState { get; set; }

    [SgpaColumn(Order = 5)]
    public int? WriteState { get; set; }

    [SgpaColumn(Order = 6)]
    public int? CreateState { get; set; }

    [SgpaColumn(Order = 7)]
    public int? DeleteState { get; set; }

    [SgpaColumn(Order = 8)]
    public int? NavigateState { get; set; }

}
