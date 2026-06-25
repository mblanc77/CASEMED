using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Verificación de FK del afiliado (port de txtCI_LostFocus / Bcpart.AfiliadoActivo). Las consultas a base
/// son de sólo lectura; el caso 0/inexistente no muta nada y valida además que el SQL/función portada exista.
/// </summary>
public class AfiliadoServiceFkTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static AfiliadoService NewService() => new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]
    public async Task ExisteAsync_cedula_cero_es_false_sin_tocar_base()
        => Assert.False(await new AfiliadoService(null!).ExisteAsync(0));

    [Fact]
    public async Task EsActivoAsync_cedula_cero_es_false_sin_tocar_base()
        => Assert.False(await new AfiliadoService(null!).EsActivoAsync(0));

    [Fact]
    public async Task ExisteAsync_cedula_inexistente_es_false()
        => Assert.False(await NewService().ExisteAsync(999_999_999));

    [Fact]
    public async Task EsActivoAsync_cedula_inexistente_es_false()   // valida que acc_sgpa_403_AportesUlt12xCI_q existe
        => Assert.False(await NewService().EsActivoAsync(999_999_999));
}
