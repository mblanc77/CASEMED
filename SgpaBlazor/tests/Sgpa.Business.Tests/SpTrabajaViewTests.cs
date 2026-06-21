using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: las tablas SP_* que en la app SP eran linkeadas a la base SGPA
/// (SP_Trabaja, SP_Afiliado, SP_Empresa) deben reflejar sus tablas SGPA (son vistas). Al consolidar
/// quedaban vacías y rompían las queries migradas (aportes, promedio, etc.). Este test blinda esa paridad.
/// </summary>
public class SpLinkedViewsTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Theory]
    [InlineData("SP_Trabaja", "Trabaja")]
    [InlineData("SP_Afiliado", "Afiliado")]
    [InlineData("SP_Empresa", "Empresa")]
    public async Task SP_refleja_la_tabla_SGPA_y_no_esta_vacia(string spTable, string sgpaTable)
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var sgpa = await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM dbo.{sgpaTable}");
        var sp = await db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM dbo.{spTable}");

        Assert.True(sgpa > 0, $"dbo.{sgpaTable} no debería estar vacía.");
        Assert.Equal(sgpa, sp);   // si la SP_ vuelve a ser una tabla vacía, esto falla
    }
}
