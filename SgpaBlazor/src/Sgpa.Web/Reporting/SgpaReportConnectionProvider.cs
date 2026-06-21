using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using DevExpress.Utils;
using Microsoft.Data.SqlClient;

namespace Sgpa.Web.Reporting;

/// <summary>
/// Expone NewSgpa2 como la única conexión SQL del wizard del diseñador de reportes y la resuelve en runtime.
/// DevExpress llama <see cref="GetDataConnectionParameters"/> tanto al diseñar (elegir tablas y relaciones para
/// las sub-bandas master-detail) como al correr el reporte (resolver por nombre la conexión guardada en el layout),
/// así que la cadena de conexión real nunca viaja dentro del .repx: sólo el nombre lógico "NewSgpa2".
/// </summary>
public sealed class SgpaReportConnectionProvider : IDataSourceWizardConnectionStringsProvider
{
    public const string ConnectionName = "NewSgpa2";
    private readonly string _cs;

    public SgpaReportConnectionProvider(IConfiguration config)
        => _cs = config.GetConnectionString(ConnectionName)
                 ?? throw new InvalidOperationException("Falta la cadena de conexión NewSgpa2.");

    public Dictionary<string, string> GetConnectionDescriptions()
        => new() { [ConnectionName] = "SGPA (NewSgpa2)" };

    public DataConnectionParametersBase GetDataConnectionParameters(string name) => BuildParameters(_cs);

    /// <summary>
    /// Traduce un connection string de SQL Server a <see cref="MsSqlConnectionParameters"/> de DevExpress,
    /// preservando Encrypt/TrustServerCertificate. Reusado por el builder de layouts para resolver el schema.
    /// </summary>
    public static MsSqlConnectionParameters BuildParameters(string connectionString)
    {
        var b = new SqlConnectionStringBuilder(connectionString);
        // Preservar Encrypt/TrustServerCertificate: sin ellos, SqlClient moderno cifra por defecto y, al no
        // confiar en el certificado del server, falla con "Unable to open a database".
        var encrypt = EncryptOn(b) ? DefaultBoolean.True : DefaultBoolean.False;
        var trust = b.TrustServerCertificate ? DefaultBoolean.True : DefaultBoolean.False;
        return b.IntegratedSecurity
            ? new MsSqlConnectionParameters(b.DataSource, b.InitialCatalog, null, null, MsSqlAuthorizationType.Windows, encrypt, trust)
            : new MsSqlConnectionParameters(b.DataSource, b.InitialCatalog, b.UserID, b.Password, MsSqlAuthorizationType.SqlServer, encrypt, trust);
    }

    // Encrypt puede venir como bool o como enum (True/False/Strict/Mandatory/Optional) según la versión de SqlClient.
    private static bool EncryptOn(SqlConnectionStringBuilder b)
    {
        var v = b.TryGetValue("Encrypt", out var raw) ? raw?.ToString() : null;
        if (v is null) return true; // default de SqlClient moderno = cifrar
        return !v.Equals("False", StringComparison.OrdinalIgnoreCase)
            && !v.Equals("Optional", StringComparison.OrdinalIgnoreCase);
    }
}
