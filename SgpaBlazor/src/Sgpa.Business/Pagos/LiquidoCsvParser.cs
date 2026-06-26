using System.Globalization;

namespace Sgpa.Business.Pagos;

/// <summary>Una fila del archivo de líquidos: cédula + importe líquido.</summary>
public sealed record LiquidoFila(long CI, double Importe);

/// <summary>Resultado de parsear el archivo de líquidos: filas válidas + cuántas se ignoraron (encabezado/blancos/no numéricas).</summary>
public sealed record LiquidoParse(IReadOnlyList<LiquidoFila> Filas, int Ignoradas);

/// <summary>
/// Parser del archivo de carga automática de líquidos (port de <c>frmCargaLiquido.CargarExcel</c>, app VB6 "SP").
/// El VB6 leía un Excel eligiendo hoja + columna de cédula + columna de importe; acá se usa CSV (la convención del
/// proyecto: el operador exporta la planilla a CSV, igual que en los subsidios). Por defecto la cédula va en la
/// 1ª columna y el importe en la 2ª (el formato genérico del VB6: columnas A y B), configurable. Las filas cuya
/// cédula no es numérica (encabezado, blancos, totales) se ignoran, como hacía el VB6.
/// </summary>
public static class LiquidoCsvParser
{
    public static LiquidoParse Parse(string? contenido, int ciCol = 0, int impCol = 1)
    {
        var filas = new List<LiquidoFila>();
        var ignoradas = 0;
        if (string.IsNullOrWhiteSpace(contenido)) return new LiquidoParse(filas, 0);

        var lineas = contenido.TrimStart('﻿').Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
        foreach (var linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var celdas = linea.Split(DetectarSeparador(linea));
            if (celdas.Length <= Math.Max(ciCol, impCol)) { ignoradas++; continue; }

            var ciStr = LimpiarCI(celdas[ciCol]);
            if (!long.TryParse(ciStr, NumberStyles.None, CultureInfo.InvariantCulture, out var ci) || ci <= 0)
            {
                ignoradas++;   // encabezado / fila sin cédula numérica
                continue;
            }
            var importe = ParseImporte(celdas[impCol]);
            if (importe is null) { ignoradas++; continue; }

            filas.Add(new LiquidoFila(ci, importe.Value));
        }
        return new LiquidoParse(filas, ignoradas);
    }

    // Excel en es-UY exporta con ';' (la coma es el separador decimal). Si no hay ';', se prueba tab y luego ','.
    private static char DetectarSeparador(string linea)
        => linea.Contains(';') ? ';' : linea.Contains('\t') ? '\t' : ',';

    private static string LimpiarCI(string s)
        => new string(s.Where(char.IsDigit).ToArray());

    /// <summary>Importe tolerante: "1.234,56" (es-UY) o "1234.56" (invariante).</summary>
    private static double? ParseImporte(string s)
    {
        s = s.Trim();
        if (s.Length == 0) return null;
        if (s.Contains(','))   // formato es-UY: punto de miles, coma decimal
        {
            var t = s.Replace(".", "").Replace(',', '.');
            if (double.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
        }
        return double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var w) ? w : null;
    }
}
