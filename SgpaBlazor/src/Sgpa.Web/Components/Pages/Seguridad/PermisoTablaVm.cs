using Sgpa.Domain.Security;

namespace Sgpa.Web.Components.Pages.Seguridad;

/// <summary>
/// Fila de la grilla maestra de permisos por tabla de un rol. Es referencia compartida entre la grilla y el
/// DetailView (<c>RolTablaPermisoDetalle</c>): al togglear una acción en el detalle, el resumen de la fila se refresca.
/// </summary>
public sealed class PermisoTablaVm
{
    public required string Tabla { get; init; }
    public required string Display { get; init; }
    public PermissionAction Acciones { get; set; }

    public bool Has(PermissionAction a) => (Acciones & a) == a;
}
