using System;
using System.Linq;
using Dapper;
using Sgpa.Data.Crud;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Traductor de expresiones escalares (campos calculados) a SQL parametrizado. Casos unitarios (sin DB).</summary>
public class ScalarSqlTranslatorTests
{
    // Resolver simple para los tests: columna -> [Columna] (el CRUD usa esto; el builder de reportes usaría alias.[col]).
    private static readonly Func<string, string> Bare = name => $"[{name}]";

    private static string Tr(ScalarNode node, out DynamicParameters p)
    {
        p = new DynamicParameters();
        int n = 0;
        return ScalarSqlTranslator.Translate(node, Bare, p, ref n);
    }

    [Fact]
    public void Duracion_dateDiff_mas_uno()
    {
        // DateDiffDay([FechaIni],[FechaFin]) + 1
        var node = new ScalarBinary(ScalarBinOp.Add,
            new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("FechaIni"), new ScalarColumn("FechaFin") }),
            new ScalarConst(1));

        var sql = Tr(node, out var p);

        Assert.Equal("(DATEDIFF(day, [FechaIni], [FechaFin]) + @p0)", sql);
        Assert.Single(p.ParameterNames);
    }

    [Fact]
    public void Resta_de_columnas()
    {
        var node = new ScalarBinary(ScalarBinOp.Subtract, new ScalarColumn("ImpNominal"), new ScalarColumn("ImpAguinaldo"));
        Assert.Equal("([ImpNominal] - [ImpAguinaldo])", Tr(node, out _));
    }

    [Fact]
    public void Concat_y_constante()
    {
        var node = new ScalarFunc(ScalarFn.Concat, new ScalarNode[]
        {
            new ScalarColumn("Apellido1"), new ScalarConst(", "), new ScalarColumn("Nombres")
        });
        var sql = Tr(node, out var p);
        Assert.Equal("CONCAT([Apellido1], @p0, [Nombres])", sql);
        Assert.Single(p.ParameterNames);
    }

    [Fact]
    public void Iif_con_condicion()
    {
        // Iif([Activo] = 1, 'Sí', 'No')
        var node = new ScalarFunc(ScalarFn.Iif, new ScalarNode[]
        {
            new ScalarCondition(new ScalarColumn("Activo"), ScalarCompareOp.Equal, new ScalarConst(1)),
            new ScalarConst("Sí"), new ScalarConst("No")
        });
        var sql = Tr(node, out var p);
        Assert.Equal("(CASE WHEN ([Activo] = @p0) THEN @p1 ELSE @p2 END)", sql);
        Assert.Equal(3, p.ParameterNames.Count());
    }

    [Fact]
    public void Resolver_aplica_alias()
    {
        Func<string, string> aliased = name => $"t0.[{name}]";
        var p = new DynamicParameters(); int n = 0;
        var sql = ScalarSqlTranslator.Translate(new ScalarColumn("FechaFin"), aliased, p, ref n);
        Assert.Equal("t0.[FechaFin]", sql);
    }

    [Fact]
    public void Aridad_invalida_lanza()
    {
        var bad = new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("A") }); // faltan args
        Assert.Throws<ArgumentException>(() => Tr(bad, out _));
    }
}
