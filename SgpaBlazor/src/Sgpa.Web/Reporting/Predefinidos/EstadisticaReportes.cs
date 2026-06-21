using System.Globalization;
using DevExpress.XtraReports.UI;
using Sgpa.Web.Reports;
using Sgpa.Web.Reports.Estadisticos;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>
/// Arma el reporte (gráfico) de un Informe Estadístico a partir de su IdRpt y los parámetros de pantalla.
/// Resuelve el catálogo, corre la cadena de TVFs (vía <see cref="IEstadisticaReporteData"/>) y bindea el
/// <see cref="EstadisticaChartReport"/> con título, filtro, nota y tipo de gráfico.
/// </summary>
public interface IEstadisticaReportes
{
    Task<XtraReport?> BuildAsync(int idRpt, InformeParametros p, TipoGrafico? tipo, CancellationToken ct = default);
}

public sealed class EstadisticaReportes(IEstadisticaReporteData data, IWebHostEnvironment env) : IEstadisticaReportes
{
    private static readonly CultureInfo EsUy = CultureInfo.GetCultureInfo("es-UY");

    // Informes cuya distribución es de pocas categorías => torta por defecto. El resto (rankings, franjas,
    // tramos etarios) => barras. El usuario puede cambiarlo con el selector en pantalla.
    private static readonly HashSet<int> _torta = new() { 2, 3, 6, 7, 8, 9, 10, 12, 17, 18 };

    public static TipoGrafico TipoDefault(int idRpt) => _torta.Contains(idRpt) ? TipoGrafico.Circular : TipoGrafico.Barras;

    public async Task<XtraReport?> BuildAsync(int idRpt, InformeParametros p, TipoGrafico? tipo, CancellationToken ct = default)
    {
        var def = await data.GetDefAsync(idRpt, ct);
        if (def is null) return null;

        var puntos = await data.GetPuntosAsync(idRpt, p, ct);

        // Nombre de la patología para el subtítulo (sólo en los informes que la filtran).
        string? patNombre = null;
        if (def.Patologia && p.CodPatologia is { } cod)
            patNombre = (await data.GetPatologiasAsync(ct)).FirstOrDefault(x => x.CodPatologia == cod)?.Descrip;

        var report = new EstadisticaChartReport();
        report.Name = $"estadistico-{idRpt}";
        report.Bind(puntos, def.TituloRpt, ConstruirFiltro(def, p, patNombre), def.Comentario,
            tipo ?? TipoDefault(idRpt), ReportBrand.LogoPath(env.WebRootPath));

        ReportPie.Aplicar(report);
        return report;
    }

    /// <summary>Texto del subtítulo con los parámetros aplicados (réplica de las fórmulas "Filtro" del VB6).</summary>
    private static string ConstruirFiltro(InformeEstadisticoDef def, InformeParametros p, string? patNombre)
    {
        var partes = new List<string>();

        if (def.Empresa)
            partes.Add("Empresa: " + (p.CodEmpresa > 0 ? p.CodEmpresa.ToString() : "(Todas)"));

        if (def.MesAnio)
            partes.Add("Mes: " + (p.Mes is { } m ? m.ToString() : "—"));

        if (def.Periodo)
        {
            var per = p.FechaIni.HasValue && p.FechaFin.HasValue
                ? $"{p.FechaIni:dd/MM/yyyy} - {p.FechaFin:dd/MM/yyyy}"
                : "(Todas)";
            partes.Add("Período: " + per);
        }

        if (def.Fecha)
            partes.Add("Fecha: " + (p.Fecha is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "(Actual)"));

        if (def.GrupoEtario)
            partes.Add("Grupo etario: " + EstadisticaReporteData.GruposEtarios.First(g => g.Id == p.GrupoEtario).Label);

        if (def.Patologia)
            partes.Add("Patología: " + (p.CodPatologia.HasValue ? (patNombre ?? p.CodPatologia.ToString()) : "(Todas)"));

        return string.Join("   ·   ", partes);
    }
}
