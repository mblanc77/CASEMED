using System;
using System.Linq;
using Sgpa.Business.Prestamos;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Código de barras Abitab de las facturas de cuota (port de cAdmFactura.GenCodigoBarra).
/// Funciones puras: el oráculo se calculó replicando el algoritmo VB6 fuera del código bajo prueba.
/// </summary>
public class PrestamoBarcodeTests
{
    [Fact]
    public void Genera_el_codigo_esperado_para_un_caso_conocido()
    {
        var venc = new DateTime(2026, 7, 15);
        var cod = PrestamoBarcode.Generar(
            nroEmpresa: "12", nroFactura: 5, importe: 1234.56, codAbitab: "1", ci: 12345678, fechaVencimiento: venc);

        // empresa(2) + cliente(7) + factura(7) + venc(8) + importe(11) + moneda(1) + cuota(2) + mora(1) + tipo(1) + cuenta(7) + verificador(1)
        Assert.Equal("121234567000000515072026000001234561000100000003", cod);
    }

    [Fact]
    public void El_codigo_es_todo_digitos_y_de_largo_fijo()
    {
        var cod = PrestamoBarcode.Generar("99", 1, 0.01, "2", 49999990, new DateTime(2030, 1, 2));
        Assert.Equal(2 + 45 + 1, cod.Length);   // empresa(2) + datos(45) + verificador(1)
        Assert.True(cod.All(char.IsDigit));
    }

    [Fact]
    public void El_importe_se_codifica_en_centavos_a_11_digitos()
    {
        // 1234.56 -> 123456 -> "00000123456"
        var cod = PrestamoBarcode.Generar("12", 5, 1234.56, "1", 12345678, new DateTime(2026, 7, 15));
        Assert.Contains("00000123456", cod);
    }

    [Fact]
    public void Cambiar_un_dato_cambia_el_codigo()
    {
        var venc = new DateTime(2026, 7, 15);
        var a = PrestamoBarcode.Generar("12", 5, 1234.56, "1", 12345678, venc);
        var b = PrestamoBarcode.Generar("12", 6, 1234.56, "1", 12345678, venc);
        Assert.NotEqual(a, b);
    }
}
