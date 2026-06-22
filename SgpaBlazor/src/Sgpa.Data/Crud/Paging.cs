namespace Sgpa.Data.Crud;

/// <summary>Pedido de una página de datos para una grilla server-side.</summary>
/// <param name="Skip">Filas a saltar (offset).</param>
/// <param name="Take">Tamaño de página.</param>
/// <param name="Search">Texto a buscar (LIKE sobre columnas string + claves). Null/vacío = sin filtro.</param>
/// <param name="FilterColumn">Columna de filtro fijo (ej. FK del padre en un master-detail). Null = sin filtro.</param>
/// <param name="FilterValue">Valor del filtro fijo.</param>
public sealed record PageQuery(
    int Skip,
    int Take,
    string? Search = null,
    string? FilterColumn = null,
    object? FilterValue = null,
    IReadOnlyList<SortColumn>? Sort = null,
    FilterNode? Filter = null)
{
    /// <summary>
    /// Campos calculados de la tabla (de <c>ICalculatedFieldCatalog</c>, ya en forma neutral). Si el filtro
    /// referencia alguno por nombre, <see cref="FilterSqlTranslator"/> lo inserta inline en el WHERE.
    /// </summary>
    public IReadOnlyList<CalculatedField>? Calc { get; init; }
}

/// <summary>Una página de resultados más el total de filas que cumplen el filtro.</summary>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
