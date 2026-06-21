namespace Sgpa.Domain.Security;

/// <summary>Permiso de un rol sobre una entidad/tabla concreta.</summary>
public sealed class TablePermission
{
    public required string Role { get; init; }
    public required string Table { get; init; }
    public PermissionAction Actions { get; set; } = PermissionAction.None;

    public bool Can(PermissionAction action) => (Actions & action) == action;
}
