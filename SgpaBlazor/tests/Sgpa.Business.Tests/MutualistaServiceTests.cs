using System.Threading.Tasks;
using Sgpa.Business.Mutualistas;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: auto-numeración de mutualistas (port de AbmMutua).</summary>
public class MutualistaServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task ProximoCodMutualista_es_max_mas_uno()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = new MutualistaService(db);

        var max = await db.ExecuteScalarAsync<int?>("SELECT MAX(CodMutualista) FROM dbo.Mutualista") ?? 0;
        Assert.Equal(max + 1, await svc.ProximoCodMutualistaAsync());
    }
}
