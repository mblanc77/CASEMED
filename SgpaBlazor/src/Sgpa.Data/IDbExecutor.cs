using System.Data;

namespace Sgpa.Data;

/// <summary>
/// Punto único de ejecución contra NewSgpa2. Combina Dapper (mapeo a POCOs) con
/// ADO.NET crudo (DataTable) para los escenarios dinámicos (FilterControl, exports,
/// archivos vinculados). Modernización de ADOHelper.bas de los VB6.
/// </summary>
public interface IDbExecutor
{
    // ---- Dapper: mapeo a POCO ----
    Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

    Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

    Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

    // ---- Stored procedures (los 597 SP migrados desde las queries Access) ----
    Task<IReadOnlyList<T>> QueryProcAsync<T>(string procName, object? param = null,
        CancellationToken cancellationToken = default);

    Task<int> ExecuteProcAsync(string procName, object? param = null,
        CancellationToken cancellationToken = default);

    // ---- ADO.NET crudo: binding dinámico a grillas / export ----
    Task<DataTable> GetDataTableAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);

    // ---- Transacción (unidad de trabajo atómica) ----
    /// <summary>Inicia una transacción. Todas las operaciones del scope corren sobre una única conexión.</summary>
    Task<IDbTransactionScope> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
