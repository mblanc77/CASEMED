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
