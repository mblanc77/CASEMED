using Microsoft.Extensions.Logging;
using Sgpa.Data.Connection;

namespace Sgpa.Data.Errors;

/// <summary>
/// Implementación de <see cref="IErrorLog"/>: loguea el error con <see cref="ILogger"/> (que el host
/// envía al archivo por fecha) e inserta una fila en dbo.Z_ErrorLog. La inserción en base es best-effort:
/// si falla (ej. la base caída), se registra esa falla en el archivo y se sigue, sin propagar.
/// </summary>
public sealed class ErrorLog : IErrorLog
{
    private readonly ILogger<ErrorLog> _logger;
    private readonly IDbConnectionFactory _factory;

    public ErrorLog(ILogger<ErrorLog> logger, IDbConnectionFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task LogAsync(string origen, Exception ex, string? usuario = null, CancellationToken ct = default)
    {
        // 1) Archivo / consola (vía ILogger; el host configura el sink de archivo por fecha).
        _logger.LogError(ex, "[{Origen}] usuario={Usuario}: {Mensaje}", origen, usuario ?? "?", ex.Message);

        // 2) Base de datos (best-effort; jamás propaga).
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.Z_ErrorLog (Login, Origen, Mensaje, Detalle) VALUES (@login, @origen, @mensaje, @detalle)";
            cmd.Parameters.AddWithValue("@login", Recortar(usuario, 50) ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@origen", Recortar(origen, 200) ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@mensaje", Recortar(ex.Message, 1000) ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@detalle", (object?)ex.ToString() ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
        catch (Exception dbEx)
        {
            _logger.LogWarning(dbEx, "No se pudo registrar el error en Z_ErrorLog; queda sólo el log de archivo.");
        }
    }

    private static string? Recortar(string? s, int max) => s is null ? null : (s.Length <= max ? s : s[..max]);
}
