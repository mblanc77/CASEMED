using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Reporting;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Motor de reportes SQL: guard de sólo lectura, detección de tokens, literales tipados/escapados y sustitución.
/// Los últimos casos describen/ejecutan una consulta real contra NewSgpa2.
/// </summary>
public class SqlReportEngineTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Theory]
    [InlineData("SELECT 1", true)]
    [InlineData("  with x as (select 1 a) select * from x", true)]
    [InlineData("SELECT * FROM dbo.Afiliado WHERE CI = @ci", true)]
    [InlineData("SELECT 1 -- ; DROP TABLE x", true)]            // el DROP queda en un comentario
    [InlineData("SELECT * FROM dbo.SP_Afiliado", true)]         // tabla con prefijo SP_ (no es un proc)
    [InlineData("DELETE FROM dbo.Afiliado", false)]
    [InlineData("UPDATE dbo.Afiliado SET CI = 1", false)]
    [InlineData("SELECT 1; DROP TABLE x", false)]               // segunda sentencia
    [InlineData("SELECT 1 /* */ ; TRUNCATE TABLE x", false)]
    [InlineData("EXEC sp_who", false)]
    [InlineData("SELECT * INTO #t FROM dbo.Afiliado", false)]
    [InlineData("", false)]
    public void EnsureReadOnly_clasifica(string sql, bool ok)
        => Assert.Equal(ok, SqlReportEngine.EnsureReadOnly(sql) is null);

    [Fact]
    public void DetectarTokens_sin_repetir_en_orden()
    {
        var t = SqlReportEngine.DetectarTokens("SELECT @A, @B FROM t WHERE x = @A AND y = @C");
        Assert.Equal(new[] { "A", "B", "C" }, t);
    }

    [Fact]
    public void Literal_escapa_y_tipa()
    {
        Assert.Equal("N'O''Brien'", SqlReportEngine.Literal(SqlParamTipo.Texto, "O'Brien"));
        Assert.Equal("5", SqlReportEngine.Literal(SqlParamTipo.Entero, 5));
        Assert.Equal("1", SqlReportEngine.Literal(SqlParamTipo.Bool, true));
        Assert.Equal("'2026-06-21T00:00:00'", SqlReportEngine.Literal(SqlParamTipo.Fecha, new System.DateTime(2026, 6, 21)));
        Assert.Equal("NULL", SqlReportEngine.Literal(SqlParamTipo.Texto, null));
    }

    [Fact]
    public void Sustituir_reemplaza_declarados_y_respeta_otros()
    {
        var defs = new[]
        {
            new SqlParamDef("A", null, SqlParamTipo.Texto, null),
            new SqlParamDef("B", null, SqlParamTipo.Entero, null),
        };
        var vals = new Dictionary<string, object?> { ["A"] = "O'x", ["B"] = 5 };
        var sql = SqlReportEngine.Sustituir("WHERE x=@A AND y=@B AND z=@Otro", defs, vals);
        Assert.Equal("WHERE x=N'O''x' AND y=5 AND z=@Otro", sql);
    }

    [Fact]
    public void Sustituir_quita_comillas_que_rodean_al_token()
    {
        var defs = new[] { new SqlParamDef("A", null, SqlParamTipo.Texto, null) };
        var vals = new Dictionary<string, object?> { ["A"] = "blanc" };
        // Da igual escribir @A o '@A': el motor aporta el entrecomillado por tipo.
        Assert.Equal("WHERE Nombre = N'blanc'", SqlReportEngine.Sustituir("WHERE Nombre = '@A'", defs, vals));
        Assert.Equal("WHERE Nombre = N'blanc'", SqlReportEngine.Sustituir("WHERE Nombre = @A", defs, vals));
    }

    [Fact]
    public async Task Describir_lista_columnas_del_select()
    {
        var svc = new DapperReporteSqlService(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));
        var defs = new[] { new SqlParamDef("Mut", null, SqlParamTipo.Entero, null) };
        var cols = await svc.DescribirAsync(
            "SELECT TOP 5 CI, Nombres FROM dbo.Afiliado WHERE CodMutualista = @Mut", defs);

        Assert.Contains(cols, c => c.Name.Equals("CI", System.StringComparison.OrdinalIgnoreCase));
        Assert.Contains(cols, c => c.Name.Equals("Nombres", System.StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task Sustituir_y_ejecutar_contra_la_base()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var defs = new[] { new SqlParamDef("Top", null, SqlParamTipo.Entero, null) };
        var sql = "SELECT TOP (@Top) CI, Nombres FROM dbo.Afiliado ORDER BY CI";
        var final = SqlReportEngine.Sustituir(sql, defs, new Dictionary<string, object?> { ["Top"] = 3 });

        var rows = await db.QueryAsync<dynamic>(final);
        Assert.True(rows.Count is > 0 and <= 3);
        Assert.True(((IDictionary<string, object>)rows[0]).ContainsKey("CI"));
    }
}
