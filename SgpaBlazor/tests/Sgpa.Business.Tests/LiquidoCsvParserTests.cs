using System.Linq;
using Sgpa.Business.Pagos;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Tests del parser de la carga automática de líquidos (puros, sin base): separadores, encabezado, formato de
/// importe es-UY/invariante, columnas configurables y filas ignoradas.
/// </summary>
public class LiquidoCsvParserTests
{
    [Fact]
    public void Parsea_csv_con_encabezado_y_separador_punto_y_coma()
    {
        var csv = "CI;Importe\r\n12345678;1.234,56\r\n23456789;1000\r\n";
        var p = LiquidoCsvParser.Parse(csv);

        Assert.Equal(2, p.Filas.Count);
        Assert.Equal(1, p.Ignoradas);   // el encabezado
        Assert.Equal(12345678L, p.Filas[0].CI);
        Assert.Equal(1234.56, p.Filas[0].Importe, 2);
        Assert.Equal(1000d, p.Filas[1].Importe, 2);
    }

    [Fact]
    public void Parsea_sin_encabezado_y_limpia_la_cedula()
    {
        var csv = "1.234.567-2;5000,00\n2345678;7500,50\n";
        var p = LiquidoCsvParser.Parse(csv);

        Assert.Equal(2, p.Filas.Count);
        Assert.Equal(0, p.Ignoradas);
        Assert.Equal(12345672L, p.Filas[0].CI);
        Assert.Equal(5000d, p.Filas[0].Importe, 2);
    }

    [Fact]
    public void Soporta_separador_coma_con_decimal_punto()
    {
        var csv = "12345678,1234.56\n23456789,999.99\n";
        var p = LiquidoCsvParser.Parse(csv);

        Assert.Equal(2, p.Filas.Count);
        Assert.Equal(1234.56, p.Filas[0].Importe, 2);
    }

    [Fact]
    public void Columnas_configurables()
    {
        // Cédula en col 3, importe en col 1 (1-based 3 y 1 → 0-based 2 y 0).
        var csv = "1500,00;X;12345678\n";
        var p = LiquidoCsvParser.Parse(csv, ciCol: 2, impCol: 0);

        Assert.Single(p.Filas);
        Assert.Equal(12345678L, p.Filas[0].CI);
        Assert.Equal(1500d, p.Filas[0].Importe, 2);
    }

    [Fact]
    public void Ignora_filas_sin_cedula_numerica_o_sin_importe()
    {
        var csv = "Total general;99999\n;1000\n12345678;\n12345678;2500\n";
        var p = LiquidoCsvParser.Parse(csv);

        Assert.Single(p.Filas);
        Assert.Equal(12345678L, p.Filas[0].CI);
        Assert.Equal(2500d, p.Filas[0].Importe, 2);
        Assert.Equal(3, p.Ignoradas);
    }

    [Fact]
    public void Contenido_vacio_no_falla()
    {
        var p = LiquidoCsvParser.Parse("");
        Assert.Empty(p.Filas);
        Assert.Equal(0, p.Ignoradas);
    }
}
