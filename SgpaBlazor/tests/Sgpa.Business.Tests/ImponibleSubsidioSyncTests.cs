using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sgpa.Business.Subsidios;
using Sgpa.Data;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Orquestación CUD → Imponible emp900: al dar de alta/baja un SubsidioCabezal (vía el CRUD genérico con el
/// handler enganchado) se mantiene la fila Imponible emp900 (Concepto='1') con SUM(ImpNominal) del período.
/// Todo corre en una transacción con ROLLBACK sobre un período centinela (2099/12), sin tocar datos reales.
/// </summary>
public class ImponibleSubsidioSyncTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private const int Anio = 2099;
    private const byte Mes = 12;

    [Fact]
    public async Task Alta_y_baja_de_cabezal_mantiene_imponible_emp900()
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var db = new ScopedDbExecutor(cn, tx);

        // Afiliado con registro Trabaja emp900 (ancla del imponible: Fechaingreso/IdTrabaja).
        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Trabaja WHERE CodEmpresa = 900 ORDER BY CI");
        var baseId = await db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(IdSubsidio), 0) FROM dbo.SubsidioCabezal");

        ICurrentUser user = new FakeCurrentUser();
        var crud = TestCrud.Create<SubsidioCabezal>(db, user,
            new SubsidioCabezalImponibleHandler(new ImponibleSubsidioSync(), user));

        // Alta del primer cabezal liquidado → aparece la fila emp900 con Importe = ImpNominal, 30 días.
        await crud.InsertAsync(Cabezal(baseId + 1, ci, impNominal: 1000));
        Assert.Equal(1000d, (await ImporteEmp900(db, ci))!.Value, 3);
        Assert.Equal(30, await DiasEmp900(db, ci));

        // Segundo cabezal mismo período → la fila se recalcula como suma (1000 + 250).
        await crud.InsertAsync(Cabezal(baseId + 2, ci, impNominal: 250));
        Assert.Equal(1250d, (await ImporteEmp900(db, ci))!.Value, 3);

        // Baja de ambos → ya no quedan cabezales liquidados → la fila emp900 se borra.
        await crud.DeleteAsync(Cabezal(baseId + 1, ci, impNominal: 1000));
        await crud.DeleteAsync(Cabezal(baseId + 2, ci, impNominal: 250));
        Assert.Null(await ImporteEmp900(db, ci));

        await tx.RollbackAsync();
    }

    [Fact]
    public async Task BorradoMasivo_porClave_sincroniza_imponible_emp900()
    {
        await using var cn = new SqlConnection(ConnectionString);
        await cn.OpenAsync();
        await using var tx = (SqlTransaction)await cn.BeginTransactionAsync();
        var db = new ScopedDbExecutor(cn, tx);

        var ci = await db.ExecuteScalarAsync<long>("SELECT TOP 1 CI FROM dbo.Trabaja WHERE CodEmpresa = 900 ORDER BY CI");
        var baseId = await db.ExecuteScalarAsync<int>("SELECT ISNULL(MAX(IdSubsidio), 0) FROM dbo.SubsidioCabezal");

        ICurrentUser user = new FakeCurrentUser();
        var crud = TestCrud.Create<SubsidioCabezal>(db, user,
            new SubsidioCabezalImponibleHandler(new ImponibleSubsidioSync(), user));

        await crud.InsertAsync(Cabezal(baseId + 1, ci, impNominal: 1000));
        await crud.InsertAsync(Cabezal(baseId + 2, ci, impNominal: 250));
        Assert.Equal(1250d, (await ImporteEmp900(db, ci))!.Value, 3);

        // Borrado masivo con entidades SÓLO-CLAVE (como "seleccionar todo del filtro", sin CI/Año/Mes):
        // DeleteManyAsync debe releer la fila completa para que el handler limpie el imponible emp900.
        var soloClave = new List<SubsidioCabezal>
        {
            new() { IdSubsidio = baseId + 1 },
            new() { IdSubsidio = baseId + 2 },
        };
        await crud.DeleteManyAsync(soloClave);
        Assert.Null(await ImporteEmp900(db, ci));

        await tx.RollbackAsync();
    }

    private static SubsidioCabezal Cabezal(int id, long ci, double impNominal) => new()
    {
        IdSubsidio = id, CI = ci, Anio = Anio, Mes = Mes, Liquidar = true,
        ValorJornal = 0, Dias = 30, ImpNominal = impNominal, ImpAguinaldo = 0, ImpLiquido = 0
    };

    private static Task<double?> ImporteEmp900(IDbExecutor db, long ci) =>
        db.QuerySingleOrDefaultAsync<double?>(
            "SELECT Importe FROM dbo.Imponible WHERE CI=@ci AND CodEmpresa=900 AND Concepto='1' AND Anio=@Anio AND Mes=@Mes",
            new { ci, Anio, Mes });

    private static Task<int?> DiasEmp900(IDbExecutor db, long ci) =>
        db.QuerySingleOrDefaultAsync<int?>(
            "SELECT DiasTrabajados FROM dbo.Imponible WHERE CI=@ci AND CodEmpresa=900 AND Concepto='1' AND Anio=@Anio AND Mes=@Mes",
            new { ci, Anio, Mes });

    [Fact]
    public async Task Update_con_cambio_de_periodo_sincroniza_viejo_y_nuevo()
    {
        var sync = new RecordingSync();
        var handler = new SubsidioCabezalImponibleHandler(sync, new FakeCurrentUser());

        var actual = new SubsidioCabezal { CI = 100, Anio = 2026, Mes = 5, Liquidar = true };
        var previo = new SubsidioCabezal { CI = 100, Anio = 2026, Mes = 4, Liquidar = true };

        await handler.HandleAsync(new EntityChange<SubsidioCabezal>(EntityChangeKind.Updated, actual, previo, null!));

        Assert.Contains((2026, 5, (long?)100), sync.Calls);
        Assert.Contains((2026, 4, (long?)100), sync.Calls);
        Assert.Equal(2, sync.Calls.Count);
    }

    private sealed class RecordingSync : IImponibleSubsidioSync
    {
        public System.Collections.Generic.List<(int Anio, int Mes, long? Ci)> Calls { get; } = new();
        public Task SincronizarPeriodoAsync(IDbExecutor db, int anio, int mes, long? ci, string usr, CancellationToken ct = default)
        {
            Calls.Add((anio, mes, ci));
            return Task.CompletedTask;
        }
    }
}
