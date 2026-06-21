using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using DevExpress.Blazor;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Estado de personalización de una grilla que se persiste por usuario+pantalla (tabla PreferenciaVista).
/// Combina el <see cref="GridPersistentLayout"/> nativo del DxGrid (columnas, orden, anchos, sort,
/// agrupación, filtros) con la lista de sumarios del pie, que el DxGrid no administra como feature
/// runtime — esa la maneja el componente y se serializa acá.
/// </summary>
public sealed class VistaState
{
    public GridPersistentLayout? Layout { get; set; }

    public List<SummaryDef> Summaries { get; set; } = new();

    /// <summary>Tamaño de página elegido por el usuario (incluye "ver todo"). Null = el default de la pantalla.</summary>
    public int? PageSize { get; set; }

    /// <summary>¿Columna de selección múltiple activa? Null = el default de la pantalla (no).</summary>
    public bool? MultiSelect { get; set; }

    private static readonly JsonSerializerOptions Opts = new()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public string ToJson() => JsonSerializer.Serialize(this, Opts);

    /// <summary>
    /// Quita del JSON de la vista lo "transitorio" que no se debe recordar entre visitas: el filtro aplicado
    /// (<c>Layout.FilterCriteria</c>) y el texto de búsqueda (<c>Layout.SearchText</c>). El usuario filtra/busca,
    /// se va y al volver la grilla abre limpia. El orden, las columnas, los anchos y la agrupación SÍ se conservan.
    /// <b>Importante</b>: un <c>SearchText</c> persistido revienta el FTS de DevExpress al recargar
    /// (<c>Nullable.GetUnderlyingType(null)</c> en <c>GridSearchHelper</c>) — por eso se aplica también al cargar.
    /// Ambas propiedades son init-only, por eso se opera sobre el JSON y no sobre el objeto. Tolerante: ante JSON
    /// inválido lo devuelve sin cambios.
    /// </summary>
    public static string? StripLayoutFilter(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return json;
        try
        {
            var node = JsonNode.Parse(json);
            if (node?["Layout"] is JsonObject layout)
            {
                layout.Remove("FilterCriteria");
                layout.Remove("SearchText");
            }
            return node?.ToJsonString(Opts) ?? json;
        }
        catch { return json; }
    }

    /// <summary>
    /// Normaliza el tamaño de página persistido contra los valores válidos ACTUALES del selector. Un layout viejo
    /// puede traer <c>ShowAllRows=true</c> ("ver todo", opción que ya se quitó del selector) o un PageSize que ya
    /// no existe; con una tabla grande eso intenta materializar/renderizar todo y congela el sistema. Si el valor no
    /// machea (o es "ver todo"), se cae al <paramref name="defaultPageSize"/>. Se aplica al CARGAR. Tolerante: ante
    /// JSON inválido lo devuelve sin cambios.
    /// </summary>
    public static string? NormalizePageSize(string? json, IReadOnlyList<int> allowedPageSizes, int defaultPageSize)
    {
        if (string.IsNullOrWhiteSpace(json)) return json;
        try
        {
            var node = JsonNode.Parse(json);
            if (node is null) return json;

            bool showAll = false;
            if (node["Layout"] is JsonObject layout)
            {
                showAll = layout["ShowAllRows"]?.GetValue<bool>() ?? false;
                layout.Remove("ShowAllRows");                          // "ver todo" ya no es opción del selector
                if (showAll || NotAllowed(layout["PageSize"], allowedPageSizes))
                    layout["PageSize"] = defaultPageSize;
            }
            // El PageSize "suelto" del VistaState (bindeado al grid) se valida igual.
            if (showAll || NotAllowed(node["PageSize"], allowedPageSizes))
                node["PageSize"] = defaultPageSize;

            return node.ToJsonString(Opts);
        }
        catch { return json; }

        static bool NotAllowed(JsonNode? n, IReadOnlyList<int> allowed)
            => n is not null && !allowed.Contains(n.GetValue<int>());
    }

    /// <summary>Deserializa de forma tolerante: ante un JSON inválido (formato viejo, corrupto) devuelve null.</summary>
    public static VistaState? FromJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        try { return JsonSerializer.Deserialize<VistaState>(json, Opts); }
        catch { return null; }
    }
}

/// <summary>Un sumario del pie: la columna y la función agregada a aplicar.</summary>
public sealed class SummaryDef
{
    public string Column { get; set; } = string.Empty;
    public GridSummaryItemType Type { get; set; } = GridSummaryItemType.Sum;
}
