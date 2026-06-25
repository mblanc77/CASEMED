using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Sgpa.Web.Controllers;

/// <summary>Contenido de un archivo Abitab subido vía DxUpload, a la espera de procesarse en la página.</summary>
public sealed record AbitabUpload(string FileName, string[] Lineas);

/// <summary>
/// Endpoint de subida del archivo de cobranza Abitab. DxUpload sube el archivo acá (fuera del circuito Blazor);
/// guardamos su contenido en memoria bajo un token efímero que la página generó y pasó por la URL. Al terminar la
/// subida, la página (CargaAbitab) recupera el contenido por ese token y habilita "Procesar carga".
/// </summary>
[ApiController]
[Authorize]
[Route("pagos/carga")]
public sealed class CargaAbitabController : ControllerBase
{
    // Tope alineado con el StreamReader previo y con MaxFileSize del DxUpload (16 MB).
    private const long MaxBytes = 16 * 1024 * 1024;

    private readonly IMemoryCache _cache;
    public CargaAbitabController(IMemoryCache cache) => _cache = cache;

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

        _cache.Set(CacheKey(token), new AbitabUpload(file.FileName, lineas),
            new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
        return Ok();
    }

    /// <summary>Clave de caché del contenido subido para un token (la usa la página para recuperarlo).</summary>
    public static string CacheKey(string token) => $"abitab-upload:{token}";
}
