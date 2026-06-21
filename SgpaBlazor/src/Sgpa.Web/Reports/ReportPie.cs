using DevExpress.XtraReports.UI;

namespace Sgpa.Web.Reports;

/// <summary>
/// Pie de página global de los reportes predefinidos: separador + dirección de CASEMED a la izquierda y la
/// fecha/hora de emisión a la derecha. Se inyecta como <see cref="PageFooterBand"/> (sale en TODAS las páginas)
/// desde los builders de los catálogos, así aplica a todos los reportes sin duplicar la banda en cada diseñador.
/// </summary>
public static class ReportPie
{
    private const string Direccion = "Casemed - Arenal Grande 1676 – Montevideo   telefax: 2403 3568/69/70";

    public static void Aplicar(XtraReport report)
    {
        // No duplicar si el reporte ya define un pie de página.
        foreach (Band b in report.Bands)
            if (b is PageFooterBand) return;

        var footer = new PageFooterBand { Dpi = 96F, HeightF = 30F, Name = "PageFooter" };

        var separador = new XRLine
        {
            Dpi = 96F,
            LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F),
            SizeF = new System.Drawing.SizeF(714F, 1F),
            ForeColor = System.Drawing.Color.FromArgb(210, 216, 232),
        };

        var izq = new XRLabel
        {
            Dpi = 96F,
            LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F),
            SizeF = new System.Drawing.SizeF(480F, 14F),
            Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F),
            ForeColor = System.Drawing.Color.FromArgb(102, 102, 102),
            Text = Direccion,
        };

        var der = new XRLabel
        {
            Dpi = 96F,
            LocationFloat = new DevExpress.Utils.PointFloat(484F, 8F),
            SizeF = new System.Drawing.SizeF(230F, 14F),
            Font = new DevExpress.Drawing.DXFont("Segoe UI", 8F),
            ForeColor = System.Drawing.Color.FromArgb(102, 102, 102),
            TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight,
            Text = "Emitido: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
        };

        footer.Controls.AddRange(new XRControl[] { separador, izq, der });
        report.Bands.Add(footer);
    }
}
