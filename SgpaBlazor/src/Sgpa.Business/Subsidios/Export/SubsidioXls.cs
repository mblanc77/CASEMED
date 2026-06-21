using System.Globalization;
using System.Text;

namespace Sgpa.Business.Subsidios.Export;

/// <summary>
/// Genera una planilla en formato <b>SpreadsheetML 2003</b> (XML que Excel abre nativamente como .xls,
/// con celdas tipadas — números reales, no texto). Sin dependencias externas: es un string, se descarga
/// igual que el CSV. Da paridad real con los .xls del VB6 (a diferencia del CSV).
/// </summary>
public static class SubsidioXls
{
    /// <summary>Arma el XML de una planilla (una hoja) lista para descargar como .xls.</summary>
    public static string Build(string sheetName, IReadOnlyList<string> headers, IEnumerable<IReadOnlyList<object?>> rows)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
        sb.Append("<?mso-application progid=\"Excel.Sheet\"?>\r\n");
        sb.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ")
          .Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\r\n");
        sb.Append("<Worksheet ss:Name=\"").Append(Attr(sheetName)).Append("\">\r\n<Table>\r\n");

        sb.Append("<Row>");
        foreach (var h in headers)
            sb.Append("<Cell><Data ss:Type=\"String\">").Append(Xml(h)).Append("</Data></Cell>");
        sb.Append("</Row>\r\n");

        foreach (var row in rows)
        {
            sb.Append("<Row>");
            foreach (var v in row) sb.Append(Cell(v));
            sb.Append("</Row>\r\n");
        }

        sb.Append("</Table>\r\n</Worksheet>\r\n</Workbook>");
        return sb.ToString();
    }

    private static string Cell(object? value) => value switch
    {
        null => "<Cell/>",
        DateTime d => Str(d.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)),
        decimal m => Num(m.ToString(CultureInfo.InvariantCulture)),
        double db => Num(db.ToString(CultureInfo.InvariantCulture)),
        float f => Num(f.ToString(CultureInfo.InvariantCulture)),
        int or long or short or byte => Num(Convert.ToString(value, CultureInfo.InvariantCulture)!),
        _ => Str(value.ToString() ?? string.Empty)
    };

    private static string Num(string n) => "<Cell><Data ss:Type=\"Number\">" + n + "</Data></Cell>";
    private static string Str(string s) => "<Cell><Data ss:Type=\"String\">" + Xml(s) + "</Data></Cell>";
    private static string Xml(string s) => s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
    private static string Attr(string s) => Xml(s).Replace("\"", "&quot;");
}
