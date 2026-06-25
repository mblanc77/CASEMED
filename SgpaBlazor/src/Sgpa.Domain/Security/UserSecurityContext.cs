namespace Sgpa.Domain.Security;

/// <summary>
/// Restricción efectiva de una columna para el usuario (ya colapsada en unión sobre sus roles).
/// Sólo se materializa para columnas con alguna restricción; la ausencia de regla = acceso pleno.
/// </summary>
public sealed record ColumnRule(bool Read, bool Write);

/// <summary>
/// Filtro efectivo de registros para el usuario sobre una (tabla, acción), colapsado en unión sobre sus roles.
/// <see cref="Unrestricted"/> = ve/opera todas las filas (algún rol no tiene criterio). Si no, sólo las que
/// matchean alguno de <see cref="Criterios"/> (strings CriteriaOperator), combinados con OR.
/// </summary>
public sealed record RecordRule(bool Unrestricted, IReadOnlyList<string> Criterios)
{
    public static readonly RecordRule SinRestriccion = new(true, Array.Empty<string>());
}

/// <summary>
/// Contexto de seguridad de un usuario autenticado: roles, condición de admin, permisos CRUD por tabla,
/// y (estilo XAF) restricciones por columna y filtros por registro. Un rol admin (EsAdmin) concede acceso
/// total (bypass de toda verificación).
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

    /// <summary>
    /// Restricciones por columna, clave <c>"tabla|columna"</c> (case-insensitive). Sólo contiene las columnas
    /// con alguna restricción; la ausencia de entrada significa acceso pleno (leer + modificar).
    /// </summary>
    public IReadOnlyDictionary<string, ColumnRule> ColumnRules { get; init; }
        = new Dictionary<string, ColumnRule>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Filtros por registro, clave <c>(tabla, acción)</c> donde la acción es una sola de Read/Write/Delete.
    /// Sólo contiene las combinaciones donde el acceso queda efectivamente restringido (todos los roles del
    /// usuario aportan criterio); la ausencia de entrada significa sin filtro (todas las filas).
    /// </summary>
    public IReadOnlyDictionary<(string Tabla, PermissionAction Accion), RecordRule> RecordRules { get; init; }
        = new Dictionary<(string, PermissionAction), RecordRule>();

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

    /// <summary>¿Puede leer la columna? (true salvo restricción explícita; siempre true si es admin).</summary>
    public bool CanReadColumn(string table, string column)
    {
        if (IsAdmin) return true;
        return !ColumnRules.TryGetValue(Key(table, column), out var rule) || rule.Read;
    }

    /// <summary>¿Puede modificar la columna? (true salvo restricción explícita; siempre true si es admin).</summary>
    public bool CanWriteColumn(string table, string column)
    {
        if (IsAdmin) return true;
        return !ColumnRules.TryGetValue(Key(table, column), out var rule) || rule.Write;
    }

    /// <summary>
    /// Filtro de registros para (tabla, acción): null = sin restricción (todas las filas / admin). Si no es null,
    /// sólo se permiten las filas que matchean alguno de los criterios.
    /// </summary>
    public RecordRule? RecordFilter(string table, PermissionAction action)
    {
        if (IsAdmin) return null;
        return RecordRules.TryGetValue((table, action), out var rule) ? rule : null;
    }

    /// <summary>Clave canónica de <see cref="ColumnRules"/> para una (tabla, columna).</summary>
    public static string Key(string table, string column) => table + "|" + column;
}
