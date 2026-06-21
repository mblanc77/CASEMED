using System.Collections;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Sgpa.Data.Configuracion;
using Sgpa.Data.Connection;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Reporting;

/// <summary>
/// Carga el grafo de objetos para reportes con ObjectDataSource. Usa el mapa <see cref="ReportNavMap"/>
/// (generado por NavGen, alineado con las nav props) para poblar referencias y colecciones por nombre,
/// con queries batcheadas (<c>WHERE join IN (...)</c>). Respeta el flag DisponibleReportes por tabla.
/// </summary>
public sealed class ReportGraphLoader : IReportGraphLoader
{
    private readonly IDbConnectionFactory _factory;
    private readonly ITablaConfigService _tablaConfig;

    public ReportGraphLoader(IDbConnectionFactory factory, ITablaConfigService tablaConfig)
    {
        _factory = factory;
        _tablaConfig = tablaConfig;
    }

    public async Task<IList> LoadAsync(Type rootType, IReadOnlyCollection<object>? keyFilter, int maxDepth = 2,
        IReadOnlySet<string>? usedRelations = null, CancellationToken ct = default)
    {
        await _tablaConfig.EnsureLoadedAsync(ct).ConfigureAwait(false);
        await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);

        var rootMeta = EntityMetadata.For(rootType);
        var roots = await LoadByColumnAsync(cn, rootType,
            rootMeta.Keys.Count == 1 ? rootMeta.Key.Property.Name : null, keyFilter, ct).ConfigureAwait(false);

        await PopulateAsync(cn, rootType, roots, maxDepth, usedRelations, ct).ConfigureAwait(false);
        return MakeTypedList(rootType, roots);
    }

    public async Task<long> CountAsync(Type rootType, CancellationToken ct = default)
    {
        var meta = EntityMetadata.For(rootType);
        await using var cn = await _factory.CreateOpenAsync(ct).ConfigureAwait(false);
        return await cn.ExecuteScalarAsync<long>(
            new CommandDefinition($"SELECT COUNT_BIG(*) FROM {meta.QualifiedTable}", cancellationToken: ct))
            .ConfigureAwait(false);
    }

    // SQL Server admite máx. 2100 parámetros por request; chunkeamos el IN para no pasarnos con selecciones grandes.
    private const int MaxInValues = 1000;

    // Filas de 'type' filtrando por 'joinProperty IN values'; sin joinProperty/values → todas las filas.
    // joinProperty es el nombre de PROPIEDAD C#; la columna DB real se resuelve vía metadata (pueden diferir).
    private static async Task<List<object>> LoadByColumnAsync(DbConnection cn, Type type,
        string? joinProperty, IReadOnlyCollection<object>? values, CancellationToken ct)
    {
        var meta = EntityMetadata.For(type);

        if (joinProperty is null || values is null)
        {
            var all = await cn.QueryAsync(type,
                new CommandDefinition($"SELECT * FROM {meta.QualifiedTable}", cancellationToken: ct)).ConfigureAwait(false);
            return all.ToList();
        }
        if (values.Count == 0) return new List<object>();

        var dbColumn = meta.Columns.FirstOrDefault(c =>
            c.Property.Name.Equals(joinProperty, StringComparison.OrdinalIgnoreCase))?.Name ?? joinProperty;
        var sql = $"SELECT * FROM {meta.QualifiedTable} WHERE [{dbColumn}] IN @vals";
        var result = new List<object>();
        foreach (var chunk in values.Chunk(MaxInValues))
        {
            var rows = await cn.QueryAsync(type,
                new CommandDefinition(sql, new { vals = chunk }, cancellationToken: ct)).ConfigureAwait(false);
            result.AddRange(rows);
        }
        return result;
    }

    private async Task PopulateAsync(DbConnection cn, Type type, List<object> instances, int depth,
        IReadOnlySet<string>? usedRelations, CancellationToken ct)
    {
        if (depth <= 0 || instances.Count == 0) return;
        if (!ReportNavMap.Map.TryGetValue(type, out var links)) return;

        foreach (var link in links)
        {
            // Optimización: si el reporte declara qué relaciones usa, saltamos las que no aparecen.
            if (usedRelations is not null && !usedRelations.Contains(link.PropName)) continue;

            var remoteMeta = EntityMetadata.For(link.Remote);
            if (!_tablaConfig.DisponibleReportes(remoteMeta.Table)) continue; // respeta la curación del admin

            var joinProp = Prop(type, link.JoinColumn);
            var remoteJoinProp = Prop(link.Remote, link.JoinColumn);
            var targetProp = Prop(type, link.PropName);
            if (joinProp is null || remoteJoinProp is null || targetProp is null) continue;

            var values = instances.Select(i => joinProp.GetValue(i)).Where(v => v is not null).Distinct().ToList();
            if (values.Count == 0) continue;

            var remoteRows = await LoadByColumnAsync(cn, link.Remote, link.JoinColumn, values!, ct).ConfigureAwait(false);
            if (remoteRows.Count == 0) continue;

            if (link.Kind == ReportNavKind.Reference)
            {
                var byKey = new Dictionary<object, object>();
                foreach (var r in remoteRows)
                    if (remoteJoinProp.GetValue(r) is { } k) byKey[k] = r;
                foreach (var inst in instances)
                    if (joinProp.GetValue(inst) is { } k && byKey.TryGetValue(k, out var r))
                        targetProp.SetValue(inst, r);
            }
            else // Collection
            {
                var grouped = new Dictionary<object, List<object>>();
                foreach (var r in remoteRows)
                {
                    if (remoteJoinProp.GetValue(r) is not { } k) continue;
                    if (!grouped.TryGetValue(k, out var l)) grouped[k] = l = new List<object>();
                    l.Add(r);
                }
                foreach (var inst in instances)
                {
                    if (joinProp.GetValue(inst) is not { } k || !grouped.TryGetValue(k, out var l)) continue;
                    var list = (IList)targetProp.GetValue(inst)!; // inicializada con = new() en la nav prop generada
                    foreach (var r in l) list.Add(r);
                }
            }

            await PopulateAsync(cn, link.Remote, remoteRows, depth - 1, usedRelations, ct).ConfigureAwait(false);
        }
    }

    private static readonly Dictionary<(Type, string), PropertyInfo?> _propCache = new();

    private static PropertyInfo? Prop(Type t, string name)
    {
        lock (_propCache)
        {
            if (_propCache.TryGetValue((t, name), out var p)) return p;
            // IgnoreCase: la misma columna puede tener distinta capitalización entre tablas (ej. IDFactura/IdFactura).
            p = t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            _propCache[(t, name)] = p;
            return p;
        }
    }

    private static IList MakeTypedList(Type elementType, List<object> items)
    {
        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType))!;
        foreach (var it in items) list.Add(it);
        return list;
    }
}
