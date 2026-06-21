using Microsoft.Data.SqlClient;

namespace Sgpa.Data.Connection;

public sealed class SqlDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlDbConnectionFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("La cadena de conexión a NewSgpa2 no puede ser vacía.", nameof(connectionString));
        _connectionString = connectionString;
    }

    public SqlConnection Create() => new(_connectionString);

    public async Task<SqlConnection> CreateOpenAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        return connection;
    }
}
