namespace Sgpa.Data.Preferencias;

/// <summary>
/// Persistencia de la personalización de una pantalla (grilla) por usuario logueado.
/// Guarda un blob JSON opaco (layout del grid + sumarios) por par (Login, Vista).
/// Es la base del "auto-guardado tipo XAF": el componente serializa su estado y lo persiste acá.
/// </summary>
public interface IPreferenciaVistaStore
{
    /// <summary>JSON guardado para (login, vista), o null si el usuario no personalizó esa pantalla.</summary>
    Task<string?> GetAsync(string login, string vista, CancellationToken ct = default);

    /// <summary>Upsert del JSON de personalización para (login, vista).</summary>
    Task SaveAsync(string login, string vista, string json, CancellationToken ct = default);

    /// <summary>Borra la personalización de (login, vista) — "Restablecer vista".</summary>
    Task ResetAsync(string login, string vista, CancellationToken ct = default);
}
