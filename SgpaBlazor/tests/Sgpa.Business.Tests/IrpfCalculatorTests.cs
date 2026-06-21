using System.Threading.Tasks;
using Sgpa.Business.Subsidios;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Test de integración del IRPF contra NewSgpa2 (lee Parametros.BCP y FranjaIRPF reales).
/// Con BCP=6864 y las franjas migradas, IRPF(100000) = 0 + 3432 + 4704 = 8136.
/// </summary>
public class IrpfCalculatorTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task Calcula_IRPF_progresivo_por_franjas()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var calc = new IrpfCalculator(db);

        var irpf = await calc.CalcularAsync(100000m);

        Assert.Equal(8136m, irpf);
    }

    [Fact]
    public async Task IRPF_de_cero_es_cero()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var calc = new IrpfCalculator(db);

        Assert.Equal(0m, await calc.CalcularAsync(0m));
    }
}
