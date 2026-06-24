using Sgpa.Domain.Security;

namespace Sgpa.Data.Security.Admin;

/// <summary>
/// Administración del esquema de seguridad (seg.*): usuarios, roles, asignación de roles
/// y permisos CRUD por tabla. Sólo lo consume el módulo de administración (pantallas para admins).
/// Mantiene la invariante de que el sistema nunca queda sin un administrador activo.
/// </summary>
public interface ISeguridadAdminService
{
    // ---- Usuarios ----
    Task<IReadOnlyList<UsuarioAdmin>> ListUsuariosAsync(CancellationToken ct = default);
    Task<UsuarioAdmin?> GetUsuarioAsync(string login, CancellationToken ct = default);

    /// <summary>Alta de usuario con clave inicial (se hashea PBKDF2) y roles asignados.</summary>
    Task CreateUsuarioAsync(string login, string? nombre, string password, bool activo,
        IEnumerable<int> rolIds, string actor, CancellationToken ct = default);

    /// <summary>Modifica nombre, estado (activo) y roles. No toca la clave.</summary>
    Task UpdateUsuarioAsync(string login, string? nombre, bool activo,
        IEnumerable<int> rolIds, string actor, CancellationToken ct = default);

    /// <summary>Restablece la clave de un usuario (la hashea).</summary>
    Task SetPasswordAsync(string login, string password, string actor, CancellationToken ct = default);

    /// <summary>
    /// Cambio de clave de auto-servicio: el propio usuario verifica su clave actual antes de fijar la nueva.
    /// Lanza <see cref="SeguridadAdminException"/> si la clave actual es incorrecta o la nueva es inválida.
    /// </summary>
    Task ChangeOwnPasswordAsync(string login, string currentPassword, string newPassword, CancellationToken ct = default);

    Task DeleteUsuarioAsync(string login, CancellationToken ct = default);

    // ---- Roles ----
    Task<IReadOnlyList<RolAdmin>> ListRolesAsync(CancellationToken ct = default);
    Task<int> CreateRolAsync(string nombre, bool esAdmin, string? codSistema, string actor, CancellationToken ct = default);
    Task UpdateRolAsync(int id, string nombre, bool esAdmin, string? codSistema, string actor, CancellationToken ct = default);
    Task DeleteRolAsync(int id, CancellationToken ct = default);

    // ---- Permisos por tabla ----
    /// <summary>Permisos explícitos de un rol (tabla → acciones). Las tablas sin fila = sin permiso.</summary>
    Task<IReadOnlyList<RolPermiso>> ListPermisosAsync(int rolId, CancellationToken ct = default);

    /// <summary>Fija las acciones de un rol sobre una tabla. None borra la fila.</summary>
    Task SetPermisoAsync(int rolId, string tabla, PermissionAction acciones, CancellationToken ct = default);
}

/// <summary>Se lanza cuando una operación dejaría al sistema sin un administrador activo.</summary>
public sealed class SeguridadAdminException : Exception
{
    public SeguridadAdminException(string message) : base(message) { }
}
