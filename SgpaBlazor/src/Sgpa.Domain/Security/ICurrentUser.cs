namespace Sgpa.Domain.Security;

/// <summary>
/// Usuario autenticado actual. Lo consume la auditoría (columnas Usr/Ts) y, más
/// adelante, la verificación de permisos por tabla. En la Fase 5 se conecta al
/// sistema de seguridad portado desde los VB6 (Z_acceso / segurida.mdb).
/// </summary>
public interface ICurrentUser
{
    string UserName { get; }
    bool IsInRole(string role);
    bool Can(string table, PermissionAction action);
}
