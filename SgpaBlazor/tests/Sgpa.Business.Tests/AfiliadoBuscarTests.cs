using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: la búsqueda de afiliados por cédula o nombre (port de frmBuscarxNombre).</summary>
public class AfiliadoBuscarTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static AfiliadoService Svc() => new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]
    public async Task Busca_por_cedula_prefijo()
    {
        var res = await Svc().BuscarAsync("41474882");
        Assert.Contains(res, a => a.CI == 41474882);
    }

    [Fact]
    public async Task Termino_vacio_devuelve_vacio()
        => Assert.Empty(await Svc().BuscarAsync("   "));

    [Fact]
    public async Task Multipalabra_matchea_apellido_y_nombre_en_cualquier_orden()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var a = await db.QuerySingleOrDefaultAsync<Sgpa.Domain.Entities.Afiliado>(
            @"SELECT TOP 1 CI, Apellido1, Nombres FROM dbo.Afiliado
              WHERE Apellido1 IS NOT NULL AND Nombres IS NOT NULL AND LEN(Apellido1) >= 4 AND LEN(Nombres) >= 4
              ORDER BY CI");
        Assert.NotNull(a);

        var palApellido = a!.Apellido1!.Split(' ')[0];
        var palNombre = a.Nombres!.Split(' ')[0];

        // "nombre apellido" (orden invertido respecto a la base) igual debe encontrarlo.
        var res = await Svc().BuscarAsync($"{palNombre} {palApellido}");
        Assert.Contains(res, x => x.CI == a.CI);
    }

    [Fact]
    public async Task Busca_por_apellido_y_arma_nombre_completo()
    {
        // Tomamos un apellido real de la base y verificamos que la búsqueda lo encuentre.
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var apellido = await db.ExecuteScalarAsync<string>(
            "SELECT TOP 1 Apellido1 FROM dbo.Afiliado WHERE Apellido1 IS NOT NULL AND LEN(Apellido1) >= 5 ORDER BY CI");
        Assert.False(string.IsNullOrWhiteSpace(apellido));

        var res = await Svc().BuscarAsync(apellido!);
        Assert.NotEmpty(res);
        Assert.All(res, a => Assert.False(string.IsNullOrWhiteSpace(a.NombreCompleto)));
    }
}
