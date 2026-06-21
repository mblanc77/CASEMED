using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: el agrupamiento y los sumarios server-side (GROUP BY / agregados)
/// que habilitan agrupar y totalizar en las listas grandes (paridad XAF).
/// </summary>
public class CrudGroupingTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static DapperCrudService<Afiliado> Service()
        => TestCrud.Create<Afiliado>(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)), new FakeCurrentUser());

    [Fact]
    public async Task Los_conteos_por_grupo_suman_el_total()
    {
        var svc = Service();
        var ctx = new PageQuery(0, 1);

        var total = (await svc.GetPageAsync(ctx)).TotalCount;
        var groups = await svc.GetGroupsAsync("Sexo", descending: false, ctx, Array.Empty<SummarySpec>());

        Assert.Equal(total, groups.Sum(g => g.Count));
    }

    [Fact]
    public async Task El_sumario_total_coincide_con_la_suma_por_grupos()
    {
        var svc = Service();
        var ctx = new PageQuery(0, 1);
        var specs = new[] { new SummarySpec("CI", AggKind.Sum) };

        var groups = await svc.GetGroupsAsync("Sexo", descending: false, ctx, specs);
        var sumByGroups = groups.Sum(g => Convert.ToDouble(g.Summaries[0] ?? 0.0));

        var total = await svc.GetTotalSummaryAsync(specs, ctx);
        Assert.Single(total);
        Assert.Equal(Convert.ToDouble(total[0]), sumByGroups, 0);
    }

    [Fact]
    public async Task El_sumario_Count_da_el_total_de_filas()
    {
        var svc = Service();
        var ctx = new PageQuery(0, 1);

        var total = (await svc.GetPageAsync(ctx)).TotalCount;
        var count = await svc.GetTotalSummaryAsync(new[] { new SummarySpec("CI", AggKind.Count) }, ctx);

        Assert.Equal(total, Convert.ToInt32(count[0]));
    }
}
