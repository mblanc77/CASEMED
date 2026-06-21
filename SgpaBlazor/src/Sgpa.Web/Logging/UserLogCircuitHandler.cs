using Microsoft.AspNetCore.Components.Server.Circuits;
using Serilog.Context;
using Sgpa.Web.Security;

namespace Sgpa.Web.Logging;

/// <summary>
/// Empuja el usuario logueado al <see cref="LogContext"/> de Serilog en cada actividad entrante del circuito
/// (interacción del usuario). Así las excepciones NO controladas que ocurren durante esa actividad —que loguea
/// el framework— quedan enriquecidas con la propiedad "Usuario", que el <see cref="DbErrorLogSink"/> guarda en
/// el campo Login de Z_ErrorLog. Es scoped: un handler por circuito, con su <see cref="WebCurrentUser"/>.
/// </summary>
public sealed class UserLogCircuitHandler : CircuitHandler
{
    private readonly WebCurrentUser _user;

    public UserLogCircuitHandler(WebCurrentUser user) => _user = user;

    public override Func<CircuitInboundActivityContext, Task> CreateInboundActivityHandler(
        Func<CircuitInboundActivityContext, Task> next)
        => async context =>
        {
            string usuario;
            try { await _user.EnsureLoadedAsync(); usuario = _user.UserName; }
            catch { usuario = "anon"; }

            using (LogContext.PushProperty("Usuario", usuario))
                await next(context);
        };
}
