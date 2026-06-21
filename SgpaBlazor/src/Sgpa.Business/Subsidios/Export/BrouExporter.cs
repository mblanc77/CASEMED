using System.Globalization;
using System.Text;
using Sgpa.Data;
using static Sgpa.Business.Formatting.FixedWidthFormatter;

namespace Sgpa.Business.Subsidios.Export;

/// <summary>
/// Genera el archivo de pagos del BROU (registros de detalle + registro total).
/// Port de la clase VB6 <c>cBROU</c>.
/// </summary>
public sealed class BrouExporter
{
    private const string CodigoEmpresa = "R6";
    private const int CodigoConvenio = 1240;
    private const int CodigoServicio = 0;
    private const int CodigoMoneda = 98;

    private readonly IDbExecutor _db;
    public BrouExporter(IDbExecutor db) => _db = db;

    public async Task<BrouResult> GenerarAsync(int mes, int anio, DateTime fechaPago, bool liquidar,
        CancellationToken cancellationToken = default)
    {
        var rows = await _db.QueryProcAsync<BankExportRow>("dbo.acc_sgpa_Rs_Export_BROU",
            new { pMes = mes.ToString(), pAnio = anio.ToString(), pLiquidar = liquidar ? "1" : "0", pFecha = fechaPago },
            cancellationToken).ConfigureAwait(false);

        var sb = new StringBuilder();
        decimal total = 0m;
        var cantidad = 0;
        foreach (var r in rows)
        {
            sb.Append(BuildDetalle(r, fechaPago)).Append("\r\n");
            total += SubsidioMath.Money2(r.ImpLiquido);
            cantidad++;
        }
        sb.Append(BuildTotal(fechaPago, cantidad, total)).Append("\r\n");
        return new BrouResult(sb.ToString(), total, cantidad);
    }

    /// <summary>Registro de detalle BROU. Puro: testeable sin BD.</summary>
    public static string BuildDetalle(BankExportRow r, DateTime fecha)
    {
        var cuenta = long.TryParse(r.NroCuenta, NumberStyles.Integer, CultureInfo.InvariantCulture, out var c) ? c : 0L;
        var importeCent = (long)(SubsidioMath.Money2(r.ImpLiquido) * 100m);
        return Numero(1, 1)                          // Tipo registro
             + Texto("", 1)                          // Marca
             + Numero(1, 3)                          // Banco
             + Texto(CodigoEmpresa, 2)               // Empresa
             + fecha.ToString("yyMMdd")              // Fecha vencimiento
             + Numero(CodigoServicio, 15)            // Servicio
             + Numero(CodigoConvenio, 6)             // Convenio
             + Numero(CodigoMoneda, 2)               // Moneda
             + Texto("D", 1)                         // Código registro
             + Numero(cuenta, 11)                    // Cuenta
             + Numero(long.Parse(fecha.ToString("yyMM")), 4) // Producción
             + Numero(importeCent, 15)               // Importe
             + Numero(0, 13)                         // Comisión
             + Texto("", 48);                        // Filler
    }

    /// <summary>Registro total (trailer) BROU. Puro: testeable sin BD.</summary>
    public static string BuildTotal(DateTime fecha, int cantidad, decimal total)
    {
        var totalCent = (long)(SubsidioMath.Money2(total) * 100m);
        return Numero(2, 1)                          // Tipo registro
             + Texto("", 1)                          // Marca
             + Numero(1, 3)                          // Banco
             + Texto(CodigoEmpresa, 2)               // Empresa
             + fecha.ToString("yyMMdd")              // Fecha vencimiento
             + Numero(cantidad, 6)                   // Pagos a acreditar
             + Numero(totalCent, 18)                 // Total a acreditar
             + Numero(0, 6) + Numero(0, 18)          // Acreditados
             + Numero(0, 6) + Numero(0, 18)          // No acreditados
             + Numero(0, 16)                         // Comisiones BROU
             + Texto("", 27);                        // Filler
    }
}

public sealed record BrouResult(string Content, decimal Total, int Cantidad);
