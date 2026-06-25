using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Crud;

/// <summary>Orden por una columna (nombre físico o de propiedad).</summary>
public sealed record SortColumn(string Column, bool Descending);

/// <summary>Función agregada de un sumario (pie de grilla / por grupo).</summary>
public enum AggKind { Count, Sum, Min, Max, Avg }

/// <summary>Pedido de un sumario: la columna y la función agregada. (Count ignora la columna.)</summary>
public sealed record SummarySpec(string Column, AggKind Kind);

/// <summary>Un grupo resuelto server-side: su valor de clave, el conteo de filas y los sumarios por grupo.</summary>
public sealed record GroupBucket(object? Value, int Count, IReadOnlyList<object?> Summaries);

public enum FilterOp { Equal, NotEqual, Greater, Less, GreaterOrEqual, LessOrEqual, Like }

public enum FilterFunc { Contains, StartsWith, EndsWith }

/// <summary>
/// Árbol de filtro neutral (sin dependencia de DevExpress). La capa Web traduce el
/// <c>CriteriaOperator</c> del grid a este árbol; <see cref="DapperCrudService{TEntity}"/> lo
/// traduce a SQL parametrizado validando los nombres de columna contra la metadata.
/// </summary>
public abstract record FilterNode;

/// <summary>Comparación binaria columna ↔ valor.</summary>
public sealed record FilterCompare(string Column, FilterOp Op, object? Value) : FilterNode;

/// <summary>Comparación columna ↔ otra columna de la MISMA tabla (ej. <c>[FechaBaja] &gt;= [FechaAlta]</c>).</summary>
public sealed record FilterCompareColumns(string Column, FilterOp Op, string OtherColumn) : FilterNode;

/// <summary>Función de texto (Contains/StartsWith/EndsWith) sobre una columna.</summary>
public sealed record FilterText(string Column, FilterFunc Func, string Value) : FilterNode;

/// <summary>IS NULL / IS NOT NULL.</summary>
public sealed record FilterNull(string Column, bool IsNull) : FilterNode;

/// <summary>columna IN (valores).</summary>
public sealed record FilterIn(string Column, IReadOnlyList<object?> Values) : FilterNode;

/// <summary>Combinación lógica (AND si <paramref name="And"/> es true, si no OR).</summary>
public sealed record FilterGroup(bool And, IReadOnlyList<FilterNode> Nodes) : FilterNode;

/// <summary>Negación.</summary>
public sealed record FilterNot(FilterNode Inner) : FilterNode;

/// <summary>
/// EXISTS sobre una tabla hija 1-N (filtro tipo "el afiliado tiene algún empleo/certificación/... que cumple X").
/// Genera: [NOT] EXISTS (SELECT 1 FROM &lt;Child&gt; AS x WHERE x.[ChildFkColumn] = &lt;tablaPadre&gt;.[ParentKeyColumn]
/// [AND &lt;Inner&gt; con columnas de la hija]). <paramref name="Child"/> aporta tabla y metadata para validar/quotear
/// las columnas hijas (anti-inyección). <paramref name="Inner"/> es la subcondición sobre la hija (puede ser null).
/// </summary>
public sealed record FilterExists(
    EntityMetadata Child, string ChildFkColumn, string ParentKeyColumn, FilterNode? Inner, bool Negate = false) : FilterNode;

/// <summary>
/// Comparación contra un agregado de una tabla hija ("Certificaciones.Count() &gt; 1", "Imponibles.Sum(Importe) &gt;= X").
/// Genera: (SELECT &lt;agg&gt; FROM &lt;Child&gt; AS x WHERE x.[ChildFkColumn] = &lt;padre&gt;.[ParentKeyColumn] [AND &lt;Inner&gt;]) &lt;Op&gt; &lt;Value&gt;.
/// <paramref name="AggColumn"/> es la columna agregada (null para Count = COUNT(*)). <paramref name="Inner"/> es la
/// condición opcional sobre la hija (ej. contar sólo certificaciones efectivas).
/// </summary>
public sealed record FilterAggregate(
    EntityMetadata Child, string ChildFkColumn, string ParentKeyColumn,
    AggKind Kind, string? AggColumn, FilterNode? Inner, FilterOp Op, object? Value) : FilterNode;
