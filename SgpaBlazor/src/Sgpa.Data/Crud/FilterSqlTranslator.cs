using Dapper;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Crud;

/// <summary>
/// Traduce el árbol de filtro neutral (<see cref="FilterNode"/>) a SQL parametrizado. Los nombres de columna se
/// validan SIEMPRE contra la metadata (anti-inyección por el FieldName del grid). Las relaciones se resuelven como
/// subconsultas correlacionadas: <see cref="FilterExists"/> → <c>EXISTS</c>, <see cref="FilterAggregate"/> → agregado.
/// <para>Se parametriza por la entidad raíz (<paramref name="rootMeta"/>) y por cómo referenciarla en la correlación
/// de un EXISTS de primer nivel (<paramref name="rootRef"/>) y el prefijo de sus columnas (<paramref name="rootPrefix"/>):
/// el CRUD genérico usa la tabla calificada sin alias (<c>[dbo].[Afiliado]</c>, prefijo vacío → <c>[col]</c>); el
/// builder de reportes dinámicos usa un alias (<c>t0</c>, prefijo <c>t0.</c> → <c>t0.[col]</c>).</para>
/// <para><paramref name="calcLookup"/> (opcional) permite filtrar por <b>campos calculados</b>: si el nombre del
/// operando izquierdo es un calculado, se reemplaza por su expresión escalar inline (vía <see cref="ScalarSqlTranslator"/>),
/// porque SQL no admite el alias del SELECT en el WHERE. El CRUD lo pasa null (sin cambio de comportamiento).</para>
/// </summary>
public static class FilterSqlTranslator
{
    // Ámbito de resolución de columnas: cómo validar el nombre (contra la metadata que corresponda) y con qué
    // prefijo (alias) renderizarlo. Externo = entidad raíz; dentro de un EXISTS = hija con alias.
    private readonly record struct ColScope(Func<string, ColumnMetadata> Resolve, string Prefix);

    /// <summary>Traduce <paramref name="node"/> a una condición SQL; <paramref name="n"/> numera los parámetros.</summary>
    public static string Translate(FilterNode node, DynamicParameters p, ref int n,
        EntityMetadata rootMeta, string rootRef, string rootPrefix = "", Func<string, ScalarNode?>? calcLookup = null)
    {
        var outer = new ColScope(c => ResolveColumnIn(rootMeta, c), rootPrefix);
        return Translate(node, p, ref n, outer, rootMeta, rootRef, calcLookup);
    }

    public static ColumnMetadata ResolveColumnIn(EntityMetadata meta, string columnName) =>
        meta.Columns.FirstOrDefault(c =>
            c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)
            || c.Property.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"{meta.Table} no tiene la columna '{columnName}'.");

    // Resuelve el operando IZQUIERDO de una hoja: un campo calculado (expresión inline) o una columna (prefijo[col]).
    private static string ResolveLhs(string column, ColScope scope, EntityMetadata rootMeta, string rootRef,
        Func<string, ScalarNode?>? calc, DynamicParameters p, ref int n)
    {
        if (calc?.Invoke(column) is { } node)
            return ScalarSqlTranslator.Translate(node, ScalarSqlTranslator.ColumnResolver(rootMeta, scope.Prefix, rootRef), p, ref n);
        return $"{scope.Prefix}[{scope.Resolve(column).Name}]";
    }

    private static string Translate(FilterNode node, DynamicParameters p, ref int n, ColScope scope,
        EntityMetadata rootMeta, string rootRef, Func<string, ScalarNode?>? calc)
    {
        switch (node)
        {
            case FilterGroup g:
            {
                if (g.Nodes.Count == 0) return string.Empty;
                var parts = new List<string>();
                foreach (var child in g.Nodes)
                {
                    var s = Translate(child, p, ref n, scope, rootMeta, rootRef, calc);
                    if (!string.IsNullOrEmpty(s)) parts.Add(s);
                }
                if (parts.Count == 0) return string.Empty;
                return "(" + string.Join(g.And ? " AND " : " OR ", parts) + ")";
            }
            case FilterNot not:
            {
                var s = Translate(not.Inner, p, ref n, scope, rootMeta, rootRef, calc);
                return string.IsNullOrEmpty(s) ? string.Empty : $"NOT ({s})";
            }
            case FilterNull fn:
            {
                var lhs = ResolveLhs(fn.Column, scope, rootMeta, rootRef, calc, p, ref n);
                return $"{lhs} IS {(fn.IsNull ? "NULL" : "NOT NULL")}";
            }
            case FilterIn fin:
            {
                var lhs = ResolveLhs(fin.Column, scope, rootMeta, rootRef, calc, p, ref n);
                if (fin.Values.Count == 0) return "1=0";
                var names = new List<string>();
                foreach (var v in fin.Values)
                {
                    var pn = "@f" + n++;
                    p.Add(pn, v);
                    names.Add(pn);
                }
                return $"{lhs} IN ({string.Join(",", names)})";
            }
            case FilterText ft:
            {
                var lhs = ResolveLhs(ft.Column, scope, rootMeta, rootRef, calc, p, ref n);
                var pn = "@f" + n++;
                var pattern = ft.Func switch
                {
                    FilterFunc.StartsWith => ft.Value + "%",
                    FilterFunc.EndsWith => "%" + ft.Value,
                    _ => "%" + ft.Value + "%"
                };
                p.Add(pn, pattern);
                return $"CAST({lhs} AS nvarchar(4000)) LIKE {pn}";
            }
            case FilterCompare fc:
            {
                var lhs = ResolveLhs(fc.Column, scope, rootMeta, rootRef, calc, p, ref n);
                if (fc.Value is null)
                    return fc.Op == FilterOp.NotEqual ? $"{lhs} IS NOT NULL" : $"{lhs} IS NULL";
                var pn = "@f" + n++;
                p.Add(pn, fc.Value);
                var op = fc.Op switch
                {
                    FilterOp.Equal => "=",
                    FilterOp.NotEqual => "<>",
                    FilterOp.Greater => ">",
                    FilterOp.Less => "<",
                    FilterOp.GreaterOrEqual => ">=",
                    FilterOp.LessOrEqual => "<=",
                    FilterOp.Like => "LIKE",
                    _ => "="
                };
                return $"{lhs} {op} {pn}";
            }
            case FilterExists fe:
            {
                var alias = "ex" + n++;
                var fk = ResolveColumnIn(fe.Child, fe.ChildFkColumn).Name;          // FK en la hija
                var pk = scope.Resolve(fe.ParentKeyColumn).Name;                    // clave en el padre (scope actual)
                // Correlación: hija.fk = padre.pk. El padre se referencia por su tabla/alias raíz (scope externo) o por
                // el alias del scope si este EXISTS está anidado dentro de otro.
                var parentRef = scope.Prefix.Length == 0 ? $"{rootRef}.[{pk}]" : $"{scope.Prefix}[{pk}]";
                var childScope = new ColScope(c => ResolveColumnIn(fe.Child, c), alias + ".");
                // En la subcondición de la hija no aplican los calculados de la tabla raíz.
                var inner = fe.Inner is null ? string.Empty : Translate(fe.Inner, p, ref n, childScope, rootMeta, rootRef, null);
                var corr = $"{alias}.[{fk}] = {parentRef}";
                var body = string.IsNullOrEmpty(inner) ? corr : $"{corr} AND {inner}";
                return $"{(fe.Negate ? "NOT " : "")}EXISTS (SELECT 1 FROM {fe.Child.QualifiedTable} AS {alias} WHERE {body})";
            }
            case FilterAggregate fa:
            {
                var alias = "ag" + n++;
                var fkc = ResolveColumnIn(fa.Child, fa.ChildFkColumn).Name;
                var pk = scope.Resolve(fa.ParentKeyColumn).Name;
                var parentRef = scope.Prefix.Length == 0 ? $"{rootRef}.[{pk}]" : $"{scope.Prefix}[{pk}]";
                var childScope = new ColScope(c => ResolveColumnIn(fa.Child, c), alias + ".");
                var inner = fa.Inner is null ? string.Empty : Translate(fa.Inner, p, ref n, childScope, rootMeta, rootRef, null);
                var corr = $"{alias}.[{fkc}] = {parentRef}";
                var whereSub = string.IsNullOrEmpty(inner) ? corr : $"{corr} AND {inner}";
                var aggExpr = fa.Kind switch
                {
                    AggKind.Count => "COUNT(*)",
                    AggKind.Sum => $"SUM({alias}.[{ResolveColumnIn(fa.Child, fa.AggColumn!).Name}])",
                    AggKind.Min => $"MIN({alias}.[{ResolveColumnIn(fa.Child, fa.AggColumn!).Name}])",
                    AggKind.Max => $"MAX({alias}.[{ResolveColumnIn(fa.Child, fa.AggColumn!).Name}])",
                    AggKind.Avg => $"AVG({alias}.[{ResolveColumnIn(fa.Child, fa.AggColumn!).Name}])",
                    _ => "COUNT(*)"
                };
                var pn = "@f" + n++;
                p.Add(pn, fa.Value);
                var op = fa.Op switch
                {
                    FilterOp.Equal => "=",
                    FilterOp.NotEqual => "<>",
                    FilterOp.Greater => ">",
                    FilterOp.Less => "<",
                    FilterOp.GreaterOrEqual => ">=",
                    FilterOp.LessOrEqual => "<=",
                    _ => "="
                };
                var aggSql = $"(SELECT {aggExpr} FROM {fa.Child.QualifiedTable} AS {alias} WHERE {whereSub})";
                if (fa.Kind == AggKind.Count) aggSql = $"ISNULL({aggSql}, 0)";   // sin filas → Count = 0
                return $"{aggSql} {op} {pn}";
            }
            default:
                throw new NotSupportedException($"Nodo de filtro no soportado: {node.GetType().Name}");
        }
    }
}
