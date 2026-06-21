using System.Reflection;

namespace Sgpa.Domain.Metadata;

/// <summary>
/// Catálogo de todas las entidades mapeadas (las generadas desde NewSgpa2 + las que se
/// agreguen a mano). Alimenta la navegación dinámica y el menú filtrado por permisos.
/// </summary>
public static class EntityCatalog
{
    private static readonly IReadOnlyList<EntityMetadata> _all;
    private static readonly IReadOnlyDictionary<string, EntityMetadata> _byName;
    // Entidades de clave SIMPLE indexadas por el nombre de su columna clave (para resolver FK por convención).
    private static readonly IReadOnlyDictionary<string, EntityMetadata> _byKeyColumn;

    // Dueño canónico cuando varias entidades de clave simple comparten el nombre de columna clave.
    // "CI" la tienen AdPreJub, Afiliado y PrimaFallecimiento → el destino real de una FK por CI es Afiliado.
    private static readonly IReadOnlyDictionary<string, string> KeyOwnerOverride =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { ["CI"] = "Afiliado" };

    static EntityCatalog()
    {
        var all = typeof(SgpaTableAttribute).Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && t.GetCustomAttribute<SgpaTableAttribute>() is not null)
            .Select(EntityMetadata.For)
            .OrderBy(m => m.Table, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var byName = new Dictionary<string, EntityMetadata>(StringComparer.OrdinalIgnoreCase);
        foreach (var m in all)
        {
            byName.TryAdd(m.EntityType.Name, m); // clave primaria de ruta = nombre de la clase
            byName.TryAdd(m.Table, m);           // alias por nombre de tabla
        }

        // Sólo entidades con clave simple y columna descriptiva son candidatas a destino de lookup.
        // Cuando varias comparten el NOMBRE de columna clave (ej. "CI" la tienen AdPreJub, Afiliado y
        // PrimaFallecimiento), el dueño canónico se decide por override; si no, gana la de más columnas (la
        // entidad "maestra"). Antes era un TryAdd ciego que adjudicaba la clave a la primera alfabética (AdPreJub).
        var byKey = new Dictionary<string, EntityMetadata>(StringComparer.OrdinalIgnoreCase);
        foreach (var grp in all.Where(m => m.Keys.Count == 1 && m.DisplayColumn is not null)
                               .GroupBy(m => m.Keys[0].Name, StringComparer.OrdinalIgnoreCase))
        {
            var owner = KeyOwnerOverride.TryGetValue(grp.Key, out var table)
                ? grp.FirstOrDefault(m => m.Table.Equals(table, StringComparison.OrdinalIgnoreCase))
                : null;
            byKey[grp.Key] = owner ?? grp.OrderByDescending(m => m.Columns.Count).First();
        }

        _all = all;
        _byName = byName;
        _byKeyColumn = byKey;
    }

    /// <summary>
    /// Si <paramref name="col"/> es una clave foránea por convención (su nombre coincide con la columna
    /// clave de OTRA entidad de clave simple con descripción), devuelve esa entidad destino; si no, null.
    /// Ej.: columna <c>CodMutualista</c> en Afiliado → entidad <c>Mutualista</c>.
    /// </summary>
    public static EntityMetadata? LookupTargetFor(ColumnMetadata col, EntityMetadata owner)
    {
        // Para el FORMULARIO de edición: combo de FK. Se excluye sólo la IDENTIDAD propia del registro
        // (la clave simple de la entidad, ej. el CI de un afiliado: es un número que se tipea, no un combo).
        // Las FK que forman parte de una clave COMPUESTA sí son combo (ej. Trabaja.CodEmpresa → Empresa).
        if (col.IsAudit) return null;
        if (owner.Keys.Count == 1 && string.Equals(owner.Key.Name, col.Name, StringComparison.OrdinalIgnoreCase))
            return null;
        return ResolveTarget(col, owner);
    }

    /// <summary>
    /// Como <see cref="LookupTargetFor"/> pero para MOSTRAR la descripción en una grilla: resuelve también las
    /// columnas FK que forman parte de la clave primaria compuesta (ej. <c>Trabaja.CodEmpresa</c> → Empresa).
    /// El self-lookup (la clave propia de la entidad) se evita con <c>target != owner</c>.
    /// </summary>
    public static EntityMetadata? LookupDisplayTargetFor(ColumnMetadata col, EntityMetadata owner)
    {
        if (col.IsAudit) return null;
        return ResolveTarget(col, owner);
    }

    private static EntityMetadata? ResolveTarget(ColumnMetadata col, EntityMetadata owner)
        => _byKeyColumn.TryGetValue(col.Name, out var target) && target.EntityType != owner.EntityType
            ? target
            : null;

    /// <summary>Todas las entidades mapeadas (ordenadas por tabla).</summary>
    public static IReadOnlyList<EntityMetadata> All => _all;

    /// <summary>Entidades con clave primaria (aptas para CRUD genérico).</summary>
    public static IEnumerable<EntityMetadata> CrudCapable => _all.Where(m => m.Keys.Count > 0);

    public static EntityMetadata? TryGet(string name) =>
        _byName.TryGetValue(name, out var m) ? m : null;
}
