namespace Sgpa.Web.Components.Layout;

/// <summary>
/// Íconos (Font Awesome) por entidad para los títulos del CRUD genérico (<c>/crud/{Entity}</c>).
/// Reflejan los íconos del menú lateral (<see cref="NavMenu"/>): los catálogos comparten <c>fa-tag</c>
/// y unas pocas tablas tienen ícono propio. El título de cada pantalla muestra el mismo ícono que el
/// ítem del menú, en grande, a la izquierda del encabezado.
/// Mantener en sync con NavMenu si se cambia el ícono de un ítem.
/// </summary>
public static class NavIcons
{
    // Íconos propios (no-catálogo) que el menú asigna a ciertas tablas servidas por /crud/{Entity}.
    private static readonly Dictionary<string, string> _porEntidad = new(StringComparer.OrdinalIgnoreCase)
    {
        ["EmpresaPago"] = "fa-building-columns",
        ["ErrCargaAbitab"] = "fa-triangle-exclamation",
        ["SP_PrestamoEstado"] = "fa-tags",
        ["SP_CtrlPrestamoEstado"] = "fa-right-left",
    };

    /// <summary>
    /// Clase Font Awesome del ícono para la entidad (sin el prefijo <c>fa-solid</c>).
    /// Default <c>fa-tag</c>, igual que los catálogos del menú.
    /// </summary>
    public static string For(string? entity)
        => entity is not null && _porEntidad.TryGetValue(entity, out var i) ? i : "fa-tag";
}
