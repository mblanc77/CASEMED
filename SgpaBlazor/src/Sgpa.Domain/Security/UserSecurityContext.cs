namespace Sgpa.Domain.Security;

/// <summary>
/// Contexto de seguridad de un usuario autenticado: roles, condición de admin y
/// permisos CRUD por tabla. Un rol admin (EsAdmin) concede acceso total (bypass).
/// </summary>
public sealed class UserSecurityContext
{
    public required string Login { get; init; }
    public string? Nombre { get; init; }
    public bool IsAdmin { get; init; }
    public IReadOnlyCollection<string> Roles { get; init; } = Array.Empty<string>();

    /// <summary>Permisos por tabla (nombre de tabla → acciones). Case-insensitive.</summary>
    public IReadOnlyDictionary<string, PermissionAction> TablePermissions { get; init; }
        = new Dictionary<string, PermissionAction>(StringComparer.OrdinalIgnoreCase);

    public bool Can(string table, PermissionAction action)
    {
        if (IsAdmin) return true;
        return TablePermissions.TryGetValue(table, out var actions) && (actions & action) == action;
    }

    /// <summary>Acciones permitidas sobre una tabla (todas si es admin).</summary>
    public PermissionAction PermissionsFor(string table)
    {
        if (IsAdmin) return PermissionAction.All;
        return TablePermissions.TryGetValue(table, out var actions) ? actions : PermissionAction.None;
    }
}
