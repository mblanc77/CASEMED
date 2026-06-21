using Sgpa.Data;

namespace Sgpa.Web.Reporting;

/// <summary>Metadata de un reporte guardado (sin el layout binario).</summary>
public sealed record ReporteInfo(int Id, string Nombre, string? TablaRoot, DateTime Fecha);

/// <summary>
/// Catálogo de reportes (dbo.Reporte) para la gestión y para listarlos desde el ListView de su tabla root.
/// El layout (.repx) lo maneja <see cref="SgpaReportStorage"/>; acá sólo la metadata (Nombre/TablaRoot).
/// </summary>
public interface IReporteCatalogo
{
    /// <summary>Crea el reporte (Nombre + tabla root) con un layout inicial ya bindeado a esa tabla. Devuelve el Id.</summary>
    Task<int> CreateAsync(string nombre, string tablaRoot, string login, CancellationToken ct = default);
    Task<IReadOnlyList<ReporteInfo>> AllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<ReporteInfo>> ForTableAsync(string tabla, CancellationToken ct = default);
    Task<ReporteInfo?> GetAsync(int id, CancellationToken ct = default);
    Task SetTablaRootAsync(int id, string? tablaRoot, CancellationToken ct = default);
    Task RenameAsync(int id, string nombre, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}

public sealed class ReporteCatalogo(IDbExecutor db) : IReporteCatalogo
{
    private const string Select = "SELECT Id, Nombre, TablaRoot, Fecha FROM dbo.Reporte";

    public async Task<int> CreateAsync(string nombre, string tablaRoot, string login, CancellationToken ct = default)
    {
        var layout = ReportLayoutBuilder.BuildInitialLayout(tablaRoot);
        return await db.ExecuteScalarAsync<int>(
            @"INSERT INTO dbo.Reporte (Nombre, TablaRoot, Layout, Login)
              VALUES (@nombre, @tablaRoot, @layout, @login);
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { nombre, tablaRoot, layout, login }, cancellationToken: ct);
    }

    public Task<IReadOnlyList<ReporteInfo>> AllAsync(CancellationToken ct = default)
        => db.QueryAsync<ReporteInfo>($"{Select} ORDER BY Nombre", cancellationToken: ct);

    public Task<IReadOnlyList<ReporteInfo>> ForTableAsync(string tabla, CancellationToken ct = default)
        => db.QueryAsync<ReporteInfo>($"{Select} WHERE TablaRoot = @tabla ORDER BY Nombre",
            new { tabla }, cancellationToken: ct);

    public Task<ReporteInfo?> GetAsync(int id, CancellationToken ct = default)
        => db.QuerySingleOrDefaultAsync<ReporteInfo>($"{Select} WHERE Id = @id", new { id }, cancellationToken: ct);

    public Task SetTablaRootAsync(int id, string? tablaRoot, CancellationToken ct = default)
        => db.ExecuteAsync("UPDATE dbo.Reporte SET TablaRoot = @tablaRoot WHERE Id = @id",
            new { id, tablaRoot }, cancellationToken: ct);

    public Task RenameAsync(int id, string nombre, CancellationToken ct = default)
        => db.ExecuteAsync("UPDATE dbo.Reporte SET Nombre = @nombre WHERE Id = @id",
            new { id, nombre }, cancellationToken: ct);

    public Task DeleteAsync(int id, CancellationToken ct = default)
        => db.ExecuteAsync("DELETE FROM dbo.Reporte WHERE Id = @id", new { id }, cancellationToken: ct);
}
