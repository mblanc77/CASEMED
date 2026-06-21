using System;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Business.Prestaciones;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: receta óptica de prestaciones (port del cluster Receta de AbmPrest).
/// El test de grabado usa una fecha lejana para no chocar con recetas reales y limpia lo que inserta.
/// </summary>
public class RecetaServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static (RecetaService Svc, IDbExecutor Db) Build()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        return (new RecetaService(db, new DefaultCurrentUser()), db);
    }

    [Fact]
    public async Task LlevaReceta_refleja_el_flag_del_tipo()
    {
        var (svc, db) = Build();
        var conReceta = await db.ExecuteScalarAsync<int>("SELECT TOP 1 CodPrestacionTipo FROM dbo.PrestacionTipo WHERE Receta=1 ORDER BY CodPrestacionTipo");
        var sinReceta = await db.ExecuteScalarAsync<int?>("SELECT TOP 1 CodPrestacionTipo FROM dbo.PrestacionTipo WHERE Receta=0 ORDER BY CodPrestacionTipo");

        Assert.True(await svc.LlevaRecetaAsync(conReceta));
        if (sinReceta is not null)
            Assert.False(await svc.LlevaRecetaAsync(sinReceta.Value));
    }

    [Fact]
    public async Task GetTiposConReceta_devuelve_solo_los_que_llevan()
    {
        var (svc, db) = Build();
        var tipos = await svc.GetTiposConRecetaAsync();
        Assert.NotEmpty(tipos);
        var total = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.PrestacionTipo WHERE Receta=1");
        Assert.Equal(total, tipos.Count);
    }

    [Fact]
    public async Task Grabar_reemplaza_las_recetas_de_cerca_y_lejos()
    {
        var (svc, db) = Build();
        var tipo = await db.ExecuteScalarAsync<int>("SELECT TOP 1 CodPrestacionTipo FROM dbo.PrestacionTipo WHERE Receta=1 ORDER BY CodPrestacionTipo");
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Afiliado WHERE CI BETWEEN 1000000 AND 60000000 ORDER BY CI DESC");
        var fecha = new DateTime(2099, 1, 1);

        try
        {
            // Alta de cerca + lejos.
            await svc.GrabarAsync(ci, fecha, tipo,
                cerca: new Receta { Esf_I = 1.5f, Esf_D = 1.25f, Cil_I = -0.5f, Cil_D = -0.75f },
                lejos: new Receta { Esf_I = 2f, Esf_D = 2.25f, Cil_I = -1f, Cil_D = -1.25f });
            var dos = await svc.GetAsync(ci, fecha, tipo);
            Assert.Equal(2, dos.Count);
            Assert.Equal(1.5f, dos.Single(r => r.CodRecetaDistancia == "cer").Esf_I);

            // Re-grabar sólo cerca → borra la de lejos y deja una.
            await svc.GrabarAsync(ci, fecha, tipo,
                cerca: new Receta { Esf_I = 3f }, lejos: null);
            var una = await svc.GetAsync(ci, fecha, tipo);
            Assert.Single(una);
            Assert.Equal("cer", una[0].CodRecetaDistancia);
            Assert.Equal(3f, una[0].Esf_I);

            // Re-grabar sin ninguna → no queda receta.
            await svc.GrabarAsync(ci, fecha, tipo, cerca: null, lejos: null);
            Assert.Empty(await svc.GetAsync(ci, fecha, tipo));
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.Receta WHERE CI=@ci AND Fecha=@fecha AND CodPrestacionTipo=@tipo",
                new { ci, fecha, tipo });
        }
    }
}
