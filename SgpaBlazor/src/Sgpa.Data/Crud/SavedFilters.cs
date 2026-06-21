using Dapper;

namespace Sgpa.Data.Crud;

/// <summary>Filtro guardado de un ListView (equivalente moderno de la tabla VB6 'Seleccion').</summary>
/// <param name="Criteria">String de DevExpress CriteriaOperator (round-trip con Parse/ToString); puede contener
/// parámetros <c>?Nombre</c> que se piden al ejecutar.</param>
/// <param name="Parametros">JSON con la definición de los parámetros (null/'' = filtro sin parámetros).</param>
public sealed record SavedFilter(int Id, string Entity, string Nombre, string Criteria, bool EsSistema, string? Usr, string? Parametros = null);

/// <summary>Persistencia de filtros guardados por entidad (tabla dbo.SgpaFiltro).</summary>
public interface ISavedFilterService
{
    /// <summary>Filtros del usuario + los de sistema/compartidos, para una entidad.</summary>
    Task<IReadOnlyList<SavedFilter>> GetForEntityAsync(string entity, string user, CancellationToken cancellationToken = default);

    /// <summary>Crea o actualiza (por Entity+Nombre+Usr) un filtro del usuario. Devuelve el Id.</summary>
    Task<int> SaveAsync(string entity, string nombre, string criteria, string user, string? parametros = null, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DapperSavedFilterService : ISavedFilterService
{
    private readonly IDbExecutor _db;
    public DapperSavedFilterService(IDbExecutor db) => _db = db;

    public Task<IReadOnlyList<SavedFilter>> GetForEntityAsync(string entity, string user, CancellationToken cancellationToken = default)
        => _db.QueryAsync<SavedFilter>(
            @"SELECT Id, Entity, Nombre, Criteria, EsSistema, Usr, Parametros FROM dbo.SgpaFiltro
              WHERE Entity = @entity AND (EsSistema = 1 OR Usr = @user)
              ORDER BY EsSistema DESC, Nombre",
            new { entity, user }, cancellationToken: cancellationToken);

    public async Task<int> SaveAsync(string entity, string nombre, string criteria, string user, string? parametros = null, CancellationToken cancellationToken = default)
    {
        var existing = await _db.QuerySingleOrDefaultAsync<int?>(
            "SELECT Id FROM dbo.SgpaFiltro WHERE Entity=@entity AND Nombre=@nombre AND Usr=@user",
            new { entity, nombre, user }, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (existing is int id)
        {
            await _db.ExecuteAsync(
                "UPDATE dbo.SgpaFiltro SET Criteria=@criteria, Parametros=@parametros, Ts=SYSDATETIME() WHERE Id=@id",
                new { criteria, parametros, id }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return id;
        }

        return await _db.ExecuteScalarAsync<int>(
            @"INSERT INTO dbo.SgpaFiltro (Entity, Nombre, Criteria, EsSistema, Usr, Parametros, Ts)
              VALUES (@entity, @nombre, @criteria, 0, @user, @parametros, SYSDATETIME());
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { entity, nombre, criteria, user, parametros }, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        => _db.ExecuteAsync("DELETE FROM dbo.SgpaFiltro WHERE Id=@id", new { id }, cancellationToken: cancellationToken);
}
