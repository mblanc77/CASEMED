using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Prestamos;

namespace NewSgpa.Module.Services;

/// <summary>
/// Result DTO for cuadro de amortización calculation.
/// Replaces VB6 cCuota class from colCuotas collection.
/// </summary>
public class CuotaAmortizacion
{
    public int Nro { get; set; }
    public double Monto { get; set; }
    public double ImporteCuota { get; set; }
    public double Interes { get; set; }
    public double Amortizacion { get; set; }
    public double Saldo { get; set; }
}

/// <summary>
/// Business service for Sistema de Préstamos operations.
/// Replaces VB6 classes: cAdmPrestamo, cAdmFactura, cAdmCuota, cAdmRetencion, cAdmPago.
///
/// Key operations migrated:
///   - CargarCuadroAmortizacion → CalcularCuadroAmortizacion
///   - Cancelar                 → CancelarPrestamoAsync
///   - Anular                   → AnularPrestamoAsync
///   - CalcularCancelar         → CalcularImporteCancelacionAsync
///   - Refinanciar              → RefinanciarPrestamoAsync
///   - TopePrestamo             → CalcularTopePrestamoAsync
///   - ImporteSueldos           → GetImporteSueldosAsync
///   - ImportePrestamoAbierto   → GetImportePrestamoAbiertoAsync
///   - ImporteCuotaPrestamoAbierto → GetImporteCuotaAbiertoAsync
///   - IngresarPagoParcial      → IngresarPagoParcialAsync
///   - ImportexMora (cAdmFactura) → CalcularMoraAsync
/// </summary>
public class PrestamoAdminService(NewSgpaEFCoreDbContext db)
{
    // Loan states (from VB6 pcPrestamoEstado* constants)
    private const string EstadoActivo = "act";
    private const string EstadoCancelado = "can";
    private const string EstadoAnulado = "anu";
    private const string EstadoFinalizado = "fin";

    // Invoice states
    private const string FacturaEmitida = "emi";
    private const string FacturaAnulada = "anu";
    private const string FacturaPagada = "pag";

    // Cuota states
    private const string CuotaPendiente = "pen";
    private const string CuotaPagada = "pag";
    private const string CuotaAnulada = "anu";
    private const string CuotaCancelada = "can";

    private const string FacturaTipoCancelacion = "can";
    private const string MonedaDolar = "USD";

    /// <summary>
    /// Calculates the amortization schedule for a loan.
    /// Replaces VB6 cAdmPrestamo.CargarCuadroAmortizacion.
    /// Uses French amortization system (fixed installment).
    /// </summary>
    public List<CuotaAmortizacion> CalcularCuadroAmortizacion(int cuotas, double tasa, double monto)
    {
        var result = new List<CuotaAmortizacion>(cuotas);
        double tasaMensual = tasa / 100.0 / 12.0;
        double importeCuota = monto * (tasaMensual * Math.Pow(1 + tasaMensual, cuotas))
                              / (Math.Pow(1 + tasaMensual, cuotas) - 1);

        double saldo = monto;

        for (int i = 1; i <= cuotas; i++)
        {
            double interes = Redondear(saldo * tasaMensual);
            double amortizacion = Redondear(importeCuota - interes);
            saldo = Redondear(saldo - amortizacion);

            // Adjust last installment rounding
            if (i == cuotas && Math.Abs(saldo) < 0.01)
            {
                amortizacion += saldo;
                saldo = 0;
            }

            result.Add(new CuotaAmortizacion
            {
                Nro = i,
                Monto = Redondear(saldo + amortizacion),
                ImporteCuota = Redondear(importeCuota),
                Interes = interes,
                Amortizacion = amortizacion,
                Saldo = saldo
            });
        }

        return result;
    }

    /// <summary>
    /// Calculates early cancellation amount for a loan.
    /// Replaces VB6 cAdmPrestamo.CalcularCancelar.
    /// </summary>
    public async Task<double> CalcularImporteCancelacionAsync(int idPrestamo)
    {
        var prestamo = await db.SpPrestamos.AsNoTracking()
            .FirstOrDefaultAsync(p => p.IDPrestamo == idPrestamo)
            ?? throw new InvalidOperationException($"Préstamo {idPrestamo} no encontrado.");

        // Remaining balance from amortization table
        var saldoAmortizable = await db.SpCuadrosAmortizacion.AsNoTracking()
            .Where(ca => ca.IDPrestamo == idPrestamo)
            .OrderByDescending(ca => ca.NroCuota)
            .Select(ca => (double?)ca.Saldo)
            .FirstOrDefaultAsync() ?? 0;

        // Fallback: remaining pending cuotas
        var saldoPendienteCuotas = await db.SpCuotas.AsNoTracking()
            .Where(c => c.IDPrestamo == idPrestamo && c.CodCuotaEstado == CuotaPendiente)
            .SumAsync(c => (double)(c.Importe ?? 0));

        // Add interest for current period
        double tasa = await db.SpMonedas.AsNoTracking()
            .Where(m => m.CodMoneda == prestamo.CodMoneda)
            .Select(m => (double)(m.Tasa ?? 0))
            .FirstOrDefaultAsync();

        double tasaMensual = tasa / 100.0 / 12.0;

        double saldoBase = saldoAmortizable > 0 ? saldoAmortizable : saldoPendienteCuotas;
        return Redondear(saldoBase + (saldoBase * tasaMensual));
    }

    /// <summary>
    /// Cancels a loan early: annuls pending invoices, creates cancellation invoice.
    /// Replaces VB6 cAdmPrestamo.Cancelar.
    /// </summary>
    public async Task<bool> CancelarPrestamoAsync(int idPrestamo, string usuario)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            double importeCancelacion = await CalcularImporteCancelacionAsync(idPrestamo);

            // Annul all emitted invoices
            var facturasEmitidas = await db.SpFacturas
                .Where(f => f.IdPrestamo == idPrestamo && f.CodFacturaEstado == FacturaEmitida)
                .ToListAsync();

            foreach (var factura in facturasEmitidas)
            {
                factura.CodFacturaEstado = FacturaAnulada;
                factura.Usr = usuario;
                factura.Ts = DateTime.Now;
            }

            // Create cancellation invoice
            int proxId = (await db.SpFacturas.MaxAsync(f => (int?)f.IDFactura) ?? 0) + 1;
            int proxNro = (await db.SpFacturas.MaxAsync(f => (int?)f.NroFactura) ?? 0) + 1;

            var prestamo = await db.SpPrestamos.FirstAsync(p => p.IDPrestamo == idPrestamo);

            var facCancelacion = new SpFactura
            {
                IDFactura = proxId,
                NroFactura = proxNro,
                IdPrestamo = idPrestamo,
                FechaEmitida = DateTime.Today,
                FechaVencimiento = DateTime.Today,
                CodMoneda = prestamo.CodMoneda,
                Importe = (float)importeCancelacion,
                CodFacturaEstado = FacturaEmitida,
                CodFacturaTipo = FacturaTipoCancelacion,
                Usr = usuario,
                Ts = DateTime.Now
            };
            db.SpFacturas.Add(facCancelacion);

            // Update loan state
            prestamo.CodPrestamoEstado = EstadoCancelado;
            prestamo.Usr = usuario;
            prestamo.Ts = DateTime.Now;

            // Mark pending cuotas as cancelled
            var cuotasPendientes = await db.SpCuotas
                .Where(c => c.IDPrestamo == idPrestamo && c.CodCuotaEstado == CuotaPendiente)
                .ToListAsync();

            foreach (var cuota in cuotasPendientes)
            {
                cuota.CodCuotaEstado = CuotaCancelada;
                cuota.FechaPago = DateTime.Today;
                cuota.Usr = usuario;
                cuota.Ts = DateTime.Now;
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Annuls a loan completely: marks all invoices and cuotas as annulled.
    /// Replaces VB6 cAdmPrestamo.Anular.
    /// </summary>
    public async Task<bool> AnularPrestamoAsync(int idPrestamo, string usuario)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var prestamo = await db.SpPrestamos.FirstAsync(p => p.IDPrestamo == idPrestamo);
            prestamo.CodPrestamoEstado = EstadoAnulado;
            prestamo.Usr = usuario;
            prestamo.Ts = DateTime.Now;

            await db.SpFacturas
                .Where(f => f.IdPrestamo == idPrestamo)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(f => f.CodFacturaEstado, FacturaAnulada)
                    .SetProperty(f => f.Usr, usuario)
                    .SetProperty(f => f.Ts, DateTime.Now));

            await db.SpCuotas
                .Where(c => c.IDPrestamo == idPrestamo)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.CodCuotaEstado, CuotaAnulada)
                    .SetProperty(c => c.Usr, usuario)
                    .SetProperty(c => c.Ts, DateTime.Now));

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Refinances a loan: creates a new loan with remaining balance.
    /// Replaces VB6 cAdmPrestamo.Refinanciar.
    /// </summary>
    public async Task<int> RefinanciarPrestamoAsync(
        int idPrestamo, DateTime fecha, int cuotas, bool mismaTasa, string usuario)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var prestamoOriginal = await db.SpPrestamos.FirstAsync(p => p.IDPrestamo == idPrestamo);

            // Calculate remaining balance
            double saldo = await CalcularImporteCancelacionAsync(idPrestamo);

            // Get rate
#pragma warning disable IDE0063
_ = mismaTasa; // kept for future differentiated logic
#pragma warning restore IDE0063
            double tasa = await db.SpMonedas.AsNoTracking()
                .Where(m => m.CodMoneda == prestamoOriginal.CodMoneda)
                .Select(m => (double)(m.Tasa ?? 0))
                .FirstOrDefaultAsync();

            // Calculate new cuadro
            var cuadro = CalcularCuadroAmortizacion(cuotas, tasa, saldo);

            // Cancel original loan
            await CancelarPrestamoAsync(idPrestamo, usuario);

            // Create new loan
            int proxId = (await db.SpPrestamos.MaxAsync(p => (int?)p.IDPrestamo) ?? 0) + 1;
            var nuevoPrestamo = new SpPrestamo
            {
                IDPrestamo = proxId,
                CI = prestamoOriginal.CI,
                Fecha = fecha,
                CodEmpresa = prestamoOriginal.CodEmpresa,
                CodMoneda = prestamoOriginal.CodMoneda,
                Importe = (float)saldo,
                Cuotas = cuotas,
                ImporteCuota = (float)(cuadro.Count > 0 ? cuadro[0].ImporteCuota : 0),
                CodPrestamoEstado = EstadoActivo,
                Observaciones = $"Refinanciación del préstamo #{idPrestamo}",
                Usr = usuario,
                Ts = DateTime.Now
            };
            db.SpPrestamos.Add(nuevoPrestamo);

            // Save cuadro de amortización
            foreach (var c in cuadro)
            {
                db.SpCuadrosAmortizacion.Add(new SpCuadroAmortizacion
                {
                    IDPrestamo = proxId,
                    NroCuota = c.Nro,
                    Monto = (float)c.Monto,
                    ImporteCuota = (float)c.ImporteCuota,
                    Interes = (float)c.Interes,
                    Amortizacion = (float)c.Amortizacion,
                    Saldo = (float)c.Saldo,
                    Usr = usuario,
                    Ts = DateTime.Now
                });
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return proxId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Maximum loan amount for a CI.
    /// Replaces VB6 cAdmPrestamo.TopePrestamo.
    /// </summary>
    public async Task<double> CalcularTopePrestamoAsync(string codMoneda, long ci)
    {
        double importeSueldos = await GetImporteSueldosAsync(ci);
        double prestamoAbierto = await GetImportePrestamoAbiertoAsync(ci);
        double tope = importeSueldos - prestamoAbierto;
        return tope < 0 ? 0 : Redondear(tope);
    }

    /// <summary>
    /// Total salary income for a CI over the configured period.
    /// Replaces VB6 cAdmPrestamo.ImporteSueldos.
    /// </summary>
    public async Task<double> GetImporteSueldosAsync(long ci)
    {
        if (ci == 0) return 0;

        // Last 12 months of imponible
        int mesesAtras = 12;
        int desde = int.Parse(DateTime.Today.AddMonths(-mesesAtras).ToString("yyyyMM"));
        int hasta = int.Parse(DateTime.Today.AddMonths(-1).ToString("yyyyMM"));

        return await db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci && i.AnioMes >= desde && i.AnioMes <= hasta)
            .SumAsync(i => i.Importe ?? 0);
    }

    /// <summary>
    /// Total open loan amount for a CI (converted to local currency if USD).
    /// Replaces VB6 cAdmPrestamo.ImportePrestamoAbierto.
    /// </summary>
    public async Task<double> GetImportePrestamoAbiertoAsync(long ci)
    {
        var prestamosActivos = await db.SpPrestamos.AsNoTracking()
            .Where(p => p.CI == ci && p.CodPrestamoEstado == EstadoActivo)
            .Select(p => new { p.Importe, p.CodMoneda })
            .ToListAsync();

        double total = 0;
        foreach (var p in prestamosActivos)
        {
            double importe = p.Importe ?? 0;
            if (p.CodMoneda == MonedaDolar)
            {
                var tc = await db.SpMonedas.AsNoTracking()
                    .Where(m => m.CodMoneda == MonedaDolar)
                    .Select(m => (double)(m.TasaCambio ?? 1))
                    .FirstOrDefaultAsync();
                importe *= tc;
            }
            total += importe;
        }

        return total;
    }

    /// <summary>
    /// Current installment amount for active loans of a CI.
    /// Replaces VB6 cAdmPrestamo.ImporteCuotaPrestamoAbierto.
    /// </summary>
    public async Task<double> GetImporteCuotaAbiertoAsync(long ci)
    {
        var prestamos = await db.SpPrestamos.AsNoTracking()
            .Where(p => p.CI == ci && p.CodPrestamoEstado == EstadoActivo)
            .Select(p => new { p.ImporteCuota, p.CodMoneda })
            .ToListAsync();

        double total = 0;
        foreach (var p in prestamos)
        {
            double cuota = p.ImporteCuota ?? 0;
            if (p.CodMoneda == MonedaDolar)
            {
                var tc = await db.SpMonedas.AsNoTracking()
                    .Where(m => m.CodMoneda == MonedaDolar)
                    .Select(m => (double)(m.TasaCambio ?? 1))
                    .FirstOrDefaultAsync();
                cuota *= tc;
            }
            total += cuota;
        }

        return total;
    }

    /// <summary>
    /// Records a partial payment for a loan.
    /// Replaces VB6 cAdmPrestamo.IngresarPagoParcial.
    /// </summary>
    public async Task<bool> IngresarPagoParcialAsync(
        int idPrestamo, DateTime fecha, double importe, string usuario)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            // Find next emitted/unpaid invoices and pay them in order
            var facturasPendientes = await db.SpFacturas
                .Where(f => f.IdPrestamo == idPrestamo
                            && f.CodFacturaEstado == FacturaEmitida
                            && f.FechaPago == null)
                .OrderBy(f => f.FechaVencimiento)
                .ToListAsync();

            double restante = importe;

            foreach (var factura in facturasPendientes)
            {
                if (restante <= 0) break;

                double importeFactura = factura.Importe ?? 0;

                if (restante >= importeFactura)
                {
                    // Pay fully
                    factura.FechaPago = fecha;
                    factura.CodFacturaEstado = FacturaPagada;
                    factura.Usr = usuario;
                    factura.Ts = DateTime.Now;

                    db.SpPagos.Add(new SpPago
                    {
                        IDFactura = factura.IDFactura,
                        Fecha = fecha,
                        Importe = (float)importeFactura,
                        CodPagoOrigen = "cas",
                        Usr = usuario,
                        Ts = DateTime.Now
                    });

                    // Update cuota estado
                    var cuotaRelacionada = await db.SpCuotas
                        .Where(c => c.IDPrestamo == idPrestamo && c.CodCuotaEstado == CuotaPendiente)
                        .OrderBy(c => c.Nro)
                        .FirstOrDefaultAsync();

                    if (cuotaRelacionada != null)
                    {
                        cuotaRelacionada.CodCuotaEstado = CuotaPagada;
                        cuotaRelacionada.FechaPago = fecha;
                        cuotaRelacionada.Usr = usuario;
                        cuotaRelacionada.Ts = DateTime.Now;
                    }

                    restante -= importeFactura;
                }
            }

            // Check if loan is fully paid
            bool todasPagadas = !await db.SpCuotas
                .AnyAsync(c => c.IDPrestamo == idPrestamo && c.CodCuotaEstado == CuotaPendiente);

            if (todasPagadas)
            {
                var prestamo = await db.SpPrestamos.FirstAsync(p => p.IDPrestamo == idPrestamo);
                prestamo.CodPrestamoEstado = EstadoFinalizado;
                prestamo.Usr = usuario;
                prestamo.Ts = DateTime.Now;
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Calculates late payment interest for an invoice.
    /// Replaces VB6 cAdmFactura.ImportexMora.
    /// </summary>
    public async Task<double> CalcularMoraAsync(int nroFactura, DateTime fecha, bool tolerancia = true)
    {
        var factura = await db.SpFacturas.AsNoTracking()
            .FirstOrDefaultAsync(f => f.NroFactura == nroFactura);

        if (factura?.FechaVencimiento == null) return 0;

        int diasAtraso = (fecha - factura.FechaVencimiento.Value).Days;
        if (tolerancia) diasAtraso -= 5; // 5-day grace period

        if (diasAtraso <= 0) return 0;

        double tasaMora = await db.SpMonedas.AsNoTracking()
            .Where(m => m.CodMoneda == factura.CodMoneda)
            .Select(m => (double)(m.TasaMora ?? 0))
            .FirstOrDefaultAsync();

        double tasaDiaria = tasaMora / 100.0 / 365.0;
        return Redondear((factura.Importe ?? 0) * tasaDiaria * diasAtraso);
    }

    private static double Redondear(double valor)
        => Math.Round(valor, 2, MidpointRounding.AwayFromZero);
}
