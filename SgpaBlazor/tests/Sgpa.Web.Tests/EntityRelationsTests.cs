using System;
using System.Collections.Generic;
using System.Linq;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
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

    [Fact]
    public void Afiliado_expone_colecciones_con_FK_en_clave_compuesta()
    {
        var rels = EntityRelations.For(typeof(Afiliado));

        // Empleos (Trabaja) e Imponibles correlacionan por CI, que en esas tablas es parte de la clave
        // compuesta. Antes se descartaban por `col.IsKey` y no aparecían en los filtros EXISTS.
        var trabaja = Assert.Single(rels, r => r.Child.Table == "Trabaja");
        Assert.True(trabaja.IsCollection);
        Assert.Equal("Empleos", trabaja.Label);
        Assert.Equal("CI", trabaja.ChildFkColumn);
        Assert.Equal("CI", trabaja.ParentKeyColumn);

        var imponible = Assert.Single(rels, r => r.Child.Table == "Imponible");
        Assert.True(imponible.IsCollection);
        Assert.Equal("CI", imponible.ChildFkColumn);
    }

    [Fact]
    public void Nodo_de_coleccion_no_colisiona_con_columnas_escalares()
    {
        // El DxFilterBuilder resuelve FieldName de forma plana: si el nodo de una colección se llama igual que
        // una columna escalar del árbol, deja de ofrecer agregados. Caso real: "Trabaja" (Empleos) chocaba con
        // la columna booleana Certificacion.Trabaja. El FieldPrefix del nodo debe quedar libre de colisiones.
        var rels = EntityRelations.For(typeof(Afiliado));
        var owner = EntityCatalog.All.First(m => m.EntityType == typeof(Afiliado));

        var escalares = owner.ListColumns.Select(c => c.Name)
            .Concat(rels.Where(r => r.IsCollection).SelectMany(r => r.Child.ListColumns.Select(c => c.Name)))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        Assert.All(rels.Where(r => r.IsCollection),
            r => Assert.DoesNotContain(r.FieldPrefix, escalares));

        // En concreto, la colección de Trabaja quedó renombrada (ya no es el bare "Trabaja"),
        // pero su etiqueta visible sigue siendo "Empleos" y ByPrefix la resuelve por el nuevo FieldPrefix.
        var trabaja = Assert.Single(rels, r => r.Child.Table == "Trabaja");
        Assert.NotEqual("Trabaja", trabaja.FieldPrefix);
        Assert.Equal("Empleos", trabaja.Label);
        Assert.Same(trabaja, EntityRelations.ByPrefix(typeof(Afiliado), trabaja.FieldPrefix));
    }

    [Fact]
    public void Tabla_de_detalle_expone_sus_FK_de_clave_compuesta_como_referencias()
    {
        // Desde Trabaja, las columnas que forman la clave compuesta y son FK (CI→Afiliado, CodEmpresa→Empresa)
        // deben verse como referencias N-1 navegables.
        var rels = EntityRelations.For(typeof(Trabaja));

        Assert.Contains(rels, r => r.Child.Table == "Afiliado" && !r.IsCollection);
        Assert.Contains(rels, r => r.Child.Table == "Empresa" && !r.IsCollection);
    }
}
