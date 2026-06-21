namespace Sgpa.Business.Pagos;

/// <summary>Resultado del ingreso de un pago (port de <c>eErrPago</c>, cAdmPago.cls).</summary>
public enum ResultadoPago
{
    Ok = 0,
    NoCoincideImporte = 1,
    FacturaInexistente = 2,
    NoCoincideInteres = 3,
    EstadoIncorrecto = 4,
    ErrorInesperado = 5,
    Retenida = 6,
}

/// <summary>Origen del pago (Bcpart.bas, pcPagoOrigen*).</summary>
public static class PagoOrigen
{
    public const string Casemed = "cas";
    public const string Abitab = "abi";
    public const string Fideicomiso = "fid";
    public const string Refinanciacion = "ref";
    public const string Retencion = "ret";
}

/// <summary>
/// Pedido de ingreso de un pago manual (port de los argumentos de <c>cAdmPago.Ingresar</c>).
/// <paramref name="Importe"/> es el importe de la factura (base, sin mora); <paramref name="ImporteMora"/>
/// es el interés por mora calculado aparte. El pago registrado es la suma de ambos.
/// </summary>
public sealed record IngresarPagoRequest(
    int NroFactura,
    DateTime FechaPago,
    double Importe,
    double ImporteMora = 0,
    string CodPagoOrigen = PagoOrigen.Casemed,
    string? CodSucursal = null);

/// <summary>Factura de un préstamo para la grilla de cobro / consulta (1003_FacturasxIDPrestamo).</summary>
public sealed class FacturaPagoView
{
    public int NroFactura { get; set; }
    public int IDFactura { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaPago { get; set; }
    public double Importe { get; set; }
    public string CodFacturaEstado { get; set; } = "";
    public string? DescFacturaEstado { get; set; }
    public string CodMoneda { get; set; } = "";
}

/// <summary>Datos que el diálogo de cobro necesita de una factura, más la mora calculada a una fecha.</summary>
public sealed record FacturaCobro(
    int NroFactura, int IDFactura, DateTime? FechaVencimiento, double Importe,
    string CodFacturaEstado, string? DescFacturaEstado, string CodMoneda);

/// <summary>Una línea con error en la carga batch del archivo Abitab.</summary>
public sealed record CargaPagoError(int NroReng, ResultadoPago Resultado, string Descripcion);

/// <summary>
/// Resultado de la carga batch de pagos (port de eErrCarga + el detalle por línea). <see cref="ConError"/>
/// son las líneas con error (registradas en ErrCargaAbitab); las demás se procesaron bien.
/// </summary>
public sealed record CargaPagosResultado(int Procesados, int ConError, IReadOnlyList<CargaPagoError> Errores)
{
    public int Ok => Procesados - ConError;
    public bool HuboErrores => ConError > 0;
}
