using System.Collections;
using System.Reflection;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;

namespace Sgpa.Business.Tests;

/// <summary>
/// Valida (sin base) que el mapa de navegación generado (ReportNavMap) sea consistente con las nav props y el
/// metadata, y que la heurística de tablas reportables se comporte como esperamos. Atrapa bugs de generación.
/// </summary>
public class ReportNavTests
{
    [Fact]
    public void NavMap_links_son_consistentes_con_entidades()
    {
        var problemas = new List<string>();
        foreach (var (rootType, links) in ReportNavMap.Map)
        {
            foreach (var link in links)
            {
                if (JoinProp(rootType, link.JoinColumn) is null)
                    problemas.Add($"{rootType.Name}: join '{link.JoinColumn}' no es propiedad del root (link {link.PropName}→{link.Remote.Name})");
                if (JoinProp(link.Remote, link.JoinColumn) is null)
                    problemas.Add($"{link.Remote.Name}: join '{link.JoinColumn}' no es propiedad del remoto (root {rootType.Name})");

                var nav = rootType.GetProperty(link.PropName);
                if (nav is null) { problemas.Add($"{rootType.Name}.{link.PropName} no existe"); continue; }

                if (link.Kind == ReportNavKind.Collection)
                {
                    if (!(nav.PropertyType.IsGenericType && nav.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                          && nav.PropertyType.GetGenericArguments()[0] == link.Remote))
                        problemas.Add($"{rootType.Name}.{link.PropName} debería ser List<{link.Remote.Name}>");
                }
                else if (nav.PropertyType != link.Remote)
                {
                    problemas.Add($"{rootType.Name}.{link.PropName} debería ser {link.Remote.Name}");
                }
            }
        }
        Assert.True(problemas.Count == 0, string.Join("\n", problemas));
    }

    // El join puede diferir en capitalización entre tablas (ej. IDFactura/IdFactura); el loader resuelve igual.
    private static PropertyInfo? JoinProp(Type t, string name)
        => t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

    [Fact]
    public void NavMap_Afiliado_tiene_las_relaciones_esperadas()
    {
        Assert.True(ReportNavMap.Map.TryGetValue(typeof(Afiliado), out var links));

        // 1-a-N: empleos (Trabaja por CI).
        Assert.Contains(links!, l => l.PropName == "Trabajas"
            && l.Kind == ReportNavKind.Collection && l.Remote == typeof(Trabaja) && l.JoinColumn == "CI");

        // N-a-1: alguna referencia a un padre (ej. Mutualista).
        Assert.Contains(links!, l => l.Kind == ReportNavKind.Reference && l.Remote == typeof(Mutualista));
    }

    [Fact]
    public void Reportables_incluye_Afiliado_y_excluye_basura()
    {
        var all = EntityCatalog.All.ToList();
        var reportable = all.Where(ReportableTables.IsDefault).ToList();

        Assert.Contains(reportable, m => m.Table == "Afiliado");
        Assert.True(reportable.Count < all.Count, "deberían excluirse temporales/XAF/working tables");
        // Tablas _NNN_ (nombre empieza con dígito) quedan afuera.
        Assert.DoesNotContain(reportable, m => m.Table.Length > 0 && char.IsDigit(m.Table[0]));
    }

    [Fact]
    public void Nav_props_no_se_cuelan_como_columnas_del_metadata()
    {
        // Afiliado tiene nav props (Trabajas, Mutualista) que NO deben aparecer como columnas de la tabla.
        var meta = EntityMetadata.For<Afiliado>();
        Assert.DoesNotContain(meta.Columns, c => c.Name == "Trabajas");
        Assert.DoesNotContain(meta.Columns, c => typeof(IEnumerable).IsAssignableFrom(c.ClrType) && c.ClrType != typeof(string));
        Assert.Contains(meta.Columns, c => c.Name == "CI"); // las columnas reales siguen
    }
}
