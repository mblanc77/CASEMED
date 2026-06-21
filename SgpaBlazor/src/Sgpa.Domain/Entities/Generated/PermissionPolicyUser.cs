using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>
[SgpaTable("PermissionPolicyUser")]
public partial class PermissionPolicyUser
{
    [SgpaColumn(Order = 1, Required = true)]
    [SgpaKey]
    public Guid ID { get; set; }

    [SgpaColumn(Order = 2)]
    public string? UserName { get; set; }

    [SgpaColumn(Order = 3, Required = true)]
    public bool IsActive { get; set; }

    [SgpaColumn(Order = 4, Required = true)]
    public bool ChangePasswordOnFirstLogon { get; set; }

    [SgpaColumn(Order = 5)]
    public string? StoredPassword { get; set; }

    [SgpaColumn(Order = 6, Required = true, MaxLength = 21)]
    public string? Discriminator { get; set; }

    [SgpaColumn(Order = 7)]
    public int? AccessFailedCount { get; set; }

    [SgpaColumn(Order = 8)]
    public DateTime? LockoutEnd { get; set; }

}
