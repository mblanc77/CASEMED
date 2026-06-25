using Sgpa.Domain.Security;

namespace Sgpa.Data.Security;

public sealed class DapperSecurityService : ISecurityService
{
    private readonly IDbExecutor _db;

    public DapperSecurityService(IDbExecutor db) => _db = db;

    public async Task<UserSecurityContext?> AuthenticateAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        // Sólo usuarios activos pueden autenticarse (los desactivados desde el módulo de seguridad quedan bloqueados).
        var user = await _db.QuerySingleOrDefaultAsync<UserRow>(
            "SELECT Login, Pass, Nombre FROM seg.Usuario WHERE Login = @login AND Activo = 1",
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
            @"SELECT r.Id AS RolId, r.Nombre, r.EsAdmin
              FROM seg.UsuarioRol ur JOIN seg.Rol r ON r.Id = ur.RolId
              WHERE ur.Login = @login",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var isAdmin = roles.Any(r => r.EsAdmin);

        var context = new UserSecurityContext
        {
            Login = login,
            Nombre = nombre,
            IsAdmin = isAdmin,
            Roles = roles.Select(r => r.Nombre).Distinct(StringComparer.OrdinalIgnoreCase).ToArray(),
        };
        if (isAdmin) return context;   // admin: acceso total, sin cargar reglas

        var roleIds = roles.Select(r => r.RolId).Distinct().ToArray();

        // Permisos por tabla (unión de acciones sobre los roles del usuario) + grant por rol (para colapsar reglas).
        var permRows = await _db.QueryAsync<TablePermRow>(
            @"SELECT p.RolId, p.Tabla, p.Acciones
              FROM seg.RolPermisoTabla p
              WHERE p.RolId IN (SELECT RolId FROM seg.UsuarioRol WHERE Login = @login)",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var perms = new Dictionary<string, PermissionAction>(StringComparer.OrdinalIgnoreCase);
        foreach (var row in permRows)
        {
            var action = (PermissionAction)row.Acciones;
            perms[row.Tabla] = perms.TryGetValue(row.Tabla, out var existing) ? existing | action : action;
        }

        // Grant por (rol, tabla): para saber, por rol, qué acciones concede sobre la tabla.
        var grantByRole = new Dictionary<(int RolId, string Tabla), PermissionAction>(RoleTableComparer.Instance);
        foreach (var row in permRows)
        {
            var key = (row.RolId, row.Tabla);
            grantByRole[key] = grantByRole.TryGetValue(key, out var a) ? a | (PermissionAction)row.Acciones : (PermissionAction)row.Acciones;
        }
        bool RoleGrants(int rolId, string tabla, PermissionAction action) =>
            grantByRole.TryGetValue((rolId, tabla), out var a) && (a & action) == action;

        var columnRows = await _db.QueryAsync<ColumnPermRow>(
            @"SELECT c.RolId, c.Tabla, c.Columna, c.Leer, c.Modificar
              FROM seg.RolPermisoColumna c
              WHERE c.RolId IN (SELECT RolId FROM seg.UsuarioRol WHERE Login = @login)",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var recordRows = await _db.QueryAsync<RecordPermRow>(
            @"SELECT r.RolId, r.Tabla, r.Acciones, r.Criteria
              FROM seg.RolPermisoRegistro r
              WHERE r.RolId IN (SELECT RolId FROM seg.UsuarioRol WHERE Login = @login)",
            new { login }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var columnRules = CollapseColumnRules(columnRows, roleIds, RoleGrants);
        var recordRules = CollapseRecordRules(recordRows, roleIds, RoleGrants);

        return new UserSecurityContext
        {
            Login = login,
            Nombre = nombre,
            IsAdmin = false,
            Roles = context.Roles,
            TablePermissions = perms,
            ColumnRules = columnRules,
            RecordRules = recordRules
        };
    }

    // Colapsa las restricciones por columna a la unión sobre los roles del usuario: una columna es legible/
    // modificable si ALGÚN rol que concede Read/Write sobre la tabla la permite (sin fila restrictiva o con el bit en 1).
    private static Dictionary<string, ColumnRule> CollapseColumnRules(
        IReadOnlyList<ColumnPermRow> columnRows, int[] roleIds, Func<int, string, PermissionAction, bool> roleGrants)
    {
        var byKey = columnRows.ToDictionary(c => (c.RolId, c.Tabla, c.Columna), c => (c.Leer, c.Modificar), RoleTableColComparer.Instance);
        var result = new Dictionary<string, ColumnRule>(StringComparer.OrdinalIgnoreCase);

        foreach (var grp in columnRows
                     .Select(c => (c.Tabla, c.Columna))
                     .Distinct())
        {
            var (tabla, columna) = grp;

            var readGranting = roleIds.Where(r => roleGrants(r, tabla, PermissionAction.Read)).ToList();
            bool readable = readGranting.Count == 0
                || readGranting.Any(r => !byKey.TryGetValue((r, tabla, columna), out var cr) || cr.Leer);

            var writeGranting = roleIds.Where(r => roleGrants(r, tabla, PermissionAction.Write)).ToList();
            bool writable = writeGranting.Count == 0
                || writeGranting.Any(r => !byKey.TryGetValue((r, tabla, columna), out var cr) || cr.Modificar);

            if (!readable || !writable)
                result[UserSecurityContext.Key(tabla, columna)] = new ColumnRule(readable, writable);
        }
        return result;
    }

    // Colapsa los filtros por registro: para cada (tabla, acción), si algún rol que concede la acción no tiene
    // criterio → sin restricción; si todos aportan criterio → unión (OR) de los criterios de todos los roles.
    private static Dictionary<(string, PermissionAction), RecordRule> CollapseRecordRules(
        IReadOnlyList<RecordPermRow> recordRows, int[] roleIds, Func<int, string, PermissionAction, bool> roleGrants)
    {
        var result = new Dictionary<(string, PermissionAction), RecordRule>();
        var acciones = new[] { PermissionAction.Read, PermissionAction.Write, PermissionAction.Delete };

        foreach (var tabla in recordRows.Select(r => r.Tabla).Distinct(StringComparer.OrdinalIgnoreCase))
        foreach (var accion in acciones)
        {
            var granting = roleIds.Where(r => roleGrants(r, tabla, accion)).ToList();
            if (granting.Count == 0) continue;   // la verificación de tabla bloquea de todos modos

            var criterios = new List<string>();
            bool algunoSinRestriccion = false;
            foreach (var rolId in granting)
            {
                var crits = recordRows
                    .Where(rr => rr.RolId == rolId
                                 && rr.Tabla.Equals(tabla, StringComparison.OrdinalIgnoreCase)
                                 && ((PermissionAction)rr.Acciones & accion) == accion)
                    .Select(rr => rr.Criteria)
                    .ToList();
                if (crits.Count == 0) { algunoSinRestriccion = true; break; }
                criterios.AddRange(crits);
            }
            if (algunoSinRestriccion) continue;

            result[(tabla, accion)] = new RecordRule(false,
                criterios.Distinct(StringComparer.Ordinal).ToList());
        }
        return result;
    }

    private sealed record UserRow(string Login, string Pass, string? Nombre);
    private sealed record RoleRow(int RolId, string Nombre, bool EsAdmin);
    private sealed record TablePermRow(int RolId, string Tabla, int Acciones);
    private sealed record ColumnPermRow(int RolId, string Tabla, string Columna, bool Leer, bool Modificar);
    private sealed record RecordPermRow(int RolId, string Tabla, int Acciones, string Criteria);

    private sealed class RoleTableComparer : IEqualityComparer<(int RolId, string Tabla)>
    {
        public static readonly RoleTableComparer Instance = new();
        public bool Equals((int RolId, string Tabla) x, (int RolId, string Tabla) y) =>
            x.RolId == y.RolId && string.Equals(x.Tabla, y.Tabla, StringComparison.OrdinalIgnoreCase);
        public int GetHashCode((int RolId, string Tabla) o) =>
            HashCode.Combine(o.RolId, StringComparer.OrdinalIgnoreCase.GetHashCode(o.Tabla));
    }

    private sealed class RoleTableColComparer : IEqualityComparer<(int RolId, string Tabla, string Columna)>
    {
        public static readonly RoleTableColComparer Instance = new();
        public bool Equals((int RolId, string Tabla, string Columna) x, (int RolId, string Tabla, string Columna) y) =>
            x.RolId == y.RolId
            && string.Equals(x.Tabla, y.Tabla, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.Columna, y.Columna, StringComparison.OrdinalIgnoreCase);
        public int GetHashCode((int RolId, string Tabla, string Columna) o) =>
            HashCode.Combine(o.RolId,
                StringComparer.OrdinalIgnoreCase.GetHashCode(o.Tabla),
                StringComparer.OrdinalIgnoreCase.GetHashCode(o.Columna));
    }
}
