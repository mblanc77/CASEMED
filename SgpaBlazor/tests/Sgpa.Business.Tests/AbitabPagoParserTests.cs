using System;
using System.Collections.Generic;
using Sgpa.Business.Pagos;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit puro del parser de archivos Abitab (port de cArchPago.Leer), con un mapeo sintético.</summary>
public class AbitabPagoParserTests
{
    // Layout: NroFactura(1,10) FechaPago(11,8 ddmmaaaa) Importe(19,12 cent) ImporteCobrado(31,12) Agencia(43,4) SubAgencia(47,4)
    private static readonly IReadOnlyList<AbitabCampo> Mapeo = new List<AbitabCampo>
    {
        new("NroFactura", 1, 10),
        new("FechaPago", 11, 8),
        new("Importe", 19, 12),
        new("ImporteCobrado", 31, 12),
        new("NroAgencia", 43, 4),
        new("NroSubAgencia", 47, 4),
    };

    private static string Linea(long nroFactura, DateTime fecha, double importe, double cobrado, int ag, int subag)
        => nroFactura.ToString().PadLeft(10, '0')
         + fecha.ToString("ddMMyyyy")
         + ((long)Math.Round(importe * 100)).ToString().PadLeft(12, '0')
         + ((long)Math.Round(cobrado * 100)).ToString().PadLeft(12, '0')
         + ag.ToString().PadLeft(4, '0')
         + subag.ToString().PadLeft(4, '0');

    [Fact]
    public void Parsea_los_campos_de_una_linea()
    {
        var p = AbitabPagoParser.Parse(Mapeo, Linea(12345, new DateTime(2026, 6, 1), 1500.50, 1600.75, 1, 2));

        Assert.Equal(12345, p.NroFactura);
        Assert.Equal(new DateTime(2026, 6, 1), p.FechaPago);
        Assert.Equal(1500.50, p.Importe, 2);
        Assert.Equal(1600.75, p.ImporteCobrado, 2);
        Assert.Equal(1, p.NroAgencia);
        Assert.Equal(2, p.NroSubAgencia);
        Assert.Equal("1/2", p.CodSucursal);
        Assert.Equal(100.25, p.Interes, 2);   // cobrado - importe
    }

    [Fact]
    public void Falta_de_campo_en_el_mapeo_lanza()
    {
        var incompleto = new List<AbitabCampo> { new("NroFactura", 1, 10) };
        Assert.Throws<InvalidOperationException>(() => AbitabPagoParser.Parse(incompleto, "0000012345"));
    }

    [Fact]
    public void Mid_es_1_based_y_no_desborda()
    {
        Assert.Equal("234", AbitabPagoParser.Mid("123456", 2, 3));
        Assert.Equal("56", AbitabPagoParser.Mid("123456", 5, 10)); // recorta al largo
        Assert.Equal("", AbitabPagoParser.Mid("123", 9, 3));        // fuera de rango
    }

    [Fact]
    public void Val_toma_el_prefijo_numerico()
    {
        Assert.Equal(0, AbitabPagoParser.Val("   "), 6);
        Assert.Equal(42, AbitabPagoParser.Val("0042"), 6);
        Assert.Equal(3.14, AbitabPagoParser.Val("3.14abc"), 6);
    }

    [Fact]
    public void ConvToFecha_interpreta_ddmmaaaa()
    {
        Assert.Equal(new DateTime(2026, 6, 1), AbitabPagoParser.ConvToFecha("01062026"));
        Assert.Equal(new DateTime(2025, 12, 31), AbitabPagoParser.ConvToFecha("31122025"));
    }
}
