using Sgpa.Data;

namespace Sgpa.Business.Empresas;

/// <summary>Reglas de negocio de empresas (port de AbmEmpre.frm). Por ahora: auto-numeración del código.</summary>
public sealed class EmpresaService
{
    private readonly IDbExecutor _db;
    public EmpresaService(IDbExecutor db) => _db = db;

    /// <summary>Próximo CodEmpresa para una empresa nueva (001_Empresa_Max = MAX+1, excluyendo la 900).</summary>
    public async Task<int> ProximoCodEmpresaAsync(CancellationToken ct = default)
        => (int)(await _db.ExecuteScalarAsync<double?>(
            "SELECT [Max] FROM dbo.acc_sgpa_001_Empresa_Max_q", cancellationToken: ct).ConfigureAwait(false) ?? 1d);
}
