using System.Globalization;
using System.Text;

namespace Sgpa.Business.Pagos;

/// <summary>Una columna del archivo de cobranza Abitab (tabla MapeoAbitab): nombre y posición de ancho fijo.</summary>
public sealed record AbitabCampo(string Campo, int Inicio, int Largo);

/// <summary>Datos de una línea del archivo Abitab relevantes para el ingreso del pago (cArchPago).</summary>
public sealed record AbitabPagoLinea(
    long NroFactura, DateTime FechaPago, double Importe, double ImporteCobrado,
    int NroAgencia, int NroSubAgencia)
{
    /// <summary>Sucursal del pago = agencia/subagencia (como arma cAdmPago.ProcesarPagos).</summary>
    public string CodSucursal => $"{NroAgencia}/{NroSubAgencia}";

    /// <summary>Interés cobrado = lo cobrado por encima del importe de la factura.</summary>
    public double Interes => ImporteCobrado - Importe;
}

/// <summary>
/// Parser puro del archivo de cobranza Abitab (port de cArchPago.Leer). Las posiciones de cada campo
/// son data-driven (tabla MapeoAbitab), por eso el parser recibe el mapeo y es testeable sin base.
/// Los importes vienen en centésimos (se dividen entre 100) y las fechas en ddmmaaaa.
/// </summary>
public static class AbitabPagoParser
{
    // Campos del mapeo que usa el procesamiento de pagos.
    private const string CampoNroFactura = "NroFactura";
    private const string CampoFechaPago = "FechaPago";
    private const string CampoImporte = "Importe";
    private const string CampoImporteCobrado = "ImporteCobrado";
    private const string CampoNroAgencia = "NroAgencia";
    private const string CampoNroSubAgencia = "NroSubAgencia";

    public static AbitabPagoLinea Parse(IReadOnlyList<AbitabCampo> mapeo, string linea)
    {
        var idx = mapeo.ToDictionary(c => c.Campo, StringComparer.OrdinalIgnoreCase);

        string Campo(string nombre)
        {
            if (!idx.TryGetValue(nombre, out var c))
                throw new InvalidOperationException($"Falta el campo '{nombre}' en el mapeo Abitab (MapeoAbitab).");
            return Mid(linea, c.Inicio, c.Largo);
        }

        return new AbitabPagoLinea(
            NroFactura: (long)Val(Campo(CampoNroFactura)),
            FechaPago: ConvToFecha(Campo(CampoFechaPago)),
            Importe: Val(Campo(CampoImporte)) / 100.0,
            ImporteCobrado: Val(Campo(CampoImporteCobrado)) / 100.0,
            NroAgencia: (int)Val(Campo(CampoNroAgencia)),
            NroSubAgencia: (int)Val(Campo(CampoNroSubAgencia)));
    }

    /// <summary>Equivalente a VB Mid(s, start, len): start es 1-based, sin desbordar el largo de la cadena.</summary>
    public static string Mid(string s, int start, int len)
    {
        var i = start - 1;
        if (i < 0 || len <= 0 || i >= s.Length) return "";
        var l = Math.Min(len, s.Length - i);
        return s.Substring(i, l);
    }

    /// <summary>Equivalente a VB Val(): número con el prefijo numérico de la cadena (ignora el resto); 0 si no hay.</summary>
    public static double Val(string s)
    {
        s = s.Trim();
        if (s.Length == 0) return 0;
        var sb = new StringBuilder();
        var i = 0;
        if (s[i] is '+' or '-') { sb.Append(s[i]); i++; }
        var dot = false;
        for (; i < s.Length; i++)
        {
            var c = s[i];
            if (char.IsDigit(c)) sb.Append(c);
            else if (c == '.' && !dot) { sb.Append('.'); dot = true; }
            else break;
        }
        return double.TryParse(sb.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : 0;
    }

    /// <summary>Port de cArchPago.ConvToFecha: ddmmaaaa → fecha (año de 2 dígitos se asume 2000+).</summary>
    public static DateTime ConvToFecha(string s)
    {
        s = s.Trim();
        var dia = (int)Val(Mid(s, 1, 2));
        var mes = (int)Val(Mid(s, 3, 2));
        var anio = (int)Val(Mid(s, 5, s.Length));
        if (anio < 100) anio += 2000;
        return new DateTime(anio, mes, dia);
    }
}
