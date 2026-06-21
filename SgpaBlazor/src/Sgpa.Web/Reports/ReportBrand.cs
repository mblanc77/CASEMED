using System.Drawing;
using DevExpress.Drawing;

namespace Sgpa.Web.Reports;

/// <summary>Estilos de marca compartidos por los reportes predefinidos (colores, fuentes, logo).</summary>
public static class ReportBrand
{
    public static readonly Color Primary = Color.FromArgb(0x1C, 0x36, 0x80);   // #1C3680
    public static readonly Color PrimarySoft = Color.FromArgb(0xD6, 0xDC, 0xEC);
    public static readonly Color Gris = Color.FromArgb(0x66, 0x66, 0x66);
    public static readonly Color Linea = Color.FromArgb(0xDD, 0xDD, 0xDD);
    public static readonly Color Texto = Color.FromArgb(0x22, 0x22, 0x22);

    public const string Family = "Segoe UI";

    public static DXFont Font(float size, DXFontStyle style = DXFontStyle.Regular) => new(Family, size, style);

    public static string? LogoPath(string webRootPath)
    {
        var p = Path.Combine(webRootPath, "img", "logo-casemed.png");
        return File.Exists(p) ? p : null;
    }
}
