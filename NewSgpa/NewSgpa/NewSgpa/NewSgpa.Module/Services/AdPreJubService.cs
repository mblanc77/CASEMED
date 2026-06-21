using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for AdPreJub (Adelanto Pre-Jubilatorio) operations.
/// Replaces VB6 frmAdPreJub: GenerarPagos, Grabar.
/// </summary>
public class AdPreJubService(NewSgpaEFCoreDbContext db)
{
    /// <summary>
    /// Generates monthly pre-jubilation payments for all eligible afiliados.
    /// Replaces VB6 frmAdPreJub.GenerarPagos
    /// (queries 470_DeleteAdPreJubPagoxMes + 470_InsertAdPreJubPagoxMes).
    /// </summary>
    public async Task<int> GenerarPagosAsync(int mes, int anio, string usuario)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            // Delete existing payments for this month
            await db.Set<AdPreJubPago>()
                .Where(p => p.Mes == mes && p.Anio == anio)
                .ExecuteDeleteAsync();

            var fechaIni = new DateTime(anio, mes, 1);
            var fechaFin = fechaIni.AddMonths(1).AddDays(-1);

            // Get all eligible AdPreJub (active, within date range)
            var elegibles = await db.Set<AdPreJub>().AsNoTracking()
                .Where(a => a.FechaPresentacion != null
                            && a.FechaPresentacion <= fechaFin
                            && (a.FechaJubilacion == null || a.FechaJubilacion >= fechaIni))
                .ToListAsync();

            int count = 0;
            foreach (var adPreJub in elegibles)
            {
                db.Set<AdPreJubPago>().Add(new AdPreJubPago
                {
                    CI = adPreJub.CI,
                    Mes = mes,
                    Anio = anio,
                    Fecha = null, // Set when actually paid
                    Importe = adPreJub.ImporteMensual.HasValue ? (float)adPreJub.ImporteMensual.Value : 0,
                    Observaciones = null,
                    Usr = usuario,
                    Ts = DateTime.Now
                });
                count++;
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return count;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Records a payment for a specific AdPreJub.
    /// Replaces VB6 frmAdPreJub.Grabar (query 470_AdPreJubPagoxCI-Mes + update).
    /// </summary>
    public async Task<bool> RegistrarPagoAsync(
        long ci, int mes, int anio, DateTime fecha, float importe,
        string? observaciones, string usuario)
    {
        var pago = await db.Set<AdPreJubPago>()
            .FirstOrDefaultAsync(p => p.CI == ci && p.Mes == mes && p.Anio == anio);

        if (pago == null) return false;

        pago.Fecha = fecha;
        pago.Importe = importe;
        pago.Observaciones = observaciones;
        pago.Usr = usuario;
        pago.Ts = DateTime.Now;

        await db.SaveChangesAsync();
        return true;
    }
}
