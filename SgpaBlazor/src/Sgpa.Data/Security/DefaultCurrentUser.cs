using Sgpa.Domain.Security;

namespace Sgpa.Data.Security;

/// <summary>
/// Implementación temporal de Fase 1: identifica al usuario del proceso y concede
/// acceso total. Se reemplaza en la Fase 5 por el sistema de seguridad real
/// (roles + permisos por tabla portados de segurida.mdb).
/// </summary>
public sealed class DefaultCurrentUser : ICurrentUser
{
    public string UserName { get; } = Environment.UserName ?? "sistema";
    public bool IsInRole(string role) => true;
    public bool Can(string table, PermissionAction action) => true;
}
