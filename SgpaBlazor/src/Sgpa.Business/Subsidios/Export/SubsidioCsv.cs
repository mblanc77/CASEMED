using System.Globalization;
using System.Text;

namespace Sgpa.Business.Subsidios.Export;

/// <summary>
/// Utilidades CSV para los exports/imports de subsidios. Usa separador <c>;</c> y BOM UTF-8
/// (así Excel respeta acentos y separa columnas en es-UY sin pasos extra). Los archivos del VB6
/// eran .xls; CSV es el equivalente liviano y sin dependencias, que Excel abre nativamente.
/// </summary>
public static class SubsidioCsv
{
    public const char Separator = ';';

    /// <summary>Arma un CSV (BOM + encabezado + filas) listo para descargar.</summary>
    public static string Build(IReadOnlyList<string> headers, IEnumerable<IReadOnlyList<object?>> rows)
    {
        var sb = new StringBuilder();
        sb.Append('﻿'); // BOM: Excel detecta UTF-8
        sb.Append(string.Join(Separator, headers.Select(Escape))).Append("\r\n");
        foreach (var row in rows)
            sb.Append(string.Join(Separator, row.Select(Cell))).Append("\r\n");
        return sb.ToString();
    }

    private static string Cell(object? value) => value switch
    {
        null => string.Empty,
        DateTime d => d.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
        decimal m => m.ToString("0.##", CultureInfo.InvariantCulture),
        double db => db.ToString("0.##", CultureInfo.InvariantCulture),
        float f => f.ToString("0.##", CultureInfo.InvariantCulture),
        IFormattable n when value is int or long or short or byte => n.ToString(null, CultureInfo.InvariantCulture),
        _ => Escape(value.ToString() ?? string.Empty)
    };

    private static string Escape(string s)
    {
        if (s.IndexOf(Separator) < 0 && s.IndexOf('"') < 0 && s.IndexOf('\n') < 0 && s.IndexOf('\r') < 0)
            return s;
        return "\"" + s.Replace("\"", "\"\"") + "\"";
    }

    /// <summary>Parsea un CSV (separador <c>;</c>, primera fila = encabezados). Tolera comillas y BOM.</summary>
    public static (List<string> Headers, List<Dictionary<string, string>> Rows) Parse(string content)
    {
        var lines = SplitLines(content);
        var headers = new List<string>();
        var rows = new List<Dictionary<string, string>>();
        if (lines.Count == 0) return (headers, rows);

        headers.AddRange(ParseLine(lines[0]).Select(h => h.Trim()));
        for (int i = 1; i < lines.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var cells = ParseLine(lines[i]);
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int c = 0; c < headers.Count; c++)
                dict[headers[c]] = c < cells.Count ? cells[c] : string.Empty;
            rows.Add(dict);
        }
        return (headers, rows);
    }

    private static List<string> SplitLines(string content)
    {
        content = content.TrimStart('﻿');
        return content.Replace("\r\n", "\n").Replace('\r', '\n')
            .Split('\n').Where((_, _) => true).ToList();
    }

    private static List<string> ParseLine(string line)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;
        for (int i = 0; i < line.Length; i++)
        {
            char ch = line[i];
            if (inQuotes)
            {
                if (ch == '"')
                {
                    if (i + 1 < line.Length && line[i + 1] == '"') { sb.Append('"'); i++; }
                    else inQuotes = false;
                }
                else sb.Append(ch);
            }
            else if (ch == '"') inQuotes = true;
            else if (ch == Separator) { result.Add(sb.ToString()); sb.Clear(); }
            else sb.Append(ch);
        }
        result.Add(sb.ToString());
        return result;
    }
}
