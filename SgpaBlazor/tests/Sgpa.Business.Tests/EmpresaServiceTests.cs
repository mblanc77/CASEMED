using System.Threading.Tasks;
using Sgpa.Business.Empresas;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: auto-numeración de empresas (port de AbmEmpre).</summary>
public class EmpresaServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task ProximoCodEmpresa_es_max_mas_uno_excluyendo_la_900()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = new EmpresaService(db);

        var max = await db.ExecuteScalarAsync<int?>("SELECT MAX(CodEmpresa) FROM dbo.Empresa WHERE CodEmpresa<>900") ?? 0;
        Assert.Equal(max + 1, await svc.ProximoCodEmpresaAsync());
    }
}
