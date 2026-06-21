using Sgpa.Domain.Security;

namespace Sgpa.Data.Security;

public sealed class DapperSecurityService : ISecurityService
{
    private readonly IDbExecutor _db;

    public DapperSecurityService(IDbExecutor db) => _db = db;

    public async Task<UserSecurityContext?> AuthenticateAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        var user = await _db.QuerySingleOrDefaultAsync<UserRow>(
            "SELECT Login, Pass, Nombre FROM seg.Usuario WHERE Login = @login",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (user is null || !PasswordHasher.Verify(password, user.Pass))
            return null;

        await _db.ExecuteAsync("UPDATE seg.Usuario SET UltAcceso = SYSDATETIME() WHERE Login = @login",
            new { login = user.Login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        return await BuildContextAsync(user.Login, user.Nombre, cancellationToken).ConfigureAwait(false);
    }

    public async Task<UserSecurityContext?> LoadContextAsync(string login, CancellationToken cancellationToken = default)
    {
        var user = await _db.QuerySingleOrDefaultAsync<UserRow>(
            "SELECT Login, Pass, Nombre FROM seg.Usuario WHERE Login = @login",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);
        return user is null ? null : await BuildContextAsync(user.Login, user.Nombre, cancellationToken).ConfigureAwait(false);
    }

    private async Task<UserSecurityContext> BuildContextAsync(string login, string? nombre, CancellationToken cancellationToken)
    {
        var roles = await _db.QueryAsync<RoleRow>(
            @"SELECT r.Nombre, r.EsAdmin
              FROM seg.UsuarioRol ur JOIN seg.Rol r ON r.Id = ur.RolId
              WHERE ur.Login = @login",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var isAdmin = roles.Any(r => r.EsAdmin);

        var perms = new Dictionary<string, PermissionAction>(StringComparer.OrdinalIgnoreCase);
        if (!isAdmin)
        {
            var rows = await _db.QueryAsync<PermRow>(
                @"SELECT p.Tabla, p.Acciones
                  FROM seg.RolPermisoTabla p
                  WHERE p.RolId IN (SELECT RolId FROM seg.UsuarioRol WHERE Login = @login)",
                new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

            foreach (var row in rows)
            {
                var action = (PermissionAction)row.Acciones;
                perms[row.Tabla] = perms.TryGetValue(row.Tabla, out var existing) ? existing | action : action;
            }
        }

        return new UserSecurityContext
        {
            Login = login,
            Nombre = nombre,
            IsAdmin = isAdmin,
            Roles = roles.Select(r => r.Nombre).Distinct(StringComparer.OrdinalIgnoreCase).ToArray(),
            TablePermissions = perms
        };
    }

    private sealed record UserRow(string Login, string Pass, string? Nombre);
    private sealed record RoleRow(string Nombre, bool EsAdmin);
    private sealed record PermRow(string Tabla, int Acciones);
}
