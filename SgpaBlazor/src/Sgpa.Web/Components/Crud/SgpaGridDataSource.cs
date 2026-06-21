using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Blazor;
using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Fuente de datos server-side para DxGrid sobre <see cref="ISgpaCrudService{TEntity}"/>.
/// El grid delega paginado, orden, fila de filtros, menú de filtro, búsqueda y export a estos
/// overrides; aquí se traduce todo a un <see cref="PageQuery"/> y se resuelve por SQL.
/// </summary>
public sealed class SgpaGridDataSource<T> : GridCustomDataSource where T : class
{
    // Tope para export/"traer todo" cuando el grid pide Count no acotado.
    private const int FetchAllCap = 50000;

    private readonly ISgpaCrudService<T> _service;
    private readonly string? _filterColumn;
    private readonly object? _filterValue;
    private readonly FilterNode? _baseFilter;   // filtro fijo AND-combinado con el del grid (ej. período Mes+Año)
    private readonly bool _blocked;             // true = no consultar nada (carga diferida hasta elegir filtro)
    // Callback opcional tras traer cada página: resuelve las descripciones de las FK de esas filas (server-side,
    // por página) — para FK a tablas grandes (ej. Afiliado por CI) que no se materializan enteras en un combo.
    private readonly Func<IReadOnlyList<T>, CancellationToken, Task>? _onItemsLoaded;

    public SgpaGridDataSource(ISgpaCrudService<T> service, string? filterColumn = null, object? filterValue = null,
        FilterNode? baseFilter = null, bool blocked = false,
        Func<IReadOnlyList<T>, CancellationToken, Task>? onItemsLoaded = null)
    {
        _service = service;
        _filterColumn = filterColumn;
        _filterValue = filterValue;
        _baseFilter = baseFilter;
        _blocked = blocked;
        _onItemsLoaded = onItemsLoaded;
    }

    // Último filtro que pidió la grilla (fila de filtros + menú + búsqueda traducidos). Se guarda para poder
    // resolver "seleccionar todo lo del filtro" (GetKeyValuesAsync) con el MISMO filtro activo en pantalla.
    private CriteriaOperator? _lastGridFilter;

    /// <summary>Ítems de la última página entregada al grid (las MISMAS instancias que el grid muestra) —
    /// para marcar la página visible como seleccionada por referencia ("seleccionar todo").</summary>
    public IReadOnlyList<T> LastPageItems { get; private set; } = System.Array.Empty<T>();

    public override async Task<IList> GetItemsAsync(
        GridCustomDataSourceItemsOptions options, CancellationToken cancellationToken)
    {
        _lastGridFilter = options.FilterCriteria;
        if (_blocked) return new List<T>();

        // ParentGroupInfo: al expandir un grupo, restringe las filas hoja a los valores de los grupos padre.
        var query = BuildQuery(options.StartIndex, options.Count, options.SortInfo, options.FilterCriteria, options.ParentGroupInfo);
        var page = await _service.GetPageAsync(query, cancellationToken).ConfigureAwait(false);
        var items = page.Items.ToList();
        // Resuelve las descripciones FK de esta página ANTES de devolver, así la grilla ya pinta el nombre.
        if (_onItemsLoaded is not null) await _onItemsLoaded(items, cancellationToken).ConfigureAwait(false);
        LastPageItems = items;
        return items;
    }

    // Agrupamiento server-side: traduce a GROUP BY (vía el servicio). Devuelve un grupo por valor distinto
    // de la columna, con su conteo y los sumarios por grupo, respetando filtro + grupos padre.
    public override async Task<IList<GridCustomDataSourceGroupInfo>> GetGroupInfoAsync(
        GridCustomDataSourceGroupingOptions options, CancellationToken cancellationToken)
    {
        if (_blocked) return new List<GridCustomDataSourceGroupInfo>();
        var ctx = BuildQuery(0, 0, null, options.FilterCriteria, options.ParentGroupInfo);
        var summaries = MapSummaries(options.SummaryInfo);
        var groups = await _service.GetGroupsAsync(options.FieldName, options.DescendingSortOrder, ctx, summaries, cancellationToken)
            .ConfigureAwait(false);
        return groups.Select(g => new GridCustomDataSourceGroupInfo
        {
            Value = g.Value,
            DataItemCount = g.Count,
            SummaryValues = g.Summaries.ToArray()
        }).ToList();
    }

    // Panel de pie server-side: sumarios totales sobre todo el filtro, en el orden pedido por el grid.
    public override async Task<IList> GetTotalSummaryAsync(
        GridCustomDataSourceTotalSummaryOptions options, CancellationToken cancellationToken)
    {
        if (_blocked) return System.Array.Empty<object>();
        var ctx = BuildQuery(0, 0, null, options.FilterCriteria);
        var summaries = MapSummaries(options.SummaryInfo);
        var values = await _service.GetTotalSummaryAsync(summaries, ctx, cancellationToken).ConfigureAwait(false);
        return values.ToArray();
    }

    private static List<SummarySpec> MapSummaries(IReadOnlyList<GridCustomDataSourceSummaryInfo>? infos)
        => infos is null ? new() : infos.Select(s => new SummarySpec(s.FieldName, MapAgg(s.SummaryType))).ToList();

    private static AggKind MapAgg(GridSummaryItemType t) => t switch
    {
        GridSummaryItemType.Sum => AggKind.Sum,
        GridSummaryItemType.Min => AggKind.Min,
        GridSummaryItemType.Max => AggKind.Max,
        GridSummaryItemType.Avg => AggKind.Avg,
        _ => AggKind.Count
    };

    /// <summary>
    /// Todas las claves (valores de <paramref name="keyColumn"/>) que matchean el filtro activo de la grilla
    /// (último visto) + el filtro base, hasta <paramref name="max"/>. Para "seleccionar todo lo del filtro".
    /// </summary>
    public async Task<IReadOnlyList<object>> GetKeyValuesAsync(string keyColumn, int max, CancellationToken cancellationToken)
    {
        if (_blocked) return System.Array.Empty<object>();
        var ctx = BuildQuery(0, 1, null, _lastGridFilter);   // CombineFilter le agrega el filtro base
        var values = await _service.GetDistinctValuesAsync(keyColumn, ctx, max, cancellationToken).ConfigureAwait(false);
        return values.Where(v => v is not null).Cast<object>().ToList();
    }

    public override async Task<int> GetItemCountAsync(
        GridCustomDataSourceCountOptions options, CancellationToken cancellationToken)
    {
        _lastGridFilter = options.FilterCriteria;
        if (_blocked) return 0;
        var page = await _service.GetPageAsync(BuildQuery(0, 1, null, options.FilterCriteria), cancellationToken)
            .ConfigureAwait(false);
        return page.TotalCount;
    }

    public override async Task<object[]> GetUniqueValuesAsync(
        GridCustomDataSourceUniqueValuesOptions options, CancellationToken cancellationToken)
    {
        if (_blocked) return System.Array.Empty<object>();
        var ctx = BuildQuery(0, 1, null, options.FilterCriteria);
        var values = await _service.GetDistinctValuesAsync(options.FieldName, ctx, 1000, cancellationToken)
            .ConfigureAwait(false);
        return values.Where(v => v is not null).Cast<object>().ToArray();
    }

    private PageQuery BuildQuery(int skip, int count, IEnumerable<GridCustomDataSourceSortInfo>? sort,
        CriteriaOperator? filter, IReadOnlyList<GridCustomDataSourceGroupCriterion>? parentGroups = null)
    {
        var sorts = sort?
            .Select(s => new SortColumn(s.FieldName, s.DescendingSortOrder))
            .ToList();
        var take = count <= 0 || count > FetchAllCap ? FetchAllCap : count;
        return new PageQuery(
            Math.Max(0, skip), take,
            Search: null,
            FilterColumn: _filterColumn,
            FilterValue: _filterValue,
            Sort: sorts is { Count: > 0 } ? sorts : null,
            Filter: CombineFilter(filter, parentGroups));
    }

    // Combina el filtro del grid + las igualdades de los grupos padre + el filtro base fijo (todos en un AND).
    private FilterNode? CombineFilter(CriteriaOperator? gridFilter,
        IReadOnlyList<GridCustomDataSourceGroupCriterion>? parentGroups)
    {
        var node = SgpaCriteriaTranslator.Translate(gridFilter);

        var nodes = new List<FilterNode>();
        if (node is not null) nodes.Add(node);
        if (_baseFilter is not null) nodes.Add(_baseFilter);
        if (parentGroups is { Count: > 0 })
            foreach (var pc in parentGroups)
                nodes.Add(new FilterCompare(pc.FieldName, FilterOp.Equal, pc.Value));

        return nodes.Count switch { 0 => null, 1 => nodes[0], _ => new FilterGroup(true, nodes) };
    }
}
