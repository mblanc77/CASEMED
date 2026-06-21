using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Dashboard;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: los indicadores del dashboard de inicio.</summary>
public class DashboardServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task GetAsync_trae_indicadores_consistentes()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = new DashboardService(db);

        var d = await svc.GetAsync();

        Assert.True(d.Afiliados > 0);
        Assert.True(d.AfiliadosActivos > 0 && d.AfiliadosActivos <= d.Afiliados);
        Assert.True(d.Prestamos > 0);
        Assert.True(d.PrestamosVigentes <= d.Prestamos);
        Assert.True(d.ImportePendiente >= 0);
        Assert.NotEmpty(d.PrestamosPorEstado);
        // El desglose excluye cancelados/anulados → suma los préstamos vigentes.
        Assert.Equal(d.PrestamosVigentes, d.PrestamosPorEstado.Sum(e => e.Cantidad));
    }
}
