using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Enforcement granular (campo + registro) del <see cref="DapperCrudService{T}"/> contra NewSgpa2, usando la tabla
/// Banco con filas de prueba propias (CodBanco 990011/990012) que se limpian al final.
/// </summary>
public class SeguridadGranularCrudTests : IAsyncLifetime
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";
    private const int Cod1 = 990011, Cod2 = 990012;

    private static DbExecutor NewDb() => new(new SqlDbConnectionFactory(ConnectionString));

    public async Task InitializeAsync()
    {
        var db = NewDb();
        await LimpiarAsync(db);
        var alta = TestCrud.Create<Banco>(db);
        await alta.InsertAsync(new Banco { CodBanco = Cod1, Descripcion = "BANCO UNO" });
        await alta.InsertAsync(new Banco { CodBanco = Cod2, Descripcion = "BANCO DOS" });
    }

    public async Task DisposeAsync() => await LimpiarAsync(NewDb());

    private static Task LimpiarAsync(IDbExecutor db) =>
        db.ExecuteAsync("DELETE FROM dbo.Banco WHERE CodBanco IN (@a, @b)", new { a = Cod1, b = Cod2 });

    private static UserSecurityContext Ctx(
        Dictionary<string, ColumnRule>? columnas = null,
        Dictionary<(string, PermissionAction), RecordRule>? registros = null) => new()
    {
        Login = "test",
        IsAdmin = false,
        TablePermissions = new Dictionary<string, PermissionAction>(StringComparer.OrdinalIgnoreCase)
        {
            ["Banco"] = PermissionAction.All
        },
        ColumnRules = columnas ?? new(StringComparer.OrdinalIgnoreCase),
        RecordRules = registros ?? new()
    };

    [Fact]
    public async Task Filtro_de_registro_lectura_solo_devuelve_las_filas_que_matchean()
    {
        var db = NewDb();
        var ctx = Ctx(registros: new()
        {
            [("Banco", PermissionAction.Read)] = new RecordRule(false, new[] { "dummy" })
        });
        // El compilador de prueba traduce el criterio a "CodBanco = 990011".
        var compiler = new TestCriteriaCompiler(new FilterCompare("CodBanco", FilterOp.Equal, Cod1));
        var svc = TestCrud.Create<Banco>(db, new FakeCurrentUser(ctx), compiler);

        var all = await svc.GetAllAsync();

        Assert.Contains(all, b => b.CodBanco == Cod1);
        Assert.DoesNotContain(all, b => b.CodBanco == Cod2);
    }

    [Fact]
    public async Task Columna_sin_permiso_de_lectura_vuelve_enmascarada()
    {
        var db = NewDb();
        var ctx = Ctx(columnas: new(StringComparer.OrdinalIgnoreCase)
        {
            [UserSecurityContext.Key("Banco", "Descripcion")] = new ColumnRule(Read: false, Write: true)
        });
        var svc = TestCrud.Create<Banco>(db, new FakeCurrentUser(ctx));

        var banco = await svc.GetByKeyAsync(new object?[] { Cod1 });

        Assert.NotNull(banco);
        Assert.Equal(Cod1, banco!.CodBanco);          // la clave nunca se enmascara
        Assert.Null(banco.Descripcion);               // enmascarada a NULL
    }

    [Fact]
    public async Task Columna_sin_permiso_de_escritura_no_se_persiste()
    {
        var db = NewDb();
        var ctx = Ctx(columnas: new(StringComparer.OrdinalIgnoreCase)
        {
            [UserSecurityContext.Key("Banco", "Descripcion")] = new ColumnRule(Read: true, Write: false)
        });
        var svc = TestCrud.Create<Banco>(db, new FakeCurrentUser(ctx));

        await svc.UpdateAsync(new Banco { CodBanco = Cod1, Descripcion = "CAMBIO PROHIBIDO" });

        // Releído sin restricciones: la descripción quedó como estaba (el SET excluyó la columna).
        var admin = TestCrud.Create<Banco>(db);
        var banco = await admin.GetByKeyAsync(new object?[] { Cod1 });
        Assert.Equal("BANCO UNO", banco!.Descripcion);
    }

    [Fact]
    public async Task Admin_ignora_enmascarado_y_filtros()
    {
        var db = NewDb();
        var adminCtx = new UserSecurityContext
        {
            Login = "adm", IsAdmin = true,
            ColumnRules = new Dictionary<string, ColumnRule>(StringComparer.OrdinalIgnoreCase)
            {
                [UserSecurityContext.Key("Banco", "Descripcion")] = new ColumnRule(false, false)
            },
            RecordRules = new Dictionary<(string, PermissionAction), RecordRule>
            {
                [("Banco", PermissionAction.Read)] = new RecordRule(false, new[] { "dummy" })
            }
        };
        var svc = TestCrud.Create<Banco>(db, new FakeCurrentUser(adminCtx),
            new TestCriteriaCompiler(new FilterCompare("CodBanco", FilterOp.Equal, Cod1)));

        var all = await svc.GetAllAsync();

        Assert.Contains(all, b => b.CodBanco == Cod1 && b.Descripcion == "BANCO UNO");   // sin enmascarar
        Assert.Contains(all, b => b.CodBanco == Cod2);                                   // sin filtro de registro
    }
}
