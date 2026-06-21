using System.Collections;

namespace Sgpa.Data.Reporting;

/// <summary>
/// Materializa, para un reporte con ObjectDataSource, el grafo de objetos de una entidad root (root +
/// referencias N-a-1 + colecciones 1-a-N) desde NewSgpa2 vía Dapper, poblando las propiedades de navegación
/// generadas. Es el "eager loading" que en XAF/XPO daría el ORM; acá se arma con el mapa <c>ReportNavMap</c>.
/// </summary>
public interface IReportGraphLoader
{
    /// <param name="rootType">Tipo de entidad root (ej. typeof(Afiliado)).</param>
    /// <param name="keyFilter">Claves a incluir (selección/filtro); null = todos.</param>
    /// <param name="maxDepth">Profundidad de navegación a poblar (1 = root + relaciones directas).</param>
    /// <param name="usedRelations">
    /// Nombres de nav props que el reporte realmente usa (de sus DataMembers/expresiones). Si se pasa, sólo se
    /// pueblan esas relaciones (optimización: evita traer las ~N colecciones que el reporte no usa). null = todas.
    /// </param>
    /// <returns>Lista tipada de instancias root con su grafo poblado.</returns>
    Task<IList> LoadAsync(Type rootType, IReadOnlyCollection<object>? keyFilter, int maxDepth = 2,
        IReadOnlySet<string>? usedRelations = null, CancellationToken ct = default);

    /// <summary>Cantidad total de filas de la tabla root (para advertir antes de procesar muchos registros).</summary>
    Task<long> CountAsync(Type rootType, CancellationToken ct = default);
}
