using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Sgpa.Data;

namespace Sgpa.Business.Tests;

/// <summary>
/// IDbExecutor de prueba ligado a una SqlConnection + SqlTransaction únicas, para ejercitar
/// el camino real de escritura (SPs + inserts) y luego hacer ROLLBACK sin persistir nada.
/// </summary>
public sealed class ScopedDbExecutor : IDbExecutor
{
    private readonly SqlConnection _cn;
    private readonly SqlTransaction _tx;

    public ScopedDbExecutor(SqlConnection cn, SqlTransaction tx) { _cn = cn; _tx = tx; }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => (await _cn.QueryAsync<T>(new CommandDefinition(sql, param, _tx, commandType: commandType, cancellationToken: cancellationToken))).AsList();

    public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.QuerySingleOrDefaultAsync<T?>(new CommandDefinition(sql, param, _tx, commandType: commandType, cancellationToken: cancellationToken));

    public Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.ExecuteAsync(new CommandDefinition(sql, param, _tx, commandType: commandType, cancellationToken: cancellationToken));

    public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        => _cn.ExecuteScalarAsync<T?>(new CommandDefinition(sql, param, _tx, commandType: commandType, cancellationToken: cancellationToken));

    public Task<IReadOnlyList<T>> QueryProcAsync<T>(string procName, object? param = null, CancellationToken cancellationToken = default)
        => QueryAsync<T>(procName, param, CommandType.StoredProcedure, cancellationToken);

    public Task<int> ExecuteProcAsync(string procName, object? param = null, CancellationToken cancellationToken = default)
        => ExecuteAsync(procName, param, CommandType.StoredProcedure, cancellationToken);

    public async Task<DataTable> GetDataTableAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cmd = new SqlCommand(sql, _cn, _tx) { CommandType = commandType };
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        var dt = new DataTable();
        dt.Load(reader);
        return dt;
    }

    // El servicio abre su propia transacción y la confirma; en los tests neutralizamos el Commit
    // (delega a esta misma conexión/tx) y el control de persistencia lo lleva el rollback del test.
    public Task<IDbTransactionScope> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IDbTransactionScope>(new NoCommitScope(this));

    private sealed class NoCommitScope : IDbTransactionScope
    {
        private readonly IDbExecutor _inner;
        public NoCommitScope(IDbExecutor inner) => _inner = inner;

        public Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken ct = default)
            => _inner.QueryAsync<T>(sql, param, commandType, ct);
        public Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken ct = default)
            => _inner.QuerySingleOrDefaultAsync<T>(sql, param, commandType, ct);
        public Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken ct = default)
            => _inner.ExecuteAsync(sql, param, commandType, ct);
        public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken ct = default)
            => _inner.ExecuteScalarAsync<T>(sql, param, commandType, ct);
        public Task<IReadOnlyList<T>> QueryProcAsync<T>(string procName, object? param = null, CancellationToken ct = default)
            => _inner.QueryProcAsync<T>(procName, param, ct);
        public Task<int> ExecuteProcAsync(string procName, object? param = null, CancellationToken ct = default)
            => _inner.ExecuteProcAsync(procName, param, ct);
        public Task<DataTable> GetDataTableAsync(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken ct = default)
            => _inner.GetDataTableAsync(sql, param, commandType, ct);
        public Task<IDbTransactionScope> BeginTransactionAsync(CancellationToken ct = default)
            => Task.FromResult<IDbTransactionScope>(this);
        public Task CommitAsync(CancellationToken ct = default) => Task.CompletedTask;
        public Task RollbackAsync(CancellationToken ct = default) => Task.CompletedTask;
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
