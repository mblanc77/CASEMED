using Sgpa.Data;

namespace Sgpa.Business.Mutualistas;

/// <summary>Reglas de negocio de mutualistas (port de AbmMutua.frm). Por ahora: auto-numeración del código.</summary>
public sealed class MutualistaService
{
    private readonly IDbExecutor _db;
    public MutualistaService(IDbExecutor db) => _db = db;

    /// <summary>Próximo CodMutualista para una mutualista nueva (001_Mutualista_Max = MAX+1).</summary>
    public async Task<int> ProximoCodMutualistaAsync(CancellationToken ct = default)
        => (int)(await _db.ExecuteScalarAsync<double?>(
            "SELECT [Max] FROM dbo.acc_sgpa_001_Mutualista_Max_q", cancellationToken: ct).ConfigureAwait(false) ?? 1d);
}
