namespace Sgpa.Business.Pagos;

/// <summary>Cálculos puros de pagos (testeables sin base). Port de cAdmFactura.ImportexMora.</summary>
public static class PagoCalculo
{
    /// <summary>
    /// Interés por mora de un importe vencido (port de la fórmula de <c>ImportexMora</c>): capitaliza la
    /// tasa de mora anual (en %) a tasa diaria base 360 y la aplica por la cantidad de días de atraso.
    /// Nunca devuelve negativo. La decisión de aplicarla (tolerancia de días) la toma el servicio.
    /// </summary>
    public static double CalcularMora(double monto, double tasaMoraAnualPct, int dias)
    {
        var diaria = Math.Pow(1 + tasaMoraAnualPct / 100.0, 1.0 / 360.0) - 1;
        var mora = monto * Math.Pow(1 + diaria, dias) - monto;
        return Math.Max(mora, 0);
    }
}
