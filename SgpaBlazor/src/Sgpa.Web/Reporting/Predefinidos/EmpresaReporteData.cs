using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Una línea del resumen de aportes de la empresa (agregado mensual de dbo.Rpt_Aporte). Port de AportesEmpresa.rpt.</summary>
public sealed class EmpresaAporteLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public int CodEmpresa { get; set; }
    public string? DescEmpresa { get; set; }
    public int Anio { get; set; }
    public int Mes { get; set; }
    public int Afiliados { get; set; }
    public double ImporteAporte { get; set; }
    public double ImporteAguinaldo { get; set; }

    public string PeriodoFmt => Mes >= 1 && Mes <= 12 ? $"{Mes:00}/{Anio}" : Anio.ToString();
    public string ImporteAporteFmt => ((decimal)ImporteAporte).ToString("N2", EsUy);
    public string ImporteAguinaldoFmt => ((decimal)ImporteAguinaldo).ToString("N2", EsUy);
    public string TotalFmt => ((decimal)(ImporteAporte + ImporteAguinaldo)).ToString("N2", EsUy);
}

/// <summary>Provee los datos de los reportes predefinidos de la empresa desde NewSgpa2.</summary>
public interface IEmpresaReporteData
{
    Task<IReadOnlyList<EmpresaAporteLinea>> GetAportesAsync(int codEmpresa, CancellationToken ct = default);
}

public sealed class EmpresaReporteData(IDbExecutor db) : IEmpresaReporteData
{
    public Task<IReadOnlyList<EmpresaAporteLinea>> GetAportesAsync(int codEmpresa, CancellationToken ct = default)
        => db.QueryAsync<EmpresaAporteLinea>(
            @"SELECT CodEmpresa, MIN(DescEmpresa) AS DescEmpresa, Anio, Mes, COUNT(*) AS Afiliados,
                     SUM(ImporteAporte) AS ImporteAporte, SUM(ImporteAguinaldo) AS ImporteAguinaldo
              FROM dbo.Rpt_Aporte
              WHERE CodEmpresa = @codEmpresa
              GROUP BY CodEmpresa, Anio, Mes
              ORDER BY Anio DESC, Mes DESC",
            new { codEmpresa }, cancellationToken: ct);
}
