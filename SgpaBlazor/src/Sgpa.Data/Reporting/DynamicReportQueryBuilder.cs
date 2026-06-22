using System.Text;
using Dapper;
using Sgpa.Data.Crud;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Reporting;

/// <summary>Una columna del resultado del reporte (para configurar las columnas dinámicas de la grilla).</summary>
public sealed record ReportColumn(string FieldName, string Caption, Type ClrType, string? DisplayFormat);

/// <summary>Consulta resuelta de un reporte dinámico: SQL + parámetros + descripción de columnas.</summary>
public sealed record DynamicReportQuery(string Sql, DynamicParameters Parameters, IReadOnlyList<ReportColumn> Columns);

/// <summary>
/// Construye la consulta SQL de un <see cref="ReporteDinamicoDef"/>: <c>SELECT</c> de las columnas elegidas con
/// <c>LEFT JOIN</c> encadenados que recorren las relaciones <b>N-1</b> ("hacia arriba", N niveles) resueltas por
/// convención con <see cref="EntityCatalog.LookupDisplayTargetFor"/>. El <c>WHERE</c> (ya traducido a
/// <see cref="FilterNode"/> y con los parámetros sustituidos por la capa Web) se delega en
/// <see cref="FilterSqlTranslator"/> (mismo motor que el CRUD; relaciones del filtro vía EXISTS correlacionado a la
/// raíz aliaseada <c>t0</c>). Todos los nombres de columna/tabla se validan contra la metadata (anti-inyección).
/// </summary>
public static class DynamicReportQueryBuilder
{
    private const string RootAlias = "t0";
    public const int DefaultMaxRows = 50000;

    /// <summary>
    /// Construye la consulta. <paramref name="filter"/> es el árbol de filtro ya resuelto (o null). <paramref name="rootCalc"/>
    /// son los campos calculados de la tabla raíz (de <c>ICalculatedFieldCatalog</c>): se emiten inline en el SELECT y,
    /// si el filtro los referencia, también inline en el WHERE (SQL no admite el alias del SELECT en el WHERE).
    /// </summary>
    public static DynamicReportQuery Build(ReporteDinamicoDef def, FilterNode? filter, int maxRows = DefaultMaxRows,
        IReadOnlyList<CalculatedField>? rootCalc = null)
    {
        var root = EntityCatalog.TryGet(def.RootTable)
            ?? throw new ArgumentException($"La tabla raíz '{def.RootTable}' no existe en el catálogo.");
        if (def.Columns.Count == 0)
            throw new ArgumentException("El reporte no tiene columnas seleccionadas.");

        var calcByName = (rootCalc ?? Array.Empty<CalculatedField>())
            .ToDictionary(c => c.Nombre, StringComparer.OrdinalIgnoreCase);
        // Resolver de columnas para las expresiones de los calculados (siempre sobre la raíz aliaseada t0).
        // Resolver de columnas para los calculados: t0.[col] simple, o subconsulta correlacionada para FK (Tabla.Col).
        var rootColResolver = ScalarSqlTranslator.ColumnResolver(root, RootAlias + ".", RootAlias);

        // alias de JOIN por path acumulado (clave canónica) → (alias, entidad destino). El primer registro es la raíz.
        var nodes = new Dictionary<string, (string Alias, EntityMetadata Meta)>(StringComparer.OrdinalIgnoreCase)
        {
            [string.Empty] = (RootAlias, root),
        };
        var joins = new StringBuilder();

        var selectParts = new List<string>();
        var columns = new List<ReportColumn>();
        var usedFieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var p = new DynamicParameters();
        int n = 0;   // contador de parámetros compartido entre SELECT (calculados) y WHERE

        foreach (var fieldRef in def.Columns)
        {
            var fieldName = UniqueFieldName(fieldRef, usedFieldNames);
            if (fieldRef.Calc)
            {
                if (fieldRef.Path.Length != 0)
                    throw new ArgumentException("Los campos calculados sólo se soportan sobre la tabla raíz (v1).");
                if (!calcByName.TryGetValue(fieldRef.Column, out var cf))
                    throw new ArgumentException($"El campo calculado '{fieldRef.Column}' no existe en {root.Table}.");
                var exprSql = ScalarSqlTranslator.Translate(cf.Expr, rootColResolver, p, ref n);
                selectParts.Add($"({exprSql}) AS [{fieldName}]");
                columns.Add(new ReportColumn(fieldName, fieldRef.Caption ?? cf.Caption, cf.ClrType, fieldRef.DisplayFormat ?? cf.DisplayFormat));
                continue;
            }
            var (alias, meta) = ResolvePath(fieldRef.Path, root, nodes, joins);
            var col = FilterSqlTranslator.ResolveColumnIn(meta, fieldRef.Column);
            selectParts.Add($"{alias}.[{col.Name}] AS [{fieldName}]");
            var caption = fieldRef.Caption
                ?? (fieldRef.Path.Length == 0 ? col.Caption : $"{meta.Table}.{col.Caption}");
            columns.Add(new ReportColumn(fieldName, caption, col.UnderlyingType, fieldRef.DisplayFormat ?? col.DisplayFormat));
        }

        var where = string.Empty;
        if (filter is not null)
        {
            Func<string, ScalarNode?> calcLookup = name => calcByName.TryGetValue(name, out var cf) ? cf.Expr : null;
            var sql = FilterSqlTranslator.Translate(filter, p, ref n, root, RootAlias, RootAlias + ".", calcLookup);
            if (!string.IsNullOrEmpty(sql)) where = " WHERE " + sql;
        }

        // ORDER BY por los campos de agrupamiento (estabiliza el agrupado de la grilla). Reusa el FieldName de salida.
        var orderBy = BuildOrderBy(def, columns);

        var top = Math.Clamp(maxRows, 1, DefaultMaxRows);
        var sqlText = $"SELECT TOP ({top}) {string.Join(", ", selectParts)} " +
                      $"FROM {root.QualifiedTable} AS {RootAlias}{joins}{where}{orderBy}";
        return new DynamicReportQuery(sqlText, p, columns);
    }

    /// <summary>
    /// Tablas involucradas (raíz + cada destino N-1 de columnas y del filtro + tablas referenciadas por los campos
    /// calculados usados), para el control de permisos. <paramref name="rootCalc"/> son los calculados de la raíz.
    /// </summary>
    public static IReadOnlySet<string> InvolvedTables(ReporteDinamicoDef def, FilterNode? filter,
        IReadOnlyList<CalculatedField>? rootCalc = null)
    {
        var tables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var root = EntityCatalog.TryGet(def.RootTable);
        if (root is null) return tables;
        tables.Add(root.Table);

        foreach (var fieldRef in def.Columns)
        {
            var current = root;
            foreach (var fk in fieldRef.Path)
            {
                var col = current.Columns.FirstOrDefault(c =>
                    c.Name.Equals(fk, StringComparison.OrdinalIgnoreCase) || c.Property.Name.Equals(fk, StringComparison.OrdinalIgnoreCase));
                var target = col is null ? null : EntityCatalog.LookupDisplayTargetFor(col, current);
                if (target is null) break;
                tables.Add(target.Table);
                current = target;
            }
        }

        if (filter is not null) CollectFilterTables(filter, tables);
        CollectCalcFkTargets(rootCalc, def, filter, root, tables);
        return tables;
    }

    // Suma las tablas N-1 que referencian los campos calculados USADOS (como columna o en el filtro), p. ej. un
    // calculado [Mutualista.Descrip] agrega Mutualista a las tablas a chequear por permiso.
    private static void CollectCalcFkTargets(IReadOnlyList<CalculatedField>? rootCalc, ReporteDinamicoDef def,
        FilterNode? filter, EntityMetadata root, HashSet<string> tables)
    {
        if (rootCalc is not { Count: > 0 }) return;
        var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var col in def.Columns) if (col.Calc) used.Add(col.Column);
        if (filter is not null) CollectFilterColumns(filter, used);

        foreach (var cf in rootCalc)
        {
            if (!used.Contains(cf.Nombre)) continue;
            foreach (var dotted in DottedColumns(cf.Expr))
            {
                var prefix = dotted[..dotted.IndexOf('.')];
                var target = root.Columns
                    .Select(fk => EntityCatalog.LookupDisplayTargetFor(fk, root))
                    .FirstOrDefault(t => t is not null && t.Table.Equals(prefix, StringComparison.OrdinalIgnoreCase));
                if (target is not null) tables.Add(target.Table);
            }
        }
    }

    // Nombres de columna referenciados en las hojas de nivel raíz del filtro (para detectar uso de calculados).
    private static void CollectFilterColumns(FilterNode node, HashSet<string> cols)
    {
        switch (node)
        {
            case FilterGroup g: foreach (var c in g.Nodes) CollectFilterColumns(c, cols); break;
            case FilterNot not: CollectFilterColumns(not.Inner, cols); break;
            case FilterCompare fc: cols.Add(fc.Column); break;
            case FilterText ft: cols.Add(ft.Column); break;
            case FilterNull fn: cols.Add(fn.Column); break;
            case FilterIn fin: cols.Add(fin.Column); break;
            // FilterExists/FilterAggregate: columnas de la hija, no de la raíz → no aplican a calculados.
        }
    }

    private static IEnumerable<string> DottedColumns(ScalarNode node) => node switch
    {
        ScalarColumn c when c.Name.Contains('.') => new[] { c.Name },
        ScalarColumn => Array.Empty<string>(),
        ScalarConst => Array.Empty<string>(),
        ScalarNegate u => DottedColumns(u.Operand),
        ScalarBinary b => DottedColumns(b.Left).Concat(DottedColumns(b.Right)),
        ScalarCondition cn => DottedColumns(cn.Left).Concat(DottedColumns(cn.Right)),
        ScalarFunc f => f.Args.SelectMany(DottedColumns),
        _ => Array.Empty<string>()
    };

    private static void CollectFilterTables(FilterNode node, HashSet<string> tables)
    {
        switch (node)
        {
            case FilterGroup g: foreach (var c in g.Nodes) CollectFilterTables(c, tables); break;
            case FilterNot not: CollectFilterTables(not.Inner, tables); break;
            case FilterExists fe:
                tables.Add(fe.Child.Table);
                if (fe.Inner is not null) CollectFilterTables(fe.Inner, tables);
                break;
            case FilterAggregate fa:
                tables.Add(fa.Child.Table);
                if (fa.Inner is not null) CollectFilterTables(fa.Inner, tables);
                break;
        }
    }

    // Recorre el path FK desde la raíz, registrando los LEFT JOIN necesarios (deduplicados por path acumulado).
    private static (string Alias, EntityMetadata Meta) ResolvePath(
        string[] path, EntityMetadata root,
        Dictionary<string, (string Alias, EntityMetadata Meta)> nodes, StringBuilder joins)
    {
        var current = (Alias: RootAlias, Meta: root);
        var cumulative = new List<string>();
        foreach (var fk in path)
        {
            cumulative.Add(fk);
            var key = string.Join("/", cumulative);
            if (nodes.TryGetValue(key, out var existing)) { current = existing; continue; }

            var col = current.Meta.Columns.FirstOrDefault(c =>
                c.Name.Equals(fk, StringComparison.OrdinalIgnoreCase) || c.Property.Name.Equals(fk, StringComparison.OrdinalIgnoreCase))
                ?? throw new ArgumentException($"{current.Meta.Table} no tiene la columna FK '{fk}'.");
            var target = EntityCatalog.LookupDisplayTargetFor(col, current.Meta)
                ?? throw new ArgumentException($"La columna '{current.Meta.Table}.{fk}' no es una FK navegable.");

            var alias = "j" + nodes.Count;   // determinista por orden de aparición del path
            joins.Append($" LEFT JOIN {target.QualifiedTable} AS {alias} ON {current.Alias}.[{col.Name}] = {alias}.[{target.Key.Name}]");
            current = (alias, target);
            nodes[key] = current;
        }
        return current;
    }

    private static string BuildOrderBy(ReporteDinamicoDef def, List<ReportColumn> columns)
    {
        if (def.Groups.Count == 0) return string.Empty;
        var parts = new List<string>();
        foreach (var g in def.Groups)
        {
            // Empareja el campo de grupo con la columna de salida ya seleccionada (mismo path+columna).
            var fieldName = DefaultFieldName(g);
            var match = columns.FirstOrDefault(c => c.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            if (match is not null) parts.Add($"[{match.FieldName}]");
        }
        return parts.Count == 0 ? string.Empty : " ORDER BY " + string.Join(", ", parts);
    }

    private static string UniqueFieldName(ReportFieldRef fieldRef, HashSet<string> used)
    {
        var baseName = DefaultFieldName(fieldRef);
        var name = baseName;
        var k = 1;
        while (!used.Add(name)) name = baseName + "_" + (++k);
        return name;
    }

    // FieldName canónico de un campo: path (columnas FK) + columna, saneado a identificador seguro para la grilla.
    private static string DefaultFieldName(ReportFieldRef fieldRef)
    {
        var raw = fieldRef.Path.Length == 0 ? fieldRef.Column : string.Join("_", fieldRef.Path) + "_" + fieldRef.Column;
        var sb = new StringBuilder(raw.Length);
        foreach (var ch in raw) sb.Append(char.IsLetterOrDigit(ch) || ch == '_' ? ch : '_');
        return sb.Length == 0 ? "Col" : sb.ToString();
    }
}
