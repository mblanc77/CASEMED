using DevExpress.XtraReports.UI;
using Sgpa.Web.Reports;
using Sgpa.Web.Reports.Prestamos;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Un reporte predefinido del workbench de préstamos (lo que muestra el diálogo de impresión).</summary>
public sealed record PrestamoReporteDef(string Key, string Nombre, string Icono, bool SoloFideicomiso = false);

/// <summary>
/// Catálogo + builder de los reportes predefinidos de préstamos. El diálogo lista <see cref="Disponibles"/> con
/// checkboxes; <see cref="BuildAsync"/> arma los tildados y los <b>fusiona</b> en un único documento (para ver
/// todos juntos o imprimir en batch).
/// </summary>
public interface IPrestamoReportes
{
    IReadOnlyList<PrestamoReporteDef> Disponibles { get; }
    Task<XtraReport?> BuildAsync(IReadOnlyCollection<string> keys, int idPrestamo, CancellationToken ct = default);
}

public sealed class PrestamoReportes(IPrestamoReporteData data, IWebHostEnvironment env) : IPrestamoReportes
{
    public IReadOnlyList<PrestamoReporteDef> Disponibles { get; } = new[]
    {
        new PrestamoReporteDef("ficha", "Ficha de préstamo", "fa-file-lines"),
        new PrestamoReporteDef("autorizacion", "Autorización de descuento", "fa-file-signature"),
        new PrestamoReporteDef("vale", "Vale por el préstamo", "fa-file-invoice"),
        new PrestamoReporteDef("cesion", "Cesión de créditos", "fa-file-contract", SoloFideicomiso: true),
        new PrestamoReporteDef("facturas", "Facturas", "fa-file-invoice-dollar"),
        new PrestamoReporteDef("solicitud", "Solicitud de imponibles", "fa-file-lines"),
    };

    private string? Logo => ReportBrand.LogoPath(env.WebRootPath);

    private async Task<XtraReport?> BuildOneAsync(string key, int id, CancellationToken ct)
    {
        switch (key)
        {
            case "ficha":
                var cuadro = await data.GetCuadroAsync(id, ct);
                if (cuadro.Count == 0) return null;
                var rFicha = new FichaReport();
                rFicha.Bind(cuadro, Logo);
                return rFicha;
            case "autorizacion":
                var aut = await data.GetAutorizacionAsync(id, ct);
                if (aut is null) return null;
                var rAut = new AutorizacionReport();
                rAut.Bind(new[] { aut }, Logo);
                return rAut;
            case "vale":
                var vale = await data.GetValeAsync(id, ct);
                if (vale is null) return null;
                var rVale = new ValeReport();
                rVale.Bind(new[] { vale }, Logo);
                return rVale;
            case "cesion":
                var ces = await data.GetValeAsync(id, ct);
                if (ces is null) return null;
                var rCes = new CesionReport();
                rCes.Bind(new[] { ces }, Logo);
                return rCes;
            case "facturas":
                var facturas = await data.GetFacturasAsync(id, ct);
                if (facturas.Count == 0) return null;
                var rFac = new FacturaReport();
                rFac.Bind(facturas, Logo);
                return rFac;
            case "solicitud":
                var sol = await data.GetSolicitudAsync(id, ct);
                if (sol is null) return null;
                var rSol = new SolicitudReport();
                rSol.Bind(new[] { sol }, Logo);
                return rSol;
            default:
                return null;
        }
    }

    public async Task<XtraReport?> BuildAsync(IReadOnlyCollection<string> keys, int idPrestamo, CancellationToken ct = default)
    {
        XtraReport? master = null;
        // Respeta el orden del catálogo, no el de selección.
        foreach (var key in Disponibles.Select(d => d.Key).Where(keys.Contains))
        {
            var r = await BuildOneAsync(key, idPrestamo, ct);
            if (r is null) continue;
            ReportPie.Aplicar(r);
            r.CreateDocument();
            if (master is null) master = r;
            else master.Pages.AddRange(r.Pages);
        }
        if (master is not null) master.PrintingSystem.ContinuousPageNumbering = true;
        return master;
    }
}
