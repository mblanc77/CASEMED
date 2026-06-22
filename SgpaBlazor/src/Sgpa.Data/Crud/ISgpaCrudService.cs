namespace Sgpa.Data.Crud;

/// <summary>
/// Operaciones CRUD genéricas sobre una entidad de NewSgpa2. Implementación
/// por defecto: <see cref="DapperCrudService{TEntity}"/> (SQL derivado de la metadata).
/// Una entidad puede proveer su propia implementación cuando la lógica lo requiera.
/// </summary>
public interface ISgpaCrudService<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Filas cuyo valor en <paramref name="columnName"/> (nombre físico o de propiedad) es igual a
    /// <paramref name="value"/>. Para grillas hijas de un master-detail (ej. Trabaja por CI).
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetByColumnAsync(string columnName, object? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Una página de datos (con total) para grillas server-side. Aplica búsqueda y filtro fijo,
    /// ordena por la clave primaria y usa OFFSET/FETCH. Para tablas grandes (Afiliado, Certificacion,
    /// SubsidioCabezal) que no conviene materializar enteras en el cliente.
    /// </summary>
    Task<PagedResult<TEntity>> GetPageAsync(PageQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Valores distintos (hasta <paramref name="max"/>) de una columna, respetando el filtro de
    /// <paramref name="filterContext"/> (FilterColumn/Filter). Para el menú de filtro del grid server-side.
    /// </summary>
    Task<IReadOnlyList<object?>> GetDistinctValuesAsync(string column, PageQuery filterContext, int max,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Grupos (valores distintos de <paramref name="groupColumn"/>) con su conteo y los sumarios por grupo,
    /// respetando el filtro de <paramref name="filterContext"/>. Es el agrupamiento server-side del grid
    /// (traducido a GROUP BY). Default: sin soporte (lista vacía) para implementaciones que no lo provean.
    /// </summary>
    Task<IReadOnlyList<GroupBucket>> GetGroupsAsync(string groupColumn, bool descending, PageQuery filterContext,
        IReadOnlyList<SummarySpec> summaries, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<GroupBucket>>(Array.Empty<GroupBucket>());

    /// <summary>
    /// Sumarios totales (sobre todas las filas que cumplen el filtro) en el orden pedido. Es el panel de
    /// pie server-side. Default: lista vacía.
    /// </summary>
    Task<IReadOnlyList<object?>> GetTotalSummaryAsync(IReadOnlyList<SummarySpec> summaries, PageQuery filterContext,
        CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<object?>>(Array.Empty<object?>());

    /// <summary>
    /// Valores de los campos calculados <paramref name="calc"/> para las filas con las claves dadas (sólo tablas de
    /// clave simple): devuelve <c>clave.ToString() → (nombreCalc → valor)</c>. Para mostrar columnas calculadas en la
    /// grilla tipada con un side-fetch por página (mismo patrón que las descripciones FK). Default: vacío.
    /// </summary>
    Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, object?>>> GetCalcValuesAsync(
        IReadOnlyCollection<object> keys, IReadOnlyList<CalculatedField> calc, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyDictionary<string, IReadOnlyDictionary<string, object?>>>(
            new Dictionary<string, IReadOnlyDictionary<string, object?>>());

    /// <summary>Obtiene por clave. Los valores van en el orden de <c>EntityMetadata.Keys</c>.</summary>
    Task<TEntity?> GetByKeyAsync(object?[] keyValues, CancellationToken cancellationToken = default);

    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>Elimina por entidad (toma todas las columnas clave). Soporta clave compuesta.</summary>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
