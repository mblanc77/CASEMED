using Microsoft.AspNetCore.Components.Authorization;
using Sgpa.Data.Security;
using Sgpa.Domain.Security;

namespace Sgpa.Web.Security;

/// <summary>
/// Usuario actual para Blazor Server. Carga el contexto de seguridad (roles + permisos
/// por tabla) una vez por circuito y lo cachea para consultas sincrónicas.
/// </summary>
public sealed class WebCurrentUser : ICurrentUser
{
    private readonly AuthenticationStateProvider _authProvider;
    private readonly ISecurityService _security;
    private UserSecurityContext? _context;
    private Task? _loadTask;

    public WebCurrentUser(AuthenticationStateProvider authProvider, ISecurityService security)
    {
        _authProvider = authProvider;
        _security = security;
    }

    public UserSecurityContext? Context => _context;
    public bool IsAuthenticated => _context is not null;
    public bool IsAdmin => _context?.IsAdmin ?? false;

    public string UserName => _context?.Login ?? "anon";

    public bool IsInRole(string role) =>
        _context?.Roles.Contains(role, StringComparer.OrdinalIgnoreCase) ?? false;

    public bool Can(string table, PermissionAction action) =>
        _context?.Can(table, action) ?? false;

    public PermissionAction PermissionsFor(string table) =>
        _context?.PermissionsFor(table) ?? PermissionAction.None;

    /// <summary>
    /// Carga el contexto desde la identidad autenticada. Idempotente y seguro ante
    /// llamadas concurrentes: cachea la misma Task para que todos vean el contexto cargado.
    /// </summary>
    public Task EnsureLoadedAsync() => _loadTask ??= LoadCoreAsync();

    private async Task LoadCoreAsync()
    {
        var state = await _authProvider.GetAuthenticationStateAsync();
        var login = state.User.Identity?.Name;
        if (!string.IsNullOrEmpty(login) && state.User.Identity!.IsAuthenticated)
            _context = await _security.LoadContextAsync(login);
    }
}
