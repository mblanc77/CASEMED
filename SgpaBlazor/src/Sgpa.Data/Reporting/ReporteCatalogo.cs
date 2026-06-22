namespace Sgpa.Data.Reporting;

/// <summary>Tipo de reporte a medida: asistido (por tabla/metadata) o SQL crudo.</summary>
public enum ReporteTipo { Asistido, Sql }

/// <summary>
/// Ítem unificado del catálogo de reportes a medida (cabecera común a ambos tipos), para la lista única y el menú.
/// <see cref="RootTable"/> aplica al tipo asistido (para el permiso por tabla); <see cref="SoloAdmin"/> al tipo SQL.
/// </summary>
public sealed record ReporteItem(ReporteTipo Tipo, int Id, string Nombre, DateTime Fecha, string? Login, string? RootTable, bool SoloAdmin)
{
    /// <summary>Ruta del viewer según el tipo.</summary>
    public string VerUrl => Tipo == ReporteTipo.Asistido ? $"/reportes/din/{Id}" : $"/reportes/sql/{Id}";
    /// <summary>Ruta del editor según el tipo.</summary>
    public string EditarUrl => Tipo == ReporteTipo.Asistido ? $"/reportes/din/editor/{Id}" : $"/reportes/sql/editor/{Id}";
}

/// <summary>Fachada de sólo lectura que unifica los dos tipos de reporte a medida en un único catálogo.</summary>
public interface IReportesMedidaCatalogo
{
    Task<IReadOnlyList<ReporteItem>> AllAsync(CancellationToken ct = default);
}

public sealed class ReportesMedidaCatalogo : IReportesMedidaCatalogo
{
    private readonly IReporteDinamicoService _din;
    private readonly IReporteSqlService _sql;
    public ReportesMedidaCatalogo(IReporteDinamicoService din, IReporteSqlService sql) { _din = din; _sql = sql; }

    public async Task<IReadOnlyList<ReporteItem>> AllAsync(CancellationToken ct = default)
    {
        var din = await _din.AllAsync(ct).ConfigureAwait(false);
        var sql = await _sql.AllAsync(ct).ConfigureAwait(false);
        return din
            .Select(r => new ReporteItem(ReporteTipo.Asistido, r.Id, r.Nombre, r.Fecha, r.Login, r.RootTable, false))
            .Concat(sql.Select(r => new ReporteItem(ReporteTipo.Sql, r.Id, r.Nombre, r.Fecha, r.Login, null, r.SoloAdmin)))
            .OrderBy(i => i.Nombre, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
