using Sgpa.Business.Formatting;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Importe en letras para cheques (port funcional de Numero2Letra/UnNumero). Función pura.</summary>
public class ImporteEnLetrasTests
{
    [Theory]
    [InlineData(0, "cero")]
    [InlineData(1, "uno")]
    [InlineData(15, "quince")]
    [InlineData(16, "dieciséis")]
    [InlineData(21, "veintiuno")]
    [InlineData(30, "treinta")]
    [InlineData(31, "treinta y uno")]
    [InlineData(100, "cien")]
    [InlineData(101, "ciento uno")]
    [InlineData(115, "ciento quince")]
    [InlineData(200, "doscientos")]
    [InlineData(999, "novecientos noventa y nueve")]
    [InlineData(1000, "mil")]
    [InlineData(1001, "mil uno")]
    [InlineData(2000, "dos mil")]
    [InlineData(21000, "veintiún mil")]
    [InlineData(31000, "treinta y un mil")]
    [InlineData(100000, "cien mil")]
    [InlineData(1000000, "un millón")]
    [InlineData(2000000, "dos millones")]
    [InlineData(21000000, "veintiún millones")]
    [InlineData(1234567, "un millón doscientos treinta y cuatro mil quinientos sesenta y siete")]
    public void Entero_convierte_a_palabras(long n, string esperado)
        => Assert.Equal(esperado, ImporteEnLetras.Entero(n));

    [Fact]
    public void Convertir_formato_cheque_con_centesimos_en_mayusculas()
        => Assert.Equal("MIL DOSCIENTOS TREINTA Y CUATRO CON 56 CENTÉSIMOS", ImporteEnLetras.Convertir(1234.56m));

    [Fact]
    public void Convertir_sin_fraccion_no_agrega_centesimos()
        => Assert.Equal("MIL DOSCIENTOS TREINTA Y CUATRO", ImporteEnLetras.Convertir(1234.00m));

    [Theory]
    [InlineData(0.50, "CERO CON 50 CENTÉSIMOS")]
    [InlineData(1.05, "UNO CON 05 CENTÉSIMOS")]
    [InlineData(-5.00, "MENOS CINCO")]
    public void Convertir_casos_de_fraccion_y_signo(double importe, string esperado)
        => Assert.Equal(esperado, ImporteEnLetras.Convertir((decimal)importe));
}
