using System.Globalization;
using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// DTO for records not matched during BPS import.
/// Replaces VB6 NoCargadoHL table.
/// </summary>
public class NoCargadoHLDto
{
    public long CI { get; set; }
    public string? Nombre { get; set; }
    public double Importe { get; set; }
    public string? Motivo { get; set; }
}

/// <summary>
/// Result of a BPS Hoja de Liquidación import.
/// </summary>
public class ImportHLResult
{
    public bool Success { get; set; }
    public int Insertados { get; set; }
    public int Actualizados { get; set; }
    public List<NoCargadoHLDto> NoCargados { get; set; } = [];
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Service for BPS Hoja de Liquidación (HL) import and Imponible management.
/// Replaces VB6 frmCarHL: SplitFile, SplitFile2, GrabarImponibles,
/// and queries 100_Insert_Imponible, 100_Update_Imponible,
/// 100_Insert_Imponible_New, 100_Insert_NoCargadoHL.
///
/// BPS file format (pipe-delimited):
///   Record type 4: 4|CI|Concepto|DiasTrab|Importe|...
///   CI comes with check digit in the file.
/// </summary>
public class ImponibleImportService(NewSgpaEFCoreDbContext db)
{
    /// <summary>
    /// Parsed record from BPS HL file.
    /// </summary>
    public class BpsRecord
    {
        public long CI { get; set; }
        public string Concepto { get; set; } = "";
        public int DiasTrabajados { get; set; }
        public double Importe { get; set; }
    }

    /// <summary>
    /// Parses a BPS HL text file content into records.
    /// Replaces VB6 SplitFile + SplitFile2 logic.
    /// Handles both old format (type 4) and new format (type 7→4).
    /// </summary>
    public List<BpsRecord> ParseBpsFile(string content, bool newFormat = false)
    {
        var records = new List<BpsRecord>();
        var lines = content.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var parts = line.Split('|');
            if (parts.Length < 2) continue;

            string tipo = parts[0].Trim();

            // Map new format record types to old ones
            if (newFormat)
            {
                tipo = tipo switch { "5" => "2", "6" => "3", "7" => "4", _ => tipo };
            }

            // Only process type 4 records (payroll detail)
            if (tipo != "4") continue;
            if (parts.Length < 5) continue;

            // Remove dots/separators from CI
            string ciStr = parts[1].Trim().Replace(".", "");
            if (!long.TryParse(ciStr, out long ci) || ci == 0) continue;

            string concepto = parts.Length > 2 ? parts[2].Trim() : "";
            int diasTrab = parts.Length > 3 && int.TryParse(parts[3].Trim(), out int d) ? d : 30;

            double importe = 0;
            if (parts.Length > 4)
            {
                string impStr = parts[4].Trim().Replace(",", ".");
                double.TryParse(impStr, NumberStyles.Any, CultureInfo.InvariantCulture, out importe);
            }

            records.Add(new BpsRecord
            {
                CI = ci,
                Concepto = concepto,
                DiasTrabajados = diasTrab,
                Importe = importe
            });
        }

        return records;
    }

    /// <summary>
    /// Imports parsed BPS records into the Imponible table.
    /// Replaces VB6 GrabarImponibles function with modes:
    ///   0 = Solo insertar nuevos
    ///   1 = Insertar nuevos (ignorar existentes)
    ///   2 = Actualizar existentes + insertar nuevos
    /// </summary>
    public async Task<ImportHLResult> ImportarAsync(
        List<BpsRecord> records, int codEmpresa, int mes, int anio,
        int modo, string usuario)
    {
        var result = new ImportHLResult();

        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            foreach (var rec in records)
            {
                // Find the matching Trabaja record
                var trabaja = await db.Trabajos.AsNoTracking()
                    .Where(t => t.CI == rec.CI && t.CodEmpresa == codEmpresa
                                && (t.FechaBaja == null))
                    .OrderByDescending(t => t.FechaIngreso)
                    .FirstOrDefaultAsync();

                if (trabaja == null)
                {
                    // Not found — save as NoCargado
                    var afiliado = await db.Afiliados.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.CI == rec.CI);

                    result.NoCargados.Add(new NoCargadoHLDto
                    {
                        CI = rec.CI,
                        Nombre = afiliado != null
                            ? $"{afiliado.Apellido1} {afiliado.Apellido2}, {afiliado.Nombres}"
                            : "No encontrado",
                        Importe = rec.Importe,
                        Motivo = "Sin empleo activo en la empresa"
                    });
                    continue;
                }

                // Check if record exists
                var existing = await db.Imponibles
                    .FirstOrDefaultAsync(i => i.CI == rec.CI
                                             && i.CodEmpresa == codEmpresa
                                             && i.Mes == (byte)mes
                                             && i.Anio == anio
                                             && i.Concepto == rec.Concepto);

                if (existing != null)
                {
                    if (modo == 2) // Update + Insert
                    {
                        existing.DiasTrabajados = rec.DiasTrabajados;
                        existing.Importe = rec.Importe;
                        existing.Usr = usuario;
                        existing.Ts = DateTime.Now;
                        result.Actualizados++;
                    }
                    // mode 0 and 1: skip existing
                }
                else
                {
                    // Insert new
                    db.Imponibles.Add(new Imponible
                    {
                        CI = rec.CI,
                        CodEmpresa = codEmpresa,
                        Fechaingreso = trabaja.FechaIngreso,
                        Mes = (byte)mes,
                        Anio = anio,
                        Concepto = rec.Concepto,
                        IdTrabaja = trabaja.IdTrabaja,
                        DiasTrabajados = rec.DiasTrabajados,
                        Importe = rec.Importe,
                        AnioMes = anio * 100 + mes,
                        Usr = usuario,
                        Ts = DateTime.Now
                    });
                    result.Insertados++;
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
