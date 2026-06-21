using System;
using Sgpa.Business.Formatting;
using Sgpa.Business.Subsidios.Export;
using Xunit;

namespace Sgpa.Business.Tests;

public class FormattingTests
{
    [Fact]
    public void Numero_RellenaConCerosALaIzquierda()
    {
        Assert.Equal("00042", FixedWidthFormatter.Numero(42, 5));
        Assert.Equal("00000", FixedWidthFormatter.Numero(0, 5));
    }

    [Fact]
    public void Numero_TomaLosDigitosDeLaDerechaSiNoEntra()
    {
        Assert.Equal("345", FixedWidthFormatter.Numero(12345, 3));
    }

    [Fact]
    public void Texto_RellenaConEspaciosALaDerechaYTrunca()
    {
        Assert.Equal("AB   ", FixedWidthFormatter.Texto("AB", 5));
        Assert.Equal("ABCDE", FixedWidthFormatter.Texto("ABCDEFG", 5));
        Assert.Equal("     ", FixedWidthFormatter.Texto(null, 5));
    }

    [Fact]
    public void NbcLine_TieneLargo91YImporteEnCentesimos()
    {
        var row = new BankExportRow { ImpLiquido = 8136.00m, NroCuenta = "12345", Fecha = new DateTime(2025, 6, 10) };
        var line = NbcExporter.BuildLine(row, new DateTime(2025, 6, 10));

        Assert.Equal(91, line.Length);
        Assert.Equal("10062025", line.Substring(23, 8));            // fecha ddMMyyyy
        Assert.Equal("000000000813600", line.Substring(52, 15));    // importe en centésimos
        Assert.Equal("+", line.Substring(67, 1));                    // signo
    }

    [Fact]
    public void BrouLines_TienenLargo128()
    {
        var row = new BankExportRow { ImpLiquido = 8136.00m, NroCuenta = "999", Fecha = new DateTime(2025, 6, 10) };
        var detalle = BrouExporter.BuildDetalle(row, new DateTime(2025, 6, 10));
        var total = BrouExporter.BuildTotal(new DateTime(2025, 6, 10), 1, 8136.00m);

        Assert.Equal(128, detalle.Length);
        Assert.Equal(128, total.Length);
    }
}
