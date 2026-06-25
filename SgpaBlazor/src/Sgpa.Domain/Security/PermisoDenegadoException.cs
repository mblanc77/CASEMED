namespace Sgpa.Domain.Security;

/// <summary>
/// Se lanza desde la capa de datos cuando el usuario actual no tiene permiso para la operación
/// solicitada (acción de tabla denegada, o registro fuera de su criterio de visibilidad/edición).
/// La UI la traduce a un mensaje y, si corresponde, redirige a /denegado.
/// </summary>
public sealed class PermisoDenegadoException : Exception
{
    public PermisoDenegadoException(string message) : base(message) { }

    /// <summary>Construye un mensaje estándar para una acción negada sobre una tabla.</summary>
    public static PermisoDenegadoException Para(string tabla, PermissionAction accion) =>
        new($"No tiene permiso para {Describir(accion)} en '{tabla}'.");

    private static string Describir(PermissionAction accion) => accion switch
    {
        PermissionAction.Read => "consultar",
        PermissionAction.Create => "dar de alta",
        PermissionAction.Write => "modificar",
        PermissionAction.Delete => "borrar",
        _ => "operar"
    };
}
