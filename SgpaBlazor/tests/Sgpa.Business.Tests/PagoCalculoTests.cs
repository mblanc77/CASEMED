using Sgpa.Business.Pagos;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro de la fórmula de mora (port de ImportexMora), sin base.</summary>
public class PagoCalculoTests
{
    [Fact]
    public void Tasa_cero_no_genera_mora()
        => Assert.Equal(0, PagoCalculo.CalcularMora(10000, tasaMoraAnualPct: 0, dias: 90), 6);

    [Fact]
    public void Dias_no_positivos_no_generan_mora()
    {
        Assert.Equal(0, PagoCalculo.CalcularMora(10000, 120, dias: 0), 6);
        Assert.Equal(0, PagoCalculo.CalcularMora(10000, 120, dias: -5), 6);
    }

    [Fact]
    public void Mora_crece_con_los_dias_de_atraso()
    {
        var m30 = PagoCalculo.CalcularMora(10000, 120, 30);
        var m60 = PagoCalculo.CalcularMora(10000, 120, 60);
        Assert.True(m30 > 0);
        Assert.True(m60 > m30);
    }

    [Fact]
    public void Mora_coincide_con_la_capitalizacion_diaria_base_360()
    {
        // Réplica directa de la fórmula VB6: diaria = (1+tasa/100)^(1/360)-1; mora = monto*(1+diaria)^dias - monto.
        const double monto = 25000, tasa = 96; const int dias = 45;
        var diaria = System.Math.Pow(1 + tasa / 100.0, 1.0 / 360.0) - 1;
        var esperado = monto * System.Math.Pow(1 + diaria, dias) - monto;
        Assert.Equal(esperado, PagoCalculo.CalcularMora(monto, tasa, dias), 6);
    }
}
