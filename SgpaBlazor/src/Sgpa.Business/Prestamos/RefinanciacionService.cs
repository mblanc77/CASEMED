using Sgpa.Business.Pagos;
using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Prestamos;

/// <summary>Resultado de una refinanciación: el préstamo nuevo (ingresado) y el importe refinanciado.</summary>
public sealed record RefinanciacionResultado(int NuevoIDPrestamo, double Importe);

/// <summary>
/// Refinanciación de préstamos (port de cAdmPrestamo.Refinanciar / ImporteRefinanciado / PuedeRefinanciar,
/// app VB6 "SP", form frmIngRefinanciacion). Salda el préstamo actual (facturas y cuotas pendientes pasan a
/// "refinanciada", con su pago de origen "refinanciación") y crea un préstamo nuevo (ingresado) por el importe
/// refinanciado, referenciando al anterior. Todo corre en una transacción atómica.
/// </summary>
public sealed class RefinanciacionService
{
    private const int UsrMaxLen = 8;

    private const string EstadoEnProceso = "pro";
    private const string EstadoRetencion = "ret";
    private const string EstadoRefinanciado = "car";
    private const string EstadoIngresado = "ing";
    private const string FacturaEstadoEmitida = "emi";
    private const string FacturaRefinanciada = "car";
    private const string CuotaRefinanciada = "car";
    private const string PagoOrigenRefinanciacion = "ref";

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;
    private readonly PagoService _pagos;

    public RefinanciacionService(IDbExecutor db, ICurrentUser user, PagoService pagos)
    {
        _db = db;
        _user = user;
        _pagos = pagos;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    /// <summary>Port de PuedeRefinanciar: sólo préstamos en proceso o en retención.</summary>
    public async Task<bool> PuedeRefinanciarAsync(int idPrestamo, CancellationToken ct = default)
    {
        var estado = await _db.ExecuteScalarAsync<string>(
            "SELECT TOP 1 CodPrestamoEstado FROM dbo.SP_Prestamo WHERE IDPrestamo=@id", new { id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        return estado is EstadoEnProceso or EstadoRetencion;
    }

    /// <summary>
    /// Port de ImporteRefinanciado: por cada factura emitida (pendiente), si todavía no venció suma su
    /// amortización; si ya venció suma el importe más la mora (sin tolerancia).
    /// </summary>
    public async Task<double> CalcularImporteRefinanciadoAsync(int idPrestamo, DateTime fecha, CancellationToken ct = default)
    {
        var facturas = await GetFacturasPendientesAsync(_db, idPrestamo, ct).ConfigureAwait(false);
        var total = 0d;
        foreach (var f in facturas)
            total += await ImportePendienteFacturaAsync(f, fecha, ct).ConfigureAwait(false);
        return total;
    }

    /// <summary>
    /// Port de Refinanciar: salda el préstamo actual y crea el nuevo (ingresado) por el importe refinanciado.
    /// Devuelve el préstamo nuevo. Lanza si el préstamo no admite refinanciación.
    /// </summary>
    public async Task<RefinanciacionResultado> RefinanciarAsync(
        int idPrestamo, DateTime fecha, int cuotas, bool mismaTasa, CancellationToken ct = default)
    {
        if (cuotas <= 0) throw new ArgumentException("La cantidad de cuotas debe ser mayor a cero.", nameof(cuotas));

        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        var old = await uow.QuerySingleOrDefaultAsync<PrestamoOrigen>(
            @"SELECT TOP 1 CI, CodMoneda, CodPrestamoTipo, CAST(ISNULL(Tasa,0) AS float) Tasa, CodPrestamoEstado
              FROM dbo.SP_Prestamo WHERE IDPrestamo=@id",
            new { id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"No existe el préstamo Nº {idPrestamo}.");
        if (old.CodPrestamoEstado is not (EstadoEnProceso or EstadoRetencion))
            throw new InvalidOperationException("El préstamo no admite refinanciación (debe estar en proceso o en retención).");

        // Facturas pendientes (emitidas) + su importe a saldar (amortización o importe+mora si venció).
        var facturas = await GetFacturasPendientesAsync(uow, idPrestamo, ct).ConfigureAwait(false);
        var importe = 0d;
        foreach (var f in facturas)
        {
            var imp = await ImportePendienteFacturaAsync(f, fecha, ct).ConfigureAwait(false);
            importe += imp;

            await uow.ExecuteAsync(
                "UPDATE dbo.SP_Factura SET CodFacturaEstado=@car, FechaPago=@f, Usr=@usr, Ts=@ts WHERE IDFactura=@id",
                new { car = FacturaRefinanciada, f = fecha, usr, ts, id = f.IDFactura }, cancellationToken: ct).ConfigureAwait(false);
            await uow.ExecuteAsync(
                @"INSERT INTO dbo.SP_Pago (IDFactura, Fecha, Importe, CodPagoOrigen, Usr, Ts)
                  VALUES (@id, @f, @imp, @origen, @usr, @ts)",
                new { id = f.IDFactura, f = fecha, imp, origen = PagoOrigenRefinanciacion, usr, ts }, cancellationToken: ct).ConfigureAwait(false);
        }

        // El préstamo actual queda refinanciado, con saldo cero.
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Prestamo SET CodPrestamoEstado=@car, Saldo=0, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { car = EstadoRefinanciado, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        // Cuotas pendientes → refinanciadas.
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Cuota SET CodCuotaEstado=@car, FechaPago=@f, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id AND CodCuotaEstado='pen'",
            new { car = CuotaRefinanciada, f = fecha, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        // Nuevo préstamo (ingresado) por el importe refinanciado.
        var nuevaTasa = mismaTasa
            ? old.Tasa
            : await uow.ExecuteScalarAsync<double>(
                "SELECT CAST(ISNULL(Tasa,0) AS float) FROM dbo.SP_Moneda WHERE CodMoneda=@m", new { m = old.CodMoneda }, cancellationToken: ct).ConfigureAwait(false);
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(cuotas, nuevaTasa, importe);

        var repo = new PrestamoRepository(uow);
        var nuevoId = await repo.ProximoIdPrestamoAsync(ct).ConfigureAwait(false);
        var promedio = await CalcularPromedioAsync(uow, old.CI, ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_Prestamo
                (IDPrestamo, CI, Fecha, CodMoneda, CodPrestamoTipo, Importe, Cuotas, ImporteCuota, Saldo,
                 Tasa, CodPrestamoEstado, Promedio, IDPrestamoRef, Usr, Ts)
              VALUES
                (@id, @ci, @fecha, @moneda, @tipo, @imp, @cuotas, @impCuota, @imp,
                 @tasa, @estado, @prom, @ref, @usr, @ts)",
            new
            {
                id = nuevoId,
                ci = old.CI,
                fecha,
                moneda = old.CodMoneda,
                tipo = old.CodPrestamoTipo,
                imp = importe,
                cuotas,
                impCuota = cuadro[0].Importe,
                tasa = nuevaTasa,
                estado = EstadoIngresado,
                prom = promedio,
                @ref = idPrestamo,
                usr,
                ts,
            }, cancellationToken: ct).ConfigureAwait(false);

        foreach (var c in cuadro)
            await uow.ExecuteAsync(
                @"INSERT INTO dbo.SP_CuadroAmortizacion
                    (IDPrestamo, NroCuota, Monto, ImporteCuota, Interes, Amortizacion, Saldo, Usr, Ts)
                  VALUES (@id, @nro, @monto, @impCuota, @interes, @amort, @saldo, @usr, @ts)",
                new { id = nuevoId, nro = c.Nro, monto = c.Monto, impCuota = c.Importe, interes = c.Interes, amort = c.Amortizacion, saldo = c.Saldo, usr, ts },
                cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new RefinanciacionResultado(nuevoId, importe);
    }

    private static Task<IReadOnlyList<FacturaPendiente>> GetFacturasPendientesAsync(IDbExecutor db, int idPrestamo, CancellationToken ct)
        => db.QueryAsync<FacturaPendiente>(
            @"SELECT IDFactura, NroFactura, FechaVencimiento,
                     CAST(ISNULL(Importe,0) AS float) Importe, CAST(ISNULL(ImpAmortizable,0) AS float) ImpAmortizable
              FROM dbo.SP_Factura WHERE IdPrestamo=@id AND CodFacturaEstado=@emi",
            new { id = idPrestamo, emi = FacturaEstadoEmitida }, cancellationToken: ct);

    /// <summary>Importe a saldar de una factura: amortización si no venció; importe + mora (sin tolerancia) si venció.</summary>
    private async Task<double> ImportePendienteFacturaAsync(FacturaPendiente f, DateTime fecha, CancellationToken ct)
    {
        if (f.FechaVencimiento >= fecha) return f.ImpAmortizable;
        var mora = await _pagos.GetImporteMoraAsync(f.NroFactura, fecha, tolerancia: false, ct).ConfigureAwait(false);
        return f.Importe + mora;
    }

    /// <summary>Promedio de ingresos del afiliado (misma ventana que el alta: MesesCalculo meses, terminando 2 meses atrás).</summary>
    private static async Task<double> CalcularPromedioAsync(IDbExecutor db, long ci, CancellationToken ct)
    {
        var meses = await db.ExecuteScalarAsync<int>(
            "SELECT TOP 1 ISNULL(MesesCalculo,3) FROM dbo.SP_Parametro", cancellationToken: ct).ConfigureAwait(false);
        if (meses <= 0) meses = 3;
        var hoy = int.Parse(DateTime.Today.ToString("yyyyMM"));
        var fin = PrestamoRepository.AddMonth(hoy, -2);
        var ini = PrestamoRepository.AddMonth(hoy, -2 - meses + 1);
        return await db.ExecuteScalarAsync<double?>(
            "SELECT CAST(ISNULL(Importe,0) AS float) FROM dbo.acc_sp_1000_AfiliadoPromedioMeses_q(@ci,@ini,@fin,@meses)",
            new { ci, ini, fin, meses }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;
    }

    private sealed class PrestamoOrigen
    {
        public long CI { get; set; }
        public string CodMoneda { get; set; } = "";
        public string? CodPrestamoTipo { get; set; }
        public double Tasa { get; set; }
        public string CodPrestamoEstado { get; set; } = "";
    }

    private sealed class FacturaPendiente
    {
        public int IDFactura { get; set; }
        public int NroFactura { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public double Importe { get; set; }
        public double ImpAmortizable { get; set; }
    }
}
