using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Security;
using Sgpa.Domain.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Verifica contra NewSgpa2 cómo el servicio de seguridad resuelve el contexto del usuario 'marce'.</summary>
public class SecurityContextTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private static DapperSecurityService Svc() =>
        new(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)));

    [Fact]
    public async Task Marce_es_admin_con_permisos_totales()
    {
        var ctx = await Svc().LoadContextAsync("marce");

        Assert.NotNull(ctx);
        Assert.True(ctx!.IsAdmin, "marce debería ser admin (tiene rol EsAdmin)");
        // El bypass de admin concede todas las acciones, incluida Create, sobre cualquier tabla.
        var perms = ctx.PermissionsFor("Afiliado");
        Assert.True((perms & PermissionAction.Create) != 0, "admin debe tener Create sobre Afiliado");
        Assert.Equal(PermissionAction.All, perms);
    }
}
