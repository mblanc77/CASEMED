using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Atomicidad real del DbTransactionScope contra NewSgpa2 (usa seg.Rol, esquema propio).</summary>
public class DbTransactionScopeTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";
    private const string Nombre = "ZZ_TX_TEST";

    private static DbExecutor NewDb() => new(new SqlDbConnectionFactory(ConnectionString));

    [Fact]
    public async Task Rollback_no_persiste()
    {
        var db = NewDb();
        await db.ExecuteAsync("DELETE FROM seg.Rol WHERE Nombre = @n", new { n = Nombre });

        await using (var uow = await db.BeginTransactionAsync())
        {
            await uow.ExecuteAsync("INSERT INTO seg.Rol (Nombre, EsAdmin) VALUES (@n, 0)", new { n = Nombre });
            Assert.Equal(1, await uow.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @n", new { n = Nombre }));
            await uow.RollbackAsync();
        }

        Assert.Equal(0, await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @n", new { n = Nombre }));
    }

    [Fact]
    public async Task Dispose_sin_commit_hace_rollback()
    {
        var db = NewDb();
        await db.ExecuteAsync("DELETE FROM seg.Rol WHERE Nombre = @n", new { n = Nombre });

        await using (var uow = await db.BeginTransactionAsync())
            await uow.ExecuteAsync("INSERT INTO seg.Rol (Nombre, EsAdmin) VALUES (@n, 0)", new { n = Nombre });
        // se descarta sin Commit -> rollback automático

        Assert.Equal(0, await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @n", new { n = Nombre }));
    }

    [Fact]
    public async Task Commit_persiste()
    {
        var db = NewDb();
        await db.ExecuteAsync("DELETE FROM seg.Rol WHERE Nombre = @n", new { n = Nombre });
        try
        {
            await using (var uow = await db.BeginTransactionAsync())
            {
                await uow.ExecuteAsync("INSERT INTO seg.Rol (Nombre, EsAdmin) VALUES (@n, 0)", new { n = Nombre });
                await uow.CommitAsync();
            }
            Assert.Equal(1, await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM seg.Rol WHERE Nombre = @n", new { n = Nombre }));
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM seg.Rol WHERE Nombre = @n", new { n = Nombre });
        }
    }
}
