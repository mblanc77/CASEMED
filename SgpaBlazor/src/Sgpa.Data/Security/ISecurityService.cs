using Sgpa.Domain.Security;

namespace Sgpa.Data.Security;

public interface ISecurityService
{
    /// <summary>Valida login + clave. Devuelve el contexto si es correcto; null si no.</summary>
    Task<UserSecurityContext?> AuthenticateAsync(string login, string password, CancellationToken cancellationToken = default);

    /// <summary>Reconstruye el contexto de un usuario ya autenticado (desde la cookie).</summary>
    Task<UserSecurityContext?> LoadContextAsync(string login, CancellationToken cancellationToken = default);
}
