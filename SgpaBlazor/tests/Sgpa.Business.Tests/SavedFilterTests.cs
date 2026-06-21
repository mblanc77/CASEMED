using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Round-trip de filtros guardados (dbo.SgpaFiltro) contra NewSgpa2: guardar, listar, borrar.</summary>
public class SavedFilterTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static DapperSavedFilterService Svc() =>
        new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]
    public async Task Guardar_listar_y_borrar()
    {
        var svc = Svc();
        const string entity = "ZZ_FilterTest";
        const string user = "tester";

        // limpieza previa por las dudas
        foreach (var prev in await svc.GetForEntityAsync(entity, user))
            await svc.DeleteAsync(prev.Id);

        await svc.SaveAsync(entity, "Filtro 1", "[CI] = 13010559", user);
        var list = await svc.GetForEntityAsync(entity, user);
        Assert.Contains(list, f => f.Nombre == "Filtro 1" && f.Criteria == "[CI] = 13010559");

        // upsert: mismo nombre actualiza, no duplica
        await svc.SaveAsync(entity, "Filtro 1", "[CI] = 999", user);
        var list2 = await svc.GetForEntityAsync(entity, user);
        Assert.Single(list2, f => f.Nombre == "Filtro 1");
        Assert.Equal("[CI] = 999", list2.Single(f => f.Nombre == "Filtro 1").Criteria);

        // otro usuario no lo ve
        var otro = await svc.GetForEntityAsync(entity, "otro");
        Assert.DoesNotContain(otro, f => f.Nombre == "Filtro 1");

        foreach (var f in list2) await svc.DeleteAsync(f.Id);
        Assert.Empty(await svc.GetForEntityAsync(entity, user));
    }
}
