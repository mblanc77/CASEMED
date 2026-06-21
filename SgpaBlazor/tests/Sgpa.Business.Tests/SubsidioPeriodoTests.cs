using Sgpa.Business.Subsidios;
using Xunit;

namespace Sgpa.Business.Tests;

public class SubsidioPeriodoTests
{
    [Fact]
    public void FromYyyyMm_descomponeAnioYMes()
    {
        var p = SubsidioPeriodo.FromYyyyMm(202201);
        Assert.Equal(2022, p.Anio);
        Assert.Equal(1, p.Mes);
        Assert.Equal(202201, p.YyyyMm);
    }

    [Theory]
    [InlineData(2022, 1, -6, 202107)]   // ventana de promedio
    [InlineData(2022, 1, -12, 202101)]  // control de aportes
    [InlineData(2022, 1, -1, 202112)]   // mes anterior (cruza año)
    [InlineData(2022, 3, 2, 202205)]
    public void AddMonths_portDeAddMonth(int anio, int mes, int delta, int esperado)
    {
        Assert.Equal(esperado, new SubsidioPeriodo(anio, mes).AddMonths(delta).YyyyMm);
    }

    [Fact]
    public void Ventanas_derivadas()
    {
        var p = new SubsidioPeriodo(2022, 1);
        Assert.Equal(202107, p.MesInicioVentana);
        Assert.Equal(202101, p.MesInicioAportes);
        Assert.Equal(202112, p.MesAnterior);
    }

    /// <summary>
    /// INVARIANTE: la liquidación nunca debe usar imponibles con mes de cargo ≥ el de la liquidación.
    /// El tope superior de TODAS las ventanas (promedio, aportes, mes anterior) es M−1, estrictamente
    /// menor que el período liquidado. Si esto se rompe, el cálculo del ValorJornal se auto-referenciaría
    /// con el propio subsidio del mes (que se carga un mes atrás).
    /// </summary>
    [Theory]
    [InlineData(2022, 1)]   // cruza año
    [InlineData(2026, 4)]
    [InlineData(2026, 12)]
    [InlineData(2026, 1)]   // cruza año
    public void Ventana_nunca_alcanza_el_mes_de_liquidacion(int anio, int mes)
    {
        var p = new SubsidioPeriodo(anio, mes);
        Assert.True(p.MesAnterior < p.YyyyMm, "MesAnterior debe ser < período liquidado");
        Assert.True(p.MesInicioVentana < p.YyyyMm, "inicio de ventana debe ser < período liquidado");
        Assert.True(p.MesInicioAportes < p.YyyyMm, "inicio de aportes debe ser < período liquidado");
        // El tope es exactamente el mes inmediatamente anterior.
        Assert.Equal(p.AddMonths(-1).YyyyMm, p.MesAnterior);
    }
}
