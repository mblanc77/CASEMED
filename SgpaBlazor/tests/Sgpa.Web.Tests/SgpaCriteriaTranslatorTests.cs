using System;
using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;
using Sgpa.Web.Components.Crud;
using Xunit;

namespace Sgpa.Web.Tests;

/// <summary>Traducción del CriteriaOperator del DxGrid (fila de filtros/menú/búsqueda) al árbol neutral.</summary>
public class SgpaCriteriaTranslatorTests
{
    [Fact]
    public void Null_devuelve_null()
        => Assert.Null(SgpaCriteriaTranslator.Translate(null));

    [Fact]
    public void Igualdad_se_traduce_a_compare()
    {
        var node = SgpaCriteriaTranslator.Translate(CriteriaOperator.Parse("[CI] = 13010559"));
        var c = Assert.IsType<FilterCompare>(node);
        Assert.Equal("CI", c.Column);
        Assert.Equal(FilterOp.Equal, c.Op);
        Assert.Equal(13010559, Convert.ToInt64(c.Value));
    }

    [Fact]
    public void Mayor_se_traduce_a_compare()
    {
        var node = SgpaCriteriaTranslator.Translate(CriteriaOperator.Parse("[Importe] > 100"));
        var c = Assert.IsType<FilterCompare>(node);
        Assert.Equal("Importe", c.Column);
        Assert.Equal(FilterOp.Greater, c.Op);
    }

    [Fact]
    public void Contains_se_traduce_a_text()
    {
        var node = SgpaCriteriaTranslator.Translate(CriteriaOperator.Parse("Contains([Nombres], 'PER')"));
        var t = Assert.IsType<FilterText>(node);
        Assert.Equal("Nombres", t.Column);
        Assert.Equal(FilterFunc.Contains, t.Func);
        Assert.Equal("PER", t.Value);
    }

    [Fact]
    public void And_se_traduce_a_grupo()
    {
        var node = SgpaCriteriaTranslator.Translate(CriteriaOperator.Parse("[Mes] = 1 And [Anio] = 2022"));
        var g = Assert.IsType<FilterGroup>(node);
        Assert.True(g.And);
        Assert.Equal(2, g.Nodes.Count);
    }

    [Fact]
    public void In_se_traduce_a_filterin()
    {
        var node = SgpaCriteriaTranslator.Translate(CriteriaOperator.Parse("[CI] In (1, 2, 3)"));
        var f = Assert.IsType<FilterIn>(node);
        Assert.Equal("CI", f.Column);
        Assert.Equal(3, f.Values.Count);
    }
}
