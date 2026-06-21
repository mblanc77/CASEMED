using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: filtrado por columna del CRUD genérico (master-detail).
/// El afiliado 13010559 tiene empleos/especialidades/apuntes conocidos.
/// </summary>
public class CrudFilterTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private const long Ci = 13010559;

    private static DapperCrudService<T> Svc<T>() where T : class =>
        TestCrud.Create<T>(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)), new FakeCurrentUser());

    [Fact]
    public async Task GetByColumn_filtra_empleos_del_afiliado()
    {
        var empleos = await Svc<Trabaja>().GetByColumnAsync("CI", Ci);

        Assert.NotEmpty(empleos);
        Assert.All(empleos, e => Assert.Equal(Ci, e.CI)); // sólo filas del padre
    }

    [Fact]
    public async Task GetByColumn_filtra_especialidades_y_apuntes()
    {
        var esp = await Svc<AfiliadoEspecialidad>().GetByColumnAsync("CI", Ci);
        var apuntes = await Svc<AfiliadoApunte>().GetByColumnAsync("CI", Ci);

        Assert.All(esp, e => Assert.Equal(Ci, e.CI));
        Assert.All(apuntes, a => Assert.Equal(Ci, a.CI));
    }

    [Fact]
    public async Task GetByColumn_columna_inexistente_lanza()
    {
        await Assert.ThrowsAsync<System.ArgumentException>(
            () => Svc<Trabaja>().GetByColumnAsync("NoExiste", Ci));
    }
}
