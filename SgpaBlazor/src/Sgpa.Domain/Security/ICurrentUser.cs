namespace Sgpa.Domain.Security;

/// <summary>
/// Usuario autenticado actual. Lo consume la auditoría (columnas Usr/Ts) y la verificación de permisos
/// de la capa de datos: acciones CRUD por tabla, restricciones por columna y filtros por registro (estilo XAF).
/// </summary>
public interface ICurrentUser
{
    string UserName { get; }
    bool IsInRole(string role);
    bool Can(string table, PermissionAction action);

    /// <summary>¿Puede leer la columna? (true salvo restricción explícita; siempre true para admin).</summary>
    bool CanReadColumn(string table, string column) => true;

    /// <summary>¿Puede modificar la columna? (true salvo restricción explícita; siempre true para admin).</summary>
    bool CanWriteColumn(string table, string column) => true;

    /// <summary>
    /// Filtro de registros para (tabla, acción): null = sin restricción. Si no es null, sólo se permiten
    /// las filas que matchean alguno de los criterios de <see cref="RecordRule.Criterios"/>.
    /// </summary>
    RecordRule? RecordFilter(string table, PermissionAction action) => null;
}
