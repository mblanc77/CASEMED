namespace Sgpa.Business.Prestamos;

/// <summary>Una fila del cuadro de amortización (sistema francés, cuota fija).</summary>
public sealed record CuotaAmortizacion(
    int Nro,
    double Monto,          // saldo al inicio del período (capital sobre el que se calcula el interés)
    double Importe,        // valor de la cuota (fijo)
    double Interes,        // Monto * tasa mensual
    double Amortizacion,   // Importe - Interes
    double Saldo);         // Monto - Amortizacion (saldo al final del período)

/// <summary>Una fila de plan: para una cantidad de cuotas, el monto y la cuota correspondientes.</summary>
public sealed record PlanCuota(int Cuotas, double Monto, double Importe);

/// <summary>
/// Motor financiero de préstamos (port de <c>cAdmPrestamo</c>, app VB6 "SP"). Sistema francés
/// de cuota fija. Funciones puras (sin BD): testeables y reutilizables por el workbench.
/// </summary>
public static class PrestamoCalculator
{
    /// <summary>Tasa mensual equivalente a partir de la tasa anual (%): (1+anual/100)^(1/12)−1.</summary>
    public static double TasaMensual(double tasaAnual) => Math.Pow(1 + tasaAnual / 100d, 1d / 12d) - 1d;

    /// <summary>Valor de la cuota fija para monto/cuotas/tasa anual. Si la tasa es 0, es monto/cuotas.</summary>
    public static double ValorCuota(int cuotas, double tasaAnual, double monto)
    {
        if (cuotas <= 0) return 0d;
        var t = TasaMensual(tasaAnual);
        if (t == 0d) return monto / cuotas;
        return monto / ((1 - Math.Pow(1 + t, -cuotas)) / t);
    }

    /// <summary>
    /// Cuadro de amortización completo (port de CargarCuadroAmortizacion): cuota fija, y por período
    /// interés = saldo·t, amortización = cuota − interés, saldo decrece hasta ~0 en la última cuota.
    /// </summary>
    public static IReadOnlyList<CuotaAmortizacion> CargarCuadroAmortizacion(int cuotas, double tasaAnual, double monto)
    {
        var rows = new List<CuotaAmortizacion>(Math.Max(cuotas, 0));
        if (cuotas <= 0) return rows;

        var t = TasaMensual(tasaAnual);
        var cuota = t == 0d ? monto / cuotas : monto / ((1 - Math.Pow(1 + t, -cuotas)) / t);

        var saldo = monto;
        for (int i = 1; i <= cuotas; i++)
        {
            var interes = saldo * t;
            var amort = cuota - interes;
            rows.Add(new CuotaAmortizacion(i, saldo, cuota, interes, amort, saldo - amort));
            saldo -= amort;
        }
        return rows;
    }

    /// <summary>Importe amortizable de una cuota puntual (port de ImporteAmortizable).</summary>
    public static double ImporteAmortizable(int cuotas, int nroCuota, double tasaAnual, double monto)
    {
        if (cuotas <= 0 || nroCuota <= 0) return 0d;
        var t = TasaMensual(tasaAnual);
        var cuota = t == 0d ? monto / cuotas : monto / ((1 - Math.Pow(1 + t, -cuotas)) / t);

        double amort = 0d, saldo = monto;
        for (int i = 1; i <= nroCuota; i++)
        {
            amort = cuota - saldo * t;
            saldo -= amort;
        }
        return amort;
    }

    /// <summary>
    /// Plan por cuota (port de CargarPlanesxCuota): fija el valor de cuota objetivo y, para cada
    /// cantidad de cuotas 1..n, calcula el monto que se podría prestar.
    /// </summary>
    public static IReadOnlyList<PlanCuota> PlanesPorCuota(double cuotaObjetivo, int maxCuotas, double tasaAnual)
    {
        var planes = new List<PlanCuota>(Math.Max(maxCuotas, 0));
        var t = TasaMensual(tasaAnual);
        for (int i = 1; i <= maxCuotas; i++)
        {
            var monto = t == 0d ? cuotaObjetivo * i : cuotaObjetivo * ((1 - Math.Pow(1 + t, -i)) / t);
            planes.Add(new PlanCuota(i, monto, cuotaObjetivo));
        }
        return planes;
    }

    /// <summary>
    /// Plan por monto (port de CargarPlanesxMonto): fija el monto y, para cada cantidad de cuotas
    /// 1..n, calcula el valor de la cuota resultante.
    /// </summary>
    public static IReadOnlyList<PlanCuota> PlanesPorMonto(double monto, int maxCuotas, double tasaAnual)
    {
        var planes = new List<PlanCuota>(Math.Max(maxCuotas, 0));
        var t = TasaMensual(tasaAnual);
        for (int i = 1; i <= maxCuotas; i++)
        {
            var cuota = t == 0d ? monto / i : monto / ((1 - Math.Pow(1 + t, -i)) / t);
            planes.Add(new PlanCuota(i, monto, cuota));
        }
        return planes;
    }

    /// <summary>
    /// Planes "por cuota fija" viables (port de frmPlan, ePorCuota): para una cuota objetivo, lista las
    /// combinaciones 1..maxCuotas y corta cuando el monto supera el tope de préstamo. El monto crece con
    /// las cuotas, así que siempre incluye la primera fila y descarta las que exceden el tope.
    /// </summary>
    public static IReadOnlyList<PlanCuota> PlanesPorCuotaHastaTope(double cuota, int maxCuotas, double tasaAnual, double tope)
    {
        var res = new List<PlanCuota>();
        foreach (var p in PlanesPorCuota(cuota, maxCuotas, tasaAnual))
        {
            if (res.Count > 0 && p.Monto > tope) break;
            res.Add(p);
        }
        return res;
    }

    /// <summary>
    /// Planes "por monto fijo" viables (port de frmPlan, ePorMonto): para un monto, lista sólo las
    /// combinaciones cuya cuota resultante no supera la cuota objetivo (la que el afiliado puede pagar).
    /// </summary>
    public static IReadOnlyList<PlanCuota> PlanesPorMontoConCuotaMax(double monto, int maxCuotas, double tasaAnual, double cuotaMax)
        => PlanesPorMonto(monto, maxCuotas, tasaAnual).Where(p => p.Importe <= cuotaMax).ToList();

    /// <summary>
    /// Importe a pagar para cancelar anticipadamente (port de CalcularCancelar): saldo + interés diario
    /// compuesto desde el último vencimiento no pendiente hasta hoy. Tasa diaria = (1+anual/100)^(1/360)−1.
    /// Si no pasaron días (o la fecha es futura), el importe es el saldo sin interés.
    /// </summary>
    public static double CalcularCancelacion(double saldo, double tasaAnual, DateTime fechaUltVencimiento, DateTime hoy)
    {
        var dias = (hoy.Date - fechaUltVencimiento.Date).Days;
        if (dias <= 0) return saldo;
        var tasaDiaria = Math.Pow(1 + tasaAnual / 100d, 1d / 360d) - 1d;
        var interes = saldo * (Math.Pow(1 + tasaDiaria, dias) - 1d);
        return saldo + interes;
    }
}
