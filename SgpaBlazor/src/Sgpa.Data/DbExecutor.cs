using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Sgpa.Data.Connection;

namespace Sgpa.Data;

public sealed class DbExecutor : IDbExecutor
{
    private readonly IDbConnectionFactory _factory;

    public DbExecutor(IDbConnectionFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        var cmd = new CommandDefinition(sql, param, commandType: commandType, cancellationToken: cancellationToken);
        var rows = await cn.QueryAsync<T>(cmd).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        var cmd = new CommandDefinition(sql, param, commandType: commandType, cancellationToken: cancellationToken);
        return await cn.QuerySingleOrDefaultAsync<T>(cmd).ConfigureAwait(false);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {   
        await using var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        var cmd = new CommandDefinition(sql, param, commandType: commandType, cancellationToken: cancellationToken);
        return await cn.ExecuteAsync(cmd).ConfigureAwait(false);
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        var cmd = new CommandDefinition(sql, param, commandType: commandType, cancellationToken: cancellationToken);
        return await cn.ExecuteScalarAsync<T>(cmd).ConfigureAwait(false);
    }

    public Task<IReadOnlyList<T>> QueryProcAsync<T>(string procName, object? param = null,
        CancellationToken cancellationToken = default)
        => QueryAsync<T>(procName, param, CommandType.StoredProcedure, cancellationToken);

    public Task<int> ExecuteProcAsync(string procName, object? param = null,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(procName, param, CommandType.StoredProcedure, cancellationToken);

    public async Task<IDbTransactionScope> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        var tx = (SqlTransaction)await cn.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        return new DbTransactionScope(cn, tx);
    }

    public async Task<DataTable> GetDataTableAsync(string sql, object? param = null,
        CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        await using var cn = await _factory.CreateOpenAsync(cancellationToken).ConfigureAwait(false);
        await using var command = new SqlCommand(sql, cn) { CommandType = commandType };
        if (param is not null)
            AddParameters(command, param);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    private static void AddParameters(SqlCommand command, object param)
    {
        foreach (var prop in param.GetType().GetProperties())
        {
            var value = prop.GetValue(param) ?? DBNull.Value;
            command.Parameters.AddWithValue("@" + prop.Name, value);
        }
    }
}
