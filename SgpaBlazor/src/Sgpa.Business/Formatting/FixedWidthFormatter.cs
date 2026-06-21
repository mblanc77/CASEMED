using System.Globalization;

namespace Sgpa.Business.Formatting;

/// <summary>
/// Formateo de campos de ancho fijo para los archivos de pago de bancos.
/// Port de la clase VB6 <c>cStringFormat</c> (usada por cNBC/cBROU).
/// </summary>
public static class FixedWidthFormatter
{
    /// <summary>Número justificado a la derecha, relleno con ceros (toma los <paramref name="largo"/> dígitos de la derecha).</summary>
    public static string Numero(long value, int largo)
    {
        var s = value.ToString(CultureInfo.InvariantCulture);
        s = new string('0', largo) + s;
        return s[^largo..];
    }

    /// <summary>Texto justificado a la izquierda, relleno con espacios y truncado a <paramref name="largo"/>.</summary>
    public static string Texto(string? value, int largo)
    {
        value ??= string.Empty;
        value += new string(' ', largo);
        return value[..largo];
    }
}
