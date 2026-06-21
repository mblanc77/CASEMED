namespace Sgpa.Data;

/// <summary>
/// Unidad de trabajo transaccional: un <see cref="IDbExecutor"/> cuyas operaciones corren todas
/// sobre la misma conexión y transacción. Hacer <see cref="CommitAsync"/> para confirmar; si se
/// descarta sin confirmar, se hace rollback. Lo usa el orquestador de liquidación para ser atómico.
/// </summary>
public interface IDbTransactionScope : IDbExecutor, IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
