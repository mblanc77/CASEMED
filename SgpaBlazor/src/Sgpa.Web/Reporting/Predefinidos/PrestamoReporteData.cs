using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Una fila del cuadro de amortización (vista dbo.Rpt_PrestamoCuadro): cabecera repetida + datos de la cuota.</summary>
public sealed class PrestamoCuadroLinea
{
    public int IDPrestamo { get; set; }
    public long CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public DateTime Fecha { get; set; }
    public string? DescMoneda { get; set; }
    public string? DescMonedaLarga { get; set; }
    public decimal Importe { get; set; }     // monto total del préstamo
    public int Cuotas { get; set; }
    public decimal Tasa { get; set; }
    public decimal Promedio { get; set; }    // imponibles (promedio de ingresos)

    public int NroCuota { get; set; }
    public decimal Monto { get; set; }        // saldo al inicio de la cuota
    public decimal ImporteCuota { get; set; } // valor de la cuota
    public decimal Interes { get; set; }
    public decimal Amortizacion { get; set; }
    public decimal Saldo { get; set; }

    public string? CodPrestamoTipo { get; set; }
    public string? DescPrestamoTipo { get; set; }

    public string NombreCompleto => $"{Nombres} {Apellido1} {Apellido2}".Replace("  ", " ").Trim();
}

/// <summary>Datos del Vale / Cesión (TVF dbo.[1009_PrestamoVale]).</summary>
public sealed class PrestamoValeData
{
    public int IDPrestamo { get; set; }
    public long CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? Direccion { get; set; }
    public string? DescMoneda { get; set; }
    public string? DescMonedaLarga { get; set; }
    public int Cuotas { get; set; }
    public double ImporteCuota { get; set; }
    public DateTime FechaVencimiento { get; set; }   // vencimiento de la 1ª cuota
    public double Tasa { get; set; }
    public double Importe { get; set; }              // monto del préstamo (principal)

    public string NombreCompleto => $"{Nombres} {Apellido1} {Apellido2}".Replace("  ", " ").Trim();
    public decimal ImporteTotal => (decimal)ImporteCuota * Cuotas;   // total a pagar (POR del vale)

    // Campos formateados/derivados para bindear los reportes-documento (sin lógica en el .repx/.Designer).
    private static System.Globalization.CultureInfo EsUy => System.Globalization.CultureInfo.GetCultureInfo("es-UY");
    public string CIFormato => CI.ToString("#,#", EsUy);
    public string FechaLarga => DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", EsUy);
    public string MonedaTexto => string.IsNullOrWhiteSpace(DescMonedaLarga) ? "Pesos uruguayos" : DescMonedaLarga!;
    public string ImporteTotalFmt => ImporteTotal.ToString("N2", EsUy);
    public string ImporteTotalLetras => Sgpa.Business.Formatting.ImporteEnLetras.Convertir(ImporteTotal).ToUpper(EsUy) + " .-";
    public string ImporteCuotaFmt => ((decimal)ImporteCuota).ToString("N2", EsUy);
    public string ImporteFmt => ((decimal)Importe).ToString("N2", EsUy);
    public string TasaFmt => ((decimal)Tasa).ToString("N2", EsUy);
    public string PrimerVtoFmt => FechaVencimiento.ToString("dd/MM/yyyy", EsUy);
    public int DiaVencimiento => FechaVencimiento.Day;
}

/// <summary>Datos de la Autorización de descuento (TVF dbo.[2000_Rpt_AutorizacionxIDPrestamo]).</summary>
public sealed class PrestamoAutorizacionData
{
    public int IDPrestamo { get; set; }
    public int CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string NombreCompleto => $"{Nombres} {Apellido1} {Apellido2}".Replace("  ", " ").Trim();
    public string CIFormato => CI.ToString("#,#", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
    public string FechaLarga => DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
}

/// <summary>Una factura del préstamo (vista dbo.Rpt_Factura): una fila por cuota facturada.</summary>
public sealed class FacturaLinea
{
    public int IDPrestamo { get; set; }
    public long CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? Direccion { get; set; }
    public int NroFactura { get; set; }
    public DateTime FechaEmitida { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public double Tasa { get; set; }
    public string? DescMoneda { get; set; }
    public string? Descrip { get; set; }
    public double Importe { get; set; }
    public int NroCuota { get; set; }
    public string? CodigoBarra { get; set; }

    public string NombreCompleto => $"{Nombres} {Apellido1} {Apellido2}".Replace("  ", " ").Trim();
    public string CIFormato => CI.ToString("#,#", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
    public string NroFacturaFmt => NroFactura.ToString("0000000");
    public string ImporteFmt => ((decimal)Importe).ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
    public string TasaFmt => ((decimal)Tasa).ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
}

/// <summary>Datos del solicitante para la Solicitud de imponibles (TVF dbo.[2000_Rpt_AfiliadoxCI], por CI).</summary>
public sealed class SolicitudData
{
    public int CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string NombreCompleto => $"{Nombres} {Apellido1} {Apellido2}".Replace("  ", " ").Trim();
    public string CIFormato => CI.ToString("#,#", System.Globalization.CultureInfo.GetCultureInfo("es-UY"));
    public string FechaHoy => DateTime.Today.ToString("dd/MM/yyyy");
}

/// <summary>Provee los datos de los reportes predefinidos de préstamos desde NewSgpa2.</summary>
public interface IPrestamoReporteData
{
    Task<IReadOnlyList<PrestamoCuadroLinea>> GetCuadroAsync(int idPrestamo, CancellationToken ct = default);
    Task<PrestamoValeData?> GetValeAsync(int idPrestamo, CancellationToken ct = default);
    Task<PrestamoAutorizacionData?> GetAutorizacionAsync(int idPrestamo, CancellationToken ct = default);
    Task<IReadOnlyList<FacturaLinea>> GetFacturasAsync(int idPrestamo, CancellationToken ct = default);
    Task<SolicitudData?> GetSolicitudAsync(int idPrestamo, CancellationToken ct = default);
}

public sealed class PrestamoReporteData(IDbExecutor db) : IPrestamoReporteData
{
    public Task<IReadOnlyList<PrestamoCuadroLinea>> GetCuadroAsync(int idPrestamo, CancellationToken ct = default)
        => db.QueryAsync<PrestamoCuadroLinea>(
            "SELECT * FROM dbo.Rpt_PrestamoCuadro WHERE IDPrestamo = @idPrestamo ORDER BY NroCuota",
            new { idPrestamo }, cancellationToken: ct);

    public Task<PrestamoValeData?> GetValeAsync(int idPrestamo, CancellationToken ct = default)
        => db.QuerySingleOrDefaultAsync<PrestamoValeData>(
            "SELECT TOP 1 * FROM dbo.[1009_PrestamoVale](@idPrestamo) ORDER BY FechaVencimiento",
            new { idPrestamo }, cancellationToken: ct);

    public Task<PrestamoAutorizacionData?> GetAutorizacionAsync(int idPrestamo, CancellationToken ct = default)
        => db.QuerySingleOrDefaultAsync<PrestamoAutorizacionData>(
            "SELECT TOP 1 * FROM dbo.[2000_Rpt_AutorizacionxIDPrestamo](@idPrestamo)",
            new { idPrestamo }, cancellationToken: ct);

    public Task<IReadOnlyList<FacturaLinea>> GetFacturasAsync(int idPrestamo, CancellationToken ct = default)
        => db.QueryAsync<FacturaLinea>(
            "SELECT * FROM dbo.Rpt_Factura WHERE IDPrestamo = @idPrestamo ORDER BY NroCuota",
            new { idPrestamo }, cancellationToken: ct);

    public Task<SolicitudData?> GetSolicitudAsync(int idPrestamo, CancellationToken ct = default)
        => db.QuerySingleOrDefaultAsync<SolicitudData>(
            "SELECT TOP 1 a.* FROM dbo.[2000_Rpt_AfiliadoxCI]((SELECT CI FROM dbo.SP_Prestamo WHERE IDPrestamo = @idPrestamo)) a",
            new { idPrestamo }, cancellationToken: ct);
}
