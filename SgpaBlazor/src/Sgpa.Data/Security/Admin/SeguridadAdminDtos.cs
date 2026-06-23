using Sgpa.Domain.Security;

namespace Sgpa.Data.Security.Admin;

/// <summary>Usuario tal como lo edita el módulo de administración (seg.Usuario + sus roles).</summary>
public sealed record UsuarioAdmin
{
    public required string Login { get; init; }
    public string? Nombre { get; init; }
    public bool Activo { get; init; } = true;
    public DateTime? UltAcceso { get; init; }
    /// <summary>Roles asignados (ids de seg.Rol).</summary>
    public IReadOnlyList<int> RolIds { get; init; } = Array.Empty<int>();
    /// <summary>Nombres de los roles asignados (para mostrar en la grilla).</summary>
    public IReadOnlyList<string> RolNombres { get; init; } = Array.Empty<string>();
    /// <summary>¿Es administrador? (tiene al menos un rol con EsAdmin).</summary>
    public bool EsAdmin { get; init; }
}

/// <summary>Rol del sistema (seg.Rol). Un rol admin concede acceso total (bypass de permisos por tabla).</summary>
public sealed record RolAdmin
{
    public int Id { get; init; }
    public required string Nombre { get; init; }
    public bool EsAdmin { get; init; }
    public string? CodSistema { get; init; }
    /// <summary>Cantidad de usuarios que tienen este rol (para mostrar/avisar antes de borrar).</summary>
    public int Usuarios { get; init; }
}

/// <summary>Permiso CRUD de un rol sobre una tabla (seg.RolPermisoTabla).</summary>
public sealed record RolPermiso
{
    public int RolId { get; init; }
    public required string Tabla { get; init; }
    public PermissionAction Acciones { get; init; } = PermissionAction.None;
}
