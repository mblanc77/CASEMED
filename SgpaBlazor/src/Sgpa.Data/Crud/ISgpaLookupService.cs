using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Crud;

/// <summary>Par (valor de clave → texto descriptivo) para un editor de lookup de FK.</summary>
public sealed class LookupItem
{
    public object? Value { get; set; }
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Carga los pares (clave → descripción) de una entidad de referencia para los lookups de FK
/// del CRUD genérico. Devuelve null si la entidad no es apta (sin clave simple / sin columna
/// descriptiva) o si supera el tope de filas (tablas grandes como Afiliado no se materializan).
/// </summary>
public interface ISgpaLookupService
{
    Task<IReadOnlyList<LookupItem>?> GetAsync(Type entityType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Devuelve sólo las descripciones de las claves indicadas (clave → descripción), resueltas con un IN.
    /// Sirve para mostrar el texto de columnas FK en una grilla aunque la tabla destino sea grande (Empresa,
    /// etc.): se consultan únicamente los códigos presentes. La clave del mapa es <c>valor.ToString()</c>
    /// (invariante) para evitar desajustes de tipo (int/long) al cruzar con el valor de la celda.
    /// </summary>
    Task<IReadOnlyDictionary<string, string>> GetDescriptionsAsync(
        Type entityType, IReadOnlyCollection<object> keys, CancellationToken cancellationToken = default);
}
