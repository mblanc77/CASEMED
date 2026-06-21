namespace Sgpa.Domain.Security;

/// <summary>
/// Acciones permitidas sobre una tabla/entidad. Extiende el esquema de seguridad
/// por rol de los VB6 (Z_acceso / segurida.mdb) con desglose CRUD por tabla.
/// </summary>
[Flags]
public enum PermissionAction
{
    None = 0,
    Read = 1,
    Create = 2,
    Write = 4,
    Delete = 8,
    All = Read | Create | Write | Delete
}
