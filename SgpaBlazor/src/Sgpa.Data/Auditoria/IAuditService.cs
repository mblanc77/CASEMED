using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Auditoria;

/// <summary>
/// Auditoría de cambios por campo (estilo XAF): registra alta/modificación/baja en dbo.AuditCambio.
/// Best-effort: nunca propaga (un fallo de auditoría no debe abortar la operación de negocio).
/// </summary>
public interface IAuditService
{
    /// <summary>Alta: una fila por campo con el valor inicial.</summary>
    Task LogInsertAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default);

    /// <summary>Modificación: una fila por campo CAMBIADO con valor anterior/nuevo.</summary>
    Task LogUpdateAsync(EntityMetadata meta, object oldEntity, object newEntity, string? login, CancellationToken ct = default);

    /// <summary>Baja: snapshot de los campos del registro borrado.</summary>
    Task LogDeleteAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default);
}
