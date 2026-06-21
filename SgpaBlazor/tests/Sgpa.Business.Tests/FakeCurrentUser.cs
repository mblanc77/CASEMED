using Sgpa.Domain.Security;

namespace Sgpa.Business.Tests;

/// <summary>Usuario de prueba con login corto (las columnas Usr de la base son nvarchar(8)).</summary>
internal sealed class FakeCurrentUser : ICurrentUser
{
    public string UserName => "test";
    public bool IsInRole(string role) => true;
    public bool Can(string table, PermissionAction action) => true;
}
