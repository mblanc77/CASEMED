using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: paginado server-side (GetPageAsync) con búsqueda y filtro fijo.</summary>
[Collection("AfiliadoDb")]
public class PagingTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static DapperCrudService<T> Svc<T>() where T : class =>
        TestCrud.Create<T>(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)), new FakeCurrentUser());

    [Fact]
    public async Task Pagina_devuelve_tamano_y_total()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var total = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.Afiliado");

        var page = await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 20));

        Assert.Equal(20, page.Items.Count);
        Assert.Equal(total, page.TotalCount); // el total paginado coincide con el real (robusto a altas/bajas)
    }

    [Fact]
    public async Task Paginas_distintas_no_se_solapan()
    {
        var svc = Svc<Afiliado>();
        var p0 = await svc.GetPageAsync(new PageQuery(0, 10));
        var p1 = await svc.GetPageAsync(new PageQuery(10, 10));

        // Ordenado por CI (clave): la primera de la página 1 es mayor que la última de la página 0.
        Assert.NotEqual(p0.Items[0].CI, p1.Items[0].CI);
        Assert.True(p1.Items[0].CI > p0.Items[^1].CI);
    }

    [Fact]
    public async Task Busqueda_por_clave_encuentra_el_registro()
    {
        var page = await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 20, Search: "13010559"));

        Assert.True(page.TotalCount >= 1);
        Assert.Contains(page.Items, a => a.CI == 13010559);
    }

    [Fact]
    public async Task Filtro_fijo_pagina_solo_filas_del_padre()
    {
        var page = await Svc<Trabaja>().GetPageAsync(
            new PageQuery(0, 10, FilterColumn: "CI", FilterValue: 13010559L));

        Assert.Equal(4, page.TotalCount); // 4 empleos del afiliado
        Assert.All(page.Items, t => Assert.Equal(13010559, t.CI));
    }

    [Fact]
    public async Task Filtro_por_columna_igual()
    {
        var page = await Svc<Afiliado>().GetPageAsync(
            new PageQuery(0, 20, Filter: new FilterCompare("CI", FilterOp.Equal, 13010559L)));

        Assert.Equal(1, page.TotalCount);
        Assert.Equal(13010559, page.Items[0].CI);
    }

    [Fact]
    public async Task Filtro_grupo_OR_e_IN_equivalen()
    {
        var svc = Svc<Afiliado>();
        var or = await svc.GetPageAsync(new PageQuery(0, 20, Filter: new FilterGroup(false, new FilterNode[]
        {
            new FilterCompare("CI", FilterOp.Equal, 13010559L),
            new FilterCompare("CI", FilterOp.Equal, 12962731L),
        })));
        var inn = await svc.GetPageAsync(new PageQuery(0, 20,
            Filter: new FilterIn("CI", new object?[] { 13010559L, 12962731L })));

        Assert.Equal(2, or.TotalCount);
        Assert.Equal(2, inn.TotalCount);
    }

    [Fact]
    public async Task Orden_descendente_por_clave()
    {
        var page = await Svc<Afiliado>().GetPageAsync(
            new PageQuery(0, 10, Sort: new[] { new SortColumn("CI", Descending: true) }));

        var cis = page.Items.Select(a => a.CI).ToList();
        Assert.Equal(cis.OrderByDescending(x => x).ToList(), cis); // página ordenada desc
        Assert.True(cis[0] > cis[^1]);
    }
}
