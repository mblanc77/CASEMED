using Sgpa.Data;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Prima por fallecimiento del afiliado (port del cluster Prima de AbmAfili.frm): cargar, liquidar
/// (sugerir importe), grabar (alta/modificación) y borrar. Es un registro por afiliado (PK = CI).
/// </summary>
public sealed class PrimaService
{
    private const int UsrMaxLen = 8;

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public PrimaService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    /// <summary>Prima del afiliado (110_PrimaFallecimiento_CI), o null si no tiene.</summary>
    public async Task<PrimaFallecimiento?> GetAsync(long ci, CancellationToken ct = default)
        => (await _db.QueryAsync<PrimaFallecimiento>(
            "SELECT TOP 1 CI, FechaFirma, FechaFallecimiento, Importe, FechaPago, Observaciones FROM dbo.PrimaFallecimiento WHERE CI=@ci",
            new { ci }, cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault();

    /// <summary>
    /// Importe sugerido de la prima (port de LiquidarPrima): el menor entre el tope (TopePrima·UR) y la
    /// suma de imponibles (concepto 1) de los 6 meses previos al fallecimiento. El VB6 pasaba la ventana
    /// invertida a un BETWEEN (un bug latente que daba 0); acá se usa el rango ordenado [mes−6, mes−1].
    /// </summary>
    public async Task<double> LiquidarAsync(long ci, DateTime fechaFallecimiento, CancellationToken ct = default)
    {
        var (topePrima, ur) = await GetTopeYUrAsync(ct).ConfigureAwait(false);
        var cap = topePrima * ur;

        var mesFallecimiento = fechaFallecimiento.Year * 100 + fechaFallecimiento.Month;
        var mesFin = AddMonth(mesFallecimiento, -1);
        var mesIni = AddMonth(mesFallecimiento, -6);

        var suma = await _db.ExecuteScalarAsync<double?>(
            "SELECT Importe FROM dbo.acc_sgpa_110_Imponible_Periodo_q(@ci, @mesIni, @mesFin)",
            new { ci = (int)ci, mesIni, mesFin }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

        return Math.Min(cap, suma);
    }

    /// <summary>Alta o modificación de la prima del afiliado (port de GrabarDatosPrima, upsert por CI).</summary>
    public async Task GrabarAsync(PrimaFallecimiento p, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);
        var existe = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM dbo.PrimaFallecimiento WHERE CI=@ci", new { ci = p.CI }, cancellationToken: ct).ConfigureAwait(false) > 0;

        var args = new
        {
            p.CI, p.FechaFirma, p.FechaFallecimiento, p.Importe, p.FechaPago, p.Observaciones,
            Usr = usr, Ts = DateTime.Now,
        };

        if (existe)
            await _db.ExecuteAsync(
                @"UPDATE dbo.PrimaFallecimiento SET
                      FechaFirma=@FechaFirma, FechaFallecimiento=@FechaFallecimiento, Importe=@Importe,
                      FechaPago=@FechaPago, Observaciones=@Observaciones, Usr=@Usr, Ts=@Ts
                  WHERE CI=@CI", args, cancellationToken: ct).ConfigureAwait(false);
        else
            await _db.ExecuteAsync(
                @"INSERT INTO dbo.PrimaFallecimiento
                      (CI, FechaFirma, FechaFallecimiento, Importe, FechaPago, Observaciones, Usr, Ts)
                  VALUES (@CI, @FechaFirma, @FechaFallecimiento, @Importe, @FechaPago, @Observaciones, @Usr, @Ts)",
                args, cancellationToken: ct).ConfigureAwait(false);
    }

    /// <summary>Borra la prima del afiliado (port de BorrarPrima).</summary>
    public Task BorrarAsync(long ci, CancellationToken ct = default)
        => _db.ExecuteAsync("DELETE FROM dbo.PrimaFallecimiento WHERE CI=@ci", new { ci }, cancellationToken: ct);

    private async Task<(double TopePrima, double Ur)> GetTopeYUrAsync(CancellationToken ct)
    {
        var row = (await _db.QueryAsync<ParametrosRow>(
            "SELECT TOP 1 CAST(ISNULL(TopePrima,0) AS float) TopePrima, CAST(ISNULL(UR,0) AS float) UR FROM dbo.Parametros",
            cancellationToken: ct).ConfigureAwait(false)).FirstOrDefault() ?? new ParametrosRow();
        return (row.TopePrima, row.Ur);
    }

    /// <summary>Suma n meses a un AnioMes (yyyymm); port de AddMonth de la app Sgpa.</summary>
    public static int AddMonth(int anioMes, int n)
    {
        var d = new DateTime(anioMes / 100, anioMes % 100, 1).AddMonths(n);
        return d.Year * 100 + d.Month;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    private sealed class ParametrosRow
    {
        public double TopePrima { get; set; }
        public double Ur { get; set; }
    }
}
