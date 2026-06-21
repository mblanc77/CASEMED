namespace Sgpa.Business.Subsidios;

/// <summary>
/// Período de liquidación (año/mes), representado también como entero yyyymm como en el VB6.
/// <see cref="AddMonths"/> es el port de la función VB6 <c>AddMonth</c>.
/// </summary>
public readonly record struct SubsidioPeriodo(int Anio, int Mes)
{
    /// <summary>Representación yyyymm (ej. 202201), como el <c>lMes</c> del VB6.</summary>
    public int YyyyMm => Anio * 100 + Mes;

    public static SubsidioPeriodo FromYyyyMm(int yyyymm) => new(yyyymm / 100, yyyymm % 100);

    /// <summary>Suma (o resta) meses. Port de <c>AddMonth(piCant, plMes)</c>.</summary>
    public SubsidioPeriodo AddMonths(int cantidad)
    {
        var d = new DateTime(Anio, Mes, 1).AddMonths(cantidad);
        return new SubsidioPeriodo(d.Year, d.Month);
    }

    /// <summary>Mes inicial de la ventana de promedio de imponibles (−6 meses).</summary>
    public int MesInicioVentana => AddMonths(-6).YyyyMm;

    /// <summary>Mes inicial del control de aportes (−12 meses).</summary>
    public int MesInicioAportes => AddMonths(-12).YyyyMm;

    /// <summary>Mes anterior al período (−1).</summary>
    public int MesAnterior => AddMonths(-1).YyyyMm;

    public override string ToString() => $"{Anio:D4}{Mes:D2}";
}
