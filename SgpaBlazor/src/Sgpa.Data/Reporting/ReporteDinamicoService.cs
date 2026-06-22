using System.Text.Json;

namespace Sgpa.Data.Reporting;

/// <summary>Cabecera de un reporte dinámico (sin la definición), para listar en el menú / administración.</summary>
public sealed record ReporteDinamicoInfo(int Id, string Nombre, string RootTable, DateTime Fecha, string? Login);

/// <summary>Reporte dinámico completo: cabecera + definición deserializada.</summary>
public sealed record ReporteDinamico(int Id, string Nombre, ReporteDinamicoDef Def);

/// <summary>Persistencia de los reportes dinámicos creados por el administrador (tabla <c>dbo.ReporteDinamico</c>).</summary>
public interface IReporteDinamicoService
{
    /// <summary>Cabeceras de todos los reportes activos, ordenadas por nombre.</summary>
    Task<IReadOnlyList<ReporteDinamicoInfo>> AllAsync(CancellationToken ct = default);

    /// <summary>Reporte completo (cabecera + definición) o null si no existe / está inactivo.</summary>
    Task<ReporteDinamico?> GetAsync(int id, CancellationToken ct = default);

    /// <summary>Crea un reporte y devuelve su Id.</summary>
    Task<int> CreateAsync(string nombre, ReporteDinamicoDef def, string login, CancellationToken ct = default);

    /// <summary>Actualiza nombre + definición.</summary>
    Task UpdateAsync(int id, string nombre, ReporteDinamicoDef def, CancellationToken ct = default);

    Task RenameAsync(int id, string nombre, CancellationToken ct = default);

    /// <summary>Baja lógica (Activo = 0).</summary>
    Task DeleteAsync(int id, CancellationToken ct = default);
}

public sealed class DapperReporteDinamicoService : IReporteDinamicoService
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    private readonly IDbExecutor _db;
    public DapperReporteDinamicoService(IDbExecutor db) => _db = db;

    public Task<IReadOnlyList<ReporteDinamicoInfo>> AllAsync(CancellationToken ct = default)
        => _db.QueryAsync<ReporteDinamicoInfo>(
            @"SELECT Id, Nombre, RootTable, Fecha, Login FROM dbo.ReporteDinamico
              WHERE Activo = 1 ORDER BY Nombre",
            cancellationToken: ct);

    public async Task<ReporteDinamico?> GetAsync(int id, CancellationToken ct = default)
    {
        var row = await _db.QuerySingleOrDefaultAsync<Row>(
            "SELECT Id, Nombre, DefJson FROM dbo.ReporteDinamico WHERE Id = @id AND Activo = 1",
            new { id }, cancellationToken: ct).ConfigureAwait(false);
        if (row is null) return null;
        var def = JsonSerializer.Deserialize<ReporteDinamicoDef>(row.DefJson, Json) ?? new ReporteDinamicoDef();
        return new ReporteDinamico(row.Id, row.Nombre, def);
    }

    public Task<int> CreateAsync(string nombre, ReporteDinamicoDef def, string login, CancellationToken ct = default)
        => _db.ExecuteScalarAsync<int>(
            @"INSERT INTO dbo.ReporteDinamico (Nombre, RootTable, DefJson, Activo, Login, Fecha)
              VALUES (@nombre, @rootTable, @defJson, 1, @login, SYSDATETIME());
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { nombre, rootTable = def.RootTable, defJson = JsonSerializer.Serialize(def, Json), login },
            cancellationToken: ct);

    public Task UpdateAsync(int id, string nombre, ReporteDinamicoDef def, CancellationToken ct = default)
        => _db.ExecuteAsync(
            @"UPDATE dbo.ReporteDinamico
              SET Nombre = @nombre, RootTable = @rootTable, DefJson = @defJson, Fecha = SYSDATETIME()
              WHERE Id = @id",
            new { id, nombre, rootTable = def.RootTable, defJson = JsonSerializer.Serialize(def, Json) },
            cancellationToken: ct);

    public Task RenameAsync(int id, string nombre, CancellationToken ct = default)
        => _db.ExecuteAsync(
            "UPDATE dbo.ReporteDinamico SET Nombre = @nombre, Fecha = SYSDATETIME() WHERE Id = @id",
            new { id, nombre }, cancellationToken: ct);

    public Task DeleteAsync(int id, CancellationToken ct = default)
        => _db.ExecuteAsync("UPDATE dbo.ReporteDinamico SET Activo = 0 WHERE Id = @id",
            new { id }, cancellationToken: ct);

    private sealed record Row(int Id, string Nombre, string DefJson);
}
