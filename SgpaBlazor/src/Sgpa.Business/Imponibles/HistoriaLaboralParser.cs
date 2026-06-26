using System.Globalization;

namespace Sgpa.Business.Imponibles;

/// <summary>
/// Parser del archivo de nómina ATYR <b>formato V2</b> (versión 3.x, vigente desde 2008; los formatos
/// Hermes y ATYR V1 quedaron fuera de uso). Port acotado de <c>frmCarHL.AtyroV2_2_Bps</c>. El separador de
/// campos es <c>|</c> y el de registros el salto de línea; el primer campo de cada línea es el tipo de registro.
/// Sólo se interpretan los tipos de una nómina que CASEMED consume: 1 (empresa), 4 (cabezal), 5 (persona),
/// 6 (actividad) y 7 (remuneración); el resto de los tipos se ignoran.
///
/// Soporta que la empresa envíe la declaración <b>spliteada en varios archivos</b>: se mergean en orden,
/// validando que ningún tipo de registro de datos (5/6/7) aparezca en más de un archivo (cada archivo debe
/// contener tipos distintos). El período (mes/año) se deriva del cabezal (tipo 4) y debe ser único entre todos.
/// </summary>
public sealed class HistoriaLaboralParser
{
    // Tipos de registro de datos: no pueden estar repartidos/duplicados entre archivos del mismo envío.
    private static readonly int[] TiposDato = { 5, 6, 7 };

    public ParsedAtyr Parse(IReadOnlyList<AtyrArchivo> archivos)
    {
        if (archivos is null || archivos.Count == 0)
            throw new InvalidOperationException("No se seleccionó ningún archivo.");

        var empresas = new List<AtyrEmpresa>();
        var cabezales = new List<AtyrCabezal>();
        var personas = new List<AtyrPersona>();
        var actividades = new List<AtyrActividad>();
        var remuneraciones = new List<AtyrRemuneracion>();

        // Para el control de solapamiento: por cada tipo de dato, en qué archivos apareció.
        var tipoEnArchivos = new Dictionary<int, List<string>>();

        foreach (var archivo in archivos)
        {
            var tiposEnEsteArchivo = new HashSet<int>();
            foreach (var linea in archivo.Lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var f = linea.Split('|');
                if (!int.TryParse(f[0].Trim(), out var tipo)) continue;
                tiposEnEsteArchivo.Add(tipo);

                switch (tipo)
                {
                    case 1: empresas.Add(ParseEmpresa(f)); break;
                    case 4: cabezales.Add(ParseCabezal(f)); break;
                    case 5: personas.Add(ParsePersona(f)); break;
                    case 6: actividades.Add(ParseActividad(f)); break;
                    case 7: remuneraciones.Add(ParseRemuneracion(f)); break;
                    // 2/3 (construcción), 8-12 (deducciones/contacto): fuera del alcance de la HL de CASEMED.
                }
            }

            foreach (var t in TiposDato)
                if (tiposEnEsteArchivo.Contains(t))
                    (tipoEnArchivos.TryGetValue(t, out var l) ? l : tipoEnArchivos[t] = new List<string>()).Add(archivo.FileName);
        }

        // Validación de split: ningún tipo de datos puede venir en más de un archivo.
        foreach (var (tipo, files) in tipoEnArchivos)
            if (files.Count > 1)
                throw new InvalidOperationException(
                    $"El registro tipo {tipo} aparece en más de un archivo ({string.Join(", ", files)}). " +
                    "Cuando la declaración viene en varios archivos, cada uno debe contener tipos de registro distintos.");

        // Período: del cabezal (tipo 4). Debe existir y ser único entre todos los archivos.
        if (cabezales.Count == 0)
            throw new InvalidOperationException("No se encontró el cabezal de nómina (registro tipo 4); no se puede determinar el período.");

        var periodos = cabezales.Select(c => c.MesCargo).Distinct().ToList();
        if (periodos.Count > 1)
            throw new InvalidOperationException(
                $"Los archivos corresponden a períodos distintos (mes de cargo: {string.Join(", ", periodos)}).");

        var (mes, anio) = ParseMesCargo(cabezales[0].MesCargo);

        // Empresa: debe pertenecer a un único contribuyente BPS.
        var nrosEmpresa = empresas.Select(e => e.NroEmpresa).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct().ToList();
        if (nrosEmpresa.Count > 1)
            throw new InvalidOperationException(
                $"Los archivos pertenecen a empresas distintas (N° empresa BPS: {string.Join(", ", nrosEmpresa)}).");

        // Los registros de cabecera (empresa/cabezal) se deduplican al primero; los de datos van completos.
        return new ParsedAtyr(
            mes, anio,
            empresas.Count > 0 ? new[] { empresas[0] } : Array.Empty<AtyrEmpresa>(),
            new[] { cabezales[0] },
            personas, actividades, remuneraciones);
    }

    // ----- Parsers por tipo de registro (índice 0 = tipo de registro) -----

    private static AtyrEmpresa ParseEmpresa(string[] f) => new(
        Str(f, 1), Str(f, 2), Str(f, 3), Str(f, 4), Str(f, 5),
        Int(f, 6), Str(f, 7), Str(f, 8), Str(f, 9));

    private static AtyrCabezal ParseCabezal(string[] f) => new(
        Str(f, 1), Int(f, 2), Dbl(f, 3), Int(f, 4), Int(f, 5));

    private static AtyrPersona ParsePersona(string[] f) => new(
        Int(f, 1), Str(f, 2), Str(f, 3), Ci(f, 3),
        Str(f, 4), Str(f, 5), Str(f, 6), Str(f, 7),
        Fecha(f, 8), Int(f, 9), Int(f, 10));

    private static AtyrActividad ParseActividad(string[] f) => new(
        Int(f, 2), Str(f, 3), Str(f, 4), Ci(f, 4),
        Int(f, 5), Fecha(f, 6), Int(f, 7), Int(f, 8), Int(f, 9), Int(f, 10),
        Int(f, 11), Int(f, 12), Int(f, 13), Str(f, 14), Int(f, 15), Int(f, 16),
        Int(f, 17), Int(f, 18), Fecha(f, 19));

    private static AtyrRemuneracion ParseRemuneracion(string[] f) => new(
        Int(f, 2), Str(f, 3), Str(f, 4), Ci(f, 4),
        Int(f, 5), Int(f, 6), Dbl(f, 7), Dbl(f, 8), Dbl(f, 9));

    // ----- Helpers de campo -----

    /// <summary>Mes de cargo "mmaaaa" → (mes, año). Lanza si el formato es inválido.</summary>
    public static (int Mes, int Anio) ParseMesCargo(string? mesCargo)
    {
        var s = mesCargo?.Trim() ?? "";
        if (s.Length != 6 || !int.TryParse(s.Substring(0, 2), out var mes) || !int.TryParse(s.Substring(2, 4), out var anio) || mes < 1 || mes > 12)
            throw new InvalidOperationException($"El mes de cargo del cabezal es inválido: '{mesCargo}' (se esperaba mmaaaa).");
        return (mes, anio);
    }

    private static string? Str(string[] f, int i)
    {
        if (i >= f.Length) return null;
        var v = f[i].Trim();
        return v.Length == 0 ? null : v;
    }

    private static int? Int(string[] f, int i)
        => Str(f, i) is { } s && int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v) ? v : null;

    private static double? Dbl(string[] f, int i)
        => Str(f, i) is { } s && double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v) ? v : null;

    private static DateTime? Fecha(string[] f, int i)
        => Str(f, i) is { } s && DateTime.TryParseExact(s, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d) ? d : null;

    /// <summary>Cédula numérica a partir del número de documento (sólo si es enteramente numérico).</summary>
    private static long? Ci(string[] f, int i)
        => Str(f, i) is { } s && long.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out var v) ? v : null;
}
