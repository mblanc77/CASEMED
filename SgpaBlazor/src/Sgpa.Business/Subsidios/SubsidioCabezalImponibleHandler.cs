using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Subsidios;

/// <summary>
/// Handler CUD de <see cref="SubsidioCabezal"/>: ante alta/modificación/baja de un cabezal recalcula el
/// imponible emp900 del afiliado para ese período (ver <see cref="IImponibleSubsidioSync"/>). Cubre las
/// ediciones manuales del ABM de subsidios; la liquidación masiva llama al mismo servicio al cerrar.
/// </summary>
public sealed class SubsidioCabezalImponibleHandler : IEntityChangeHandler<SubsidioCabezal>
{
    private readonly IImponibleSubsidioSync _sync;
    private readonly ICurrentUser _user;

    public SubsidioCabezalImponibleHandler(IImponibleSubsidioSync sync, ICurrentUser user)
    {
        _sync = sync;
        _user = user;
    }

    public async Task HandleAsync(EntityChange<SubsidioCabezal> change, CancellationToken cancellationToken = default)
    {
        var usr = _user.UserName;

        // Recalcula el período del estado actual del cabezal (alta/modificación/baja).
        await SincronizarAsync(change.Db, change.Entity, usr, cancellationToken).ConfigureAwait(false);

        // Si en una modificación cambió el (CI, Anio, Mes), el período viejo también debe recalcularse
        // (puede haber quedado sin cabezales → su fila emp900 se borra).
        if (change.Kind == EntityChangeKind.Updated && change.Previous is { } prev
            && !MismoPeriodo(prev, change.Entity))
            await SincronizarAsync(change.Db, prev, usr, cancellationToken).ConfigureAwait(false);
    }

    private Task SincronizarAsync(Sgpa.Data.IDbExecutor db, SubsidioCabezal cab, string usr, CancellationToken ct)
    {
        // Sólo los cabezales reales (Liquidar=true) con período/CI completos alimentan el imponible;
        // los simulados (Liquidar=false) no se promedian en el jornal. El recálculo es por (CI, período):
        // sincronizamos ese CI aunque el cabezal sea simulado, por si conviven liquidados del mismo período.
        if (cab.CI is not { } ci || cab.Anio is not { } anio || cab.Mes is not { } mes)
            return Task.CompletedTask;
        return _sync.SincronizarPeriodoAsync(db, anio, mes, ci, usr, ct);
    }

    private static bool MismoPeriodo(SubsidioCabezal a, SubsidioCabezal b)
        => a.CI == b.CI && a.Anio == b.Anio && a.Mes == b.Mes;
}
