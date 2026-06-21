namespace Sgpa.Business.Subsidios;

/// <summary>Redondeos del cálculo de subsidios (port de helpers VB6).</summary>
public static class SubsidioMath
{
    /// <summary>
    /// Redondeo interno de importes de subsidio. Port de <c>Rdo</c> (VB6 Format "0.000").
    /// 3 decimales, medio-hacia-arriba. (Verificar contra salida de producción si hay
    /// discrepancias por banker's rounding del VB6 original.)
    /// </summary>
    public static decimal Rdo(decimal importe) => Math.Round(importe, 3, MidpointRounding.AwayFromZero);

    /// <summary>Redondeo a 2 decimales usado por los archivos bancarios (Round(ImpLiquido, 2)).</summary>
    public static decimal Money2(decimal importe) => Math.Round(importe, 2, MidpointRounding.AwayFromZero);
}
