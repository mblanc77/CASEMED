using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Relación de una entidad hacia otra tabla, para construir filtros tipo <c>EXISTS</c>/agregado desde el FilterControl.
/// Cubre dos formas (ambas se traducen a una subconsulta correlacionada, sólo cambian las columnas que correlacionan):
/// <list type="bullet">
///   <item><b>1-N (colección)</b>: <c>IsCollection=true</c>. Ej. Afiliado→Empleos: hija.CI = padre.CI
///   (<see cref="ChildFkColumn"/>=FK en la hija, <see cref="ParentKeyColumn"/>=clave del padre).</item>
///   <item><b>N-1 (referencia/FK)</b>: <c>IsCollection=false</c>. Ej. Afiliado→Banco: hija.CodBanco (PK) = padre.CodBanco (FK)
///   (<see cref="ChildFkColumn"/>=PK de la hija, <see cref="ParentKeyColumn"/>=columna FK del padre).</item>
/// </list>
/// </summary>
public sealed record ExistsRelation(
    string Label, string FieldPrefix, EntityMetadata Child, string ChildFkColumn, string ParentKeyColumn, bool IsCollection);

/// <summary>
/// Relaciones expuestas a los filtros del listview de cualquier entidad, <b>derivadas de la metadata</b>:
/// <list type="bullet">
///   <item><b>N-1</b>: cada columna FK del owner → su tabla destino.</item>
///   <item><b>1-N</b>: cada entidad que tiene una FK apuntando al owner (requiere clave simple del owner).</item>
/// </list>
/// Las etiquetas amigables de algunas colecciones conocidas se ajustan vía <see cref="_labelOverrides"/>.
/// </summary>
public static class EntityRelations
{
    // Caption amigable para colecciones (parent, child) → etiqueta. El FieldName sigue siendo el nombre de tabla.
    private static readonly Dictionary<(Type Parent, Type Child), string> _labelOverrides = new()
    {
        [(typeof(Afiliado), typeof(Trabaja))] = "Empleos",
        [(typeof(Afiliado), typeof(Certificacion))] = "Certificaciones",
        [(typeof(Afiliado), typeof(Imponible))] = "Imponibles",
    };

    private static readonly Dictionary<Type, IReadOnlyList<ExistsRelation>> _cache = new();
    private static readonly object _gate = new();

    public static IReadOnlyList<ExistsRelation> For(Type entity)
    {
        lock (_gate)
        {
            if (_cache.TryGetValue(entity, out var cached)) return cached;

            var list = new List<ExistsRelation>();
            var owner = EntityCatalog.All.FirstOrDefault(m => m.EntityType == entity);
            if (owner is not null)
            {
                var usados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // N-1 (referencias): columnas del owner cuya FK resuelve a otra entidad de clave simple.
                // OJO: NO se descartan las columnas clave: una FK puede formar parte de la clave compuesta del
                // owner (ej. Trabaja.CodEmpresa→Empresa, Trabaja.CI→Afiliado). LookupTargetFor ya excluye sólo la
                // identidad propia (la clave SIMPLE del owner), así que se apoya en él para filtrar.
                foreach (var col in owner.Columns)
                {
                    if (col.IsAudit) continue;
                    var target = EntityCatalog.LookupTargetFor(col, owner);
                    if (target is null || target.Keys.Count != 1 || target.EntityType == owner.EntityType) continue;
                    if (!ReportableTables.IsDefault(target)) continue;   // descarta tablas basura (tmp/import/linked/…)
                    if (!usados.Add(target.Table)) continue;             // un solo nodo por tabla destino
                    list.Add(new ExistsRelation(target.Table, target.Table, target,
                        ChildFkColumn: target.Key.Name, ParentKeyColumn: col.Name, IsCollection: false));
                }

                // 1-N (colecciones): otras entidades que tienen una FK apuntando al owner (clave simple del owner).
                if (owner.Keys.Count == 1)
                {
                    var ownerKey = owner.Key.Name;
                    foreach (var child in EntityCatalog.All)
                    {
                        if (child.EntityType == owner.EntityType) continue;
                        if (!ReportableTables.IsDefault(child)) continue;   // descarta tablas basura como hijas
                        foreach (var col in child.Columns)
                        {
                            // No se descarta la columna clave: en las tablas de detalle la FK al owner es parte de
                            // la clave compuesta (ej. Trabaja.CI, Imponible.CI). Antes el `col.IsKey` la saltaba y
                            // por eso Afiliado no veía sus colecciones Empleos/Imponibles.
                            if (col.IsAudit) continue;
                            var target = EntityCatalog.LookupTargetFor(col, child);
                            if (target is null || target.EntityType != owner.EntityType) continue;
                            if (!usados.Add(child.Table)) break;   // ya hay un nodo con ese nombre → saltar esta hija
                            var label = _labelOverrides.TryGetValue((owner.EntityType, child.EntityType), out var lbl)
                                ? lbl : child.Table;
                            list.Add(new ExistsRelation(label, child.Table, child,
                                ChildFkColumn: col.Name, ParentKeyColumn: ownerKey, IsCollection: true));
                            break;   // una relación por tabla hija (primera FK que apunta al owner)
                        }
                    }
                }

                // Anticolisión de nombres para los nodos de COLECCIÓN. El DxFilterBuilder resuelve los campos por
                // FieldName de forma PLANA: si el FieldName de una colección coincide con el de alguna columna escalar
                // del árbol (propia del owner o subcampo de OTRA colección), el componente la trata como campo normal
                // y NO ofrece las funciones agregadas (Exists/Count/…). Caso real: la colección "Trabaja" (Empleos)
                // chocaba con la columna booleana Certificacion.Trabaja, que se renderiza como subcampo de
                // "Certificaciones". Se renombra el FieldName del nodo a un identificador único; ByPrefix sigue
                // resolviendo por FieldPrefix, y el Caption (etiqueta visible) no depende de él.
                var scalarNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var c in owner.ListColumns) scalarNames.Add(c.Name);
                foreach (var r in list)
                    if (r.IsCollection)
                        foreach (var c in r.Child.ListColumns) scalarNames.Add(c.Name);
                var taken = new HashSet<string>(list.Select(r => r.FieldPrefix), StringComparer.OrdinalIgnoreCase);
                for (var i = 0; i < list.Count; i++)
                {
                    var r = list[i];
                    if (!r.IsCollection || !scalarNames.Contains(r.FieldPrefix)) continue;
                    var unique = r.FieldPrefix;
                    do { unique += "_"; } while (scalarNames.Contains(unique) || !taken.Add(unique));
                    list[i] = r with { FieldPrefix = unique };
                }
            }

            IReadOnlyList<ExistsRelation> result = list;
            _cache[entity] = result;
            return result;
        }
    }

    public static ExistsRelation? ByPrefix(Type entity, string prefix)
        => For(entity).FirstOrDefault(r => r.FieldPrefix.Equals(prefix, StringComparison.OrdinalIgnoreCase));
}
