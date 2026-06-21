using Microsoft.Data.SqlClient;

namespace Sgpa.Data.Connection;

/// <summary>
/// Crea conexiones a la base NewSgpa2. Reemplaza al manejo de conexión ADO
/// (cn global / ADOHelper.bas) de los aplicativos VB6.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>Crea una conexión cerrada lista para abrir.</summary>
    SqlConnection Create();

    /// <summary>Crea y abre una conexión de forma asíncrona.</summary>
    Task<SqlConnection> CreateOpenAsync(CancellationToken cancellationToken = default);
}
