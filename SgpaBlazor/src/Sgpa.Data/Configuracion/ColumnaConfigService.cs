using System.Data;
using Microsoft.Extensions.Logging;
using Sgpa.Data.Connection;

namespace Sgpa.Data.Configuracion;

/// <summary>
/// Implementación singleton de <see cref="IColumnaConfigService"/> con cache en memoria. Usa
/// <see cref="IDbConnectionFactory"/> (no el executor scoped) para poder ser singleton, igual que
/// <see cref="TablaConfigService"/>. Best-effort: ante fallos de base no rompe la app (se usan los defaults).
/// </summary>
public sealed class ColumnaConfigService : IColumnaConfigService
{
    private readonly IDbConnectionFactory _factory;
    private readonly ILogger<ColumnaConfigService> _logger;
    private readonly SemaphoreSlim _lock = new(1, 1);
    // tabla → (columna → override). Comparadores case-insensitive: los nombres físicos no distinguen mayúsculas.
    private Dictionary<string, Dictionary<string, ColumnaConfig>>? _cache;

    public ColumnaConfigService(IDbConnectionFactory factory, ILogger<ColumnaConfigService> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task EnsureLoadedAsync(CancellationToken ct = default)
    {
        if (_cache is not null) return;
        await _lock.WaitAsync(ct).ConfigureAwait(false);
        try { _cache ??= await LoadAsync(ct).ConfigureAwait(false); }
        finally { _lock.Release(); }
    }

    public ColumnaConfig? Get(string tabla, string columna)
        => _cache is not null
           && _cache.TryGetValue(tabla, out var cols)
           && cols.TryGetValue(columna, out var c)
            ? c : null;

    public IReadOnlyList<ColumnaConfig> ForTable(string tabla)
        => _cache is not null && _cache.TryGetValue(tabla, out var cols)
            ? cols.Values.ToList()
            : Array.Empty<ColumnaConfig>();

    public async Task SetAsync(ColumnaConfig config, CancellationToken ct = default)
    {
        // Una fila sin ningún override no aporta nada → la tratamos como un borrado (mantiene la tabla limpia).
        if (config.EstaVacia)
        {
            await DeleteAsync(config.Tabla, config.Columna, ct).ConfigureAwait(false);
            return;
        }

        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = """
                MERGE dbo.ColumnaConfig AS t
                USING (SELECT @tabla AS Tabla, @col AS Columna) AS s ON t.Tabla = s.Tabla AND t.Columna = s.Columna
                WHEN MATCHED THEN UPDATE SET
                    Caption = @caption, DisplayFormat = @fmt, Orden = @orden, Ancho = @ancho, Alineacion = @align,
                    VisibleLista = @vlist, VisibleDetalle = @vdet, SoloLectura = @ro
                WHEN NOT MATCHED THEN INSERT
                    (Tabla, Columna, Caption, DisplayFormat, Orden, Ancho, Alineacion, VisibleLista, VisibleDetalle, SoloLectura)
                    VALUES (@tabla, @col, @caption, @fmt, @orden, @ancho, @align, @vlist, @vdet, @ro);
                """;
            AddParam(cmd, "@tabla", config.Tabla);
            AddParam(cmd, "@col", config.Columna);
            AddParam(cmd, "@caption", (object?)config.Caption);
            AddParam(cmd, "@fmt", (object?)config.DisplayFormat);
            AddParam(cmd, "@orden", (object?)config.Orden);
            AddParam(cmd, "@ancho", (object?)config.Ancho);
            AddParam(cmd, "@align", (object?)config.Alineacion);
            AddParam(cmd, "@vlist", (object?)config.VisibleLista);
            AddParam(cmd, "@vdet", (object?)config.VisibleDetalle);
            AddParam(cmd, "@ro", (object?)config.SoloLectura);
            await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo guardar ColumnaConfig de {Tabla}.{Columna}.", config.Tabla, config.Columna);
        }

        await UpdateCacheAsync(c => SetInCache(c, config), ct).ConfigureAwait(false);
    }

    public async Task DeleteAsync(string tabla, string columna, CancellationToken ct = default)
    {
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = "DELETE FROM dbo.ColumnaConfig WHERE Tabla = @tabla AND Columna = @col";
            AddParam(cmd, "@tabla", tabla);
            AddParam(cmd, "@col", columna);
            await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo borrar ColumnaConfig de {Tabla}.{Columna}.", tabla, columna);
        }

        await UpdateCacheAsync(c =>
        {
            if (c.TryGetValue(tabla, out var cols)) cols.Remove(columna);
        }, ct).ConfigureAwait(false);
    }

    private async Task UpdateCacheAsync(Action<Dictionary<string, Dictionary<string, ColumnaConfig>>> mutate, CancellationToken ct)
    {
        await _lock.WaitAsync(ct).ConfigureAwait(false);
        try { mutate(_cache ??= NewCache()); }
        finally { _lock.Release(); }
    }

    private static void SetInCache(Dictionary<string, Dictionary<string, ColumnaConfig>> cache, ColumnaConfig config)
    {
        if (!cache.TryGetValue(config.Tabla, out var cols))
            cache[config.Tabla] = cols = new Dictionary<string, ColumnaConfig>(StringComparer.OrdinalIgnoreCase);
        cols[config.Columna] = config;
    }

    private async Task<Dictionary<string, Dictionary<string, ColumnaConfig>>> LoadAsync(CancellationToken ct)
    {
        var cache = NewCache();
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = """
                SELECT Tabla, Columna, Caption, DisplayFormat, Orden, Ancho, Alineacion, VisibleLista, VisibleDetalle, SoloLectura
                FROM dbo.ColumnaConfig
                """;
            await using var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false);
            while (await rd.ReadAsync(ct).ConfigureAwait(false))
                SetInCache(cache, new ColumnaConfig(
                    rd.GetString(0),
                    rd.GetString(1),
                    rd.IsDBNull(2) ? null : rd.GetString(2),
                    rd.IsDBNull(3) ? null : rd.GetString(3),
                    rd.IsDBNull(4) ? null : rd.GetInt32(4),
                    rd.IsDBNull(5) ? null : rd.GetString(5),
                    rd.IsDBNull(6) ? null : rd.GetString(6),
                    rd.IsDBNull(7) ? null : rd.GetBoolean(7),
                    rd.IsDBNull(8) ? null : rd.GetBoolean(8),
                    rd.IsDBNull(9) ? null : rd.GetBoolean(9)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo cargar ColumnaConfig; se usan los defaults del código.");
        }
        return cache;
    }

    private static Dictionary<string, Dictionary<string, ColumnaConfig>> NewCache()
        => new(StringComparer.OrdinalIgnoreCase);

    private static void AddParam(IDbCommand cmd, string name, object? value)
    {
        var p = cmd.CreateParameter();
        p.ParameterName = name;
        p.Value = value ?? DBNull.Value;
        cmd.Parameters.Add(p);
    }
}
