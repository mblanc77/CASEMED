using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Sgpa.Data;
using Sgpa.Data.Configuracion;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: la configuración consolidada por tabla (inline/borrado/auditoría).</summary>
public class TablaConfigServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task Set_Get_round_trip_y_persistencia()
    {
        var factory = new SqlDbConnectionFactory(ConnectionString);
        var svc = new TablaConfigService(factory, NullLogger<TablaConfigService>.Instance);
        var tabla = "ZZ_Cfg_" + Guid.NewGuid().ToString("N");

        try
        {
            await svc.EnsureLoadedAsync();
            // Sin fila → default.
            Assert.Equal(TablaConfig.Default, svc.Get(tabla));

            var cfg = new TablaConfig(EdicionInline: true, ConfirmarBorrado: false, Auditar: true);
            await svc.SetAsync(tabla, cfg);
            Assert.Equal(cfg, svc.Get(tabla));

            // Otra instancia (cache fría) lo lee de la base.
            var svc2 = new TablaConfigService(factory, NullLogger<TablaConfigService>.Instance);
            await svc2.EnsureLoadedAsync();
            Assert.Equal(cfg, svc2.Get(tabla));

            // Los catálogos sembrados quedaron inline.
            Assert.True(svc.Get("Banco").EdicionInline);
        }
        finally
        {
            var db = new DbExecutor(factory);
            await db.ExecuteAsync("DELETE FROM dbo.TablaConfig WHERE Tabla=@t", new { t = tabla });
        }
    }
}
