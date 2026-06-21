using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Sgpa.Data;
using Sgpa.Data.Auditoria;
using Sgpa.Data.Connection;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Integración contra NewSgpa2: la auditoría por campo (diff de alta/modificación/baja).</summary>
public class AuditServiceTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    [Fact]
    public async Task LogUpdate_registra_solo_los_campos_cambiados()
    {
        var db = new DbExecutor(new SqlDbConnectionFactory(ConnectionString));
        var svc = new AuditService(db, NullLogger<AuditService>.Instance);
        var meta = EntityMetadata.For<Banco>();
        const string clave = "999999";

        try
        {
            var viejo = new Banco { CodBanco = 999999, Descripcion = "Anterior" };
            var nuevo = new Banco { CodBanco = 999999, Descripcion = "Nuevo" };

            await svc.LogUpdateAsync(meta, viejo, nuevo, "qa");

            var rows = await db.QueryAsync<AuditCheck>(
                "SELECT Operacion, Campo, ValorAnterior, ValorNuevo FROM dbo.AuditCambio WHERE Tabla='Banco' AND Clave=@c",
                new { c = clave });

            // Sólo cambió Descripcion (CodBanco es la clave, no cambia; Usr/Ts son auditoría técnica, se excluyen).
            Assert.Single(rows);
            Assert.Equal("U", rows[0].Operacion);
            Assert.Equal("Descripcion", rows[0].Campo);
            Assert.Equal("Anterior", rows[0].ValorAnterior);
            Assert.Equal("Nuevo", rows[0].ValorNuevo);

            // Alta: registra el/los campos con valor.
            await svc.LogInsertAsync(meta, nuevo, "qa");
            var altas = await db.QueryAsync<AuditCheck>(
                "SELECT Operacion, Campo, ValorAnterior, ValorNuevo FROM dbo.AuditCambio WHERE Tabla='Banco' AND Clave=@c AND Operacion='I'",
                new { c = clave });
            Assert.Contains(altas, a => a.Campo == "Descripcion" && a.ValorNuevo == "Nuevo");
        }
        finally
        {
            await db.ExecuteAsync("DELETE FROM dbo.AuditCambio WHERE Tabla='Banco' AND Clave=@c", new { c = clave });
        }
    }

    private sealed record AuditCheck(string Operacion, string? Campo, string? ValorAnterior, string? ValorNuevo);
}
