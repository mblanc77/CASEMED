using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: resolución dirigida de descripciones FK (GetDescriptionsAsync), usada por
/// las grillas para mostrar la descripción en lugar del código aun cuando la tabla destino sea grande.
/// </summary>
public class DapperLookupServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task GetDescriptions_resuelve_las_claves_pedidas_por_IN()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = new DapperLookupService(db);

        var row = await db.QuerySingleOrDefaultAsync<EmpRow>(
            "SELECT TOP 1 CodEmpresa, Nombre FROM dbo.Empresa WHERE Nombre IS NOT NULL ORDER BY CodEmpresa");
        Assert.NotNull(row);

        var map = await svc.GetDescriptionsAsync(typeof(Empresa), new object[] { row!.CodEmpresa });

        var clave = row.CodEmpresa.ToString();
        Assert.True(map.ContainsKey(clave));               // resuelve por valor.ToString() (sin desajuste de tipo)
        Assert.Equal(row.Nombre, map[clave]);
    }

    [Fact]
    public async Task GetDescriptions_sin_claves_devuelve_vacio()
    {
        var svc = new DapperLookupService(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));
        var map = await svc.GetDescriptionsAsync(typeof(Empresa), System.Array.Empty<object>());
        Assert.Empty(map);
    }

    private sealed record EmpRow(int CodEmpresa, string? Nombre);
}
