using System.Text;
using Sgpa.Business.Formatting;
using Sgpa.Data;
using static Sgpa.Business.Formatting.FixedWidthFormatter;

namespace Sgpa.Business.Subsidios.Export;

/// <summary>
/// Genera el archivo de pagos de ancho fijo para el Nuevo Banco Comercial (NBC).
/// Port de la clase VB6 <c>cNBC</c>. Devuelve el contenido (la descarga la maneja la UI).
/// </summary>
public sealed class NbcExporter
{
    private readonly IDbExecutor _db;
    public NbcExporter(IDbExecutor db) => _db = db;

    public async Task<string> GenerarAsync(int mes, int anio, DateTime fechaPago, bool liquidar,
        CancellationToken cancellationToken = default)
    {
        var rows = await _db.QueryProcAsync<BankExportRow>("dbo.acc_sgpa_Rs_Export_NBC",
            new { pMes = mes.ToString(), pAnio = anio.ToString(), pLiquidar = liquidar ? "1" : "0", pFecha = fechaPago },
            cancellationToken).ConfigureAwait(false);

        var sb = new StringBuilder();
        foreach (var r in rows)
            sb.Append(BuildLine(r, fechaPago)).Append("\r\n");
        return sb.ToString();
    }

    /// <summary>Construye una línea de detalle NBC (91 caracteres). Pura: testeable sin BD.</summary>
    public static string BuildLine(BankExportRow r, DateTime fecha)
    {
        var importeCent = (long)(SubsidioMath.Money2(r.ImpLiquido) * 100m);
        return Numero(0, 18)                 // Nro documento (vacío)
             + Texto("", 2)                  // Cód documento
             + Texto("J", 1)                 // Categoría cliente
             + Numero(1, 2)                  // Operativa
             + fecha.ToString("ddMMyyyy")    // Fecha (8)
             + Texto("", 21)                 // Servicio
             + Numero(importeCent, 15)       // Importe en centésimos
             + Texto("+", 1)                 // Signo
             + Numero(0, 2)                  // Moneda (NBC = 0)
             + Texto(r.NroCuenta, 21);       // Nro cuenta
    }
}
