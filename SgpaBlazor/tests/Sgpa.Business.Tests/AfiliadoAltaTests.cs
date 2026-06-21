using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el alta de un afiliado debe conservar el CI tipado (la cédula no es
/// autoincremental). Verifica que se quitó el IDENTITY de Afiliado.CI.
/// </summary>
[Collection("AfiliadoDb")]
public class AfiliadoAltaTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task Alta_de_afiliado_conserva_el_CI_tipado()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = TestCrud.Create<Afiliado>(db, new ShortUser());

        const long ci = 91234567;   // 8 dígitos, fuera del padrón real (cédulas migradas < 68.000.000)
        await db.ExecuteAsync("DELETE FROM dbo.Afiliado WHERE CI=@ci", new { ci });
        try
        {
            await svc.InsertAsync(new Afiliado { CI = ci, Nombres = "Test", Apellido1 = "QA" });

            var back = await db.ExecuteScalarAsync<long>("SELECT CI FROM dbo.Afiliado WHERE CI=@ci", new { ci });
            Assert.Equal(ci, back);   // si CI fuera IDENTITY, el valor insertado sería autogenerado, no @ci
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.Afiliado WHERE CI=@ci", new { ci });
        }
    }

    private sealed class ShortUser : ICurrentUser
    {
        public string UserName => "qa";   // Afiliado.Usr es nvarchar(8)
        public bool IsInRole(string role) => false;
        public bool Can(string table, PermissionAction action) => true;
    }
}
