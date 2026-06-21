namespace Sgpa.Business.Prestamos;

/// <summary>Datos para grabar un préstamo nuevo (alta) desde el workbench (port del alta de GrabarPrestamo).</summary>
public sealed class GrabarPrestamoRequest
{
    public long CI { get; set; }
    public string CodMoneda { get; set; } = "";
    public string CodPrestamoTipo { get; set; } = "";
    public double Importe { get; set; }
    public int Cuotas { get; set; }
    public double ImporteCuota { get; set; }
    public double Tasa { get; set; }
    public double Promedio { get; set; }
    public DateTime Fecha { get; set; }
    /// <summary>Si tiene valor, el préstamo se emite (genera cuotas + facturas); si es null queda "ingresado".</summary>
    public DateTime? FechaCobro { get; set; }
    public int NroSerieCheque { get; set; }
    public int NroCheque { get; set; }
    public string? NroCta { get; set; }
    public string? Banco { get; set; }
    public string? Sucursal { get; set; }
    public string? Observaciones { get; set; }
    /// <summary>Cuadro de amortización ya calculado por <see cref="PrestamoCalculator"/>.</summary>
    public IReadOnlyList<CuotaAmortizacion> Cuadro { get; set; } = Array.Empty<CuotaAmortizacion>();
}

/// <summary>Resultado del grabado: nº asignado, estado y cantidad de cuotas/facturas generadas.</summary>
public sealed record GrabarPrestamoResultado(int IDPrestamo, string CodPrestamoEstado, int CuotasGeneradas, int FacturasGeneradas);

/// <summary>
/// Edición de un préstamo existente (port de la rama Else de GrabarPrestamo): sólo cambian los datos
/// de cobro/cheque/banco/observaciones. Los datos financieros (importe, cuotas, tasa, moneda) son fijos.
/// </summary>
public sealed class EditarPrestamoRequest
{
    public int IDPrestamo { get; set; }
    public DateTime Fecha { get; set; }
    /// <summary>Si tiene valor y corresponde, emite el préstamo y regenera cuotas + facturas.</summary>
    public DateTime? FechaCobro { get; set; }
    public int NroSerieCheque { get; set; }
    public int NroCheque { get; set; }
    public string? NroCta { get; set; }
    public string? Banco { get; set; }
    public string? Sucursal { get; set; }
    public string? Observaciones { get; set; }
}

/// <summary>Vista de un préstamo para editar: datos financieros fijos + datos editables actuales.</summary>
public sealed class PrestamoEditView
{
    public int IDPrestamo { get; set; }
    public long CI { get; set; }
    public string CodMoneda { get; set; } = "";
    public string CodPrestamoTipo { get; set; } = "";
    public double Importe { get; set; }
    public int Cuotas { get; set; }
    public double ImporteCuota { get; set; }
    public double Tasa { get; set; }
    public double Saldo { get; set; }
    public string CodPrestamoEstado { get; set; } = "";
    public DateTime Fecha { get; set; }
    public DateTime? FechaCobro { get; set; }
    public int NroSerieCheque { get; set; }
    public int NroCheque { get; set; }
    public string? NroCta { get; set; }
    public string? Banco { get; set; }
    public string? Sucursal { get; set; }
    public string? Observaciones { get; set; }
}

/// <summary>Resultado de una cancelación anticipada: importe cobrado y la factura de cancelación generada.</summary>
public sealed record CancelacionResultado(int IDPrestamo, double Importe, int IDFactura, int NroFactura);

/// <summary>Datos de pago de una moneda (SP_Moneda): tasa de cambio y código Abitab para el código de barras.</summary>
public sealed class MonedaPago
{
    public double TasaCambio { get; set; }
    public string CodAbitab { get; set; } = "";
}
