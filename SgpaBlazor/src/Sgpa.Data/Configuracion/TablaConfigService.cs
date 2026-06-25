using Microsoft.Extensions.Logging;
using Sgpa.Data.Connection;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Configuracion;

/// <summary>
/// Implementación singleton de <see cref="ITablaConfigService"/> con cache en memoria. Usa
/// <see cref="IDbConnectionFactory"/> (no el executor scoped) para poder ser singleton, igual que ErrorLog.
/// Best-effort: ante fallos de base no rompe la app (se usan los defaults).
/// </summary>
public sealed class TablaConfigService : ITablaConfigService
{
    private readonly IDbConnectionFactory _factory;
    private readonly ILogger<TablaConfigService> _logger;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private Dictionary<string, TablaConfig>? _cache;

    public TablaConfigService(IDbConnectionFactory factory, ILogger<TablaConfigService> logger)
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

    public TablaConfig Get(string tabla)
        => _cache is not null && _cache.TryGetValue(tabla, out var c) ? c : TablaConfig.Default;

    public string DisplayName(string tabla)
    {
        var alias = Get(tabla).Alias;
        if (!string.IsNullOrWhiteSpace(alias)) return alias!;
        var t = tabla.StartsWith("SP_", StringComparison.OrdinalIgnoreCase) ? tabla[3..] : tabla;
        return Humanizar(t);
    }

    /// <summary>
    /// Nombre amigable por defecto a partir del nombre físico: separa PascalCase/camelCase y guiones bajos en
    /// palabras (sin partir siglas en mayúsculas). Ej. "SituacionMutual" → "Situacion Mutual",
    /// "SubsidioItemCod_Afiliado" → "Subsidio Item Cod Afiliado". El alias del admin tiene prioridad sobre esto.
    /// </summary>
    public static string Humanizar(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        var sb = new System.Text.StringBuilder(s.Length + 8);
        for (var i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (c == '_') { sb.Append(' '); continue; }
            if (i > 0 && char.IsUpper(c))
            {
                var prev = s[i - 1];
                var nextLower = i + 1 < s.Length && char.IsLower(s[i + 1]);
                // límite camel/Pascal: minúscula/dígito → Mayúscula, o fin de sigla (MAYÚS seguida de Mayús+minús).
                if (char.IsLower(prev) || char.IsDigit(prev) || (char.IsUpper(prev) && nextLower))
                    sb.Append(' ');
            }
            sb.Append(c);
        }
        return string.Join(' ', sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    public bool DisponibleReportes(string tabla)
    {
        if (Get(tabla).DisponibleReportes is bool explicito) return explicito;
        var meta = EntityCatalog.TryGet(tabla);
        return meta is not null && ReportableTables.IsDefault(meta);
    }

    public IReadOnlyDictionary<string, TablaConfig> All
        => _cache ?? (IReadOnlyDictionary<string, TablaConfig>)new Dictionary<string, TablaConfig>(StringComparer.OrdinalIgnoreCase);

    private const string MergeSql = """
        MERGE dbo.TablaConfig AS t
        USING (SELECT @tabla AS Tabla) AS s ON t.Tabla = s.Tabla
        WHEN MATCHED THEN UPDATE SET EdicionInline = @inline, ConfirmarBorrado = @conf, Auditar = @aud, Alias = @alias, DisponibleReportes = @disp
        WHEN NOT MATCHED THEN INSERT (Tabla, EdicionInline, ConfirmarBorrado, Auditar, Alias, DisponibleReportes)
            VALUES (@tabla, @inline, @conf, @aud, @alias, @disp);
        """;

    private static void BindMergeParams(Microsoft.Data.SqlClient.SqlCommand cmd, string tabla, TablaConfig config)
    {
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@tabla", tabla);
        cmd.Parameters.AddWithValue("@inline", config.EdicionInline);
        cmd.Parameters.AddWithValue("@conf", config.ConfirmarBorrado);
        cmd.Parameters.AddWithValue("@aud", config.Auditar);
        cmd.Parameters.AddWithValue("@alias", (object?)config.Alias ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@disp", (object?)config.DisponibleReportes ?? DBNull.Value);
    }

    public async Task SetAsync(string tabla, TablaConfig config, CancellationToken ct = default)
    {
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = MergeSql;
            BindMergeParams(cmd, tabla, config);
            await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo guardar la configuración de la tabla {Tabla}.", tabla);
        }

        await _lock.WaitAsync(ct).ConfigureAwait(false);
        try { (_cache ??= new(StringComparer.OrdinalIgnoreCase))[tabla] = config; }
        finally { _lock.Release(); }
    }

    public async Task SetManyAsync(IReadOnlyCollection<KeyValuePair<string, TablaConfig>> items, CancellationToken ct = default)
    {
        if (items.Count == 0) return;
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            using var tx = cn.BeginTransaction();
            var cmd = cn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = MergeSql;
            foreach (var (tabla, config) in items)
            {
                BindMergeParams(cmd, tabla, config);
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }
            await tx.CommitAsync(ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo guardar el lote de configuración de tablas ({Cantidad}).", items.Count);
        }

        await _lock.WaitAsync(ct).ConfigureAwait(false);
        try
        {
            var cache = _cache ??= new(StringComparer.OrdinalIgnoreCase);
            foreach (var (tabla, config) in items) cache[tabla] = config;
        }
        finally { _lock.Release(); }
    }

    private async Task<Dictionary<string, TablaConfig>> LoadAsync(CancellationToken ct)
    {
        var dict = new Dictionary<string, TablaConfig>(StringComparer.OrdinalIgnoreCase);
        try
        {
            await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
            var cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Tabla, EdicionInline, ConfirmarBorrado, Auditar, Alias, DisponibleReportes FROM dbo.TablaConfig";
            await using var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false);
            while (await rd.ReadAsync(ct).ConfigureAwait(false))
                dict[rd.GetString(0)] = new TablaConfig(
                    rd.GetBoolean(1), rd.GetBoolean(2), rd.GetBoolean(3),
                    rd.IsDBNull(4) ? null : rd.GetString(4),
                    rd.IsDBNull(5) ? null : rd.GetBoolean(5));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo cargar TablaConfig; se usan los defaults.");
        }
        return dict;
    }
}
