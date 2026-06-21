using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Prestamos;

namespace NewSgpa.Module.Services;

/// <summary>
/// A record parsed from an Abitab payment file.
/// </summary>
public class AbitabPagoRecord
{
    public long NumLinea { get; set; }
    public string CodEmpresa { get; set; } = "";
    public long CI { get; set; }
    public long NroFactura { get; set; }
    public DateTime FechaVencimiento { get; set; }
    /// <summary>Importe de la cuota (cents / 100 in the file).</summary>
    public double Importe { get; set; }
    /// <summary>Importe efectivamente cobrado, may differ by mora.</summary>
    public double ImporteCobrado { get; set; }
    public int CodMoneda { get; set; }
    public int Cuota { get; set; }
    public int TipoMora { get; set; }
    public int NroAgencia { get; set; }
    public int NroSubAgencia { get; set; }
    public DateTime FechaPago { get; set; }
}

/// <summary>
/// Result line logged when a payment record fails.
/// </summary>
public class AbitabErrorRecord
{
    public long NumLinea { get; set; }
    public string Descripcion { get; set; } = "";
}

/// <summary>
/// Result of an Abitab batch import.
/// </summary>
public class AbitabImportResult
{
    public bool Success { get; set; }
    public int Procesados { get; set; }
    public int Errores { get; set; }
    public List<AbitabErrorRecord> Detalles { get; set; } = [];
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Service for importing Abitab payment files (*.dat) into the prestamos system.
///
/// Replaces VB6: frmCargaPago + cAdmPago.ProcesarPagos + cArchPago.Abrir/Leer.
///
/// Abitab file format is fixed-width ASCII.
/// Field positions are stored in the MapeoAbitab table (migrated from spserv.mdb).
/// Default positions based on the standard ABITAB 2.x layout used by CASEMED:
///
///   Pos  Len  Field
///   001   02  CodEmpresaAbitab
///   003   08  CI (numeric, left-padded)
///   011   08  NroFactura
///   019   06  FechaVencimiento (DDMMYY)
///   025   10  Importe (cents, no decimal)
///   035   02  CodMonedaAbitab
///   037   03  Cuota
///   040   01  TipoMora
///   041   02  TipoDocumento
///   043   04  Cuenta
///   047   01  DigitoControl
///   048   10  ImporteCobrado (cents, no decimal)
///   058   04  NroAgencia
///   062   03  NroSubAgencia
///   065   06  FechaPago (DDMMYY)
/// </summary>
public class AbitabPaymentImportService(NewSgpaEFCoreDbContext db)
{
    // Default fixed-width positions (1-based) from MapeoAbitab
    private static readonly Dictionary<string, (int Inicio, int Largo)> DefaultMap = new()
    {
        ["CodEmpresaAbitab"] = (1,  2),
        ["CI"]               = (3,  8),
        ["NroFactura"]       = (11, 8),
        ["FechaVencimiento"] = (19, 6),
        ["Importe"]          = (25, 10),
        ["CodMonedaAbitab"]  = (35, 2),
        ["Cuota"]            = (37, 3),
        ["TipoMora"]         = (40, 1),
        ["TipoDocumento"]    = (41, 2),
        ["Cuenta"]           = (43, 4),
        ["DigitoControl"]    = (47, 1),
        ["ImporteCobrado"]   = (48, 10),
        ["NroAgencia"]       = (58, 4),
        ["NroSubAgencia"]    = (62, 3),
        ["FechaPago"]        = (65, 6),
    };

    /// <summary>
    /// Loads field-position mapping from the database MapeoAbitab table.
    /// Falls back to <see cref="DefaultMap"/> if the table is empty.
    /// </summary>
    private async Task<Dictionary<string, (int Inicio, int Largo)>> LoadMapAsync()
    {
        try
        {
            var rows = await db.MapeoAbitabs.AsNoTracking()
                .Select(m => new { m.Campo, m.Inicio, m.Largo })
                .ToListAsync();

            if (rows.Count == 0) return DefaultMap;

            var mapped = rows
                .Where(r => r.Campo != null && r.Inicio.HasValue && r.Largo.HasValue)
                .ToDictionary(
                    r => r.Campo!,
                    r => (r.Inicio!.Value, r.Largo!.Value));

            return mapped.Count == 0 ? DefaultMap : mapped;
        }
        catch
        {
            // Table missing or schema mismatch: use default fixed-width map.
            return DefaultMap;
        }
    }

    /// <summary>
    /// Parses an Abitab fixed-width file content into records.
    /// Replaces VB6 cArchPago.Abrir + Leer loop.
    /// </summary>
    public async Task<List<AbitabPagoRecord>> ParseFileAsync(string content)
    {
        var map = await LoadMapAsync();
        var records = new List<AbitabPagoRecord>();
        long lineNum = 0;

        foreach (var rawLine in content.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
        {
            lineNum++;
            var line = rawLine;
            if (line.Length < 50) continue; // skip short/header lines

            records.Add(new AbitabPagoRecord
            {
                NumLinea          = lineNum,
                CodEmpresa        = Extract(line, map, "CodEmpresaAbitab"),
                CI                = ExtractLong(line, map, "CI"),
                NroFactura        = ExtractLong(line, map, "NroFactura"),
                FechaVencimiento  = ExtractDate(line, map, "FechaVencimiento"),
                Importe           = ExtractDecimal(line, map, "Importe"),
                ImporteCobrado    = ExtractDecimal(line, map, "ImporteCobrado"),
                CodMoneda         = ExtractInt(line, map, "CodMonedaAbitab"),
                Cuota             = ExtractInt(line, map, "Cuota"),
                TipoMora          = ExtractInt(line, map, "TipoMora"),
                NroAgencia        = ExtractInt(line, map, "NroAgencia"),
                NroSubAgencia     = ExtractInt(line, map, "NroSubAgencia"),
                FechaPago         = ExtractDate(line, map, "FechaPago"),
            });
        }

        return records;
    }

    /// <summary>
    /// Processes parsed Abitab records against the database.
    /// Replaces VB6 cAdmPago.ProcesarPagos + cAdmPago.Ingresar.
    /// Each record: validates invoice state, records payment, updates cuota/prestamo.
    /// </summary>
    public async Task<AbitabImportResult> ImportarAsync(
        List<AbitabPagoRecord> records, DateTime fechaCarga, string usuario)
    {
        var result = new AbitabImportResult();

        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            foreach (var rec in records)
            {
                string? errorDesc = null;

                var factura = await db.SpFacturas
                    .Include(f => f.Prestamo)
                    .FirstOrDefaultAsync(f => f.NroFactura == (int)rec.NroFactura);

                if (factura == null)
                {
                    errorDesc = $"Línea {rec.NumLinea}: no existe el número de factura {rec.NroFactura}.";
                }
                else if (factura.CodFacturaEstado != "emi")
                {
                    errorDesc = $"Línea {rec.NumLinea}: estado de factura '{factura.CodFacturaEstado}' no admite pago.";
                }
                else
                {
                    // Mora = importeCobrado - importe
                    double mora = rec.ImporteCobrado - rec.Importe;

                    // Mark invoice as paid
                    factura.FechaPago = rec.FechaPago;
                    factura.CodFacturaEstado = "pag";
                    factura.Usr = usuario;
                    factura.Ts = DateTime.Now;

                    // Insert payment record
                    db.SpPagos.Add(new SpPago
                    {
                        IDFactura     = factura.IDFactura,
                        Fecha         = rec.FechaPago,
                        Importe       = (float)(rec.Importe + mora),
                        CodSucursal   = $"{rec.NroAgencia}/{rec.NroSubAgencia}",
                        CodPagoOrigen = "abt", // Abitab
                        Usr           = usuario,
                        Ts            = DateTime.Now
                    });

                    // Update the related cuota
                    var cuota = await db.SpCuotas
                        .Where(c => c.IDPrestamo == factura.IdPrestamo
                                    && c.CodCuotaEstado == "pen")
                        .OrderBy(c => c.Nro)
                        .FirstOrDefaultAsync();

                    if (cuota != null)
                    {
                        cuota.CodCuotaEstado = "pag";
                        cuota.FechaPago      = rec.FechaPago;
                        cuota.Usr            = usuario;
                        cuota.Ts             = DateTime.Now;
                    }

                    // Auto-finalize prestamo if all cuotas paid
                    if (factura.Prestamo != null)
                    {
                        bool hayPendientes = await db.SpCuotas
                            .AnyAsync(c => c.IDPrestamo == factura.IdPrestamo
                                          && c.CodCuotaEstado == "pen");
                        if (!hayPendientes)
                        {
                            factura.Prestamo.CodPrestamoEstado = "fin";
                            factura.Prestamo.Usr = usuario;
                            factura.Prestamo.Ts  = DateTime.Now;
                        }
                    }

                    result.Procesados++;
                }

                if (errorDesc != null)
                {
                    result.Detalles.Add(new AbitabErrorRecord
                    {
                        NumLinea    = rec.NumLinea,
                        Descripcion = errorDesc
                    });
                    result.Errores++;
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            result.Success       = false;
            result.ErrorMessage  = ex.Message;
        }

        return result;
    }

    // ── Fixed-width field extractors ──────────────────────────────────────

    private static string Extract(
        string line, Dictionary<string, (int Inicio, int Largo)> map, string campo)
    {
        if (!map.TryGetValue(campo, out var pos)) return "";
        int start = pos.Inicio - 1;
        if (start < 0 || start + pos.Largo > line.Length) return "";
        return line.Substring(start, pos.Largo).Trim();
    }

    private static long ExtractLong(
        string line, Dictionary<string, (int Inicio, int Largo)> map, string campo)
        => long.TryParse(Extract(line, map, campo), out var v) ? v : 0;

    private static int ExtractInt(
        string line, Dictionary<string, (int Inicio, int Largo)> map, string campo)
        => int.TryParse(Extract(line, map, campo), out var v) ? v : 0;

    /// <summary>Amounts are stored as integer cents (divide by 100).</summary>
    private static double ExtractDecimal(
        string line, Dictionary<string, (int Inicio, int Largo)> map, string campo)
        => long.TryParse(Extract(line, map, campo), out var v) ? v / 100.0 : 0;

    /// <summary>Dates stored as DDMMYY.</summary>
    private static DateTime ExtractDate(
        string line, Dictionary<string, (int Inicio, int Largo)> map, string campo)
    {
        var s = Extract(line, map, campo);
        if (s.Length != 6) return DateTime.MinValue;
        if (int.TryParse(s[0..2], out int dd) &&
            int.TryParse(s[2..4], out int mm) &&
            int.TryParse(s[4..6], out int yy))
        {
            int year = yy + (yy >= 0 && yy <= 30 ? 2000 : 1900);
            try { return new DateTime(year, mm, dd); }
            catch { return DateTime.MinValue; }
        }
        return DateTime.MinValue;
    }
}
