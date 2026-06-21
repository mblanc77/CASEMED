using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Subsidios;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2. Para el período 202201 la selección de afiliados
/// (empresas con Liquidar=1) devuelve 627 CIs distintos.
/// </summary>
public class SubsidioRepositoryTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static SubsidioRepository NewRepo() =>
        new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]
    public async Task Selecciona_afiliados_del_periodo()
    {
        var cis = await NewRepo().SeleccionarAfiliadosAsync(new SubsidioPeriodo(2022, 1), liquidar: true);
        Assert.Equal(627, cis.Count);
        Assert.Equal(cis.Count, cis.Distinct().Count()); // sin duplicados
    }

    [Fact]
    public async Task Filtra_por_CI_individual()
    {
        var repo = NewRepo();
        var todos = await repo.SeleccionarAfiliadosAsync(new SubsidioPeriodo(2022, 1), liquidar: true);
        var uno = todos.First();

        var filtrado = await repo.SeleccionarAfiliadosAsync(new SubsidioPeriodo(2022, 1), liquidar: true, ci: uno);

        Assert.Single(filtrado);
        Assert.Equal(uno, filtrado[0]);
    }
}
