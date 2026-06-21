using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyRoleBase")]
public partial class PermissionPolicyRoleBase
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? Name { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    public bool IsAdministrative { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public bool CanEditModel { get; set; }

    [SgpaColumn(Order = 5, Required = true)]
    public int PermissionPolicy { get; set; }

    [SgpaColumn(Order = 6, Required = true)]
    public bool IsAllowPermissionPriority { get; set; }

    [SgpaColumn(Order = 7, Required = true, MaxLength = 34)]
    public string? Discriminator { get; set; }

}
