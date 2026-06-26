using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sgpa.Business.Imponibles;

namespace Sgpa.Web.Controllers;

/// <summary>
/// Endpoint de subida de los archivos de nómina ATYR (Carga de Historia Laboral). DxUpload sube cada archivo
/// acá (fuera del circuito Blazor) en su propio request; los acumulamos en memoria bajo el token efímero que la
/// página generó, soportando el caso de una declaración spliteada en varios archivos. La página los recupera por
/// ese token, los parsea y dispara la carga.
/// </summary>
[ApiController]
[Authorize]
[Route("imponibles/carga-hl")]
public sealed class HistoriaLaboralController : ControllerBase
{
    private const long MaxBytes = 32 * 1024 * 1024;   // los archivos BPS pueden ser grandes (miles de líneas)
    private static readonly object _lock = new();

    private readonly IMemoryCache _cache;
    public HistoriaLaboralController(IMemoryCache cache) => _cache = cache;

    [HttpPost("upload")]
    [IgnoreAntiforgeryToken]   // handoff por token efímero en la URL + cookie de sesión; DxUpload no manda token CSRF.
    [RequestSizeLimit(MaxBytes)]
    public async Task<IActionResult> Upload([FromQuery] string token, CancellationToken ct)
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(token) || file is null || file.Length == 0)
            return BadRequest();

        using var reader = new StreamReader(file.OpenReadStream());
        var texto = await reader.ReadToEndAsync(ct);
        var lineas = texto.Replace("\r\n", "\n").Replace('\r', '\n')
                          .Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var key = CacheKey(token);
        lock (_lock)
        {
            var lista = _cache.Get<List<AtyrArchivo>>(key) ?? new List<AtyrArchivo>();
            lista.Add(new AtyrArchivo(file.FileName, lineas));
            _cache.Set(key, lista, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
        }
        return Ok();
    }

    /// <summary>Clave de caché de los archivos subidos para un token (la usa la página para recuperarlos).</summary>
    public static string CacheKey(string token) => $"hl-upload:{token}";
}
