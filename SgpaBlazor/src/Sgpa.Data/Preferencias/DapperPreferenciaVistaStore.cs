using Microsoft.Extensions.Logging;

namespace Sgpa.Data.Preferencias;

/// <summary>
/// Implementación de <see cref="IPreferenciaVistaStore"/> sobre dbo.PreferenciaVista (clave Login+Vista).
/// Best-effort: la personalización es una comodidad de UI, así que un fallo de base nunca rompe la
/// pantalla — se registra en el log y se sigue (la grilla queda con su layout por defecto).
/// </summary>
public sealed class DapperPreferenciaVistaStore : IPreferenciaVistaStore
{
    private readonly IDbExecutor _db;
    private readonly ILogger<DapperPreferenciaVistaStore> _logger;

    public DapperPreferenciaVistaStore(IDbExecutor db, ILogger<DapperPreferenciaVistaStore> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<string?> GetAsync(string login, string vista, CancellationToken ct = default)
    {
        try
        {
            return await _db.QuerySingleOrDefaultAsync<string>(
                "SELECT Json FROM dbo.PreferenciaVista WHERE Login = @login AND Vista = @vista",
                new { login, vista }, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo leer la personalización de {Vista} para {Login}.", vista, login);
            return null;
        }
    }

    public async Task SaveAsync(string login, string vista, string json, CancellationToken ct = default)
    {
        try
        {
            await _db.ExecuteAsync(
                """
                MERGE dbo.PreferenciaVista AS t
                USING (SELECT @login AS Login, @vista AS Vista) AS s
                    ON t.Login = s.Login AND t.Vista = s.Vista
                WHEN MATCHED THEN UPDATE SET Json = @json, FechaMod = SYSDATETIME()
                WHEN NOT MATCHED THEN INSERT (Login, Vista, Json) VALUES (@login, @vista, @json);
                """,
                new { login, vista, json }, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo guardar la personalización de {Vista} para {Login}.", vista, login);
        }
    }

    public async Task ResetAsync(string login, string vista, CancellationToken ct = default)
    {
        try
        {
            await _db.ExecuteAsync(
                "DELETE FROM dbo.PreferenciaVista WHERE Login = @login AND Vista = @vista",
                new { login, vista }, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo restablecer la personalización de {Vista} para {Login}.", vista, login);
        }
    }
}
