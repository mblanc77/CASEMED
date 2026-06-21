using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Prestamos;

/// <summary>
/// Acciones de la toolbar de préstamos (port de cAdmPrestamo.Anular / Cancelar, app VB6 "SP").
/// Ambas corren dentro de una transacción (atómicas como el BeginTrans/Commit del VB6).
/// </summary>
public sealed class PrestamoAccionesService
{
    private const int UsrMaxLen = 8;

    // Estados (Bcpart.bas).
    private const string EstadoAnulado = "anu";
    private const string FacturaAnulada = "anu";
    private const string CuotaAnulada = "anu";
    private const string FacturaEstadoEmitida = "emi";
    private const string FacturaTipoCancelacion = "can";   // pcFacturaTipoCancelacionAnticipada
    private const string ItemPagoCancelacion = "can";       // pcItemPagoCancelacion

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public PrestamoAccionesService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    /// <summary>
    /// Port de Anular: marca el préstamo, sus facturas y sus cuotas como anulados ("anu"). No borra nada.
    /// </summary>
    public async Task AnularAsync(int idPrestamo, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Prestamo SET CodPrestamoEstado=@estado, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { estado = EstadoAnulado, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Factura SET CodFacturaEstado=@estado, Usr=@usr, Ts=@ts WHERE IdPrestamo=@id",
            new { estado = FacturaAnulada, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Cuota SET CodCuotaEstado=@estado, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { estado = CuotaAnulada, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Importe a pagar para cancelar anticipadamente (port de CalcularCancelar): saldo + interés diario
    /// desde el último vencimiento no pendiente (o la fecha de cobro si no hay) hasta hoy. No modifica nada.
    /// </summary>
    public async Task<double> CalcularCancelacionAsync(int idPrestamo, CancellationToken ct = default)
    {
        var repo = new PrestamoRepository(_db);
        var p = await repo.GetPrestamoEditAsync(idPrestamo, ct).ConfigureAwait(false)
                ?? throw new InvalidOperationException($"No existe el préstamo Nº {idPrestamo}.");
        var ultVto = await repo.GetUltVtoCuotaNoPendienteAsync(idPrestamo, ct).ConfigureAwait(false) ?? p.FechaCobro ?? DateTime.Today;
        return PrestamoCalculator.CalcularCancelacion(p.Saldo, p.Tasa, ultVto, DateTime.Today);
    }

    /// <summary>
    /// Port de Cancelar (cancelación anticipada): anula las facturas emitidas del préstamo y genera una
    /// factura de cancelación por el saldo + interés (con su detalle). El estado del préstamo y las cuotas
    /// no se tocan, igual que el VB6 (esas líneas están comentadas en el original).
    /// </summary>
    public async Task<CancelacionResultado> CancelarAsync(int idPrestamo, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;
        var hoy = DateTime.Today;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        var repo = new PrestamoRepository(uow);

        var p = await repo.GetPrestamoEditAsync(idPrestamo, ct).ConfigureAwait(false)
                ?? throw new InvalidOperationException($"No existe el préstamo Nº {idPrestamo}.");
        var ultVto = await repo.GetUltVtoCuotaNoPendienteAsync(idPrestamo, ct).ConfigureAwait(false) ?? p.FechaCobro ?? hoy;
        var importe = PrestamoCalculator.CalcularCancelacion(p.Saldo, p.Tasa, ultVto, hoy);

        // Anula sólo las facturas emitidas.
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_Factura SET CodFacturaEstado=@anu, Usr=@usr, Ts=@ts WHERE IdPrestamo=@id AND CodFacturaEstado=@emi",
            new { anu = FacturaAnulada, emi = FacturaEstadoEmitida, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        // Factura de cancelación.
        var moneda = await repo.GetMonedaPagoAsync(p.CodMoneda, ct).ConfigureAwait(false) ?? new MonedaPago();
        var nroEmpresa = await repo.GetNroEmpresaAbitabAsync(ct).ConfigureAwait(false) ?? "";
        var idFactura = await repo.ProximoIdFacturaAsync(ct).ConfigureAwait(false);
        var nroFactura = await repo.ProximoNroFacturaAsync(ct).ConfigureAwait(false);
        var codigoBarra = PrestamoBarcode.Generar(nroEmpresa, nroFactura, importe, moneda.CodAbitab, p.CI, hoy);

        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_Factura
                (IDFactura, IdPrestamo, NroFactura, NroEmpresa, FechaEmitida, FechaVencimiento, Importe,
                 CodMoneda, CodFacturaEstado, TasaCambio, CodigoBarra, ImpAmortizable, ImpInteres, CodFacturaTipo, Usr, Ts)
              VALUES
                (@IDFactura, @IdPrestamo, @NroFactura, @NroEmpresa, @Fecha, @Fecha, @Importe,
                 @CodMoneda, @Estado, @TasaCambio, @CodigoBarra, @ImpAmortizable, @ImpInteres, @Tipo, @Usr, @Ts)",
            new
            {
                IDFactura = idFactura,
                IdPrestamo = idPrestamo,
                NroFactura = nroFactura,
                NroEmpresa = nroEmpresa,
                Fecha = hoy,
                Importe = importe,
                p.CodMoneda,
                Estado = FacturaEstadoEmitida,
                moneda.TasaCambio,
                CodigoBarra = codigoBarra,
                ImpAmortizable = p.Saldo,
                ImpInteres = importe - p.Saldo,
                Tipo = FacturaTipoCancelacion,
                Usr = usr,
                Ts = ts,
            }, cancellationToken: ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_FacturaDetalle (IdFactura, NroReng, CodItemPago, Descrip, Importe, Usr, Ts)
              VALUES (@IdFactura, 1, @CodItemPago, @Descrip, @Importe, @Usr, @Ts)",
            new
            {
                IdFactura = idFactura,
                CodItemPago = ItemPagoCancelacion,
                Descrip = "Cancelación anticipada de préstamo",
                Importe = importe,
                Usr = usr,
                Ts = ts,
            }, cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new CancelacionResultado(idPrestamo, importe, idFactura, nroFactura);
    }
}
