using System.Text;

namespace Sgpa.Domain.Metadata;

/// <summary>
/// Genera captions amigables en español a partir de los nombres de columna/propiedad.
/// Las tablas migradas usan nombres tipo <c>CodMutualista</c>, <c>FechaIngMutualista</c>,
/// <c>ImpLiquido</c>: se parten en tokens (camelCase + dígitos) y cada token se traduce
/// expandiendo abreviaturas (Cod→Código, Nro→Nº, Imp→Importe…) y agregando acentos.
/// Un <c>[SgpaColumn(Caption=...)]</c> explícito siempre tiene prioridad sobre esto.
/// </summary>
public static class SpanishCaptions
{
    // Nombre completo de columna → caption final (cuando el tokenizado no alcanza).
    private static readonly Dictionary<string, string> FullOverrides = new(StringComparer.OrdinalIgnoreCase)
    {
        ["CI"] = "Cédula",
        ["EMail"] = "Email",
        ["NroFunCuenta"] = "Nº cuenta funcionario",
        ["NroFichaEmpresa"] = "Nº ficha empresa",
    };

    // Token (en minúsculas) → texto a mostrar. Para tokens fuera del mapa se conserva el texto original;
    // los acrónimos que ya vienen en MAYÚSCULAS (BPS, IMS, IRPF…) se conservan tal cual.
    private static readonly Dictionary<string, string> TokenMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Abreviaturas
        ["cod"] = "Código",
        ["codigo"] = "Código",
        ["nro"] = "Nº",
        ["num"] = "Nº",
        ["numero"] = "Número",
        ["imp"] = "Importe",
        ["ing"] = "Ingreso",
        ["cant"] = "Cantidad",
        ["pct"] = "Porcentaje",
        ["desc"] = "Descripción",
        ["descrip"] = "Descripción",
        ["descripcion"] = "Descripción",
        ["obs"] = "Observaciones",
        ["dir"] = "Dirección",
        ["tel"] = "Teléfono",
        // Palabras españolas sin acento en la BD
        ["telefono"] = "Teléfono",
        ["direccion"] = "Dirección",
        ["movil"] = "Móvil",
        ["dias"] = "Días",
        ["dia"] = "Día",
        ["anio"] = "Año",
        ["anios"] = "Años",
        ["liquido"] = "Líquido",
        ["liquidos"] = "Líquidos",
        ["situacion"] = "Situación",
        ["jubilatorio"] = "Jubilatorio",
        ["regimen"] = "Régimen",
        ["afeccion"] = "Afección",
        ["patologia"] = "Patología",
        ["especialidad"] = "Especialidad",
        ["nominal"] = "Nominal",
        ["aguinaldo"] = "Aguinaldo",
    };

    public static string Humanize(string name)
    {
        if (string.IsNullOrEmpty(name)) return name;
        if (FullOverrides.TryGetValue(name, out var full)) return full;

        var tokens = Tokenize(name);
        if (tokens.Count == 0) return name;

        var sb = new StringBuilder();
        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];
            string text;
            if (TokenMap.TryGetValue(token, out var mapped))
                text = mapped;
            else if (IsAcronym(token))
                text = token;            // BPS, IMS, IRPF, UR… se respetan
            else if (token.Length == 1 && char.IsDigit(token[0]))
                text = token;            // dígito suelto (Apellido 1)
            else
                text = token;            // se conserva tal cual

            if (i > 0)
            {
                sb.Append(' ');
                // Sentence case: sólo la primera palabra capitalizada; el resto en minúscula
                // salvo acrónimos (BPS, IMS…) y nombres propios (Casemed).
                if (!IsAcronym(text) && !IsProperNounToken(token))
                    text = Decapitalize(text);
            }
            sb.Append(text);
        }
        return sb.ToString();
    }

    // Tokens que mantienen mayúscula inicial aunque no sean la primera palabra (nombres propios).
    private static bool IsProperNounToken(string token) =>
        token.Equals("casemed", StringComparison.OrdinalIgnoreCase);

    private static string Decapitalize(string s)
    {
        if (s.Length == 0 || !char.IsUpper(s[0])) return s;
        return char.ToLowerInvariant(s[0]) + s.Substring(1);
    }

    private static bool IsAcronym(string token) =>
        token.Length >= 2 && token.All(char.IsUpper);

    /// <summary>Parte un identificador en tokens por límites camelCase y de dígitos.</summary>
    private static List<string> Tokenize(string name)
    {
        var tokens = new List<string>();
        var current = new StringBuilder();

        void Flush()
        {
            if (current.Length > 0) { tokens.Add(current.ToString()); current.Clear(); }
        }

        for (int i = 0; i < name.Length; i++)
        {
            var ch = name[i];
            if (!char.IsLetterOrDigit(ch)) { Flush(); continue; }

            if (i > 0)
            {
                var prev = name[i - 1];
                bool boundary =
                    (char.IsUpper(ch) && !char.IsUpper(prev))                              // aA / 1A
                    || (char.IsDigit(ch) != char.IsDigit(prev))                            // letra↔dígito
                    || (char.IsUpper(ch) && char.IsUpper(prev)
                        && i + 1 < name.Length && char.IsLower(name[i + 1]));              // fin de acrónimo: XMLParser → XML|Parser
                if (boundary) Flush();
            }
            current.Append(ch);
        }
        Flush();
        return tokens;
    }
}
