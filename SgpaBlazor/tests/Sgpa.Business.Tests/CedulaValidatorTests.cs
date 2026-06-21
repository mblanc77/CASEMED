using System.Threading.Tasks;
using Sgpa.Business.Afiliados;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Validación del dígito verificador de la cédula uruguaya (port de ChkCedula). Función pura.</summary>
public class CedulaValidatorTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Theory]
    [InlineData(12345672, true)]   // dígito verificador correcto (oráculo)
    [InlineData(12345673, false)]  // mismo número, dígito alterado
    [InlineData(0, false)]
    [InlineData(-5, false)]
    public void EsValida_verifica_el_digito(long cedula, bool esperado)
        => Assert.Equal(esperado, CedulaValidator.EsValida(cedula));

    [Fact]
    public async Task Las_cedulas_reales_de_afiliados_son_validas()
    {
        var db = new Sgpa.Data.DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var cis = await db.QueryAsync<long>(
            "SELECT TOP 50 CI FROM dbo.Afiliado WHERE CI BETWEEN 1000000 AND 60000000 ORDER BY CI DESC");

        Assert.NotEmpty(cis);
        Assert.All(cis, ci => Assert.True(CedulaValidator.EsValida(ci), $"CI real {ci} debería validar"));
    }
}
