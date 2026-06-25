namespace Sgpa.Data.Crud;

/// <summary>Tipo de evento CUD emitido por <see cref="DapperCrudService{TEntity}"/>.</summary>
public enum EntityChangeKind { Inserted, Updated, Deleted }

/// <summary>
/// Contexto de un cambio CUD sobre una entidad. <see cref="Previous"/> sólo viene poblado en
/// <see cref="EntityChangeKind.Updated"/> (estado anterior al UPDATE). <see cref="Db"/> es el executor
/// ambiente para que el handler reutilice la conexión sin abrir otra.
/// </summary>
public sealed record EntityChange<TEntity>(
    EntityChangeKind Kind, TEntity Entity, TEntity? Previous, IDbExecutor Db) where TEntity : class;

/// <summary>
/// Orquestación reutilizable de procesos disparados por eventos CUD: registrá un
/// <c>IEntityChangeHandler&lt;T&gt;</c> en el DI y <see cref="DapperCrudService{TEntity}"/> lo invoca tras
/// cada Insert/Update/Delete de esa entidad. Si no hay ninguno registrado para <typeparamref name="TEntity"/>
/// el pipeline no hace nada (cero overhead). Para entidades con CRUD a medida, invocá el handler donde
/// corresponda (ej. la liquidación de subsidios reusa el mismo servicio de dominio).
/// </summary>
public interface IEntityChangeHandler<TEntity> where TEntity : class
{
    Task HandleAsync(EntityChange<TEntity> change, CancellationToken cancellationToken = default);
}
