namespace Sgpa.Web.State;

/// <summary>
/// Estado global (por circuito) de "cambios manuales sin guardar". Lo comparten la página que edita en memoria y
/// el guard de navegación (<c>UnsavedChangesGuard</c> + <see cref="Microsoft.AspNetCore.Components.Routing.NavigationLock"/>),
/// que advierte antes de navegar/recargar. Registrado como <b>scoped</b>: en Blazor Server hay un scope por circuito,
/// compartido por todas las islas interactivas, así que la página y el guard ven el mismo estado.
///
/// Una página reporta su estado con <see cref="Set"/> y lo limpia en <c>Dispose</c> con <see cref="Clear"/>. Se
/// rastrea por "owner" (la instancia del componente) para soportar varios orígenes a la vez sin pisarse.
/// </summary>
public sealed class UnsavedChangesService
{
    private readonly HashSet<object> _owners = new();

    /// <summary>¿Hay algún origen con cambios sin guardar?</summary>
    public bool HasUnsavedChanges => _owners.Count > 0;

    /// <summary>Se dispara cuando cambia <see cref="HasUnsavedChanges"/> (para re-renderizar el guard).</summary>
    public event Action? Changed;

    /// <summary>Marca/desmarca a <paramref name="owner"/> como con cambios sin guardar.</summary>
    public void Set(object owner, bool dirty)
    {
        var cambio = dirty ? _owners.Add(owner) : _owners.Remove(owner);
        if (cambio) Changed?.Invoke();
    }

    /// <summary>Limpia el estado de un origen (llamar en su <c>Dispose</c>).</summary>
    public void Clear(object owner) => Set(owner, false);
}
