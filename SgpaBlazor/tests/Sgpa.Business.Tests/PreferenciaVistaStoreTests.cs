using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Preferencias;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: persistencia de la personalización por usuario+pantalla.</summary>
public class PreferenciaVistaStoreTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task Save_Get_Reset_round_trip()
    {
        var factory = new SqlDbConnectionFactory(ConnectionString);
        var db = new DbExecutor(factory);
        var store = new DapperPreferenciaVistaStore(db, NullLogger<DapperPreferenciaVistaStore>.Instance);
        const string login = "qa";
        var vista = "Test:" + Guid.NewGuid().ToString("N");

        try
        {
            // Sin personalizar todavía.
            Assert.Null(await store.GetAsync(login, vista));

            // Alta.
            await store.SaveAsync(login, vista, "{\"v\":1}");
            Assert.Equal("{\"v\":1}", await store.GetAsync(login, vista));

            // Upsert (mismo par Login+Vista).
            await store.SaveAsync(login, vista, "{\"v\":2}");
            Assert.Equal("{\"v\":2}", await store.GetAsync(login, vista));

            // Restablecer borra la fila.
            await store.ResetAsync(login, vista);
            Assert.Null(await store.GetAsync(login, vista));
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.PreferenciaVista WHERE Login=@l AND Vista=@v",
                new { l = login, v = vista });
        }
    }
}
