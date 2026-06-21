using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Prestamos;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for Sistema de Préstamos queries.
/// Replaces spserv.mdb queries: Cobros, envios, rsCtaCteRet, rsFacturaEstado, etc.
/// </summary>
public class PrestamoQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>Get all prestamos with related data.</summary>
    public IQueryable<SpPrestamo> GetPrestamos()
        => db.SpPrestamos.AsNoTracking()
            .Include(p => p.Moneda)
            .Include(p => p.PrestamoEstado);

    /// <summary>Get prestamos for a specific CI.</summary>
    public IQueryable<SpPrestamo> GetPrestamosPorCi(long ci)
        => db.SpPrestamos.AsNoTracking()
            .Include(p => p.Moneda)
            .Include(p => p.PrestamoEstado)
            .Include(p => p.Facturas)
            .Where(p => p.CI == ci);

    /// <summary>
    /// Cobros: Total payments per prestamo (from SP_RetencionPago).
    /// Replaces query "Cobros" in spserv.mdb.
    /// </summary>
    public IQueryable<object> GetCobros()
        => db.SpRetencionPagos.AsNoTracking()
            .GroupBy(p => p.IDPrestamo)
            .Select(g => new
            {
                IDPrestamo = g.Key,
                Importe = g.Sum(x => x.Importe ?? 0)
            });

    /// <summary>
    /// envios: Total retentions sent per prestamo.
    /// Replaces query "envios" in spserv.mdb.
    /// </summary>
    public IQueryable<object> GetEnvios()
        => db.SpRetenciones.AsNoTracking()
            .GroupBy(r => r.IDPrestamo)
            .Select(g => new
            {
                IDPrestamo = g.Key,
                Importe = g.Sum(x => x.Importe ?? 0)
            });

    /// <summary>
    /// rsCtaCteRet: Account balance for retenciones per prestamo.
    /// Replaces query "rsCtaCteRet": envios LEFT JOIN cobros, saldo = envios - cobros.
    /// </summary>
    public async Task<IEnumerable<object>> GetCtaCteRetencionesAsync()
    {
        var envios = await db.SpRetenciones.AsNoTracking()
            .GroupBy(r => r.IDPrestamo)
            .Select(g => new { IDPrestamo = g.Key, Importe = g.Sum(x => (double)(x.Importe ?? 0)) })
            .ToListAsync();

        var cobros = await db.SpRetencionPagos.AsNoTracking()
            .GroupBy(p => p.IDPrestamo)
            .Select(g => new { IDPrestamo = g.Key, Importe = g.Sum(x => (double)(x.Importe ?? 0)) })
            .ToDictionaryAsync(c => c.IDPrestamo, c => c.Importe);

        return envios.Select(e =>
        {
            var cobro = cobros.GetValueOrDefault(e.IDPrestamo, 0);
            var saldo = Math.Abs(e.Importe - cobro) < 1 ? 0 : e.Importe - cobro;
            return (object)new { e.IDPrestamo, e.Importe, Cobros = cobro, Saldo = saldo };
        });
    }

    /// <summary>
    /// Rs_PrestamoInteresAmortizacion: Interest and amortization totals per prestamo.
    /// </summary>
    public IQueryable<object> GetInteresAmortizacion()
        => db.SpCuadrosAmortizacion.AsNoTracking()
            .GroupBy(ca => ca.IDPrestamo)
            .Select(g => new
            {
                IDPrestamo = g.Key,
                Interes = g.Sum(x => x.Interes ?? 0),
                Amortizacion = g.Sum(x => x.Amortizacion ?? 0)
            });

    /// <summary>
    /// rsFacturaEstado: Active invoices with estado filtering.
    /// Replaces query "rsFacturaEstado" in spserv.mdb.
    /// </summary>
    public IQueryable<object> GetFacturasActivas()
        => from f in db.SpFacturas.AsNoTracking()
           join p in db.SpPrestamos.AsNoTracking() on f.IdPrestamo equals p.IDPrestamo
           join pe in db.SpPrestamoEstados.AsNoTracking() on p.CodPrestamoEstado equals pe.CodPrestamoEstado
           where f.CodFacturaEstado != "anu"
                 && !pe.Fin
                 && pe.CodPrestamoEstado != "anu"
           select new
           {
               f.IdPrestamo,
               f.IDFactura,
               Mes = f.FechaVencimiento != null ? f.FechaVencimiento.Value.ToString("yyyyMM") : "",
               p.CodMoneda,
               p.Importe
           };

    /// <summary>Facturas for a specific prestamo.</summary>
    public IQueryable<SpFactura> GetFacturasPorPrestamo(int idPrestamo)
        => db.SpFacturas.AsNoTracking()
            .Include(f => f.FacturaEstado)
            .Include(f => f.Detalles)
            .Where(f => f.IdPrestamo == idPrestamo)
            .OrderByDescending(f => f.FechaVencimiento);

    /// <summary>Cuotas for a specific prestamo.</summary>
    public IQueryable<SpCuota> GetCuotasPorPrestamo(int idPrestamo)
        => db.SpCuotas.AsNoTracking()
            .Include(c => c.CuotaEstado)
            .Where(c => c.IDPrestamo == idPrestamo)
            .OrderBy(c => c.Nro);

    /// <summary>Cuadro de amortización for a prestamo.</summary>
    public IQueryable<SpCuadroAmortizacion> GetCuadroAmortizacion(int idPrestamo)
        => db.SpCuadrosAmortizacion.AsNoTracking()
            .Where(ca => ca.IDPrestamo == idPrestamo)
            .OrderBy(ca => ca.NroCuota);
}
