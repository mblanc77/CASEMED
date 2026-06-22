using Dapper;
using Sgpa.Data.Auditoria;
using Sgpa.Data.Configuracion;
using Sgpa.Domain.Metadata;
using Sgpa.Domain.Security;

namespace Sgpa.Data.Crud;

/// <summary>
/// CRUD genérico basado en metadata + Dapper. Deriva el SQL de <see cref="EntityMetadata"/>,
/// soporta clave simple o compuesta y completa automáticamente las columnas de auditoría (Usr/Ts).
/// </summary>
public class DapperCrudService<TEntity> : ISgpaCrudService<TEntity> where TEntity : class
{
    private static readonly EntityMetadata Meta = EntityMetadata.For<TEntity>();
    private static readonly Sql Statements = BuildSql(Meta);

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;
    private readonly IAuditService _audit;
    private readonly ITablaConfigService _tablaConfig;

    public DapperCrudService(IDbExecutor db, ICurrentUser user, IAuditService audit, ITablaConfigService tablaConfig)
    {
        _db = db;
        _user = user;
        _audit = audit;
        _tablaConfig = tablaConfig;
    }

    // ¿La tabla tiene la auditoría por campo habilitada? (TablaConfig, cacheado).
    private async Task<bool> AuditarAsync(CancellationToken ct)
    {
        await _tablaConfig.EnsureLoadedAsync(ct).ConfigureAwait(false);
        return _tablaConfig.Get(Meta.Table).Auditar;
    }

    private static object?[] KeyValues(TEntity entity) => Meta.Keys.Select(k => k.GetValue(entity)).ToArray();

    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => _db.QueryAsync<TEntity>(Statements.SelectAll, cancellationToken: cancellationToken);

    public Task<IReadOnlyList<TEntity>> GetByColumnAsync(string columnName, object? value, CancellationToken cancellationToken = default)
    {
        var col = ResolveColumn(columnName);
        var p = new DynamicParameters();
        p.Add("p", value);
        return _db.QueryAsync<TEntity>($"{Statements.SelectAll} WHERE [{col.Name}]=@p", p,
            cancellationToken: cancellationToken);
    }

    public async Task<PagedResult<TEntity>> GetPageAsync(PageQuery query, CancellationToken cancellationToken = default)
    {
        var p = new DynamicParameters();
        int n = 0;
        var where = BuildWhere(query, p, ref n);
        var orderBy = BuildOrderBy(query.Sort, query.Calc, p, ref n);

        var total = await _db.ExecuteScalarAsync<int>(
            $"SELECT COUNT(*) FROM {Meta.QualifiedTable}{where}", p, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        var take = Math.Clamp(query.Take, 1, 50000); // tope alto para permitir export-all
        var skip = Math.Max(0, query.Skip);
        p.Add("__skip", skip);
        p.Add("__take", take);

        var sql = $"{Statements.SelectAll}{where} ORDER BY {orderBy} " +
                  "OFFSET @__skip ROWS FETCH NEXT @__take ROWS ONLY";
        var items = await _db.QueryAsync<TEntity>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
        return new PagedResult<TEntity>(items, total);
    }

    // Arma el WHERE combinando: filtro fijo (FK del padre) + búsqueda libre + árbol de filtro del grid.
    // <paramref name="n"/> numera los parámetros y se comparte con el ORDER BY / GROUP BY (campos calculados).
    private static string BuildWhere(PageQuery query, DynamicParameters p, ref int n)
    {
        var conds = new List<string>();

        if (!string.IsNullOrWhiteSpace(query.FilterColumn))
        {
            var col = ResolveColumn(query.FilterColumn);
            conds.Add($"[{col.Name}]=@__fv");
            p.Add("__fv", query.FilterValue);
        }

        if (!string.IsNullOrWhiteSpace(query.Search) && SearchColumns.Length > 0)
        {
            // Busca el texto en cualquier columna string + claves (casteadas para soportar text/ntext/numéricos).
            var likes = SearchColumns.Select(c => $"CAST([{c}] AS nvarchar(4000)) LIKE @__s");
            conds.Add("(" + string.Join(" OR ", likes) + ")");
            p.Add("__s", "%" + query.Search.Trim() + "%");
        }

        if (query.Filter is not null)
        {
            var sql = FilterSqlTranslator.Translate(query.Filter, p, ref n, Meta, Meta.QualifiedTable, "", CalcLookup(query.Calc));
            if (!string.IsNullOrEmpty(sql)) conds.Add(sql);
        }

        return conds.Count > 0 ? " WHERE " + string.Join(" AND ", conds) : string.Empty;
    }

    // Resolver de campos calculados (nombre → expresión neutral) para el filtro; null si no hay calculados.
    private static Func<string, ScalarNode?>? CalcLookup(IReadOnlyList<CalculatedField>? calc)
    {
        if (calc is not { Count: > 0 }) return null;
        var byName = calc.ToDictionary(c => c.Nombre, c => c.Expr, StringComparer.OrdinalIgnoreCase);
        return name => byName.TryGetValue(name, out var node) ? node : null;
    }

    // SQL de una columna "ordenable/agrupable": columna real (<c>[col]</c>) o expresión de un campo calculado (<c>(expr)</c>).
    private static string SortableSql(string name, IReadOnlyList<CalculatedField>? calc, DynamicParameters p, ref int n)
    {
        var cf = calc?.FirstOrDefault(c => c.Nombre.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (cf is not null)
            return "(" + ScalarSqlTranslator.Translate(cf.Expr, c => $"[{ResolveColumn(c).Name}]", p, ref n) + ")";
        return $"[{ResolveColumn(name).Name}]";
    }

    private static string BuildOrderBy(IReadOnlyList<SortColumn>? sort, IReadOnlyList<CalculatedField>? calc,
        DynamicParameters p, ref int n)
    {
        if (sort is null || sort.Count == 0) return Statements.OrderByKeys;
        // Valida cada columna contra la metadata (evita inyección por FieldName) y agrega las claves
        // como desempate, sin repetir columnas (SQL Server rechaza una columna dos veces en ORDER BY).
        var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var parts = new List<string>();
        foreach (var s in sort)
        {
            var cf = calc?.FirstOrDefault(c => c.Nombre.Equals(s.Column, StringComparison.OrdinalIgnoreCase));
            if (cf is not null)
            {
                if (used.Add("calc:" + cf.Nombre))
                {
                    var expr = SortableSql(s.Column, calc, p, ref n);
                    parts.Add($"{expr}{(s.Descending ? " DESC" : "")}");
                }
                continue;
            }
            var name = ResolveColumn(s.Column).Name;
            if (used.Add(name))
                parts.Add($"[{name}]{(s.Descending ? " DESC" : "")}");
        }
        foreach (var k in Meta.Keys)
            if (used.Add(k.Name)) parts.Add($"[{k.Name}]");
        return string.Join(",", parts);
    }

    public async Task<IReadOnlyList<object?>> GetDistinctValuesAsync(string column, PageQuery filterContext, int max,
        CancellationToken cancellationToken = default)
    {
        var top = Math.Clamp(max, 1, 5000);

        // Campo calculado: distinct de la EXPRESIÓN (sin el filtro del contexto, para no mezclar parámetros).
        var calc = filterContext.Calc?.FirstOrDefault(c => c.Nombre.Equals(column, StringComparison.OrdinalIgnoreCase));
        if (calc is not null)
        {
            var pc = new DynamicParameters();
            int n = 0;
            var expr = ScalarSqlTranslator.Translate(calc.Expr, c => $"[{ResolveColumn(c).Name}]", pc, ref n);
            var sqlc = $"SELECT DISTINCT TOP ({top}) ({expr}) AS V FROM {Meta.QualifiedTable} ORDER BY ({expr})";
            var rowsc = await _db.QueryAsync<DistinctRow>(sqlc, pc, cancellationToken: cancellationToken).ConfigureAwait(false);
            return rowsc.Select(r => r.V).ToList();
        }

        var col = ResolveColumn(column);
        var p = new DynamicParameters();
        int wn = 0;
        var where = BuildWhere(filterContext, p, ref wn);
        var sql = $"SELECT DISTINCT TOP ({top}) [{col.Name}] AS V FROM {Meta.QualifiedTable}{where} ORDER BY [{col.Name}]";
        var rows = await _db.QueryAsync<DistinctRow>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
        return rows.Select(r => r.V).ToList();
    }

    private sealed class DistinctRow { public object? V { get; set; } }

    // Side-fetch de campos calculados por página (clave simple): SELECT [pk] AS __k, (expr_i) AS [c{i}] ... WHERE [pk] IN (...).
    public async Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, object?>>> GetCalcValuesAsync(
        IReadOnlyCollection<object> keys, IReadOnlyList<CalculatedField> calc, CancellationToken cancellationToken = default)
    {
        var result = new Dictionary<string, IReadOnlyDictionary<string, object?>>();
        if (Meta.Keys.Count != 1 || calc.Count == 0) return result;

        var keyName = Meta.Key.Name;
        var distinct = keys.Where(k => k is not null).Distinct().ToList();
        Func<string, string> resolver = c => $"[{ResolveColumn(c).Name}]";   // columnas desnudas (FROM sin alias)

        foreach (var chunk in distinct.Chunk(1000))
        {
            var p = new DynamicParameters();
            int n = 0;
            var exprs = new List<string>();
            for (int i = 0; i < calc.Count; i++)
                exprs.Add($"({ScalarSqlTranslator.Translate(calc[i].Expr, resolver, p, ref n)}) AS [c{i}]");

            var keyParams = new List<string>();
            foreach (var k in chunk) { var pn = "@k" + n++; p.Add(pn, k); keyParams.Add(pn); }

            var sql = $"SELECT [{keyName}] AS __k, {string.Join(", ", exprs)} FROM {Meta.QualifiedTable} " +
                      $"WHERE [{keyName}] IN ({string.Join(",", keyParams)})";
            var rows = await _db.QueryAsync<dynamic>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
            foreach (IDictionary<string, object> r in rows.Cast<IDictionary<string, object>>())
            {
                var k = r.TryGetValue("__k", out var kv) ? kv?.ToString() : null;
                if (k is null) continue;
                var vals = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < calc.Count; i++)
                    vals[calc[i].Nombre] = r.TryGetValue("c" + i, out var v) ? v : null;
                result[k] = vals;
            }
        }
        return result;
    }

    public async Task<IReadOnlyList<GroupBucket>> GetGroupsAsync(string groupColumn, bool descending, PageQuery filterContext,
        IReadOnlyList<SummarySpec> summaries, CancellationToken cancellationToken = default)
    {
        var p = new DynamicParameters();
        int n = 0;
        var where = BuildWhere(filterContext, p, ref n);
        var groupSql = SortableSql(groupColumn, filterContext.Calc, p, ref n);   // [col] o (expr) si es calculado
        var aggs = BuildAggSelects(summaries, filterContext.Calc, p, ref n);
        var sql = $"SELECT {groupSql} AS GroupKey, COUNT(*) AS Cnt{aggs} FROM {Meta.QualifiedTable}{where} " +
                  $"GROUP BY {groupSql} ORDER BY {groupSql}{(descending ? " DESC" : string.Empty)}";

        // Columnas dinámicas (A0..An según la cantidad de sumarios) → filas dinámicas de Dapper (DBNull→null).
        var rows = await _db.QueryAsync<dynamic>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
        var list = new List<GroupBucket>();
        foreach (IDictionary<string, object> r in rows.Cast<IDictionary<string, object>>())
        {
            var sums = new object?[summaries.Count];
            for (int i = 0; i < summaries.Count; i++)
                sums[i] = r["A" + i];
            list.Add(new GroupBucket(r["GroupKey"], Convert.ToInt32(r["Cnt"]), sums));
        }
        return list;
    }

    public async Task<IReadOnlyList<object?>> GetTotalSummaryAsync(IReadOnlyList<SummarySpec> summaries, PageQuery filterContext,
        CancellationToken cancellationToken = default)
    {
        if (summaries.Count == 0) return Array.Empty<object?>();
        var p = new DynamicParameters();
        int n = 0;
        var where = BuildWhere(filterContext, p, ref n);
        var aggs = BuildAggSelects(summaries, filterContext.Calc, p, ref n).TrimStart(',', ' ');
        var sql = $"SELECT {aggs} FROM {Meta.QualifiedTable}{where}";

        var rows = await _db.QueryAsync<dynamic>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
        var res = new object?[summaries.Count];
        if (rows.Cast<IDictionary<string, object>>().FirstOrDefault() is { } first)
            for (int i = 0; i < summaries.Count; i++)
                res[i] = first["A" + i];
        return res;
    }

    // Construye ", agg AS A0, agg AS A1, ..." validando cada columna contra la metadata (sin inyección).
    // Las columnas calculadas se agregan sobre su expresión inline (SUM((expr)), etc.).
    private static string BuildAggSelects(IReadOnlyList<SummarySpec> summaries, IReadOnlyList<CalculatedField>? calc,
        DynamicParameters p, ref int n)
    {
        if (summaries.Count == 0) return string.Empty;
        var parts = new List<string>();
        for (int i = 0; i < summaries.Count; i++)
            parts.Add($"{AggSql(summaries[i], calc, p, ref n)} AS A{i}");
        return ", " + string.Join(", ", parts);
    }

    private static string AggSql(SummarySpec s, IReadOnlyList<CalculatedField>? calc, DynamicParameters p, ref int n)
    {
        if (s.Kind == AggKind.Count) return "COUNT(*)";
        var c = SortableSql(s.Column, calc, p, ref n);   // [col] o (expr) si es calculado
        return s.Kind switch
        {
            AggKind.Sum => $"SUM({c})",
            AggKind.Min => $"MIN({c})",
            AggKind.Max => $"MAX({c})",
            AggKind.Avg => $"AVG(CAST({c} AS float))",
            _ => "COUNT(*)"
        };
    }

    private static ColumnMetadata ResolveColumn(string columnName) =>
        Meta.Columns.FirstOrDefault(c =>
            c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)
            || c.Property.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"{Meta.Table} no tiene la columna '{columnName}'.");

    // Columnas sobre las que busca GetPageAsync: texto + claves (no auditoría).
    private static readonly string[] SearchColumns = Meta.Columns
        .Where(c => !c.IsAudit && (c.UnderlyingType == typeof(string) || c.IsKey))
        .Select(c => c.Name)
        .Distinct()
        .ToArray();

    public async Task<TEntity?> GetByKeyAsync(object?[] keyValues, CancellationToken cancellationToken = default)
    {
        if (keyValues.Length != Meta.Keys.Count)
            throw new ArgumentException($"{Meta.Table} tiene {Meta.Keys.Count} columna(s) clave; se recibieron {keyValues.Length}.");

        var p = new DynamicParameters();
        for (int i = 0; i < Meta.Keys.Count; i++)
            p.Add(Meta.Keys[i].Property.Name, keyValues[i]);

        var rows = await _db.QueryAsync<TEntity>(Statements.SelectByKey, p, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return rows.Count > 0 ? rows[0] : null;
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        StampAudit(entity);
        if (Meta.Keys.Count == 1 && Meta.Key.IsIdentity)
        {
            var id = await _db.ExecuteScalarAsync<object>(Statements.Insert, entity, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (id is not null && id is not DBNull)
                Meta.Key.SetValue(entity, Convert.ChangeType(id, Meta.Key.UnderlyingType));
        }
        else
        {
            await _db.ExecuteAsync(Statements.Insert, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        if (await AuditarAsync(cancellationToken).ConfigureAwait(false))
            await _audit.LogInsertAsync(Meta, entity, _user.UserName, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        StampAudit(entity);

        // Para auditar el diff: se lee el registro viejo ANTES del UPDATE (la clave no cambia al editar).
        object? old = null;
        if (await AuditarAsync(cancellationToken).ConfigureAwait(false))
            old = await GetByKeyAsync(KeyValues(entity), cancellationToken).ConfigureAwait(false);

        await _db.ExecuteAsync(Statements.Update, entity, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (old is not null)
            await _audit.LogUpdateAsync(Meta, old, entity, _user.UserName, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // Snapshot ANTES del DELETE (la entidad recibida trae los valores a registrar).
        if (await AuditarAsync(cancellationToken).ConfigureAwait(false))
            await _audit.LogDeleteAsync(Meta, entity, _user.UserName, cancellationToken).ConfigureAwait(false);

        await _db.ExecuteAsync(Statements.Delete, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    private void StampAudit(TEntity entity)
    {
        foreach (var col in Meta.AuditColumns)
        {
            object? value = col.Audit switch
            {
                SgpaAuditKind.User => _user.UserName,
                SgpaAuditKind.Timestamp => DateTime.Now,
                _ => null
            };
            if (value is not null)
                col.SetValue(entity, value);
        }
    }

    private sealed record Sql(string SelectAll, string SelectByKey, string Insert, string Update, string Delete, string OrderByKeys);

    private static Sql BuildSql(EntityMetadata m)
    {
        if (m.Keys.Count == 0)
            throw new InvalidOperationException($"{m.EntityType.Name} no tiene clave primaria; no admite CRUD genérico.");

        // SELECT con alias a nombre de propiedad (para que Dapper mapee aunque la columna difiera).
        var selectList = string.Join(",", m.Columns.Select(c => $"[{c.Name}] AS [{c.Property.Name}]"));
        var where = string.Join(" AND ", m.Keys.Select(k => $"[{k.Name}]=@{k.Property.Name}"));

        // INSERT: todas las columnas salvo la clave identity (sólo aplica a clave simple).
        var insertCols = m.Columns.Where(c => !(c.IsKey && c.IsIdentity && m.Keys.Count == 1)).ToArray();
        var insertList = string.Join(",", insertCols.Select(c => $"[{c.Name}]"));
        var insertVals = string.Join(",", insertCols.Select(c => $"@{c.Property.Name}"));
        var insert = $"INSERT INTO {m.QualifiedTable} ({insertList}) VALUES ({insertVals})";
        if (m.Keys.Count == 1 && m.Key.IsIdentity)
            insert += $"; SELECT CAST(SCOPE_IDENTITY() AS {SqlTypeFor(m.Key.UnderlyingType)})";

        // UPDATE: SET columnas no-clave; WHERE todas las claves.
        var setCols = m.Columns.Where(c => !c.IsKey).Select(c => $"[{c.Name}]=@{c.Property.Name}");
        var update = $"UPDATE {m.QualifiedTable} SET {string.Join(",", setCols)} WHERE {where}";

        return new Sql(
            SelectAll: $"SELECT {selectList} FROM {m.QualifiedTable}",
            SelectByKey: $"SELECT {selectList} FROM {m.QualifiedTable} WHERE {where}",
            Insert: insert,
            Update: update,
            Delete: $"DELETE FROM {m.QualifiedTable} WHERE {where}",
            OrderByKeys: string.Join(",", m.Keys.Select(k => $"[{k.Name}]")));
    }

    private static string SqlTypeFor(Type t) =>
        t == typeof(long) ? "bigint" : t == typeof(short) ? "smallint" : "int";
}
