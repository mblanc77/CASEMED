namespace Sgpa.Data.Configuracion;

/// <summary>
/// Override de metadata por columna ("Application Model" liviano, estilo XAF). Cada campo en <c>null</c> significa
/// "usar el default del código/atributo" (<see cref="Sgpa.Domain.Metadata.ColumnMetadata"/>); sólo se persisten los
/// deltas. Identidad: (<paramref name="Tabla"/>, <paramref name="Columna"/>), nombres físicos.
/// </summary>
/// <param name="Caption">Etiqueta (XAF Caption).</param>
/// <param name="DisplayFormat">Formato .NET/DevExpress (XAF DisplayFormat).</param>
/// <param name="Orden">Orden de columnas/campos (XAF Index); menor primero.</param>
/// <param name="Ancho">Ancho en grilla (XAF Width), ej. "120px".</param>
/// <param name="Alineacion">Alineación: left | center | right (override del auto por tipo).</param>
/// <param name="VisibleLista">Visible en la grilla (XAF ListView member).</param>
/// <param name="VisibleDetalle">Visible en el formulario de edición (XAF DetailView member).</param>
/// <param name="SoloLectura">No editable (XAF AllowEdit = false).</param>
public sealed record ColumnaConfig(
    string Tabla,
    string Columna,
    string? Caption = null,
    string? DisplayFormat = null,
    int? Orden = null,
    string? Ancho = null,
    string? Alineacion = null,
    bool? VisibleLista = null,
    bool? VisibleDetalle = null,
    bool? SoloLectura = null)
{
    /// <summary>¿La fila no define ningún override? (todos los campos en null → no aporta nada y puede borrarse).</summary>
    public bool EstaVacia => Caption is null && DisplayFormat is null && Orden is null && Ancho is null
        && Alineacion is null && VisibleLista is null && VisibleDetalle is null && SoloLectura is null;
}

/// <summary>
/// Servicio singleton cacheado de overrides por columna. Best-effort: ante fallos de base no rompe la app
/// (se usan los defaults del código). Espejo de <see cref="ITablaConfigService"/> pero con grano por columna.
/// </summary>
public interface IColumnaConfigService
{
    /// <summary>Carga perezosa de la cache (idempotente).</summary>
    Task EnsureLoadedAsync(CancellationToken ct = default);

    /// <summary>Override de una columna, o <c>null</c> si no hay (usar defaults del código).</summary>
    ColumnaConfig? Get(string tabla, string columna);

    /// <summary>Overrides definidos para una tabla (puede estar vacío).</summary>
    IReadOnlyList<ColumnaConfig> ForTable(string tabla);

    /// <summary>Guarda (upsert) el override; si queda vacío, borra la fila para no ensuciar la tabla.</summary>
    Task SetAsync(ColumnaConfig config, CancellationToken ct = default);

    /// <summary>Borra el override de una columna (vuelve a los defaults del código).</summary>
    Task DeleteAsync(string tabla, string columna, CancellationToken ct = default);
}
