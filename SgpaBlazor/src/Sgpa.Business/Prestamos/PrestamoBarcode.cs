using System.Globalization;
using System.Text;

namespace Sgpa.Business.Prestamos;

/// <summary>
/// Generación del código de barras Abitab para las facturas de cuota (port de
/// <c>cAdmFactura.GenCodigoBarra</c>, app VB6 "SP"). Función pura: el <paramref name="codAbitab"/>
/// (1 dígito por moneda) y el nº de empresa se resuelven desde la base por el llamador.
/// </summary>
public static class PrestamoBarcode
{
    // Constantes del formato Abitab (cAdmFactura.cls).
    private const string Ponderacion = "45632732987634";
    private const string TipoDocumentoFactura = "1";
    private const string FormatCuota = "00";
    private const string FormatMora = "0";
    private const string FormatCuenta = "0000000";

    /// <summary>
    /// Arma el campo de datos (sin el nº de empresa) + dígito verificador y antepone el nº de empresa,
    /// exactamente como el VB6: cliente(7)+factura(7)+vencimiento(8)+importe(11)+moneda(1)+cuota(2)+
    /// mora(1)+tipoDoc(1)+cuenta(7), pondera con "45632732987634" y agrega el verificador módulo 10.
    /// </summary>
    public static string Generar(string nroEmpresa, long nroFactura, double importe, string codAbitab,
        long ci, DateTime fechaVencimiento)
    {
        var sCod = new StringBuilder()
            .Append((ci / 10).ToString("0000000", CultureInfo.InvariantCulture))      // CI sin dígito verificador (7)
            .Append(nroFactura.ToString("0000000", CultureInfo.InvariantCulture))      // NroFactura (7)
            .Append(fechaVencimiento.ToString("ddMMyyyy", CultureInfo.InvariantCulture))// Vencimiento (8)
            .Append(ImporteSinSeparador(importe))                                       // Importe (11)
            .Append(codAbitab)                                                          // Moneda (1)
            .Append(FormatCuota)                                                        // Cuota (2)
            .Append(FormatMora)                                                         // Mora (1)
            .Append(TipoDocumentoFactura)                                               // Tipo de documento (1)
            .Append(FormatCuenta)                                                       // Cuenta (7)
            .ToString();

        var digito = DigitoVerificador(sCod);
        return nroEmpresa + sCod + digito.ToString(CultureInfo.InvariantCulture);
    }

    // Format(importe, "000000000.00") sin el separador decimal == importe*100 en 11 dígitos.
    private static string ImporteSinSeparador(double importe)
        => ((long)Math.Round(importe * 100d, MidpointRounding.AwayFromZero))
            .ToString("00000000000", CultureInfo.InvariantCulture);

    // Right(repetir(Ponderacion), Len(sCod)); suma (dígito·peso mod 10); verificador = (10 − suma mod 10) mod 10.
    private static int DigitoVerificador(string sCod)
    {
        var sb = new StringBuilder();
        while (sb.Length < sCod.Length) sb.Insert(0, Ponderacion);
        var pond = sb.ToString().Substring(sb.Length - sCod.Length);

        long suma = 0;
        for (int i = 0; i < sCod.Length; i++)
            suma += (Digito(sCod[i]) * Digito(pond[i])) % 10;

        return (int)((10 - suma % 10) % 10);
    }

    private static int Digito(char c) => char.IsDigit(c) ? c - '0' : 0;
}
