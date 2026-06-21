using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Auditoria;
using Sgpa.Data.Configuracion;
using Sgpa.Data.Crud;
using Sgpa.Domain.Metadata;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Tests;

/// <summary>Construye un DapperCrudService con dependencias no-op de auditoría/config para los tests.</summary>
internal static class TestCrud
{
    public static DapperCrudService<T> Create<T>(IDbExecutor db, ICurrentUser? user = null) where T : class
        => new(db, user ?? new FakeCurrentUser(), new NoAudit(), new NoTablaConfig());
}

internal sealed class NoAudit : IAuditService
{
    public Task LogInsertAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default) => Task.CompletedTask;
    public Task LogUpdateAsync(EntityMetadata meta, object oldEntity, object newEntity, string? login, CancellationToken ct = default) => Task.CompletedTask;
    public Task LogDeleteAsync(EntityMetadata meta, object entity, string? login, CancellationToken ct = default) => Task.CompletedTask;
}

internal sealed class NoTablaConfig : ITablaConfigService
{
    public Task EnsureLoadedAsync(CancellationToken ct = default) => Task.CompletedTask;
    public TablaConfig Get(string tabla) => TablaConfig.Default;
    public string DisplayName(string tabla) => tabla;
    public bool DisponibleReportes(string tabla) => true;
    public IReadOnlyDictionary<string, TablaConfig> All { get; } = new Dictionary<string, TablaConfig>();
    public Task SetAsync(string tabla, TablaConfig config, CancellationToken ct = default) => Task.CompletedTask;
}
