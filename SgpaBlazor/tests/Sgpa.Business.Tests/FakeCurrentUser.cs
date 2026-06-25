using Sgpa.Domain.Security;

namespace Sgpa.Business.Tests;

/// <summary>
/// Usuario de prueba. Por defecto concede acceso total (login corto, las columnas Usr de la base son nvarchar(8)).
/// Si se le pasa un <see cref="UserSecurityContext"/>, delega en él (para probar permisos de campo/registro).
/// </summary>
internal sealed class FakeCurrentUser : ICurrentUser
{
    private readonly UserSecurityContext? _ctx;

    public FakeCurrentUser(UserSecurityContext? ctx = null) => _ctx = ctx;

    public string UserName => _ctx?.Login is { Length: > 0 and <= 8 } login ? login : "test";
    public bool IsInRole(string role) => true;
    public bool Can(string table, PermissionAction action) => _ctx?.Can(table, action) ?? true;
    public bool CanReadColumn(string table, string column) => _ctx?.CanReadColumn(table, column) ?? true;
    public bool CanWriteColumn(string table, string column) => _ctx?.CanWriteColumn(table, column) ?? true;
    public RecordRule? RecordFilter(string table, PermissionAction action) => _ctx?.RecordFilter(table, action);
}
