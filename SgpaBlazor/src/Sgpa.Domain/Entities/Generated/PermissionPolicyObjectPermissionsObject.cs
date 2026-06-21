using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyObjectPermissionsObject")]
public partial class PermissionPolicyObjectPermissionsObject
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? Criteria { get; set; }

    [SgpaColumn(Order = 3)]
    public int? ReadState { get; set; }

    [SgpaColumn(Order = 4)]
    public int? WriteState { get; set; }

    [SgpaColumn(Order = 5)]
    public int? DeleteState { get; set; }

    [SgpaColumn(Order = 6)]
    public int? NavigateState { get; set; }

    [SgpaColumn(Order = 7)]
    public Guid? TypePermissionObjectID { get; set; }

}
