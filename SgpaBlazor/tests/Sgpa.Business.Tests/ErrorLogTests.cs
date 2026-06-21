using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Errors;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: el logging de errores a la tabla dbo.Z_ErrorLog (best-effort).</summary>
public class ErrorLogTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task LogAsync_registra_una_fila_en_Z_ErrorLog()
    {
        var factory = new SqlDbConnectionFactory(ConnectionString);
        var log = new ErrorLog(NullLogger<ErrorLog>.Instance, factory);
        var db = new DbExecutor(factory);
        var origen = "Test." + Guid.NewGuid().ToString("N");

        try
        {
            await log.LogAsync(origen, new InvalidOperationException("prueba de logging de error"), usuario: "qa");

            var row = await db.QuerySingleOrDefaultAsync<LogRow>(
                "SELECT TOP 1 Login, Mensaje FROM dbo.Z_ErrorLog WHERE Origen=@o ORDER BY Id DESC", new { o = origen });
            Assert.NotNull(row);
            Assert.Equal("qa", row!.Login);
            Assert.Contains("logging de error", row.Mensaje);
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.Z_ErrorLog WHERE Origen=@o", new { o = origen });
        }
    }

    private sealed record LogRow(string? Login, string? Mensaje);
}
