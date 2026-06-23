using Sgpa.Domain.Security;

namespace Sgpa.Data.Security.Admin;

/// <summary>
/// Implementación Dapper de <see cref="ISeguridadAdminService"/> sobre el esquema seg.*.
/// Las claves se guardan hasheadas con <see cref="PasswordHasher"/> (mismo formato que el login y la migración).
/// </summary>
public sealed class SeguridadAdminService : ISeguridadAdminService
{
    private readonly IDbExecutor _db;

    public SeguridadAdminService(IDbExecutor db) => _db = db;

    // ---------------------------------------------------------------- Usuarios

    public async Task<IReadOnlyList<UsuarioAdmin>> ListUsuariosAsync(CancellationToken ct = default)
    {
        var usuarios = await _db.QueryAsync<UsuarioRow>(
            "SELECT Login, Nombre, Activo, UltAcceso FROM seg.Usuario ORDER BY Login",
            cancellationToken: ct).ConfigureAwait(false);

        var asign = await _db.QueryAsync<AsignRow>(
            @"SELECT ur.Login, r.Id AS RolId, r.Nombre, r.EsAdmin
              FROM seg.UsuarioRol ur JOIN seg.Rol r ON r.Id = ur.RolId",
            cancellationToken: ct).ConfigureAwait(false);

        var porLogin = asign.GroupBy(a => a.Login, StringComparer.OrdinalIgnoreCase)
                            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);

        return usuarios.Select(u =>
        {
            porLogin.TryGetValue(u.Login, out var rs);
            rs ??= new List<AsignRow>();
            return new UsuarioAdmin
            {
                Login = u.Login,
                Nombre = u.Nombre,
                Activo = u.Activo,
                UltAcceso = u.UltAcceso,
                RolIds = rs.Select(r => r.RolId).ToArray(),
                RolNombres = rs.Select(r => r.Nombre).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToArray(),
                EsAdmin = rs.Any(r => r.EsAdmin)
            };
        }).ToList();
    }

    public async Task<UsuarioAdmin?> GetUsuarioAsync(string login, CancellationToken ct = default)
        => (await ListUsuariosAsync(ct).ConfigureAwait(false))
            .FirstOrDefault(u => string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));

    public async Task CreateUsuarioAsync(string login, string? nombre, string password, bool activo,
        IEnumerable<int> rolIds, string actor, CancellationToken ct = default)
    {
        login = (login ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(login))
            throw new SeguridadAdminException("El login es obligatorio.");
        if (string.IsNullOrWhiteSpace(password))
            throw new SeguridadAdminException("La clave inicial es obligatoria.");

        var existe = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM seg.Usuario WHERE Login = @login", new { login }, cancellationToken: ct).ConfigureAwait(false);
        if (existe > 0)
            throw new SeguridadAdminException($"Ya existe un usuario con login '{login}'.");

        await _db.ExecuteAsync(
            @"INSERT INTO seg.Usuario (Login, Pass, Nombre, Activo, FechaClave, Usr, Ts)
              VALUES (@login, @pass, @nombre, @activo, SYSDATETIME(), @actor, SYSDATETIME())",
            new { login, pass = PasswordHasher.Hash(password), nombre = Nullable(nombre), activo, actor = Actor(actor) },
            cancellationToken: ct).ConfigureAwait(false);

        await ReemplazarRolesAsync(login, rolIds, ct).ConfigureAwait(false);
    }

    public async Task UpdateUsuarioAsync(string login, string? nombre, bool activo,
        IEnumerable<int> rolIds, string actor, CancellationToken ct = default)
    {
        var ids = (rolIds ?? Enumerable.Empty<int>()).Distinct().ToArray();
        // Invariante: no dejar el sistema sin un administrador activo.
        var quedaAdmin = activo && await AlgunoEsAdminAsync(ids, ct).ConfigureAwait(false);
        if (!quedaAdmin)
            await AsegurarOtroAdminActivoAsync(login, ct).ConfigureAwait(false);

        await _db.ExecuteAsync(
            @"UPDATE seg.Usuario SET Nombre = @nombre, Activo = @activo, Usr = @actor, Ts = SYSDATETIME()
              WHERE Login = @login",
            new { login, nombre = Nullable(nombre), activo, actor = Actor(actor) },
            cancellationToken: ct).ConfigureAwait(false);

        await ReemplazarRolesAsync(login, ids, ct).ConfigureAwait(false);
    }

    public async Task SetPasswordAsync(string login, string password, string actor, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new SeguridadAdminException("La clave no puede quedar vacía.");
        await _db.ExecuteAsync(
            @"UPDATE seg.Usuario SET Pass = @pass, FechaClave = SYSDATETIME(), Usr = @actor, Ts = SYSDATETIME()
              WHERE Login = @login",
            new { login, pass = PasswordHasher.Hash(password), actor = Actor(actor) },
            cancellationToken: ct).ConfigureAwait(false);
    }

    public async Task DeleteUsuarioAsync(string login, CancellationToken ct = default)
    {
        // No permitir borrar al último administrador activo.
        await AsegurarOtroAdminActivoAsync(login, ct).ConfigureAwait(false);
        // UsuarioRol cae por FK ON DELETE CASCADE.
        await _db.ExecuteAsync("DELETE FROM seg.Usuario WHERE Login = @login", new { login },
            cancellationToken: ct).ConfigureAwait(false);
    }

    // ------------------------------------------------------------------- Roles

    public async Task<IReadOnlyList<RolAdmin>> ListRolesAsync(CancellationToken ct = default)
        => await _db.QueryAsync<RolAdmin>(
            @"SELECT r.Id, r.Nombre, r.EsAdmin, r.CodSistema,
                     (SELECT COUNT(*) FROM seg.UsuarioRol ur WHERE ur.RolId = r.Id) AS Usuarios
              FROM seg.Rol r ORDER BY r.Nombre",
            cancellationToken: ct).ConfigureAwait(false);

    public async Task<int> CreateRolAsync(string nombre, bool esAdmin, string? codSistema, string actor, CancellationToken ct = default)
    {
        nombre = (nombre ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(nombre))
            throw new SeguridadAdminException("El nombre del rol es obligatorio.");
        var dup = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @nombre", new { nombre }, cancellationToken: ct).ConfigureAwait(false);
        if (dup > 0)
            throw new SeguridadAdminException($"Ya existe un rol llamado '{nombre}'.");

        return await _db.ExecuteScalarAsync<int>(
            @"INSERT INTO seg.Rol (Nombre, EsAdmin, CodSistema, Usr, Ts)
              VALUES (@nombre, @esAdmin, @codSistema, @actor, SYSDATETIME());
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { nombre, esAdmin, codSistema = Nullable(codSistema), actor = Actor(actor) },
            cancellationToken: ct).ConfigureAwait(false);
    }

    public async Task UpdateRolAsync(int id, string nombre, bool esAdmin, string? codSistema, string actor, CancellationToken ct = default)
    {
        nombre = (nombre ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(nombre))
            throw new SeguridadAdminException("El nombre del rol es obligatorio.");

        // Si se le quita la condición de admin, verificar que sigan quedando admins activos.
        if (!esAdmin)
        {
            var eraAdmin = await _db.ExecuteScalarAsync<bool>(
                "SELECT EsAdmin FROM seg.Rol WHERE Id = @id", new { id }, cancellationToken: ct).ConfigureAwait(false);
            if (eraAdmin)
            {
                var otros = await _db.ExecuteScalarAsync<int>(
                    @"SELECT COUNT(DISTINCT u.Login)
                      FROM seg.Usuario u
                      JOIN seg.UsuarioRol ur ON ur.Login = u.Login
                      JOIN seg.Rol r ON r.Id = ur.RolId
                      WHERE u.Activo = 1 AND r.EsAdmin = 1 AND r.Id <> @id",
                    new { id }, cancellationToken: ct).ConfigureAwait(false);
                if (otros == 0)
                    throw new SeguridadAdminException("No se puede quitar la condición de administrador: dejaría al sistema sin administradores activos.");
            }
        }

        var dup = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @nombre AND Id <> @id",
            new { nombre, id }, cancellationToken: ct).ConfigureAwait(false);
        if (dup > 0)
            throw new SeguridadAdminException($"Ya existe un rol llamado '{nombre}'.");

        await _db.ExecuteAsync(
            @"UPDATE seg.Rol SET Nombre = @nombre, EsAdmin = @esAdmin, CodSistema = @codSistema, Usr = @actor, Ts = SYSDATETIME()
              WHERE Id = @id",
            new { id, nombre, esAdmin, codSistema = Nullable(codSistema), actor = Actor(actor) },
            cancellationToken: ct).ConfigureAwait(false);
    }

    public async Task DeleteRolAsync(int id, CancellationToken ct = default)
    {
        var esAdmin = await _db.ExecuteScalarAsync<bool>(
            "SELECT EsAdmin FROM seg.Rol WHERE Id = @id", new { id }, cancellationToken: ct).ConfigureAwait(false);
        if (esAdmin)
        {
            var otros = await _db.ExecuteScalarAsync<int>(
                @"SELECT COUNT(DISTINCT u.Login)
                  FROM seg.Usuario u
                  JOIN seg.UsuarioRol ur ON ur.Login = u.Login
                  JOIN seg.Rol r ON r.Id = ur.RolId
                  WHERE u.Activo = 1 AND r.EsAdmin = 1 AND r.Id <> @id",
                new { id }, cancellationToken: ct).ConfigureAwait(false);
            if (otros == 0)
                throw new SeguridadAdminException("No se puede borrar el rol: es el único rol de administración con usuarios activos.");
        }
        // UsuarioRol y RolPermisoTabla caen por FK ON DELETE CASCADE.
        await _db.ExecuteAsync("DELETE FROM seg.Rol WHERE Id = @id", new { id }, cancellationToken: ct).ConfigureAwait(false);
    }

    // --------------------------------------------------------------- Permisos

    public async Task<IReadOnlyList<RolPermiso>> ListPermisosAsync(int rolId, CancellationToken ct = default)
    {
        var rows = await _db.QueryAsync<PermRow>(
            "SELECT Tabla, Acciones FROM seg.RolPermisoTabla WHERE RolId = @rolId",
            new { rolId }, cancellationToken: ct).ConfigureAwait(false);
        return rows.Select(r => new RolPermiso { RolId = rolId, Tabla = r.Tabla, Acciones = (PermissionAction)r.Acciones }).ToList();
    }

    public async Task SetPermisoAsync(int rolId, string tabla, PermissionAction acciones, CancellationToken ct = default)
    {
        if (acciones == PermissionAction.None)
        {
            await _db.ExecuteAsync(
                "DELETE FROM seg.RolPermisoTabla WHERE RolId = @rolId AND Tabla = @tabla",
                new { rolId, tabla }, cancellationToken: ct).ConfigureAwait(false);
            return;
        }
        await _db.ExecuteAsync(
            @"MERGE seg.RolPermisoTabla AS t
              USING (SELECT @rolId AS RolId, @tabla AS Tabla) AS s ON t.RolId = s.RolId AND t.Tabla = s.Tabla
              WHEN MATCHED THEN UPDATE SET Acciones = @acc
              WHEN NOT MATCHED THEN INSERT (RolId, Tabla, Acciones) VALUES (@rolId, @tabla, @acc);",
            new { rolId, tabla, acc = (int)acciones }, cancellationToken: ct).ConfigureAwait(false);
    }

    // --------------------------------------------------------------- Internos

    private async Task ReemplazarRolesAsync(string login, IEnumerable<int> rolIds, CancellationToken ct)
    {
        await _db.ExecuteAsync("DELETE FROM seg.UsuarioRol WHERE Login = @login", new { login },
            cancellationToken: ct).ConfigureAwait(false);
        foreach (var rolId in rolIds.Distinct())
            await _db.ExecuteAsync(
                "INSERT INTO seg.UsuarioRol (Login, RolId) VALUES (@login, @rolId)",
                new { login, rolId }, cancellationToken: ct).ConfigureAwait(false);
    }

    private async Task<bool> AlgunoEsAdminAsync(IReadOnlyCollection<int> rolIds, CancellationToken ct)
    {
        if (rolIds.Count == 0) return false;
        var n = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM seg.Rol WHERE EsAdmin = 1 AND Id IN @ids",
            new { ids = rolIds }, cancellationToken: ct).ConfigureAwait(false);
        return n > 0;
    }

    /// <summary>Lanza si <paramref name="excluyendoLogin"/> es el último administrador activo del sistema.</summary>
    private async Task AsegurarOtroAdminActivoAsync(string excluyendoLogin, CancellationToken ct)
    {
        var otros = await _db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(DISTINCT u.Login)
              FROM seg.Usuario u
              JOIN seg.UsuarioRol ur ON ur.Login = u.Login
              JOIN seg.Rol r ON r.Id = ur.RolId
              WHERE u.Activo = 1 AND r.EsAdmin = 1 AND u.Login <> @login",
            new { login = excluyendoLogin }, cancellationToken: ct).ConfigureAwait(false);

        // Sólo bloqueamos si el usuario afectado ERA el admin activo que sostiene el sistema.
        if (otros == 0)
        {
            var eraAdmin = await _db.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*)
                  FROM seg.Usuario u
                  JOIN seg.UsuarioRol ur ON ur.Login = u.Login
                  JOIN seg.Rol r ON r.Id = ur.RolId
                  WHERE u.Activo = 1 AND r.EsAdmin = 1 AND u.Login = @login",
                new { login = excluyendoLogin }, cancellationToken: ct).ConfigureAwait(false);
            if (eraAdmin > 0)
                throw new SeguridadAdminException("No se puede dejar al sistema sin un administrador activo.");
        }
    }

    private static object Nullable(string? s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();
    private static string Actor(string actor) => string.IsNullOrWhiteSpace(actor) ? "admin" : actor.Length > 16 ? actor[..16] : actor;

    private sealed record UsuarioRow(string Login, string? Nombre, bool Activo, DateTime? UltAcceso);
    private sealed record AsignRow(string Login, int RolId, string Nombre, bool EsAdmin);
    private sealed record PermRow(string Tabla, int Acciones);
}
