using Microsoft.Data.SqlClient;
using Serilog.Core;
using Serilog.Events;

namespace Sgpa.Web.Logging;

/// <summary>
/// Sink de Serilog que persiste en <c>dbo.Z_ErrorLog</c> las excepciones <b>no controladas</b> del framework de
/// componentes/circuitos de Blazor (render/ErrorBoundary/CircuitHost), que de otro modo sólo quedaban en el
/// archivo. Los errores controlados ya los escribe <c>IErrorLog</c> (otro SourceContext), así que no se duplican.
/// Best-effort: nunca propaga ni bloquea más de unos segundos (timeout de conexión corto).
/// </summary>
public sealed class DbErrorLogSink : ILogEventSink
{
    private readonly string _cs;

    public DbErrorLogSink(string connectionString)
    {
        // Timeout de conexión corto: si la base está caída, no bloquear el pipeline de logging.
        var b = new SqlConnectionStringBuilder(connectionString) { ConnectTimeout = 3 };
        _cs = b.ConnectionString;
    }

    public void Emit(LogEvent e)
    {
        if (e.Exception is null || e.Level < LogEventLevel.Warning) return;
        if (e.Exception is OperationCanceledException or ObjectDisposedException) return; // ruido de teardown del circuito

        var origen = SourceContext(e);
        // Sólo las no controladas del framework de componentes (las controladas van por IErrorLog, otro SourceContext).
        if (!origen.Contains("Microsoft.AspNetCore.Components", StringComparison.Ordinal)) return;

        try
        {
            using var cn = new SqlConnection(_cs);
            cn.Open();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.Z_ErrorLog (Login, Origen, Mensaje, Detalle) VALUES (@l, @o, @m, @d)";
            cmd.Parameters.AddWithValue("@l", Trunc(Usuario(e), 50));
            cmd.Parameters.AddWithValue("@o", Trunc(origen, 200));
            cmd.Parameters.AddWithValue("@m", Trunc(e.RenderMessage(), 1000));
            cmd.Parameters.AddWithValue("@d", e.Exception.ToString());
            cmd.ExecuteNonQuery();
        }
        catch
        {
            // Best-effort: jamás romper el logging por un fallo al escribir en base.
        }
    }

    private static string SourceContext(LogEvent e)
        => e.Properties.TryGetValue("SourceContext", out var v) && v is ScalarValue { Value: string s } ? s : "";

    // Usuario del circuito, empujado al LogContext por UserLogCircuitHandler (null si no se conoce).
    private static string? Usuario(LogEvent e)
        => e.Properties.TryGetValue("Usuario", out var v) && v is ScalarValue { Value: string s } ? s : null;

    private static object Trunc(string? s, int max)
        => s is null ? DBNull.Value : s.Length <= max ? s : s[..max];
}
