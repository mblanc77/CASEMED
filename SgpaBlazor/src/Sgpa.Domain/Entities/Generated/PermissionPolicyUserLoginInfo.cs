using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyUserLoginInfo")]
public partial class PermissionPolicyUserLoginInfo
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2, MaxLength = 450)]
    public string? LoginProviderName { get; set; }

    [SgpaColumn(Order = 3, MaxLength = 450)]
    public string? ProviderUserKey { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public Guid UserForeignKey { get; set; }

}
