using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Prestamos;

/// <summary>
/// Grabado de préstamos (port de <c>frmPrestamo.GrabarPrestamo</c> + <c>GrabarCuadroAmortizacion</c> +
/// <c>GenerarPagos</c>, app VB6 "SP"). Corre dentro de una transacción (atómico como el
/// BeginTrans/Commit/Rollback del VB6). Implementa el ALTA: inserta el préstamo y su cuadro de
/// amortización y, si se indica fecha de cobro, lo emite generando cuotas + facturas + detalle.
/// La edición de un préstamo existente queda para un paso posterior.
/// </summary>
public sealed class PrestamoGrabadoService
{
    // Estados / ítems (Bcpart.bas).
    private const string EstadoIngresado = "ing";
    private const string EstadoEmitido = "emi";
    private const string ItemPagoCuota = "cuo";
    private const string CuotaEstadoPendiente = "pen";
    private const string FacturaEstadoEmitida = "emi";
    private const string FacturaTipoComun = "com";

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public PrestamoGrabadoService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    private const int UsrMaxLen = 8;
    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    public async Task<GrabarPrestamoResultado> GrabarAsync(GrabarPrestamoRequest req, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(req.CodMoneda)) throw new ArgumentException("Falta la moneda.", nameof(req));
        if (req.Cuotas <= 0) throw new ArgumentException("La cantidad de cuotas debe ser mayor a cero.", nameof(req));
        if (req.Cuadro.Count != req.Cuotas)
            throw new ArgumentException("El cuadro de amortización no coincide con la cantidad de cuotas.", nameof(req));

        // Las columnas Usr de las tablas SP son nvarchar(8) (ancho del login legacy); clampeamos por las dudas.
        var usr = ClampUsr(_user.UserName);
        var emitir = req.FechaCobro is not null;
        var estado = emitir ? EstadoEmitido : EstadoIngresado;

        // Todo el alta es atómico: préstamo + cuadro + (cuotas/facturas) o nada.
        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        var repo = new PrestamoRepository(uow);

        var idPrestamo = await repo.ProximoIdPrestamoAsync(ct).ConfigureAwait(false);
        var moneda = await repo.GetMonedaPagoAsync(req.CodMoneda, ct).ConfigureAwait(false)
                     ?? new MonedaPago();

        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_Prestamo
                (IDPrestamo, CI, CodMoneda, CodPrestamoTipo, Importe, Cuotas, ImporteCuota, Saldo, Tasa,
                 CodPrestamoEstado, Fecha, FechaCobro, NroSerieCheque, NroCheque, NroCta, Banco, Sucursal,
                 Observaciones, TasaCambio, Promedio, Usr, Ts)
              VALUES
                (@IDPrestamo, @CI, @CodMoneda, @CodPrestamoTipo, @Importe, @Cuotas, @ImporteCuota, @Saldo, @Tasa,
                 @Estado, @Fecha, @FechaCobro, @NroSerieCheque, @NroCheque, @NroCta, @Banco, @Sucursal,
                 @Observaciones, @TasaCambio, @Promedio, @Usr, @Ts)",
            new
            {
                IDPrestamo = idPrestamo,
                req.CI,
                req.CodMoneda,
                req.CodPrestamoTipo,
                req.Importe,
                req.Cuotas,
                req.ImporteCuota,
                Saldo = req.Importe,
                req.Tasa,
                Estado = estado,
                req.Fecha,
                req.FechaCobro,
                req.NroSerieCheque,
                req.NroCheque,
                req.NroCta,
                req.Banco,
                req.Sucursal,
                req.Observaciones,
                moneda.TasaCambio,
                req.Promedio,
                Usr = usr,
                Ts = DateTime.Now,
            }, cancellationToken: ct).ConfigureAwait(false);

        await GrabarCuadroAmortizacionAsync(uow, idPrestamo, req, usr, ct).ConfigureAwait(false);

        var (cuotas, facturas) = emitir
            ? await GenerarPagosAsync(uow, repo,
                new PagosCtx(idPrestamo, req.CI, req.CodMoneda, req.Cuotas, req.ImporteCuota, req.Tasa, req.Importe, req.FechaCobro!.Value),
                moneda, usr, ct).ConfigureAwait(false)
            : (0, 0);

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new GrabarPrestamoResultado(idPrestamo, estado, cuotas, facturas);
    }

    /// <summary>
    /// Port de la rama de edición de GrabarPrestamo (Else): actualiza sólo los datos de cobro/cheque/banco/
    /// observaciones de un préstamo existente. Si se indica fecha de cobro y corresponde (estaba "ingresado",
    /// o cambió la fecha estando "emitido"), pasa a "emitido" y regenera cuotas + facturas. Los datos
    /// financieros (importe, cuotas, tasa, moneda) no se modifican en la edición, igual que el VB6.
    /// </summary>
    public async Task<GrabarPrestamoResultado> EditarAsync(EditarPrestamoRequest req, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        var repo = new PrestamoRepository(uow);

        var p = await repo.GetPrestamoEditAsync(req.IDPrestamo, ct).ConfigureAwait(false)
                ?? throw new InvalidOperationException($"No existe el préstamo Nº {req.IDPrestamo}.");

        // bAltaPago: sólo si hay fecha de cobro y (estaba ingresado, o cambió la fecha estando emitido).
        var emitir = false;
        var nuevoEstado = p.CodPrestamoEstado;
        if (req.FechaCobro is not null)
        {
            emitir = p.CodPrestamoEstado == EstadoIngresado
                     || (p.FechaCobro != req.FechaCobro && p.CodPrestamoEstado == EstadoEmitido);
            if (p.CodPrestamoEstado == EstadoIngresado) nuevoEstado = EstadoEmitido;
        }

        // FechaCobro sólo se actualiza si vino una nueva (el VB6 la setea dentro del If IsDate).
        var fechaCobro = req.FechaCobro ?? p.FechaCobro;

        await uow.ExecuteAsync(
            @"UPDATE dbo.SP_Prestamo SET
                  Fecha = @Fecha, FechaCobro = @FechaCobro, CodPrestamoEstado = @Estado,
                  NroSerieCheque = @NroSerieCheque, NroCheque = @NroCheque, NroCta = @NroCta,
                  Banco = @Banco, Sucursal = @Sucursal, Observaciones = @Observaciones, Usr = @Usr, Ts = @Ts
              WHERE IDPrestamo = @IDPrestamo",
            new
            {
                req.Fecha,
                FechaCobro = fechaCobro,
                Estado = nuevoEstado,
                req.NroSerieCheque,
                req.NroCheque,
                req.NroCta,
                req.Banco,
                req.Sucursal,
                req.Observaciones,
                Usr = usr,
                Ts = DateTime.Now,
                req.IDPrestamo,
            }, cancellationToken: ct).ConfigureAwait(false);

        var (cuotas, facturas) = (0, 0);
        if (emitir)
        {
            var moneda = await repo.GetMonedaPagoAsync(p.CodMoneda, ct).ConfigureAwait(false) ?? new MonedaPago();
            (cuotas, facturas) = await GenerarPagosAsync(uow, repo,
                new PagosCtx(p.IDPrestamo, p.CI, p.CodMoneda, p.Cuotas, p.ImporteCuota, p.Tasa, p.Importe, fechaCobro!.Value),
                moneda, usr, ct).ConfigureAwait(false);
        }

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new GrabarPrestamoResultado(p.IDPrestamo, nuevoEstado, cuotas, facturas);
    }

    /// <summary>Port de GrabarCuadroAmortizacion: persiste el cuadro fila a fila en SP_CuadroAmortizacion.</summary>
    private static async Task GrabarCuadroAmortizacionAsync(IDbExecutor db, int idPrestamo,
        GrabarPrestamoRequest req, string usr, CancellationToken ct)
    {
        foreach (var c in req.Cuadro)
            await db.ExecuteAsync(
                @"INSERT INTO dbo.SP_CuadroAmortizacion
                    (IDPrestamo, NroCuota, Monto, ImporteCuota, Interes, Amortizacion, Saldo, Usr, Ts)
                  VALUES (@IDPrestamo, @NroCuota, @Monto, @ImporteCuota, @Interes, @Amortizacion, @Saldo, @Usr, @Ts)",
                new
                {
                    IDPrestamo = idPrestamo,
                    NroCuota = c.Nro,
                    c.Monto,
                    ImporteCuota = c.Importe,
                    c.Interes,
                    c.Amortizacion,
                    c.Saldo,
                    Usr = usr,
                    Ts = DateTime.Now,
                }, cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>Contexto para generar cuotas/facturas: los datos financieros fijos del préstamo + fecha de cobro.</summary>
    private sealed record PagosCtx(int IdPrestamo, long CI, string CodMoneda, int Cuotas, double ImporteCuota,
        double Tasa, double Importe, DateTime FechaCobro);

    /// <summary>
    /// Port de GenerarPagos: borra cuotas/facturas previas y genera, por cada cuota, una fila de
    /// SP_Cuota (pendiente) y su factura emitida (SP_Factura) con código de barras y detalle.
    /// Vencimiento de la cuota i = FechaCobro + 30 días + (i−1) meses. El importe amortizable de cada
    /// factura se recalcula del cuadro (CargarCuadroAmortizacion sobre Cuotas/Tasa/Importe), igual que el VB6.
    /// </summary>
    private async Task<(int cuotas, int facturas)> GenerarPagosAsync(IDbExecutor db, PrestamoRepository repo,
        PagosCtx p, MonedaPago moneda, string usr, CancellationToken ct)
    {
        var baseVenc = p.FechaCobro.AddDays(30);
        var cuadro = PrestamoCalculator.CargarCuadroAmortizacion(p.Cuotas, p.Tasa, p.Importe);

        // Cuotas.
        await db.ExecuteAsync("DELETE FROM dbo.SP_Cuota WHERE IDPrestamo=@id", new { id = p.IdPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        for (int i = 1; i <= p.Cuotas; i++)
            await db.ExecuteAsync(
                @"INSERT INTO dbo.SP_Cuota
                    (IDPrestamo, Nro, FechaVencimiento, CodItemPago, Importe, CodMoneda, CodCuotaEstado, Usr, Ts)
                  VALUES (@IDPrestamo, @Nro, @FechaVencimiento, @CodItemPago, @Importe, @CodMoneda, @CodCuotaEstado, @Usr, @Ts)",
                new
                {
                    IDPrestamo = p.IdPrestamo,
                    Nro = i,
                    FechaVencimiento = baseVenc.AddMonths(i - 1),
                    CodItemPago = ItemPagoCuota,
                    Importe = p.ImporteCuota,
                    p.CodMoneda,
                    CodCuotaEstado = CuotaEstadoPendiente,
                    Usr = usr,
                    Ts = DateTime.Now,
                }, cancellationToken: ct).ConfigureAwait(false);

        // Facturas + detalle.
        await db.ExecuteAsync("DELETE FROM dbo.SP_Factura WHERE IdPrestamo=@id", new { id = p.IdPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        var nroEmpresa = await repo.GetNroEmpresaAbitabAsync(ct).ConfigureAwait(false) ?? "";
        var idFacturaBase = await repo.ProximoIdFacturaAsync(ct).ConfigureAwait(false);
        var nroFacturaBase = await repo.ProximoNroFacturaAsync(ct).ConfigureAwait(false);

        for (int i = 1; i <= p.Cuotas; i++)
        {
            var idFactura = idFacturaBase + (i - 1);
            var nroFactura = nroFacturaBase + (i - 1);
            var venc = baseVenc.AddMonths(i - 1);
            var amortizacion = cuadro[i - 1].Amortizacion;
            var codigoBarra = PrestamoBarcode.Generar(nroEmpresa, nroFactura, p.ImporteCuota, moneda.CodAbitab, p.CI, venc);

            await db.ExecuteAsync(
                @"INSERT INTO dbo.SP_Factura
                    (IDFactura, IdPrestamo, NroFactura, NroEmpresa, FechaEmitida, FechaVencimiento, Importe,
                     CodMoneda, CodFacturaEstado, TasaCambio, CodigoBarra, ImpAmortizable, ImpInteres, CodFacturaTipo, Usr, Ts)
                  VALUES
                    (@IDFactura, @IdPrestamo, @NroFactura, @NroEmpresa, @FechaEmitida, @FechaVencimiento, @Importe,
                     @CodMoneda, @Estado, @TasaCambio, @CodigoBarra, @ImpAmortizable, @ImpInteres, @Tipo, @Usr, @Ts)",
                new
                {
                    IDFactura = idFactura,
                    IdPrestamo = p.IdPrestamo,
                    NroFactura = nroFactura,
                    NroEmpresa = nroEmpresa,
                    FechaEmitida = p.FechaCobro,
                    FechaVencimiento = venc,
                    Importe = p.ImporteCuota,
                    p.CodMoneda,
                    Estado = FacturaEstadoEmitida,
                    moneda.TasaCambio,
                    CodigoBarra = codigoBarra,
                    ImpAmortizable = amortizacion,
                    ImpInteres = p.ImporteCuota - amortizacion,
                    Tipo = FacturaTipoComun,
                    Usr = usr,
                    Ts = DateTime.Now,
                }, cancellationToken: ct).ConfigureAwait(false);

            await db.ExecuteAsync(
                @"INSERT INTO dbo.SP_FacturaDetalle
                    (IdFactura, NroReng, CodItemPago, Descrip, NroCuota, Importe, Usr, Ts)
                  VALUES (@IdFactura, 1, @CodItemPago, @Descrip, @NroCuota, @Importe, @Usr, @Ts)",
                new
                {
                    IdFactura = idFactura,
                    CodItemPago = ItemPagoCuota,
                    Descrip = $"Cuota {i}/{p.Cuotas} préstamo",
                    NroCuota = i,
                    Importe = p.ImporteCuota,
                    Usr = usr,
                    Ts = DateTime.Now,
                }, cancellationToken: ct).ConfigureAwait(false);
        }

        return (p.Cuotas, p.Cuotas);
    }
}
