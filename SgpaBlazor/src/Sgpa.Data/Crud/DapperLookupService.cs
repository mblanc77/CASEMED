using System.Collections.Concurrent;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Crud;

/// <summary>
/// Implementación por defecto de <see cref="ISgpaLookupService"/>: arma el SELECT (clave, descripción)
/// desde la metadata y cachea el resultado por tipo dentro del scope (request). Las tablas con más de
/// <see cref="MaxRows"/> filas devuelven null (no aptas para un combo; el editor cae al editor numérico).
/// </summary>
public sealed class DapperLookupService : ISgpaLookupService
{
    private const int MaxRows = 2000;

    private readonly IDbExecutor _db;
    // ConcurrentDictionary + gate: el servicio es Scoped pero un mismo DetailView abre varios combos cuyos
    // OnParametersSetAsync corren concurrentemente (awaits que sueltan el SynchronizationContext). Sin esto, las
    // escrituras al cache se pisaban y corrompían el Dictionary ("non-concurrent collection ... exclusive access").
    private readonly ConcurrentDictionary<Type, IReadOnlyList<LookupItem>?> _cache = new();
    private readonly SemaphoreSlim _gate = new(1, 1);

    public DapperLookupService(IDbExecutor db) => _db = db;

    public async Task<IReadOnlyList<LookupItem>?> GetAsync(Type entityType, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(entityType, out var cached))
            return cached;

        // Serializa la carga (y deduplica cargas concurrentes del mismo tipo). El cache es concurrente, así que
        // las lecturas de afuera son seguras aunque otro hilo esté cargando.
        await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_cache.TryGetValue(entityType, out cached))
                return cached;

            var result = await LoadAsync(entityType, cancellationToken).ConfigureAwait(false);
            _cache[entityType] = result;
            return result;
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<IReadOnlyList<LookupItem>?> LoadAsync(Type entityType, CancellationToken cancellationToken)
    {
        var meta = EntityMetadata.For(entityType);
        var display = meta.DisplayColumn;
        if (meta.Keys.Count != 1 || display is null) return null;

        var key = meta.Keys[0];
        // COUNT primero: tablas grandes (ej. Afiliado por CI) no se materializan en un combo.
        var count = await _db.ExecuteScalarAsync<long>(
            $"SELECT COUNT_BIG(*) FROM {meta.QualifiedTable}", cancellationToken: cancellationToken).ConfigureAwait(false);
        if (count > MaxRows) return null;

        // Se castea la descripción a nvarchar (puede ser text/ntext) y se ordena por la expresión casteada:
        // ORDER BY sobre text/ntext crudo lanza "cannot be compared or sorted".
        var text = DisplayExpr(meta, display);

        // Baja lógica: si la entidad tiene una columna FechaBaja, los registros con baja se marcan (Baja=1) para
        // mostrarlos atenuados y se ordenan AL FINAL (los activos primero, ambos grupos alfabéticos). Sin columna
        // FechaBaja, Baja es siempre 0 y el orden es el de siempre.
        var bajaCol = meta.Columns.FirstOrDefault(c => c.Name.Equals("FechaBaja", StringComparison.OrdinalIgnoreCase));
        var bajaExpr = bajaCol is null ? null : $"CASE WHEN [{bajaCol.Name}] IS NULL THEN 0 ELSE 1 END";
        var bajaSelect = bajaExpr is null ? "CAST(0 AS bit)" : $"CAST({bajaExpr} AS bit)";
        var orderBy = bajaExpr is null ? text : $"{bajaExpr}, {text}";

        var sql = $"SELECT [{key.Name}] AS [Value], {text} AS [Text], {bajaSelect} AS [Baja] " +
                  $"FROM {meta.QualifiedTable} WHERE [{key.Name}] IS NOT NULL " +
                  $"ORDER BY {orderBy}";
        return await _db.QueryAsync<LookupItem>(sql, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyDictionary<string, string>> GetDescriptionsAsync(
        Type entityType, IReadOnlyCollection<object> keys, CancellationToken cancellationToken = default)
    {
        var empty = (IReadOnlyDictionary<string, string>)new Dictionary<string, string>();
        if (keys.Count == 0) return empty;

        var meta = EntityMetadata.For(entityType);
        var display = meta.DisplayColumn;
        if (meta.Keys.Count != 1 || display is null) return empty;

        var key = meta.Keys[0];
        var sql = $"SELECT [{key.Name}] AS [Value], {DisplayExpr(meta, display)} AS [Text] " +
                  $"FROM {meta.QualifiedTable} WHERE [{key.Name}] IN @keys";
        var items = await _db.QueryAsync<LookupItem>(sql, new { keys }, cancellationToken: cancellationToken).ConfigureAwait(false);

        var map = new Dictionary<string, string>();
        foreach (var it in items)
            if (it.Value is not null)
                map[it.Value.ToString()!] = it.Text;
        return map;
    }

    // Expresión SQL de la descripción a mostrar en combos/grillas. Caso especial Afiliado: nombre completo
    // (Apellido1 Apellido2, Nombres) en vez de su DisplayColumn, que sería sólo Nombres (el primer nombre).
    private static string DisplayExpr(EntityMetadata meta, ColumnMetadata display)
        => meta.Table.Equals("Afiliado", StringComparison.OrdinalIgnoreCase)
            ? "LTRIM(RTRIM(ISNULL([Apellido1],'') + ' ' + ISNULL([Apellido2],'') + ', ' + ISNULL([Nombres],'')))"
            : $"CAST([{display.Name}] AS nvarchar(400))";
}

