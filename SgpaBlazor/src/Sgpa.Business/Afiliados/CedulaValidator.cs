namespace Sgpa.Business.Afiliados;

/// <summary>
/// Validación del dígito verificador de la cédula uruguaya (port de ChkCedula / CALCULO_SUMA, Bcpart.bas).
/// Función pura: el último dígito es el verificador y se calcula ponderando los anteriores con [2,9,8,7,6,3,4].
/// </summary>
public static class CedulaValidator
{
    private static readonly int[] Pesos = { 0, 2, 9, 8, 7, 6, 3, 4 };

    /// <summary>True si el dígito verificador de la cédula es correcto.</summary>
    public static bool EsValida(long cedula)
    {
        if (cedula <= 0) return false;
        var digito = (int)(cedula % 10);
        var suma = CalculoSuma(cedula / 10);
        var codPrev = (int)(suma % 10);
        var codVal = codPrev == 0 ? 0 : 10 - codPrev;
        return codVal == digito;
    }

    // Pondera los dígitos (desde el menos significativo) por Pesos[7..1]; port de CALCULO_SUMA.
    private static long CalculoSuma(long numeroSinDigito)
    {
        long suma = 0;
        var i = 7;
        while (i >= 0 && numeroSinDigito != 0)
        {
            var resto = numeroSinDigito % 10;
            numeroSinDigito /= 10;
            suma += resto * Pesos[i];
            i--;
        }
        return suma;
    }
}
