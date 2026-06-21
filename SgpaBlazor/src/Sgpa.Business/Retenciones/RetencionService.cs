using Sgpa.Business.Pagos;
using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Retenciones;

/// <summary>
/// Retenciones de préstamos (port de cAdmRetencion, app VB6 "SP", forms frmRetencion / frmIngRetencion).
/// Una retención retiene un conjunto de facturas emitidas: arma/actualiza el cabezal de cuenta corriente
/// (SP_RetencionPrestamo), registra la retención (SP_Retencion) y sus ítems (SP_RetencionItem), y cobra cada
/// factura con origen "retención" (lo que la deja en estado retenida). Luego la cuenta corriente se va
/// amortizando con pagos mensuales (IngresarPago). Todo lo que muta corre en una transacción atómica.
/// </summary>
public sealed class RetencionService
{
    private const int UsrMaxLen = 8;

    private const string MonedaPeso = "$";                  // pcMonedaPeso
    private const string ItemFactura = "fac";               // pcRetencionItemCodFactura
    private const string ItemTelegrama = "tel";             // pcRetencionItemCodTelegrama
    private const string FacturaEstadoEmitida = "emi";

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;
    private readonly PagoService _pagos;

    public RetencionService(IDbExecutor db, ICurrentUser user, PagoService pagos)
    {
        _db = db;
        _user = user;
        _pagos = pagos;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    /// <summary>
    /// Facturas emitidas del préstamo candidatas a retener, con su mora a la fecha (0 si la retención es
    /// directa: la directa no cobra mora, como el chkDirecta del form).
    /// </summary>
    public async Task<IReadOnlyList<FacturaRetencionView>> GetFacturasParaRetenerAsync(
        int idPrestamo, DateTime fecha, bool directa, CancellationToken ct = default)
    {
        var facturas = await _db.QueryAsync<FacturaRetencionView>(
            @"SELECT NroFactura, FechaVencimiento, CAST(ISNULL(Importe,0) AS float) Importe, CAST(0 AS float) ImpMora
              FROM dbo.SP_Factura
              WHERE IdPrestamo=@id AND CodFacturaEstado=@emi AND FechaPago IS NULL
              ORDER BY NroFactura",
            new { id = idPrestamo, emi = FacturaEstadoEmitida }, cancellationToken: ct).ConfigureAwait(false);

        if (!directa)
            foreach (var f in facturas)
                f.ImpMora = await _pagos.GetImporteMoraAsync(f.NroFactura, fecha, tolerancia: true, ct).ConfigureAwait(false);

        return facturas;
    }

    public Task<RetencionCuentaCorriente?> GetCuentaCorrienteAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QuerySingleOrDefaultAsync<RetencionCuentaCorriente>(
            @"SELECT TOP 1 IDPrestamo, CAST(ISNULL(Importe,0) AS float) Importe,
                     CAST(ISNULL(Saldo,0) AS float) Saldo, CAST(ISNULL(ImpPago,0) AS float) ImpPago
              FROM dbo.SP_RetencionPrestamo WHERE IDPrestamo=@id",
            new { id = idPrestamo }, cancellationToken: ct);

    public Task<IReadOnlyList<RetencionView>> GetRetencionesAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<RetencionView>(
            @"SELECT Fecha, CAST(ISNULL(TipoCambio,0) AS float) TipoCambio, CAST(ISNULL(Importe,0) AS float) Importe,
                     CAST(Observaciones AS nvarchar(max)) Observaciones, Directa
              FROM dbo.SP_Retencion WHERE IDPrestamo=@id ORDER BY Fecha DESC",
            new { id = idPrestamo }, cancellationToken: ct);

    public Task<IReadOnlyList<RetencionPagoView>> GetPagosAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<RetencionPagoView>(
            @"SELECT Fecha, Mes, Anio, CAST(ISNULL(Importe,0) AS float) Importe
              FROM dbo.SP_RetencionPago WHERE IDPrestamo=@id ORDER BY Anio DESC, Mes DESC",
            new { id = idPrestamo }, cancellationToken: ct);

    public Task<IReadOnlyList<RetencionAvisoView>> GetAvisosAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<RetencionAvisoView>(
            @"SELECT Fecha, CAST(Comentario AS nvarchar(max)) Comentario
              FROM dbo.SP_RetencionAviso WHERE IDPrestamo=@id ORDER BY Fecha DESC",
            new { id = idPrestamo }, cancellationToken: ct);

    /// <summary>
    /// Ingresa una retención (port de cAdmRetencion.Ingresar). El importe total se expresa en pesos
    /// (suma de las facturas + mora, por el tipo de cambio) más el telegrama. Por cada factura: registra el
    /// ítem y la cobra con origen "retención" (queda retenida). Inserta además el ítem de telegrama.
    /// </summary>
    public async Task IngresarAsync(IngresarRetencionRequest req, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        var totalFacturas = req.Facturas.Sum(f => f.Importe + f.ImpMora);
        var importeRetencion = totalFacturas * req.TipoCambio + req.ImpTelegrama;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        // Cabezal de cuenta corriente: alta o acumulación.
        var existe = await uow.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM dbo.SP_RetencionPrestamo WHERE IDPrestamo=@id", new { id = req.IDPrestamo }, cancellationToken: ct).ConfigureAwait(false) > 0;
        if (!existe)
            await uow.ExecuteAsync(
                @"INSERT INTO dbo.SP_RetencionPrestamo (IDPrestamo, CI, Fecha, CodEmpresa, CodMoneda, Importe, Saldo, ImpPago, Usr, Ts)
                  VALUES (@id, @ci, @fecha, @emp, @moneda, @imp, @imp, 0, @usr, @ts)",
                new { id = req.IDPrestamo, ci = req.CI, fecha = req.Fecha, emp = req.CodEmpresa, moneda = MonedaPeso, imp = importeRetencion, usr, ts },
                cancellationToken: ct).ConfigureAwait(false);
        else
            await uow.ExecuteAsync(
                "UPDATE dbo.SP_RetencionPrestamo SET Importe=Importe+@imp, Saldo=Saldo+@imp, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
                new { imp = importeRetencion, usr, ts, id = req.IDPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        // La retención.
        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_Retencion (IDPrestamo, Fecha, TipoCambio, Importe, Observaciones, Directa, Usr, Ts)
              VALUES (@id, @fecha, @tc, @imp, @obs, @directa, @usr, @ts)",
            new { id = req.IDPrestamo, fecha = req.Fecha, tc = req.TipoCambio, imp = importeRetencion, obs = req.Observaciones, directa = req.Directa, usr, ts },
            cancellationToken: ct).ConfigureAwait(false);

        // Ítems por factura + cobro con origen retención (queda retenida).
        foreach (var f in req.Facturas)
        {
            var idFactura = await uow.ExecuteScalarAsync<int?>(
                "SELECT IDFactura FROM dbo.SP_Factura WHERE NroFactura=@n", new { n = f.NroFactura }, cancellationToken: ct).ConfigureAwait(false)
                ?? throw new InvalidOperationException($"No existe la factura {f.NroFactura}.");

            await uow.ExecuteAsync(
                @"INSERT INTO dbo.SP_RetencionItem (IDPrestamo, Fecha, IDFactura, CodRetencionItemCod, Importe, Usr, Ts)
                  VALUES (@id, @fecha, @idf, @cod, @imp, @usr, @ts)",
                new { id = req.IDPrestamo, fecha = req.Fecha, idf = idFactura, cod = ItemFactura, imp = (f.Importe + f.ImpMora) * req.TipoCambio, usr, ts },
                cancellationToken: ct).ConfigureAwait(false);

            var r = await _pagos.IngresarPagoEnAsync(uow,
                new IngresarPagoRequest(f.NroFactura, req.Fecha, f.Importe, f.ImpMora, PagoOrigen.Retencion, null), ct).ConfigureAwait(false);
            if (r != ResultadoPago.Ok)
                throw new InvalidOperationException($"No se pudo cobrar la factura {f.NroFactura} para la retención ({r}).");
        }

        // El VB6 registraba además un ítem de telegrama con IDFactura=0, pero en NewSgpa2 SP_RetencionItem.IDFactura
        // tiene FK a SP_Factura, así que ese ítem (sin factura) no se puede insertar. El monto del telegrama queda
        // igualmente reflejado en el importe de la retención y de la cuenta corriente.

        await uow.CommitAsync(ct).ConfigureAwait(false);
    }

    /// <summary>Port de cAdmRetencion.IngresarPago: amortiza la cuenta corriente con un pago mensual.</summary>
    public async Task IngresarPagoAsync(int idPrestamo, DateTime fecha, int mes, int anio, double importe, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "UPDATE dbo.SP_RetencionPrestamo SET Saldo=Saldo-@imp, ImpPago=ImpPago+@imp, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { imp = importe, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            @"INSERT INTO dbo.SP_RetencionPago (IDPrestamo, Fecha, Mes, Anio, Importe, Usr, Ts)
              VALUES (@id, @fecha, @mes, @anio, @imp, @usr, @ts)",
            new { id = idPrestamo, fecha, mes, anio, imp = importe, usr, ts }, cancellationToken: ct).ConfigureAwait(false);
        await uow.CommitAsync(ct).ConfigureAwait(false);
    }

    /// <summary>Port de cAdmRetencion.IngresarAviso: registra un comentario/aviso de retención.</summary>
    public async Task IngresarAvisoAsync(int idPrestamo, DateTime fecha, string comentario, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        await _db.ExecuteAsync(
            @"INSERT INTO dbo.SP_RetencionAviso (IDPrestamo, Fecha, Comentario, Usr, Ts)
              VALUES (@id, @fecha, @com, @usr, @ts)",
            new { id = idPrestamo, fecha, com = comentario, usr, ts = DateTime.Now }, cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Port de cAdmRetencion.Borrar: elimina una retención (por préstamo+fecha), baja su importe del cabezal
    /// de cuenta corriente y deshace el pago de cada factura retenida (la factura vuelve a emitida).
    /// </summary>
    public async Task BorrarAsync(int idPrestamo, DateTime fecha, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        var importe = await uow.ExecuteScalarAsync<double?>(
            "SELECT CAST(ISNULL(Importe,0) AS float) FROM dbo.SP_Retencion WHERE IDPrestamo=@id AND Fecha=@f",
            new { id = idPrestamo, f = fecha }, cancellationToken: ct).ConfigureAwait(false)
            ?? throw new InvalidOperationException("No existe la retención indicada.");

        await uow.ExecuteAsync(
            "UPDATE dbo.SP_RetencionPrestamo SET Saldo=Saldo-@imp, Importe=Importe-@imp, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { imp = importe, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);

        var idsFactura = await uow.QueryAsync<int>(
            "SELECT IDFactura FROM dbo.SP_RetencionItem WHERE IDPrestamo=@id AND Fecha=@f AND CodRetencionItemCod=@cod AND IDFactura<>0",
            new { id = idPrestamo, f = fecha, cod = ItemFactura }, cancellationToken: ct).ConfigureAwait(false);
        foreach (var idFactura in idsFactura)
            await _pagos.DeshacerPagoEnAsync(uow, idFactura, ct).ConfigureAwait(false);

        await uow.ExecuteAsync("DELETE FROM dbo.SP_RetencionItem WHERE IDPrestamo=@id AND Fecha=@f", new { id = idPrestamo, f = fecha }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync("DELETE FROM dbo.SP_Retencion WHERE IDPrestamo=@id AND Fecha=@f", new { id = idPrestamo, f = fecha }, cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
    }

    /// <summary>Port de cAdmRetencion.BorrarPago: elimina un pago mensual y devuelve su importe al saldo.</summary>
    public async Task BorrarPagoAsync(int idPrestamo, DateTime fecha, int mes, int anio, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        var importe = await uow.ExecuteScalarAsync<double?>(
            "SELECT CAST(ISNULL(Importe,0) AS float) FROM dbo.SP_RetencionPago WHERE IDPrestamo=@id AND Fecha=@f AND Mes=@mes AND Anio=@anio",
            new { id = idPrestamo, f = fecha, mes, anio }, cancellationToken: ct).ConfigureAwait(false)
            ?? throw new InvalidOperationException("No existe el pago de retención indicado.");

        await uow.ExecuteAsync(
            "UPDATE dbo.SP_RetencionPrestamo SET Saldo=Saldo+@imp, ImpPago=ImpPago-@imp, Usr=@usr, Ts=@ts WHERE IDPrestamo=@id",
            new { imp = importe, usr, ts, id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "DELETE FROM dbo.SP_RetencionPago WHERE IDPrestamo=@id AND Fecha=@f AND Mes=@mes AND Anio=@anio",
            new { id = idPrestamo, f = fecha, mes, anio }, cancellationToken: ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
    }
}
