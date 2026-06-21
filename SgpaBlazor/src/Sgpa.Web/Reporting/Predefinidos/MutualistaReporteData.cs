using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Una línea del listado de mutualistas (tabla dbo.Mutualista). Port de Mutualista.rpt (catálogo).</summary>
public sealed class MutualistaLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public int CodMutualista { get; set; }
    public string? Descrip { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public double Cuota { get; set; }

    public string NombreTexto => (Descrip ?? "").Trim();
    public string TelefonoTexto => (Telefono ?? "").Trim();
    public string DireccionTexto => (Direccion ?? "").Trim();
    public string CuotaFmt => ((decimal)Cuota).ToString("N2", EsUy);
}

/// <summary>Provee los datos de los reportes predefinidos de mutualistas desde NewSgpa2.</summary>
public interface IMutualistaReporteData
{
    Task<IReadOnlyList<MutualistaLinea>> GetListadoAsync(CancellationToken ct = default);
}

public sealed class MutualistaReporteData(IDbExecutor db) : IMutualistaReporteData
{
    public Task<IReadOnlyList<MutualistaLinea>> GetListadoAsync(CancellationToken ct = default)
        => db.QueryAsync<MutualistaLinea>(
            "SELECT CodMutualista, Descrip, Telefono, Direccion, Cuota FROM dbo.Mutualista ORDER BY Descrip",
            cancellationToken: ct);
}
