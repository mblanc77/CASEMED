using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Sgpa.Data;

/// <summary>
/// Implementación de <see cref="IDbTransactionScope"/>: ejecuta todo sobre una única
/// SqlConnection + SqlTransaction (transacción local, sin MSDTC). Si se descarta sin Commit,
/// hace Rollback automáticamente.
/// </summary>
internal sealed class DbTransactionScope : IDbTransactionScope
{
    private readonly SqlConnection _cn;
    private readonly SqlTransaction _tx;
    private bool _finished;

    public DbTransactionScope(SqlConnection cn, SqlTransaction tx)
    {
        _cn = cn;
        _tx = tx;
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => (await _cn.QueryAsync<T>(Cmd(sql, param, commandType, cancellationToken)).ConfigureAwait(false)).AsList();

    public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.QuerySingleOrDefaultAsync<T?>(Cmd(sql, param, commandType, cancellationToken));

    public Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.ExecuteAsync(Cmd(sql, param, commandType, cancellationToken));

    public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.ExecuteScalarAsync<T?>(Cmd(sql, param, commandType, cancellationToken));

    public Task<IReadOnlyList<T>> QueryProcAsync<T>(string procName, object? param = null, CancellationToken cancellationToken = default)
        => QueryAsync<T>(procName, param, CommandType.StoredProcedure, cancellationToken);

    public Task<int> ExecuteProcAsync(string procName, object? param = null, CancellationToken cancellationToken = default)
        => ExecuteAsync(procName, param, CommandType.StoredProcedure, cancellationToken);

    public async Task<DataTable> GetDataTableAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cmd = new SqlCommand(sql, _cn, _tx) { CommandType = commandType };
        if (param is not null)
            foreach (var prop in param.GetType().GetProperties())
                cmd.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(param) ?? DBNull.Value);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // Ya estamos en una transacción: devolvemos el mismo scope (commit/rollback los maneja el dueño original).
    public Task<IDbTransactionScope> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IDbTransactionScope>(this);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _tx.CommitAsync(cancellationToken).ConfigureAwait(false);
        _finished = true;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _tx.RollbackAsync(cancellationToken).ConfigureAwait(false);
        _finished = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (!_finished)
        {
            try { await _tx.RollbackAsync().ConfigureAwait(false); }
            catch { /* la conexión pudo cerrarse por error previo */ }
        }
        await _tx.DisposeAsync().ConfigureAwait(false);
        await _cn.DisposeAsync().ConfigureAwait(false);
    }

    private CommandDefinition Cmd(string sql, object? param, CommandType type, CancellationToken ct)
        => new(sql, param, _tx, commandType: type, cancellationToken: ct);
}
