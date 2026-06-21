using System.Linq;
using Sgpa.Business.Prestamos;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Verifica el motor financiero de préstamos (port de cAdmPrestamo): sistema francés de cuota fija.
/// Funciones puras → sin BD.
/// </summary>
public class PrestamoCalculatorTests
{
    [Fact]
    public void Amortizacion_CuotaFija_Y_SaldoLlegaACero()
    {
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(12, 24, 10000);

        Assert.Equal(12, cuadro.Count);
        // Cuota constante.
        Assert.All(cuadro, c => Assert.Equal(cuadro[0].Importe, c.Importe, 6));
        // El primer interés = monto * tasa mensual.
        var t = PrestamoCalculator.TasaMensual(24);
        Assert.Equal(10000 * t, cuadro[0].Interes, 6);
        // La suma de amortizaciones devuelve el capital y el saldo final es ~0.
        Assert.Equal(10000, cuadro.Sum(c => c.Amortizacion), 4);
        Assert.Equal(0, cuadro[^1].Saldo, 4);
        // Cada interés se calcula sobre el saldo de inicio del período.
        Assert.All(cuadro, c => Assert.Equal(c.Monto * t, c.Interes, 6));
    }

    [Fact]
    public void Amortizacion_TasaCero_EsMontoDdivididoCuotas()
    {
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(10, 0, 5000);

        Assert.All(cuadro, c => Assert.Equal(500, c.Importe, 6));
        Assert.All(cuadro, c => Assert.Equal(0, c.Interes, 6));
        Assert.All(cuadro, c => Assert.Equal(500, c.Amortizacion, 6));
        Assert.Equal(0, cuadro[^1].Saldo, 6);
    }

    [Fact]
    public void ValorCuota_CoincideConPrimeraCuotaDelCuadro()
    {
        var cuota = PrestamoCalculator.ValorCuota(18, 30, 25000);
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(18, 30, 25000);
        Assert.Equal(cuota, cuadro[0].Importe, 8);
    }

    [Fact]
    public void ValorCuota_FormulaFrancesa_ValorConocido()
    {
        // tasa mensual t = (1.24)^(1/12)-1; cuota = monto / ((1-(1+t)^-n)/t)
        var t = PrestamoCalculator.TasaMensual(24);
        var esperado = 10000 / ((1 - System.Math.Pow(1 + t, -12)) / t);
        Assert.Equal(esperado, PrestamoCalculator.ValorCuota(12, 24, 10000), 8);
    }

    [Fact]
    public void PlanesPorMonto_CuotaCoincideConValorCuota()
    {
        var planes = PrestamoCalculator.PlanesPorMonto(20000, 24, 18);
        Assert.Equal(24, planes.Count);
        foreach (var p in planes)
            Assert.Equal(PrestamoCalculator.ValorCuota(p.Cuotas, 18, 20000), p.Importe, 8);
    }

    [Fact]
    public void PlanesPorCuota_MontoEsInversoDeLaCuota()
    {
        // Para una cuota objetivo fija, el monto del plan de n cuotas debe reproducir esa cuota.
        var planes = PrestamoCalculator.PlanesPorCuota(1500, 24, 18);
        foreach (var p in planes)
            Assert.Equal(1500, PrestamoCalculator.ValorCuota(p.Cuotas, 18, p.Monto), 6);
    }

    [Fact]
    public void PlanesPorCuotaHastaTope_DescartaLosQueSuperanElTope()
    {
        // Tope que sólo alcanza para algunas combinaciones; el monto crece con las cuotas.
        var todos = PrestamoCalculator.PlanesPorCuota(1500, 24, 18);
        var tope = todos[9].Monto; // monto del plan de 10 cuotas
        var viables = PrestamoCalculator.PlanesPorCuotaHastaTope(1500, 24, 18, tope);

        Assert.NotEmpty(viables);
        Assert.All(viables, p => Assert.True(p.Monto <= tope || p.Cuotas == viables[0].Cuotas));
        // El primer plan siempre se incluye; ninguno supera el tope salvo (a lo sumo) el primero.
        Assert.True(viables[^1].Monto <= tope);
        Assert.True(viables.Count < todos.Count); // descartó los de monto mayor al tope
    }

    [Fact]
    public void PlanesPorCuotaHastaTope_SiempreIncluyeAlMenosElPrimerPlan()
    {
        // Tope 0: igual debe devolver la primera fila (port: la primera se agrega sin chequear el tope).
        var viables = PrestamoCalculator.PlanesPorCuotaHastaTope(1500, 24, 18, tope: 0);
        Assert.Single(viables);
        Assert.Equal(1, viables[0].Cuotas);
    }

    [Fact]
    public void PlanesPorMontoConCuotaMax_SoloLosQueNoSuperanLaCuotaObjetivo()
    {
        // La cuota decrece al aumentar las cuotas: con un cuotaMax intermedio quedan los planes "largos".
        var todos = PrestamoCalculator.PlanesPorMonto(20000, 24, 18);
        var cuotaMax = todos[11].Importe; // cuota del plan de 12 cuotas
        var viables = PrestamoCalculator.PlanesPorMontoConCuotaMax(20000, 24, 18, cuotaMax);

        Assert.NotEmpty(viables);
        Assert.All(viables, p => Assert.True(p.Importe <= cuotaMax));
        Assert.Contains(viables, p => p.Cuotas == 12);          // el de cuotaMax exacto entra
        Assert.DoesNotContain(viables, p => p.Cuotas == 1);     // el de 1 cuota (cuota más alta) queda fuera
    }

    [Fact]
    public void CalcularCancelacion_SumaInteresDiarioCompuestoSobreElSaldo()
    {
        var hoy = new System.DateTime(2026, 6, 30);
        var ultVto = hoy.AddDays(-30);
        var td = System.Math.Pow(1 + 24d / 100d, 1d / 360d) - 1d;
        var esperado = 10000 + 10000 * (System.Math.Pow(1 + td, 30) - 1d);

        Assert.Equal(esperado, PrestamoCalculator.CalcularCancelacion(10000, 24, ultVto, hoy), 6);
    }

    [Fact]
    public void CalcularCancelacion_SinDiasTranscurridos_EsElSaldo()
    {
        var hoy = new System.DateTime(2026, 6, 30);
        Assert.Equal(5000, PrestamoCalculator.CalcularCancelacion(5000, 24, hoy, hoy), 6);          // mismo día
        Assert.Equal(5000, PrestamoCalculator.CalcularCancelacion(5000, 24, hoy.AddDays(5), hoy), 6); // vencimiento futuro
    }

    [Fact]
    public void ImporteAmortizable_CoincideConElCuadro()
    {
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(12, 24, 10000);
        for (int i = 1; i <= 12; i++)
            Assert.Equal(cuadro[i - 1].Amortizacion, PrestamoCalculator.ImporteAmortizable(12, i, 24, 10000), 6);
    }
}
