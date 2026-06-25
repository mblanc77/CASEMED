using Sgpa.Data;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Reglas de elegibilidad del adelanto prejubilatorio (port de AbmAfili.AdPreJubOk): el afiliado debe tener
/// régimen jubilatorio cargado, NO estar activo (sin empleo vigente) y haber tenido algún subsidio por
/// enfermedad. Cada chequeo es una sola consulta de sólo lectura.
/// </summary>
public sealed class AdPreJubService
{
    private readonly IDbExecutor _db;
    public AdPreJubService(IDbExecutor db) => _db = db;

    /// <summary>El afiliado tiene cargado el régimen jubilatorio (Afiliado.CodRegimenJubilatorio &gt; 0).</summary>
    public async Task<bool> TieneRegimenJubilatorioAsync(long ci, CancellationToken ct = default)
        => ci > 0 && (await _db.ExecuteScalarAsync<int?>(
            "SELECT CodRegimenJubilatorio FROM dbo.Afiliado WHERE CI=@ci",
            new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) ?? 0) > 0;

    /// <summary>El afiliado tiene algún empleo activo (Trabaja sin baja, 460_TrabajaActivoxCI). Si lo tiene, NO
    /// puede generar el adelanto.</summary>
    public async Task<bool> TieneTrabajoActivoAsync(long ci, CancellationToken ct = default)
        => ci > 0 && await _db.ExecuteScalarAsync<int?>(
            "SELECT TOP 1 1 FROM dbo.acc_sgpa_460_TrabajaActivoxCI_q(@ci)",
            new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) is not null;

    /// <summary>El afiliado tuvo al menos un subsidio por enfermedad (460_AfiliadoSubsidioxCI), requisito del adelanto.</summary>
    public async Task<bool> TuvoSubsidioEnfermedadAsync(long ci, CancellationToken ct = default)
        => ci > 0 && await _db.ExecuteScalarAsync<int?>(
            "SELECT TOP 1 1 FROM dbo.acc_sgpa_460_AfiliadoSubsidioxCI_q(@ci)",
            new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) is not null;
}
