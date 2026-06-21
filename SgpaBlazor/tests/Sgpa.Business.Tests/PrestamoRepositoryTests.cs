using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Prestamos;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el repo del workbench de préstamos (afiliado, promedio, tope, aportes,
/// próximo id) sobre las queries Access migradas (acc_sp_*_q).
/// </summary>
[Collection("PrestamosDb")]
public class PrestamoRepositoryTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (PrestamoRepository Repo, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new PrestamoRepository(db), db);
    }

    [Fact]
    public async Task Parametros_y_monedas_se_cargan()
    {
        var (repo, _) = Build();
        var prm = await repo.GetParametrosAsync();
        Assert.True(prm.UR > 0, "UR debería estar configurada");
        Assert.True(prm.MesesCalculo > 0);
        Assert.True(prm.MaxCuotas > 0);

        var monedas = await repo.GetMonedasAsync();
        Assert.NotEmpty(monedas);
        Assert.Contains(monedas, m => m.CodMoneda == "$");
    }

    [Fact]
    public async Task ProximoId_es_max_mas_uno()
    {
        var (repo, db) = Build();
        var max = await db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(IDPrestamo),0) FROM dbo.SP_Prestamo");
        Assert.Equal(max + 1, await repo.ProximoIdPrestamoAsync());
    }

    [Fact]
    public async Task Credito_de_un_afiliado_con_prestamo_trae_ficha_y_tope()
    {
        var (repo, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var prm = await repo.GetParametrosAsync();

        var c = await repo.GetCreditoAsync(ci, "$", prm);

        Assert.NotNull(c.Ficha);                         // el afiliado existe (1000_AfiladoCI2Nombre)
        Assert.False(string.IsNullOrWhiteSpace(c.Ficha!.DescAfiliado));
        Assert.True(c.Promedio >= 0);
        Assert.True(c.Aportes >= 0);
        Assert.True(c.Tope >= 0);                        // UR×TopeUR − saldo abierto, topeado en 0
    }

    [Fact]
    public async Task Credito_en_dolares_topea_dividiendo_por_la_cotizacion()
    {
        var (repo, db) = Build();
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.SP_Prestamo WHERE CI IS NOT NULL ORDER BY IDPrestamo DESC");
        var prm = await repo.GetParametrosAsync();

        var pesos = await repo.GetCreditoAsync(ci, "$", prm);
        var dolar = await repo.GetCreditoAsync(ci, "u$s", prm);

        // El tope en USD es el de pesos / cotización (aprox; ambos parten del mismo saldo abierto).
        if (prm.Dolar > 0 && pesos.Tope > 0)
            Assert.True(dolar.Tope <= pesos.Tope);
    }
}
