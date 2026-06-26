using Sgpa.Data;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Prestaciones;

/// <summary>
/// Receta óptica de una prestación (port del cluster Receta de AbmPrest.frm). Una prestación que "lleva
/// receta" puede tener una receta de cerca y/o de lejos (CodRecetaDistancia 'cer'/'lej'), cada una con
/// esférico y cilíndrico por ojo (I/D). El grabado borra las recetas previas y reinserta las indicadas.
/// </summary>
public sealed class RecetaService
{
    public const string Cerca = "cer";   // PC_RECETADISTANCIA_CERCA
    public const string Lejos = "lej";   // PC_RECETADISTANCIA_LEJOS
    private const int UsrMaxLen = 8;

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public RecetaService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    /// <summary>¿El tipo de prestación lleva receta? (port de LlevaReceta = PrestacionTipo.Receta).</summary>
    public Task<bool> LlevaRecetaAsync(int codPrestacionTipo, CancellationToken ct = default)
        => _db.ExecuteScalarAsync<bool>(
            "SELECT ISNULL(Receta,0) FROM dbo.PrestacionTipo WHERE CodPrestacionTipo=@t",
            new { t = codPrestacionTipo }, cancellationToken: ct);

    /// <summary>Códigos de tipos de prestación que llevan receta (para habilitar el acceso a la receta en la grilla).</summary>
    public Task<IReadOnlyList<int>> GetTiposConRecetaAsync(CancellationToken ct = default)
        => _db.QueryAsync<int>("SELECT CodPrestacionTipo FROM dbo.PrestacionTipo WHERE Receta=1", cancellationToken: ct);

    /// <summary>Recetas (cerca/lejos) de una prestación.</summary>
    public Task<IReadOnlyList<Receta>> GetAsync(long ci, DateTime fecha, int codPrestacionTipo, CancellationToken ct = default)
        => _db.QueryAsync<Receta>(
            @"SELECT CI, Fecha, CodPrestacionTipo, CodRecetaDistancia, Esf_I, Esf_D, Cil_I, Cil_D
              FROM dbo.Receta WHERE CI=@ci AND Fecha=@fecha AND CodPrestacionTipo=@tipo",
            new { ci, fecha, tipo = codPrestacionTipo }, cancellationToken: ct);

    /// <summary>
    /// Receta(s) de la prestación ANTERIOR del afiliado para ese tipo (la más reciente con fecha menor a la dada).
    /// Port de <c>CargarUltimaReceta</c> (230_Receta_Ultima), adaptado al editor —que abre una prestación ya
    /// existente— para mostrar la receta previa como referencia (no la de la propia prestación que se edita).
    /// </summary>
    public Task<IReadOnlyList<Receta>> GetAnteriorAsync(long ci, DateTime fecha, int codPrestacionTipo, CancellationToken ct = default)
        => _db.QueryAsync<Receta>(
            @"SELECT CI, Fecha, CodPrestacionTipo, CodRecetaDistancia, Esf_I, Esf_D, Cil_I, Cil_D
              FROM dbo.Receta
              WHERE CI=@ci AND CodPrestacionTipo=@tipo AND Fecha = (
                  SELECT MAX(Fecha) FROM dbo.Receta WHERE CI=@ci AND CodPrestacionTipo=@tipo AND Fecha < @fecha)",
            new { ci, fecha, tipo = codPrestacionTipo }, cancellationToken: ct);

    /// <summary>
    /// Graba la receta de la prestación (port de GrabarDatosReceta): borra las recetas previas y reinserta
    /// la de cerca y/o la de lejos según las que se pasen (null = no se incluye). Atómico.
    /// </summary>
    public async Task GrabarAsync(long ci, DateTime fecha, int codPrestacionTipo,
        Receta? cerca, Receta? lejos, CancellationToken ct = default)
    {
        var usr = ClampUsr(_user.UserName);

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        await uow.ExecuteAsync(
            "DELETE FROM dbo.Receta WHERE CI=@ci AND Fecha=@fecha AND CodPrestacionTipo=@tipo",
            new { ci, fecha, tipo = codPrestacionTipo }, cancellationToken: ct).ConfigureAwait(false);

        if (cerca is not null) await InsertarAsync(uow, ci, fecha, codPrestacionTipo, Cerca, cerca, usr, ct).ConfigureAwait(false);
        if (lejos is not null) await InsertarAsync(uow, ci, fecha, codPrestacionTipo, Lejos, lejos, usr, ct).ConfigureAwait(false);

        await uow.CommitAsync(ct).ConfigureAwait(false);
    }

    private static Task InsertarAsync(IDbExecutor db, long ci, DateTime fecha, int tipo, string distancia,
        Receta r, string usr, CancellationToken ct)
        => db.ExecuteAsync(
            @"INSERT INTO dbo.Receta (CI, Fecha, CodPrestacionTipo, CodRecetaDistancia, Esf_I, Esf_D, Cil_I, Cil_D, Usr, Ts)
              VALUES (@ci, @fecha, @tipo, @dist, @EsfI, @EsfD, @CilI, @CilD, @usr, @ts)",
            new { ci, fecha, tipo, dist = distancia, EsfI = r.Esf_I, EsfD = r.Esf_D, CilI = r.Cil_I, CilD = r.Cil_D, usr, ts = DateTime.Now },
            cancellationToken: ct);

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];
}
