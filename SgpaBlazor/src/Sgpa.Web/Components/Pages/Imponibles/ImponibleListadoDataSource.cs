using System.Collections;
using DevExpress.Blazor;
using Sgpa.Data;

namespace Sgpa.Web.Components.Pages.Imponibles;

/// <summary>Fila del listado de imponibles (solo lectura): une Imponible con Afiliado (nombre) y Empresa.</summary>
public sealed record ImponibleListadoRow
{
    public long CI { get; init; }
    public string? NombreCompleto { get; init; }
    public string? Empresa { get; init; }
    public int Anio { get; init; }
    public byte Mes { get; init; }
    public string? Concepto { get; init; }
    public double? Importe { get; init; }
    // Parte restante de la clave compuesta (no se muestra): para abrir el imponible exacto en el ABM.
    public int CodEmpresa { get; init; }
    public DateTime Fechaingreso { get; init; }
}

/// <summary>
/// Fuente server-side del listado de imponibles. Imponible es grande (~70k), así que el paginado, el orden y
/// la búsqueda se resuelven en SQL (no se materializa todo). Solo lectura: no hay alta/edición acá (los
/// imponibles se editan en su contexto, Empleos→Imponibles del afiliado).
/// </summary>
public sealed class ImponibleListadoDataSource : GridCustomDataSource
{
    private readonly IDbExecutor _db;

    /// <summary>Texto de búsqueda (CI, nombre o empresa). Null = sin filtro.</summary>
    public string? Search { get; set; }

    public ImponibleListadoDataSource(IDbExecutor db) => _db = db;

    // ORDER BY por whitelist (campo del grid → columna SQL): evita inyección y columnas inválidas.
    private static readonly IReadOnlyDictionary<string, string> SortMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["CI"] = "i.CI",
            ["NombreCompleto"] = "a.Apellido1",
            ["Empresa"] = "e.Nombre",
            ["Anio"] = "i.Anio",
            ["Mes"] = "i.Mes",
            ["Concepto"] = "i.Concepto",
            ["Importe"] = "i.Importe",
        };

    private const string FromWhere = @"
FROM dbo.Imponible i
LEFT JOIN dbo.Afiliado a ON a.CI = i.CI
LEFT JOIN dbo.Empresa e ON e.CodEmpresa = i.CodEmpresa
WHERE (@search IS NULL
       OR CAST(i.CI AS varchar(20)) LIKE @like
       OR a.Apellido1 LIKE @like OR a.Apellido2 LIKE @like OR a.Nombres LIKE @like
       OR e.Nombre LIKE @like)";

    private object FilterParams() => new { search = Search, like = "%" + (Search ?? string.Empty) + "%" };

    public override async Task<IList> GetItemsAsync(GridCustomDataSourceItemsOptions options, CancellationToken cancellationToken)
    {
        var take = options.Count <= 0 || options.Count > 1000 ? 1000 : options.Count;
        var sql = $@"
SELECT i.CI,
       LTRIM(RTRIM(ISNULL(a.Apellido1,'') + ' ' + ISNULL(a.Apellido2,'') + ', ' + ISNULL(a.Nombres,''))) AS NombreCompleto,
       e.Nombre AS Empresa, i.Anio, i.Mes, i.Concepto, CAST(i.Importe AS float) AS Importe,
       i.CodEmpresa, i.Fechaingreso
{FromWhere}
ORDER BY {BuildOrderBy(options.SortInfo)}
OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;";
        var rows = await _db.QueryAsync<ImponibleListadoRow>(sql,
            new { search = Search, like = "%" + (Search ?? string.Empty) + "%", skip = Math.Max(0, options.StartIndex), take },
            cancellationToken: cancellationToken);
        return rows.ToList();
    }

    public override async Task<int> GetItemCountAsync(GridCustomDataSourceCountOptions options, CancellationToken cancellationToken)
        => await _db.ExecuteScalarAsync<int>($"SELECT COUNT(*) {FromWhere}", FilterParams(), cancellationToken: cancellationToken);

    // El listado no agrupa ni totaliza ni ofrece valores únicos de filtro; se implementan vacíos (abstractos).
    public override Task<IList<GridCustomDataSourceGroupInfo>> GetGroupInfoAsync(GridCustomDataSourceGroupingOptions options, CancellationToken cancellationToken)
        => Task.FromResult<IList<GridCustomDataSourceGroupInfo>>(new List<GridCustomDataSourceGroupInfo>());

    public override Task<IList> GetTotalSummaryAsync(GridCustomDataSourceTotalSummaryOptions options, CancellationToken cancellationToken)
        => Task.FromResult<IList>(Array.Empty<object>());

    public override Task<object[]> GetUniqueValuesAsync(GridCustomDataSourceUniqueValuesOptions options, CancellationToken cancellationToken)
        => Task.FromResult(Array.Empty<object>());

    private static string BuildOrderBy(IEnumerable<GridCustomDataSourceSortInfo>? sort)
    {
        var parts = new List<string>();
        if (sort is not null)
            foreach (var s in sort)
                if (SortMap.TryGetValue(s.FieldName, out var col))
                    parts.Add(s.DescendingSortOrder ? $"{col} DESC" : col);
        return parts.Count > 0 ? string.Join(", ", parts) : "i.CI, i.Anio, i.Mes";
    }
}
