using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Sgpa.Web.Controllers;

/// <summary>Contenido de un archivo de líquidos subido vía DxUpload, a la espera de procesarse en la página.</summary>
public sealed record LiquidoUpload(string FileName, string Contenido);

/// <summary>
/// Endpoint de subida del archivo de la carga automática de líquidos (CSV). DxUpload lo sube acá (fuera del
/// circuito Blazor); guardamos su contenido en memoria bajo un token efímero que la página generó y pasó por la
/// URL. Al terminar, la página (CargaLiquidos) lo recupera por ese token, lo parsea y dispara la carga.
/// </summary>
[ApiController]
[Authorize]
[Route("pagos/carga-liquido")]
public sealed class CargaLiquidoController : ControllerBase
{
    private const long MaxBytes = 16 * 1024 * 1024;

    private readonly IMemoryCache _cache;
    public CargaLiquidoController(IMemoryCache cache) => _cache = cache;

    [HttpPost("upload")]
    [IgnoreAntiforgeryToken]   // handoff por token efímero en la URL + cookie de sesión; DxUpload no manda token CSRF.
    [RequestSizeLimit(MaxBytes)]
    public async Task<IActionResult> Upload([FromQuery] string token, CancellationToken ct)
    {
        var file = Request.Form.Files.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(token) || file is null || file.Length == 0)
            return BadRequest();

        using var reader = new StreamReader(file.OpenReadStream());
        var contenido = await reader.ReadToEndAsync(ct);

        _cache.Set(CacheKey(token), new LiquidoUpload(file.FileName, contenido),
            new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
        return Ok();
    }

    /// <summary>Clave de caché del contenido subido para un token (la usa la página para recuperarlo).</summary>
    public static string CacheKey(string token) => $"liquido-upload:{token}";
}
