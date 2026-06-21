using System.Globalization;
using Microsoft.Extensions.Logging;
using Sgpa.Domain.Metadata;

namespace Sgpa.Data.Auditoria;

/// <summary>Implementación de <see cref="IAuditService"/> sobre dbo.AuditCambio (best-effort).</summary>
public sealed class AuditService : IAuditService
{
    private const string InsertSql =
        "INSERT INTO dbo.AuditCambio (Login, Tabla, Clave, Operacion, Campo, ValorAnterior, ValorNuevo) " +
        "VALUES (@Login, @Tabla, @Clave, @Operacion, @Campo, @ValorAnterior, @ValorNuevo)";

    private readonly IDbExecutor _db;
    private readonly ILogger<AuditService> _logger;

    public AuditService(IDbExecutor db, ILogger<AuditService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public Task LogInsertAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default)
        => WriteAsync('I', meta, KeyOf(meta, entity), login,
            Auditable(meta).Select(c => (c.Name, (string?)null, Str(c.GetValue(entity))))
                           .Where(x => x.Item3 is not null), ct);

    public Task LogUpdateAsync(EntityMetadata meta, object oldEntity, object newEntity, string? login, CancellationToken ct = default)
        => WriteAsync('U', meta, KeyOf(meta, newEntity), login,
            Auditable(meta).Select(c => (c.Name, Str(c.GetValue(oldEntity)), Str(c.GetValue(newEntity))))
                           .Where(x => !string.Equals(x.Item2, x.Item3, StringComparison.Ordinal)), ct);

    public Task LogDeleteAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default)
        => WriteAsync('D', meta, KeyOf(meta, entity), login,
            Auditable(meta).Select(c => (c.Name, Str(c.GetValue(entity)), (string?)null))
                           .Where(x => x.Item2 is not null), ct);

    private async Task WriteAsync(char op, EntityMetadata meta, string clave, string? login,
        IEnumerable<(string Campo, string? Old, string? New)> cambios, CancellationToken ct)
    {
        var rows = cambios.Select(c => new
        {
            Login = Trunc(login, 50),
            Tabla = meta.Table,
            Clave = Trunc(clave, 400),
            Operacion = op.ToString(),
            Campo = Trunc(c.Campo, 128),
            ValorAnterior = c.Old,
            ValorNuevo = c.New
        }).ToList();

        if (rows.Count == 0) return;

        try
        {
            // Dapper ejecuta el INSERT una vez por elemento de la lista (multi-exec).
            await _db.ExecuteAsync(InsertSql, rows, cancellationToken: ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo auditar la operación {Op} de {Tabla} (clave {Clave}).", op, meta.Table, clave);
        }
    }

    // Columnas auditables: todas menos las de auditoría técnica (Usr/Ts).
    private static IEnumerable<ColumnMetadata> Auditable(EntityMetadata meta) => meta.Columns.Where(c => !c.IsAudit);

    private static string KeyOf(EntityMetadata meta, object entity)
        => string.Join("|", meta.Keys.Select(k => Str(k.GetValue(entity)) ?? string.Empty));

    private static string? Str(object? value)
        => value is null or DBNull ? null : Convert.ToString(value, CultureInfo.InvariantCulture);

    private static string? Trunc(string? s, int max) => s is null ? null : (s.Length <= max ? s : s[..max]);
}
