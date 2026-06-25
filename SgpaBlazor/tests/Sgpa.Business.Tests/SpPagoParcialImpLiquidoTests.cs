using System.Threading.Tasks;
using Sgpa.Business.Pagos;
using Sgpa.Business.Prestamos;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Lecturas de los dos servicios portados del SP (pago parcial e importe líquido) contra NewSgpa2. Son de sólo
/// lectura sobre claves inexistentes: no mutan nada y validan que el SQL y los nombres de tabla SP_* sean válidos.
/// </summary>
public class SpPagoParcialImpLiquidoTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static DbExecutor NewDb() => new(new SqlDbConnectionFactory(ConnectionString));

    [Fact]
    public async Task PagoParcial_prestamo_inexistente_total_cero_sin_pagos()
    {
        var s = new PagoParcialService(NewDb(), new FakeCurrentUser());
        Assert.Empty(await s.GetPagosAsync(int.MaxValue));
        Assert.Equal(0d, await s.GetTotalAsync(int.MaxValue));
        Assert.Equal(1d, await s.GetTasaCambioAsync(int.MaxValue));   // default cuando no hay moneda
    }

    [Fact]
    public async Task ImpLiquido_cedula_inexistente_sin_empresas_ni_liquidos()
    {
        var s = new ImpLiquidoService(NewDb(), new FakeCurrentUser());
        Assert.Empty(await s.GetEmpresasAsync(999_999_999));
        Assert.Empty(await s.GetLiquidosAsync(999_999_999, 1));
    }

    [Fact]   // fuerza la materialización de filas reales: valida el mapeo de tipos (Mes tinyint, CodEmpresa smallint)
    public async Task ImpLiquido_materializa_filas_reales_si_hay_datos()
    {
        var db = NewDb();
        var s = new ImpLiquidoService(db, new FakeCurrentUser());

        var ci = await db.ExecuteScalarAsync<int?>(
            "SELECT TOP 1 CI FROM dbo.SP_Trabaja WHERE FechaBaja IS NULL AND CI IS NOT NULL ORDER BY CI");
        if (ci is null) return;   // sin datos en este entorno: nada que materializar
        Assert.NotEmpty(await s.GetEmpresasAsync(ci.Value));

        var fila = await db.QuerySingleOrDefaultAsync<(int CI, int Cod)>(
            "SELECT TOP 1 CI, CAST(CodEmpresa AS int) Cod FROM dbo.SP_ImpLiquido ORDER BY CI");
        if (fila.CI != 0)
            _ = await s.GetLiquidosAsync(fila.CI, fila.Cod);   // no debe arrojar al materializar
    }
}
