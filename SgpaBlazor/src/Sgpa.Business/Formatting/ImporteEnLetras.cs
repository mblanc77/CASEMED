using System.Globalization;
using System.Text;

namespace Sgpa.Business.Formatting;

/// <summary>
/// Importe en palabras para impresión de cheques/recibos (port funcional de Numero2Letra/UnNumero,
/// Bcpart.bas). El VB6 estaba lleno de parches; acá se implementa el español estándar equivalente:
/// la parte entera en letras y los centésimos en dígitos ("... con 56 centésimos"), en MAYÚSCULAS.
/// </summary>
public static class ImporteEnLetras
{
    private static readonly string[] Menores30 =
    {
        "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve",
        "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve",
        "veinte", "veintiuno", "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve"
    };
    private static readonly string[] Decenas = { "", "", "", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
    private static readonly string[] Centenas = { "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };

    /// <summary>
    /// Importe a letras estilo cheque (port de <c>Numero2Letra(Format(importe,"0.00"), , , sufijo)</c>):
    /// parte entera en palabras + " con NN &lt;sufijo&gt;" (centésimos en dígitos) si hay fracción, en MAYÚSCULAS.
    /// </summary>
    public static string Convertir(decimal importe, string sufijoCentimos = "centésimos")
    {
        var negativo = importe < 0;
        importe = Math.Abs(importe);

        var entero = (long)Math.Floor(importe);
        var centimos = (int)Math.Round((importe - entero) * 100m, MidpointRounding.AwayFromZero);
        if (centimos >= 100) { entero++; centimos -= 100; }

        var texto = Entero(entero);
        if (centimos >= 1)
            texto = $"{texto} con {centimos.ToString("00", CultureInfo.InvariantCulture)} {sufijoCentimos}";
        if (negativo)
            texto = "menos " + texto;

        return texto.ToUpperInvariant();
    }

    /// <summary>Un entero no negativo en palabras (español estándar). Soporta hasta billones (15 dígitos).</summary>
    public static string Entero(long n)
    {
        if (n == 0) return "cero";
        if (n < 0) return "menos " + Entero(-n);

        var millones = (int)(n / 1_000_000);
        var resto = (int)(n % 1_000_000);

        var sb = new StringBuilder();
        if (millones == 1)
            sb.Append("un millón");
        else if (millones > 1)
            sb.Append(Apocopar(MenorAUnMillon(millones))).Append(" millones");

        if (resto > 0)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(MenorAUnMillon(resto));
        }
        return sb.ToString();
    }

    // 1..999999: maneja los miles ("mil", "dos mil", "veintiún mil") + el resto.
    private static string MenorAUnMillon(int n)
    {
        if (n == 0) return "";
        var miles = n / 1000;
        var resto = n % 1000;

        var sb = new StringBuilder();
        if (miles == 1) sb.Append("mil");
        else if (miles > 1) sb.Append(Apocopar(Menores1000(miles))).Append(" mil");

        if (resto > 0)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(Menores1000(resto));
        }
        return sb.ToString();
    }

    // 1..999.
    private static string Menores1000(int n)
    {
        if (n == 100) return "cien";
        var sb = new StringBuilder();
        var c = n / 100;
        var resto = n % 100;
        if (c > 0) sb.Append(Centenas[c]);
        if (resto > 0)
        {
            if (sb.Length > 0) sb.Append(' ');
            if (resto < 30) sb.Append(Menores30[resto]);
            else
            {
                sb.Append(Decenas[resto / 10]);
                if (resto % 10 > 0) sb.Append(" y ").Append(Menores30[resto % 10]);
            }
        }
        return sb.ToString();
    }

    // Apócope de "uno" antes de "mil"/"millones": uno→un, veintiuno→veintiún, treinta y uno→treinta y un.
    private static string Apocopar(string s)
    {
        if (s.EndsWith("veintiuno", StringComparison.Ordinal)) return s[..^"veintiuno".Length] + "veintiún";
        if (s.EndsWith("uno", StringComparison.Ordinal)) return s[..^"uno".Length] + "un";
        return s;
    }
}
