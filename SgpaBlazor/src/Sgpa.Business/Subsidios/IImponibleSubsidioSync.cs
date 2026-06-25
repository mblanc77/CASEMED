using Sgpa.Data;

namespace Sgpa.Business.Subsidios;

/// <summary>
/// Mantiene en <c>dbo.Imponible</c> (empresa ficticia 900 = "SUBSIDIO POR ENF.") el imponible que genera
/// la liquidación de subsidios, para que el cálculo del jornal de liquidaciones futuras lo promedie
/// (lo lee <c>acc_sgpa_300_InsertSubsidioImponibleCasemed</c>, filtrando CodEmpresa=900 y Concepto='1').
/// Es idempotente: recalcula la fila de cada (CI, período) como SUM(ImpNominal) de los cabezales liquidados.
/// </summary>
public interface IImponibleSubsidioSync
{
    /// <summary>
    /// Recalcula las filas Imponible emp900 (Concepto='1') del período. Con <paramref name="ci"/>=null
    /// procesa el período completo; con un CI sólo ese afiliado. Borra la fila si ya no quedan cabezales
    /// liquidados de ese (CI, período). <paramref name="db"/> es el executor ambiente (puede ser la
    /// transacción de la liquidación, para que quede atómico).
    /// </summary>
    Task SincronizarPeriodoAsync(IDbExecutor db, int anio, int mes, long? ci, string usr,
        CancellationToken cancellationToken = default);
}
