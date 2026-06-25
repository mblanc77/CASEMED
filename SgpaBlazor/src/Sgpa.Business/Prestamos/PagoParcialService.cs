using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Prestamos;

/// <summary>Un pago parcial de un préstamo (SP_PagoParcial).</summary>
public sealed record PagoParcialView(DateTime Fecha, double Importe, double TasaCambio);

/// <summary>
/// Pagos parciales de un préstamo (port de cAdmPagoParcial / frmIngPagoParcial, app VB6 "SP"). Un pago parcial
/// es un adelanto contra el préstamo; se guarda con la tasa de cambio de la moneda del préstamo (TasaxCambio).
/// </summary>
public sealed class PagoParcialService
{
    private const int UsrMaxLen = 8;

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public PagoParcialService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    public Task<IReadOnlyList<PagoParcialView>> GetPagosAsync(int idPrestamo, CancellationToken ct = default)
        => _db.QueryAsync<PagoParcialView>(
            @"SELECT Fecha, CAST(ISNULL(Importe,0) AS float) Importe, CAST(ISNULL(TasaCambio,0) AS float) TasaCambio
              FROM dbo.SP_PagoParcial WHERE IDPrestamo=@id ORDER BY Fecha DESC",
            new { id = idPrestamo }, cancellationToken: ct);

    /// <summary>Total de pagos parciales del préstamo (port de TotalPagoParcialPorPrestamo).</summary>
    public async Task<double> GetTotalAsync(int idPrestamo, CancellationToken ct = default)
        => await _db.ExecuteScalarAsync<double?>(
            "SELECT CAST(ISNULL(SUM(Importe),0) AS float) FROM dbo.SP_PagoParcial WHERE IDPrestamo=@id",
            new { id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

    /// <summary>Tasa de cambio de la moneda del préstamo (port de TasaxCambio / 1002_TasasxCodMoneda).</summary>
    public async Task<double> GetTasaCambioAsync(int idPrestamo, CancellationToken ct = default)
        => await _db.ExecuteScalarAsync<double?>(
            @"SELECT CAST(ISNULL(m.TasaCambio,1) AS float)
              FROM dbo.SP_Prestamo p JOIN dbo.SP_Moneda m ON p.CodMoneda=m.CodMoneda
              WHERE p.IDPrestamo=@id",
            new { id = idPrestamo }, cancellationToken: ct).ConfigureAwait(false) ?? 1d;

    /// <summary>
    /// Ingresa un pago parcial (port de cAdmPagoParcial.Ingresar): guarda la tasa de cambio de la moneda del
    /// préstamo al momento del pago.
    /// </summary>
    public async Task IngresarAsync(int idPrestamo, DateTime fecha, double importe, CancellationToken ct = default)
    {
        var tasa = await GetTasaCambioAsync(idPrestamo, ct).ConfigureAwait(false);
        await _db.ExecuteAsync(
            @"INSERT INTO dbo.SP_PagoParcial (IDPrestamo, Fecha, Importe, TasaCambio, Usr, Ts)
              VALUES (@id, @fecha, @imp, @tasa, @usr, @ts)",
            new { id = idPrestamo, fecha, imp = importe, tasa, usr = ClampUsr(_user.UserName), ts = DateTime.Now },
            cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>Elimina un pago parcial por su clave (préstamo + fecha).</summary>
    public Task BorrarAsync(int idPrestamo, DateTime fecha, CancellationToken ct = default)
        => _db.ExecuteAsync(
            "DELETE FROM dbo.SP_PagoParcial WHERE IDPrestamo=@id AND Fecha=@fecha",
            new { id = idPrestamo, fecha }, cancellationToken: ct);
}
