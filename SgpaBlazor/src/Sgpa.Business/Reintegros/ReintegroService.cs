using Sgpa.Data;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Reintegros;

/// <summary>
/// Reglas de negocio de los reintegros mutuales (port de AbmReint.frm): validación del período,
/// aviso de elegibilidad (1,25 SMN) y actualización de la cuota del mutualista al grabar.
/// </summary>
public sealed class ReintegroService
{
    // Umbral de elegibilidad: el afiliado debe llegar a 1,25 SMN de promedio (6 meses) o del último mes.
    private const double FactorElegibilidad = 1.25;

    private readonly IDbExecutor _db;
    public ReintegroService(IDbExecutor db) => _db = db;

    /// <summary>Validación bloqueante del período (port de la primera parte de DatosOk).</summary>
    public Task<string?> ValidarAsync(ReintegroMutual r, CancellationToken ct = default)
    {
        if (r.Mes < 1 || r.Mes > 12) return Task.FromResult<string?>("Ingresá un mes válido (1 a 12).");
        if (r.Anio <= 0) return Task.FromResult<string?>("Ingresá un año válido.");
        return Task.FromResult<string?>(null);
    }

    /// <summary>
    /// Aviso de elegibilidad no bloqueante (port de la parte SMN de DatosOk): si el promedio de los 6 meses
    /// (terminando 2 meses antes del período) y el último mes quedan por debajo de 1,25 SMN, se avisa.
    /// </summary>
    public async Task<IReadOnlyList<string>> GetAvisosAsync(ReintegroMutual r, CancellationToken ct = default)
    {
        var avisos = new List<string>();
        if (r.CI <= 0 || r.Mes < 1 || r.Mes > 12 || r.Anio <= 0)
            return avisos;

        var smn = await GetSmnAsync(ct).ConfigureAwait(false);
        var fin = AddMonth(r.Anio * 100 + r.Mes, -2);
        var ini = AddMonth(fin, -5);

        var promedio = await PromedioAsync("acc_sgpa_320_AfiliadoPromedio_q", r.CI, ini, fin, ct).ConfigureAwait(false);
        var ultMes = await UltMesAsync(r.CI, fin, ct).ConfigureAwait(false);

        if (promedio < FactorElegibilidad * smn && ultMes < FactorElegibilidad * smn)
            avisos.Add($"El afiliado no llega a 1,25 SMN de promedio en los últimos 6 meses ni en el último mes. " +
                       "Verificá antes de ingresar el reintegro.");

        return avisos;
    }

    /// <summary>
    /// Actualiza la cuota del mutualista con el importe del reintegro (port de ActualizarCuota /
    /// 450_UpdateCuotaMutual). Se llama tras grabar el reintegro.
    /// </summary>
    public Task ActualizarCuotaMutualAsync(ReintegroMutual r, CancellationToken ct = default)
    {
        if (r.CodMutualista is null) return Task.CompletedTask;
        return _db.ExecuteAsync(
            "UPDATE dbo.Mutualista SET Cuota=@importe WHERE CodMutualista=@cod",
            new { importe = r.Importe ?? 0f, cod = r.CodMutualista.Value }, cancellationToken: ct);
    }

    private async Task<double> GetSmnAsync(CancellationToken ct)
        => await _db.ExecuteScalarAsync<double?>(
            "SELECT TOP 1 CAST(ISNULL(SMN,0) AS float) FROM dbo.Parametros", cancellationToken: ct).ConfigureAwait(false) ?? 0d;

    private async Task<double> PromedioAsync(string func, long ci, int mesIni, int mesFin, CancellationToken ct)
        => await _db.ExecuteScalarAsync<double?>(
            $"SELECT Importe FROM dbo.{func}(@ci, @mesIni, @mesFin)",
            new { ci = (int)ci, mesIni, mesFin }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

    private async Task<double> UltMesAsync(long ci, int mesAnio, CancellationToken ct)
        => await _db.ExecuteScalarAsync<double?>(
            "SELECT Importe FROM dbo.acc_sgpa_320_AfiliadoUltMes_q(@ci, @mesAnio)",
            new { ci = (int)ci, mesAnio }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

    /// <summary>Suma n meses a un AnioMes (yyyymm).</summary>
    public static int AddMonth(int anioMes, int n)
    {
        var d = new DateTime(anioMes / 100, anioMes % 100, 1).AddMonths(n);
        return d.Year * 100 + d.Month;
    }
}
