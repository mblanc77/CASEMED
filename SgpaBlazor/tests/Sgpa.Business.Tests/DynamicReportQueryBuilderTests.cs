using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Data.Reporting;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Builder de reportes dinámicos: generación de SQL (JOINs N-1 + SELECT + WHERE/EXISTS sobre la raíz aliaseada t0).
/// Los casos de SQL son unitarios (sin DB); el último ejecuta la consulta contra NewSgpa2.
/// </summary>
public class DynamicReportQueryBuilderTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    // Reporte de ejemplo: raíz Afiliado; columnas Nombres (raíz) + Banco.Descripcion (FK CodBanco→Banco).
    private static ReporteDinamicoDef AfiliadoConBanco() => new()
    {
        RootTable = "Afiliado",
        Columns =
        {
            new ReportFieldRef(Array.Empty<string>(), "Nombres"),
            new ReportFieldRef(new[] { "CodBanco" }, "Descripcion"),
        },
    };

    [Fact]
    public void Build_genera_join_N1_y_select_aliaseado()
    {
        var q = DynamicReportQueryBuilder.Build(AfiliadoConBanco(), filter: null);

        Assert.Contains("FROM [dbo].[Afiliado] AS t0", q.Sql);
        Assert.Contains("LEFT JOIN [dbo].[Banco] AS j1 ON t0.[CodBanco] = j1.[CodBanco]", q.Sql);
        Assert.Contains("t0.[Nombres] AS [Nombres]", q.Sql);
        Assert.Contains("j1.[Descripcion] AS [CodBanco_Descripcion]", q.Sql);

        Assert.Collection(q.Columns,
            c => Assert.Equal("Nombres", c.FieldName),
            c => Assert.Equal("CodBanco_Descripcion", c.FieldName));
    }

    [Fact]
    public void Build_where_referencia_la_raiz_por_alias_t0()
    {
        var def = AfiliadoConBanco();
        var filter = new FilterCompare("CI", FilterOp.Equal, 13010559L);

        var q = DynamicReportQueryBuilder.Build(def, filter);

        Assert.Contains("WHERE t0.[CI] = @f0", q.Sql);
    }

    [Fact]
    public void Build_exists_correlaciona_contra_t0()
    {
        var def = AfiliadoConBanco();
        var banco = Sgpa.Domain.Metadata.EntityMetadata.For<Sgpa.Domain.Entities.Banco>();
        // EXISTS N-1: Banco.CodBanco = Afiliado.CodBanco AND Banco.Descripcion = X
        var filter = new FilterExists(banco, "CodBanco", "CodBanco",
            new FilterCompare("Descripcion", FilterOp.Equal, "BROU"));

        var q = DynamicReportQueryBuilder.Build(def, filter);

        Assert.Contains("EXISTS (SELECT 1 FROM [dbo].[Banco] AS ex0 WHERE ex0.[CodBanco] = t0.[CodBanco]", q.Sql);
    }

    [Fact]
    public void Build_dedupe_join_para_columnas_del_mismo_path()
    {
        var def = new ReporteDinamicoDef
        {
            RootTable = "Afiliado",
            Columns =
            {
                new ReportFieldRef(new[] { "CodBanco" }, "Descripcion"),
                new ReportFieldRef(new[] { "CodBanco" }, "CodBanco"),
            },
        };

        var q = DynamicReportQueryBuilder.Build(def, filter: null);

        // Un solo JOIN aunque haya dos columnas del mismo destino.
        var joins = System.Text.RegularExpressions.Regex.Matches(q.Sql, "LEFT JOIN").Count;
        Assert.Equal(1, joins);
    }

    [Fact]
    public void InvolvedTables_incluye_raiz_y_destinos_FK()
    {
        var tablas = DynamicReportQueryBuilder.InvolvedTables(AfiliadoConBanco(), filter: null);

        Assert.Contains("Afiliado", tablas);
        Assert.Contains("Banco", tablas);
    }

    [Fact]
    public void Build_sin_columnas_lanza()
    {
        var def = new ReporteDinamicoDef { RootTable = "Afiliado" };
        Assert.Throws<ArgumentException>(() => DynamicReportQueryBuilder.Build(def, filter: null));
    }

    [Fact]
    public async Task Build_ejecuta_contra_la_base()
    {
        var q = DynamicReportQueryBuilder.Build(AfiliadoConBanco(), filter: null, maxRows: 5);
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));

        // Filas dinámicas (IDictionary<string,object>) — lo que consumirá la grilla del viewer.
        var rows = await db.QueryAsync<dynamic>(q.Sql, q.Parameters);

        Assert.True(rows.Count is > 0 and <= 5);
        var dict = (IDictionary<string, object>)rows[0];
        Assert.True(dict.ContainsKey("Nombres"));
        Assert.True(dict.ContainsKey("CodBanco_Descripcion"));
    }

    // Campo calculado de la raíz: Duracion = DateDiffDay([FechaIni],[FechaFin]) + 1 sobre Certificacion.
    private static CalculatedField Duracion() => new(
        "Certificacion", "Duracion", "Duración",
        new ScalarBinary(ScalarBinOp.Add,
            new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("FechaIni"), new ScalarColumn("FechaFin") }),
            new ScalarConst(1)),
        typeof(int), null);

    private static ReporteDinamicoDef CertConDuracion() => new()
    {
        RootTable = "Certificacion",
        Columns =
        {
            new ReportFieldRef(Array.Empty<string>(), "CI"),
            new ReportFieldRef(Array.Empty<string>(), "Duracion") { Calc = true },
        },
    };

    [Fact]
    public void Build_campo_calculado_inline_en_select_y_where()
    {
        var def = CertConDuracion();
        var filter = new FilterCompare("Duracion", FilterOp.Greater, 5);

        var q = DynamicReportQueryBuilder.Build(def, filter, rootCalc: new[] { Duracion() });

        Assert.Contains("DATEDIFF(day, t0.[FechaIni], t0.[FechaFin]) + @p0", q.Sql);
        Assert.Contains("AS [Duracion]", q.Sql);
        // En el WHERE la expresión va inline (SQL no admite el alias del SELECT en el WHERE).
        Assert.Contains("(DATEDIFF(day, t0.[FechaIni], t0.[FechaFin]) + @p1) > @f2", q.Sql);
        Assert.Equal(typeof(int), q.Columns.Single(c => c.FieldName == "Duracion").ClrType);
    }

    [Fact]
    public async Task Build_campo_calculado_ejecuta()
    {
        var def = CertConDuracion();
        var filter = new FilterCompare("Duracion", FilterOp.GreaterOrEqual, 1);
        var q = DynamicReportQueryBuilder.Build(def, filter, maxRows: 5, rootCalc: new[] { Duracion() });
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));

        var rows = await db.QueryAsync<dynamic>(q.Sql, q.Parameters);

        Assert.True(rows.Count is > 0 and <= 5);
        var dict = (IDictionary<string, object>)rows[0];
        Assert.True(dict.ContainsKey("Duracion"));
        Assert.True(Convert.ToInt32(dict["Duracion"]) >= 1);
    }
}
