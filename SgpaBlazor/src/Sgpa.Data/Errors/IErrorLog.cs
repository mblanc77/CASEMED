namespace Sgpa.Data.Errors;

/// <summary>
/// Registro centralizado de errores: deja el detalle en el log de archivo (por fecha) y, si es posible,
/// también en la tabla dbo.Z_ErrorLog. La escritura en base es best-effort: nunca propaga una excepción.
/// </summary>
public interface IErrorLog
{
    Task LogAsync(string origen, Exception ex, string? usuario = null, CancellationToken ct = default);
}
