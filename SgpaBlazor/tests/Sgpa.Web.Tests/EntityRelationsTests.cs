using System;
using System.Linq;
using Sgpa.Domain.Entities;
using Sgpa.Web.Components.Crud;
using Xunit;

namespace Sgpa.Web.Tests;

/// <summary>Las relaciones expuestas a los filtros excluyen las tablas "basura" (tmp/import/linked/…).</summary>
public class EntityRelationsTests
{
    [Fact]
    public void Relaciones_de_afiliado_excluyen_tablas_basura_y_conservan_las_reales()
    {
        var rels = EntityRelations.For(typeof(Afiliado));

        // No aparecen tablas temporales / de trabajo / reportes.
        Assert.DoesNotContain(rels, r =>
            r.Child.Table.Contains("tmp", StringComparison.OrdinalIgnoreCase)
            || r.Child.Table.Contains("temp", StringComparison.OrdinalIgnoreCase)
            || r.Child.Table.Contains("rpt", StringComparison.OrdinalIgnoreCase)
            || r.Child.Table.Contains("backup", StringComparison.OrdinalIgnoreCase)
            || r.Child.Keys.Count == 0);

        // Sí están las relaciones reales conocidas.
        Assert.Contains(rels, r => r.Child.Table == "Certificacion" && r.IsCollection);     // 1-N
        Assert.Contains(rels, r => r.Child.Table == "Mutualista" && !r.IsCollection);        // N-1
    }
}
