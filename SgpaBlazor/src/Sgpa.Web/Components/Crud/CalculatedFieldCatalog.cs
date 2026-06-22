using DevExpress.Data.Filtering;
using Microsoft.Extensions.Logging;
using Sgpa.Data;
using Sgpa.Data.Crud;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Catálogo (cacheado por circuito) de los campos calculados definidos en <c>dbo.CampoCalculado</c>. Lee las filas,
/// parsea la expresión (<c>CriteriaOperator</c>) y la traduce al árbol neutral con <see cref="SgpaScalarTranslator"/>,
/// entregando <see cref="CalculatedField"/> listos para la capa de datos. Lo consultan el editor/builder de reportes y
/// los filtros/ListViews. Best-effort: un campo con expresión inválida se omite (se loguea), no rompe el resto.
/// </summary>
public interface ICalculatedFieldCatalog
{
    Task EnsureLoadedAsync(CancellationToken ct = default);
    /// <summary>Campos calculados (válidos) de una tabla; vacío si no hay.</summary>
    IReadOnlyList<CalculatedField> For(string tabla);
    /// <summary>Fuerza recarga (tras alta/edición en el CRUD del catálogo).</summary>
    void Invalidate();
}

public sealed class CalculatedFieldCatalog : ICalculatedFieldCatalog
{
    private readonly IDbExecutor _db;
    private readonly ILogger<CalculatedFieldCatalog> _log;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private IReadOnlyDictionary<string, IReadOnlyList<CalculatedField>>? _byTable;

    public CalculatedFieldCatalog(IDbExecutor db, ILogger<CalculatedFieldCatalog> log)
    {
        _db = db;
        _log = log;
    }

    public async Task EnsureLoadedAsync(CancellationToken ct = default)
    {
        if (_byTable is not null) return;
        await _lock.WaitAsync(ct).ConfigureAwait(false);
        try { _byTable ??= await LoadAsync(ct).ConfigureAwait(false); }
        finally { _lock.Release(); }
    }

    public IReadOnlyList<CalculatedField> For(string tabla)
        => _byTable is not null && _byTable.TryGetValue(tabla, out var l) ? l : Array.Empty<CalculatedField>();

    public void Invalidate() => _byTable = null;

    private async Task<IReadOnlyDictionary<string, IReadOnlyList<CalculatedField>>> LoadAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, IReadOnlyList<CalculatedField>>(StringComparer.OrdinalIgnoreCase);
        try
        {
            var rows = await _db.QueryAsync<Row>(
                "SELECT Tabla, Nombre, Caption, Expr, TipoResultado, DisplayFormat FROM dbo.CampoCalculado WHERE Activo = 1",
                cancellationToken: ct).ConfigureAwait(false);

            foreach (var grp in rows.GroupBy(r => r.Tabla, StringComparer.OrdinalIgnoreCase))
            {
                var list = new List<CalculatedField>();
                foreach (var r in grp)
                {
                    try
                    {
                        var node = SgpaScalarTranslator.Translate(CriteriaOperator.Parse(r.Expr));
                        var caption = string.IsNullOrWhiteSpace(r.Caption) ? r.Nombre : r.Caption!;
                        list.Add(new CalculatedField(r.Tabla, r.Nombre, caption, node, MapType(r.TipoResultado), r.DisplayFormat));
                    }
                    catch (Exception ex)
                    {
                        _log.LogWarning(ex, "Campo calculado inválido {Tabla}.{Nombre}; se omite.", r.Tabla, r.Nombre);
                    }
                }
                if (list.Count > 0) result[grp.Key] = list;
            }
        }
        catch (Exception ex)
        {
            // Best-effort: si la tabla falta o la base está caída, no romper a los consumidores.
            _log.LogWarning(ex, "No se pudo cargar el catálogo de campos calculados.");
        }
        return result;
    }

    private static Type MapType(string? tipo) => (tipo ?? "decimal").Trim().ToLowerInvariant() switch
    {
        "int" or "integer" or "long" => typeof(int),
        "datetime" or "date" or "fecha" => typeof(DateTime),
        "bool" or "boolean" or "bit" => typeof(bool),
        "string" or "text" or "texto" => typeof(string),
        _ => typeof(decimal)
    };

    private sealed record Row(string Tabla, string Nombre, string? Caption, string Expr, string? TipoResultado, string? DisplayFormat);
}
