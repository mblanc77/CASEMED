using Dapper;
using Sgpa.Data.Auditoria;
using Sgpa.Data.Configuracion;
using Sgpa.Data.Security;
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

    // Columnas elegibles para INSERT (sin calculadas ni la clave identity) y para el SET del UPDATE (sin clave ni
    // calculadas). Se filtran por permiso de escritura por columna en InsertSql()/UpdateSql(). Declaradas ANTES de
    // Statements: BuildSql las usa y los inicializadores estáticos corren en orden de declaración.
    private static readonly ColumnMetadata[] InsertColumns =
        Meta.Columns.Where(c => !c.Computed && !(c.IsKey && c.IsIdentity && Meta.Keys.Count == 1)).ToArray();
    private static readonly ColumnMetadata[] SetColumns =
        Meta.Columns.Where(c => !c.IsKey && !c.Computed).ToArray();
    private static readonly string KeyWhere = string.Join(" AND ", Meta.Keys.Select(k => $"[{k.Name}]=@{k.Property.Name}"));
    private static readonly string IdentityTail = (Meta.Keys.Count == 1 && Meta.Key.IsIdentity)
        ? $"; SELECT CAST(SCOPE_IDENTITY() AS {SqlTypeFor(Meta.Key.UnderlyingType)})"
        : string.Empty;

    private static readonly Sql Statements = BuildSql(Meta);

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;
    private readonly IAuditService _audit;
    private readonly ITablaConfigService _tablaConfig;
    private readonly ISecurityCriteriaCompiler _criteria;
    private readonly IReadOnlyList<IEntityChangeHandler<TEntity>> _changeHandlers;

    // Cache por instancia (scope = circuito/request) de si el usuario tiene restricciones de columna en esta tabla.
    private bool? _hasReadRestrictions;
    private bool? _hasWriteRestrictions;

    public DapperCrudService(IDbExecutor db, ICurrentUser user, IAuditService audit, ITablaConfigService tablaConfig,
        ISecurityCriteriaCompiler criteria, IEnumerable<IEntityChangeHandler<TEntity>> changeHandlers)
    {
        _db = db;
        _user = user;
        _audit = audit;
        _tablaConfig = tablaConfig;
        _criteria = criteria;
        _changeHandlers = changeHandlers as IReadOnlyList<IEntityChangeHandler<TEntity>> ?? changeHandlers.ToArray();
    }

    // ───────────────────────────── Seguridad (campo + registro) ─────────────────────────────

    private void Exigir(PermissionAction accion)
    {
        if (!_user.Can(Meta.Table, accion))
            throw PermisoDenegadoException.Para(Meta.Table, accion);
    }

    // ¿El usuario tiene alguna columna no-clave sin permiso de lectura/escritura en esta tabla? (admin → false).
    private bool HasReadRestrictions => _hasReadRestrictions ??=
        Meta.Columns.Any(c => !c.IsKey && !_user.CanReadColumn(Meta.Table, c.Name));
    private bool HasWriteRestrictions => _hasWriteRestrictions ??=
        Meta.Columns.Any(c => !c.IsKey && !_user.CanWriteColumn(Meta.Table, c.Name));

    // Lista de columnas del SELECT, enmascarando con un valor neutro las columnas sin permiso de lectura
    // (las claves nunca se enmascaran). Sin restricciones → la lista estática completa.
    private string SelectListSql()
    {
        if (!HasReadRestrictions) return Statements.SelectList;
        return string.Join(",", Meta.Columns.Select(c =>
            (c.IsKey || _user.CanReadColumn(Meta.Table, c.Name))
                ? $"[{c.Name}] AS [{c.Property.Name}]"
                : $"{MaskExpr(c)} AS [{c.Property.Name}]"));
    }

    private string SelectAllSql() => $"SELECT {SelectListSql()} FROM {Meta.QualifiedReadSource}";

    // Valor neutro para enmascarar una columna sin permiso de lectura. NULL para tipos anulables/referencia;
    // default del tipo para value-types no anulables (la propiedad de la entidad no admite NULL).
    private static string MaskExpr(ColumnMetadata c)
    {
        var esValueNoAnulable = c.ClrType.IsValueType && Nullable.GetUnderlyingType(c.ClrType) is null;
        if (!esValueNoAnulable) return "NULL";
        var u = c.UnderlyingType;
        if (u == typeof(bool)) return "CAST(0 AS bit)";
        if (ColumnMetadata.IsNumericType(u)) return "0";
        if (u == typeof(DateTime)) return "CONVERT(datetime2, '00010101')";
        if (u == typeof(DateOnly)) return "CONVERT(date, '00010101')";
        if (u == typeof(Guid)) return "CAST(0x0 AS uniqueidentifier)";
        return "NULL";
    }

    // Condición SQL del filtro de registros para la acción dada (null = sin restricción). Une los criterios del
    // usuario con OR. Si hay criterios configurados pero NINGUNO se pudo traducir, devuelve "(1=0)" (fail-safe:
    // una restricción que no se puede evaluar no debe omitirse).
    private string? RecordConditionSql(PermissionAction accion, DynamicParameters p, ref int n)
    {
        var rule = _user.RecordFilter(Meta.Table, accion);
        if (rule is null || rule.Unrestricted) return null;

        var parts = new List<string>();
        foreach (var crit in rule.Criterios)
        {
            var node = _criteria.Compile(Meta, crit, _user);
            if (node is null) continue;
            var sql = FilterSqlTranslator.Translate(node, p, ref n, Meta, Meta.QualifiedReadSource);
            if (!string.IsNullOrEmpty(sql)) parts.Add(sql);
        }
        if (rule.Criterios.Count > 0 && parts.Count == 0) return "(1=0)";
        return parts.Count == 0 ? null : "(" + string.Join(" OR ", parts) + ")";
    }

    // Agrega (si corresponde) la condición de seguridad por registro de lectura a la lista de condiciones.
    private void AddRecordReadCondition(List<string> conds, DynamicParameters p, ref int n)
    {
        var cond = RecordConditionSql(PermissionAction.Read, p, ref n);
        if (cond is not null) conds.Add(cond);
    }

    // Verifica que la fila objetivo de una escritura/borrado caiga dentro del criterio de registro del usuario.
    // Lanza PermisoDenegadoException si la fila existe pero está fuera del criterio. Sin restricción → no hace nada.
    private async Task EnsureRecordAllowedAsync(TEntity entity, PermissionAction accion, CancellationToken ct)
    {
        var p = new DynamicParameters();
        int n = 0;
        var cond = RecordConditionSql(accion, p, ref n);
        if (cond is null) return;

        var keyConds = new List<string>();
        for (int i = 0; i < Meta.Keys.Count; i++)
        {
            var pn = "@k" + i;
            p.Add(pn, Meta.Keys[i].GetValue(entity));
            keyConds.Add($"[{Meta.Keys[i].Name}]={pn}");
        }
        var keyWhere = string.Join(" AND ", keyConds);

        // 1 = la fila existe y cumple el criterio; 0 = existe y NO cumple; null = no existe (la op normal no afecta nada).
        var permitido = await _db.ExecuteScalarAsync<int?>(
            $"SELECT TOP 1 CASE WHEN {cond} THEN 1 ELSE 0 END FROM {Meta.QualifiedReadSource} WHERE {keyWhere}",
            p, cancellationToken: ct).ConfigureAwait(false);

        if (permitido == 0)
            throw PermisoDenegadoException.Para(Meta.Table, accion);
    }

    // Orquestación CUD reutilizable: notifica a los handlers registrados para esta entidad. El executor
    // <paramref name="db"/> es el de la operación (la transacción cuando hay handlers) → los handlers escriben
    // sobre la MISMA conexión/locks que el DML, evitando la carrera entre conexiones que provoca deadlocks.
    private async Task NotifyAsync(IDbExecutor db, EntityChangeKind kind, TEntity entity, TEntity? previous, CancellationToken ct)
    {
        var change = new EntityChange<TEntity>(kind, entity, previous, db);
        foreach (var handler in _changeHandlers)
            await handler.HandleAsync(change, ct).ConfigureAwait(false);
    }

    // ¿La tabla tiene la auditoría por campo habilitada? (TablaConfig, cacheado).
    private async Task<bool> AuditarAsync(CancellationToken ct)
    {
        await _tablaConfig.EnsureLoadedAsync(ct).ConfigureAwait(false);
        return _tablaConfig.Get(Meta.Table).Auditar;
    }

    private static object?[] KeyValues(TEntity entity) => Meta.Keys.Select(k => k.GetValue(entity)).ToArray();

    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var p = new DynamicParameters();
        int n = 0;
        var conds = new List<string>();
        AddRecordReadCondition(conds, p, ref n);
        var where = conds.Count > 0 ? " WHERE " + string.Join(" AND ", conds) : string.Empty;
        return _db.QueryAsync<TEntity>($"{SelectAllSql()}{where}", p, cancellationToken: cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetByColumnAsync(string columnName, object? value, CancellationToken cancellationToken = default)
    {
        var col = ResolveColumn(columnName);
        var p = new DynamicParameters();
        p.Add("p", value);
        int n = 0;
        var conds = new List<string> { $"[{col.Name}]=@p" };
        AddRecordReadCondition(conds, p, ref n);
        return _db.QueryAsync<TEntity>($"{SelectAllSql()} WHERE {string.Join(" AND ", conds)}", p,
            cancellationToken: cancellationToken);
    }

    public async Task<PagedResult<TEntity>> GetPageAsync(PageQuery query, CancellationToken cancellationToken = default)
    {
        var p = new DynamicParameters();
        int n = 0;
        var where = BuildWhere(query, p, ref n);
        var orderBy = BuildOrderBy(query.Sort, query.Calc, p, ref n);

        var total = await _db.ExecuteScalarAsync<int>(
            $"SELECT COUNT(*) FROM {Meta.QualifiedReadSource}{where}", p, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        var take = Math.Clamp(query.Take, 1, 50000); // tope alto para permitir export-all
        var skip = Math.Max(0, query.Skip);
        p.Add("__skip", skip);
        p.Add("__take", take);

        var sql = $"{SelectAllSql()}{where} ORDER BY {orderBy} " +
                  "OFFSET @__skip ROWS FETCH NEXT @__take ROWS ONLY";
        var items = await _db.QueryAsync<TEntity>(sql, p, cancellationToken: cancellationToken).ConfigureAwait(false);
        return new PagedResult<TEntity>(items, total);
    }

    // Arma el WHERE combinando: filtro fijo (FK del padre) + búsqueda libre + árbol de filtro del grid +
    // el filtro de seguridad por registro (lectura). <paramref name="n"/> numera los parámetros y se comparte
    // con el ORDER BY / GROUP BY (campos calculados).
    private string BuildWhere(PageQuery query, DynamicParameters p, ref int n)
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
            var sql = FilterSqlTranslator.Translate(query.Filter, p, ref n, Meta, Meta.QualifiedReadSource, "", CalcLookup(query.Calc));
            if (!string.IsNullOrEmpty(sql)) conds.Add(sql);
        }

        AddRecordReadCondition(conds, p, ref n);

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
            return "(" + ScalarSqlTranslator.Translate(cf.Expr, ScalarSqlTranslator.ColumnResolver(Meta, "", Meta.QualifiedReadSource), p, ref n) + ")";
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
            var expr = ScalarSqlTranslator.Translate(calc.Expr, ScalarSqlTranslator.ColumnResolver(Meta, "", Meta.QualifiedReadSource), pc, ref n);
            var secConds = new List<string>();
            AddRecordReadCondition(secConds, pc, ref n);
            var secWhere = secConds.Count > 0 ? " WHERE " + string.Join(" AND ", secConds) : string.Empty;
            var sqlc = $"SELECT DISTINCT TOP ({top}) ({expr}) AS V FROM {Meta.QualifiedReadSource}{secWhere} ORDER BY ({expr})";
            var rowsc = await _db.QueryAsync<DistinctRow>(sqlc, pc, cancellationToken: cancellationToken).ConfigureAwait(false);
            return rowsc.Select(r => r.V).ToList();
        }

        var col = ResolveColumn(column);
        var p = new DynamicParameters();
        int wn = 0;
        var where = BuildWhere(filterContext, p, ref wn);
        var sql = $"SELECT DISTINCT TOP ({top}) [{col.Name}] AS V FROM {Meta.QualifiedReadSource}{where} ORDER BY [{col.Name}]";
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
        var resolver = ScalarSqlTranslator.ColumnResolver(Meta, "", Meta.QualifiedReadSource);   // columnas desnudas + FK por subconsulta

        foreach (var chunk in distinct.Chunk(1000))
        {
            var p = new DynamicParameters();
            int n = 0;
            var exprs = new List<string>();
            for (int i = 0; i < calc.Count; i++)
                exprs.Add($"({ScalarSqlTranslator.Translate(calc[i].Expr, resolver, p, ref n)}) AS [c{i}]");

            var keyParams = new List<string>();
            foreach (var k in chunk) { var pn = "@k" + n++; p.Add(pn, k); keyParams.Add(pn); }

            var conds = new List<string> { $"[{keyName}] IN ({string.Join(",", keyParams)})" };
            AddRecordReadCondition(conds, p, ref n);

            var sql = $"SELECT [{keyName}] AS __k, {string.Join(", ", exprs)} FROM {Meta.QualifiedReadSource} " +
                      $"WHERE {string.Join(" AND ", conds)}";
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
        var sql = $"SELECT {groupSql} AS GroupKey, COUNT(*) AS Cnt{aggs} FROM {Meta.QualifiedReadSource}{where} " +
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
        var sql = $"SELECT {aggs} FROM {Meta.QualifiedReadSource}{where}";

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
        // Suma/Promedio sólo son válidos sobre columnas numéricas; sobre texto/fecha/bool SQL Server tira
        // "Operand data type ... is invalid for sum operator" y eso rompe el render de TODA la grilla. Cuando el
        // tipo no aplica, degradamos a NULL (la celda del pie queda vacía) en lugar de generar SQL inválido. Así
        // además se recuperan vistas ya guardadas por el usuario con un total inválido (no hay que limpiarlas a mano).
        var clr = ColumnClrType(s.Column, calc);
        var esNumerica = ColumnMetadata.IsNumericType(clr);
        var esBool = (Nullable.GetUnderlyingType(clr ?? typeof(object)) ?? clr) == typeof(bool);
        return s.Kind switch
        {
            AggKind.Sum => esNumerica ? $"SUM({c})" : "NULL",
            AggKind.Avg => esNumerica ? $"AVG(CAST({c} AS float))" : "NULL",
            AggKind.Min => esBool ? "NULL" : $"MIN({c})",   // MIN/MAX(bit) también es inválido en T-SQL
            AggKind.Max => esBool ? "NULL" : $"MAX({c})",
            _ => "COUNT(*)"
        };
    }

    // Tipo CLR de la columna del sumario: campo calculado (ClrType) o columna real (UnderlyingType); null si no resuelve.
    private static Type? ColumnClrType(string column, IReadOnlyList<CalculatedField>? calc)
    {
        var cf = calc?.FirstOrDefault(c => c.Nombre.Equals(column, StringComparison.OrdinalIgnoreCase));
        if (cf is not null) return cf.ClrType;
        return Meta.Columns.FirstOrDefault(c =>
            c.Name.Equals(column, StringComparison.OrdinalIgnoreCase)
            || c.Property.Name.Equals(column, StringComparison.OrdinalIgnoreCase))?.UnderlyingType;
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

        // Lectura pública: columnas enmascaradas según permisos + filtro de registros (no devuelve filas fuera de scope).
        var p = new DynamicParameters();
        var conds = new List<string>();
        for (int i = 0; i < Meta.Keys.Count; i++)
        {
            p.Add(Meta.Keys[i].Property.Name, keyValues[i]);
            conds.Add($"[{Meta.Keys[i].Name}]=@{Meta.Keys[i].Property.Name}");
        }
        int n = 0;
        AddRecordReadCondition(conds, p, ref n);

        var rows = await _db.QueryAsync<TEntity>(
            $"{SelectAllSql()} WHERE {string.Join(" AND ", conds)}", p, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return rows.Count > 0 ? rows[0] : null;
    }

    // Lectura interna por clave SIN enmascarado ni filtro de registros: para el snapshot "old" de la auditoría y
    // los handlers de cambio, que necesitan todas las columnas reales (el diff/handler no debe quedar sesgado).
    private async Task<TEntity?> ReadFullByKeyAsync(object?[] keyValues, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();
        for (int i = 0; i < Meta.Keys.Count; i++)
            p.Add(Meta.Keys[i].Property.Name, keyValues[i]);
        var rows = await _db.QueryAsync<TEntity>(Statements.SelectByKey, p, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return rows.Count > 0 ? rows[0] : null;
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Exigir(PermissionAction.Create);
        StampAudit(entity);

        if (_changeHandlers.Count == 0)
            await InsertCoreAsync(_db, entity, cancellationToken).ConfigureAwait(false);
        else
        {
            // Con handlers: INSERT + notificación en una sola transacción (atómico, misma conexión).
            await using var uow = await _db.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            await InsertCoreAsync(uow, entity, cancellationToken).ConfigureAwait(false);
            await NotifyAsync(uow, EntityChangeKind.Inserted, entity, null, cancellationToken).ConfigureAwait(false);
            await uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        if (await AuditarAsync(cancellationToken).ConfigureAwait(false))
            await _audit.LogInsertAsync(Meta, entity, _user.UserName, cancellationToken).ConfigureAwait(false);
    }

    private async Task InsertCoreAsync(IDbExecutor db, TEntity entity, CancellationToken ct)
    {
        var insertSql = InsertSql();   // excluye las columnas sin permiso de escritura (toman el default de la BD)
        if (Meta.Keys.Count == 1 && Meta.Key.IsIdentity)
        {
            var id = await db.ExecuteScalarAsync<object>(insertSql, entity, cancellationToken: ct).ConfigureAwait(false);
            if (id is not null && id is not DBNull)
                Meta.Key.SetValue(entity, Convert.ChangeType(id, Meta.Key.UnderlyingType));
        }
        else
        {
            await db.ExecuteAsync(insertSql, entity, cancellationToken: ct).ConfigureAwait(false);
        }
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Exigir(PermissionAction.Write);
        // El registro debe caer dentro del criterio de edición del usuario (permiso por registro).
        await EnsureRecordAllowedAsync(entity, PermissionAction.Write, cancellationToken).ConfigureAwait(false);
        StampAudit(entity);

        var updateSql = UpdateSql();   // excluye del SET las columnas sin permiso de escritura
        if (updateSql is null) return; // ninguna columna modificable para este usuario → no-op

        // Se lee el registro viejo ANTES del UPDATE (la clave no cambia al editar): lo necesita la auditoría
        // (diff) y los handlers de cambio (Previous), así que se trae si hay cualquiera de los dos.
        var auditar = await AuditarAsync(cancellationToken).ConfigureAwait(false);
        TEntity? old = null;
        if (auditar || _changeHandlers.Count > 0)
            old = await ReadFullByKeyAsync(KeyValues(entity), cancellationToken).ConfigureAwait(false);

        if (_changeHandlers.Count == 0)
            await _db.ExecuteAsync(updateSql, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        else
        {
            await using var uow = await _db.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            await uow.ExecuteAsync(updateSql, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
            await NotifyAsync(uow, EntityChangeKind.Updated, entity, old, cancellationToken).ConfigureAwait(false);
            await uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        if (old is not null && auditar)
            await _audit.LogUpdateAsync(Meta, old, entity, _user.UserName, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Exigir(PermissionAction.Delete);
        // El registro debe caer dentro del criterio de borrado del usuario (permiso por registro).
        await EnsureRecordAllowedAsync(entity, PermissionAction.Delete, cancellationToken).ConfigureAwait(false);

        // Snapshot ANTES del DELETE (la entidad recibida trae los valores a registrar).
        if (await AuditarAsync(cancellationToken).ConfigureAwait(false))
            await _audit.LogDeleteAsync(Meta, entity, _user.UserName, cancellationToken).ConfigureAwait(false);

        if (_changeHandlers.Count == 0)
            await _db.ExecuteAsync(Statements.Delete, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        else
        {
            // DELETE + notificación en una sola transacción: el handler relee la tabla sobre los locks que ya
            // tomó este DELETE (sin segunda conexión compitiendo) → sin deadlock y atómico (o todo o nada).
            await using var uow = await _db.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            await uow.ExecuteAsync(Statements.Delete, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
            await NotifyAsync(uow, EntityChangeKind.Deleted, entity, null, cancellationToken).ConfigureAwait(false);
            await uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task DeleteManyAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities.Count == 0) return;
        if (entities.Count == 1) { await DeleteAsync(entities[0], cancellationToken).ConfigureAwait(false); return; }

        Exigir(PermissionAction.Delete);
        // Cada fila del lote debe caer dentro del criterio de borrado del usuario (permiso por registro).
        if (_user.RecordFilter(Meta.Table, PermissionAction.Delete) is not null)
            foreach (var entity in entities)
                await EnsureRecordAllowedAsync(entity, PermissionAction.Delete, cancellationToken).ConfigureAwait(false);

        var auditar = await AuditarAsync(cancellationToken).ConfigureAwait(false);

        // Todo el lote en UNA transacción: un único commit (y un solo flush de log) en vez de uno por fila —
        // el costo dominante al borrar cientos de filas. El handler de cada fila corre dentro de la misma
        // transacción (mismos locks → sin deadlock), igual que en DeleteAsync.
        await using var uow = await _db.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        foreach (var entity in entities)
        {
            // Si hay handlers, releer la fila completa ANTES del DELETE (sobre la misma transacción): la entidad
            // recibida puede traer sólo la clave —ej. "seleccionar todo del filtro"— y el handler necesita el resto
            // (CI/Año/Mes) para sincronizar (p.ej. el imponible emp900). Sin esto, el borrado masivo por filtro
            // dejaría imponibles huérfanos.
            var notificar = _changeHandlers.Count > 0
                ? await LeerPorClaveAsync(uow, entity, cancellationToken).ConfigureAwait(false) ?? entity
                : entity;
            await uow.ExecuteAsync(Statements.Delete, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (_changeHandlers.Count > 0)
                await NotifyAsync(uow, EntityChangeKind.Deleted, notificar, null, cancellationToken).ConfigureAwait(false);
        }
        await uow.CommitAsync(cancellationToken).ConfigureAwait(false);

        // Auditoría best-effort tras el commit (la entidad ya trae los valores a registrar).
        if (auditar)
            foreach (var entity in entities)
                await _audit.LogDeleteAsync(Meta, entity, _user.UserName, cancellationToken).ConfigureAwait(false);
    }

    // SELECT de columnas REALES (no computadas) desde la tabla base por clave: para el snapshot que se pasa al
    // handler antes del DELETE. No usa la vista de lectura (más liviano y sin depender de sus JOINs).
    private static readonly string SelectBaseByKey = BuildSelectBaseByKey(Meta);

    private static string BuildSelectBaseByKey(EntityMetadata m)
    {
        var cols = string.Join(",", m.Columns.Where(c => !c.Computed).Select(c => $"[{c.Name}] AS [{c.Property.Name}]"));
        var where = string.Join(" AND ", m.Keys.Select(k => $"[{k.Name}]=@{k.Property.Name}"));
        return $"SELECT {cols} FROM {m.QualifiedTable} WHERE {where}";
    }

    // Lee la fila por clave sobre el executor dado (la transacción del borrado), para que el handler reciba la
    // entidad completa aunque al método sólo le hayan pasado la clave. Null si ya no existe.
    private async Task<TEntity?> LeerPorClaveAsync(IDbExecutor db, TEntity keySource, CancellationToken ct)
    {
        var p = new DynamicParameters();
        foreach (var k in Meta.Keys)
            p.Add(k.Property.Name, k.GetValue(keySource));
        var rows = await db.QueryAsync<TEntity>(SelectBaseByKey, p, cancellationToken: ct).ConfigureAwait(false);
        return rows.Count > 0 ? rows[0] : null;
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

    // INSERT excluyendo las columnas sin permiso de escritura (las claves siempre se incluyen). Sin restricciones
    // → la sentencia estática. Las columnas omitidas toman su DEFAULT/NULL en la BD.
    private string InsertSql()
    {
        if (!HasWriteRestrictions) return Statements.Insert;
        var cols = InsertColumns.Where(c => c.IsKey || _user.CanWriteColumn(Meta.Table, c.Name)).ToArray();
        var list = string.Join(",", cols.Select(c => $"[{c.Name}]"));
        var vals = string.Join(",", cols.Select(c => $"@{c.Property.Name}"));
        return $"INSERT INTO {Meta.QualifiedTable} ({list}) VALUES ({vals}){IdentityTail}";
    }

    // UPDATE excluyendo del SET las columnas sin permiso de escritura. null = ninguna columna modificable (no-op).
    private string? UpdateSql()
    {
        if (!HasWriteRestrictions) return Statements.Update;
        var cols = SetColumns.Where(c => _user.CanWriteColumn(Meta.Table, c.Name)).ToArray();
        if (cols.Length == 0) return null;
        var set = string.Join(",", cols.Select(c => $"[{c.Name}]=@{c.Property.Name}"));
        return $"UPDATE {Meta.QualifiedTable} SET {set} WHERE {KeyWhere}";
    }

    private sealed record Sql(string SelectList, string SelectAll, string SelectByKey, string Insert, string Update, string Delete, string OrderByKeys);

    private static Sql BuildSql(EntityMetadata m)
    {
        if (m.Keys.Count == 0)
            throw new InvalidOperationException($"{m.EntityType.Name} no tiene clave primaria; no admite CRUD genérico.");

        // SELECT con alias a nombre de propiedad (para que Dapper mapee aunque la columna difiera).
        var selectList = string.Join(",", m.Columns.Select(c => $"[{c.Name}] AS [{c.Property.Name}]"));
        var where = string.Join(" AND ", m.Keys.Select(k => $"[{k.Name}]=@{k.Property.Name}"));

        var insertList = string.Join(",", InsertColumns.Select(c => $"[{c.Name}]"));
        var insertVals = string.Join(",", InsertColumns.Select(c => $"@{c.Property.Name}"));
        var insert = $"INSERT INTO {m.QualifiedTable} ({insertList}) VALUES ({insertVals}){IdentityTail}";

        // UPDATE: SET columnas no-clave y no-calculadas; WHERE todas las claves.
        var setCols = SetColumns.Select(c => $"[{c.Name}]=@{c.Property.Name}");
        var update = $"UPDATE {m.QualifiedTable} SET {string.Join(",", setCols)} WHERE {where}";

        // Lectura desde la vista (si la entidad declara [SgpaReadSource]); escritura siempre contra la tabla.
        return new Sql(
            SelectList: selectList,
            SelectAll: $"SELECT {selectList} FROM {m.QualifiedReadSource}",
            SelectByKey: $"SELECT {selectList} FROM {m.QualifiedReadSource} WHERE {where}",
            Insert: insert,
            Update: update,
            Delete: $"DELETE FROM {m.QualifiedTable} WHERE {where}",
            OrderByKeys: string.Join(",", m.Keys.Select(k => $"[{k.Name}]")));
    }

    private static string SqlTypeFor(Type t) =>
        t == typeof(long) ? "bigint" : t == typeof(short) ? "smallint" : "int";
}
