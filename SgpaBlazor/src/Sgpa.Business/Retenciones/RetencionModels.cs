namespace Sgpa.Business.Retenciones;

/// <summary>Factura candidata a ser retenida (emitida), con su mora calculada a la fecha (si no es directa).</summary>
public sealed class FacturaRetencionView
{
    public int NroFactura { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public double Importe { get; set; }
    public double ImpMora { get; set; }
}

/// <summary>Una factura a incluir en la retención (Nº + importe + mora). El servicio resuelve su IDFactura.</summary>
public sealed record FacturaRetencion(int NroFactura, double Importe, double ImpMora);

/// <summary>
/// Pedido de ingreso de una retención (port de los argumentos de cAdmRetencion.Ingresar): retiene un conjunto
/// de facturas de un préstamo, registra el cabezal de cuenta corriente y marca las facturas como retenidas.
/// </summary>
public sealed record IngresarRetencionRequest(
    int IDPrestamo, long CI, DateTime Fecha, int? CodEmpresa, string CodMoneda, double TipoCambio,
    IReadOnlyList<FacturaRetencion> Facturas, double ImpTelegrama, string? Observaciones, bool Directa);

/// <summary>Cabezal de cuenta corriente de retención de un préstamo (SP_RetencionPrestamo).</summary>
public sealed class RetencionCuentaCorriente
{
    public int IDPrestamo { get; set; }
    public double Importe { get; set; }
    public double Saldo { get; set; }
    public double ImpPago { get; set; }
}

/// <summary>Una retención registrada de un préstamo (SP_Retencion).</summary>
public sealed class RetencionView
{
    public DateTime Fecha { get; set; }
    public double TipoCambio { get; set; }
    public double Importe { get; set; }
    public string? Observaciones { get; set; }
    public bool Directa { get; set; }
}

/// <summary>Un pago (amortización) de la cuenta corriente de retención (SP_RetencionPago).</summary>
public sealed class RetencionPagoView
{
    public DateTime Fecha { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }
    public double Importe { get; set; }
}

/// <summary>Un aviso/comentario de retención (SP_RetencionAviso).</summary>
public sealed class RetencionAvisoView
{
    public DateTime Fecha { get; set; }
    public string? Comentario { get; set; }
}
