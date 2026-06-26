using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Pagos;

/// <summary>Una empresa donde el afiliado tiene empleo vigente (SP_Trabaja activo).</summary>
public sealed record EmpresaTrabaja(int CodEmpresa, string? DescEmpresa);

/// <summary>Un importe líquido cargado manualmente (SP_ImpLiquido).</summary>
public sealed record ImpLiquidoView(int Anio, int Mes, double Importe);

/// <summary>Resultado de la carga automática de líquidos: cédulas cargadas + las que no tienen empleo en la empresa.</summary>
public sealed record CargaLiquidoResult(int CedulasCargadas, int Filas, IReadOnlyList<long> SinEmpleo);

/// <summary>
/// Carga manual del importe líquido por afiliado/empresa/período (port de frmIngImpLiquido, app VB6 "SP").
/// El alta toma la fecha de ingreso y el id de empleo de SP_Trabaja (la clave de SP_ImpLiquido los incluye).
/// </summary>
public sealed class ImpLiquidoService
{
    private const int UsrMaxLen = 8;

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    public ImpLiquidoService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    private static string ClampUsr(string usr) => usr.Length <= UsrMaxLen ? usr : usr[..UsrMaxLen];

    /// <summary>Empresas con empleo vigente del afiliado (port de 1007_TrabajaxCI: SP_Trabaja sin baja).</summary>
    public Task<IReadOnlyList<EmpresaTrabaja>> GetEmpresasAsync(long ci, CancellationToken ct = default)
        => _db.QueryAsync<EmpresaTrabaja>(
            @"SELECT DISTINCT CAST(t.CodEmpresa AS int) AS CodEmpresa, e.Nombre AS DescEmpresa
              FROM dbo.SP_Trabaja t JOIN dbo.SP_Empresa e ON t.CodEmpresa=e.CodEmpresa
              WHERE t.CI=@ci AND t.FechaBaja IS NULL
              ORDER BY e.Nombre",
            new { ci = (int)ci }, cancellationToken: ct);

    /// <summary>Líquidos cargados para un afiliado/empresa (port de 1007_ImpLiquidoxCICodEmpresa).</summary>
    public Task<IReadOnlyList<ImpLiquidoView>> GetLiquidosAsync(long ci, int codEmpresa, CancellationToken ct = default)
        => _db.QueryAsync<ImpLiquidoView>(
            @"SELECT Anio, CAST(Mes AS int) Mes, CAST(ISNULL(Importe,0) AS float) Importe
              FROM dbo.SP_ImpLiquido WHERE CI=@ci AND CodEmpresa=@cod
              ORDER BY Anio DESC, Mes DESC",
            new { ci = (int)ci, cod = codEmpresa }, cancellationToken: ct);

    /// <summary>
    /// Alta del importe líquido (port de 1007_Insert_ImpLiquido): inserta una fila por cada empleo del afiliado
    /// en esa empresa (toma FechaIngreso e IdTrabaja de SP_Trabaja, que forman parte de la clave).
    /// </summary>
    public Task IngresarAsync(long ci, int codEmpresa, int mes, int anio, double importe, CancellationToken ct = default)
        => _db.ExecuteAsync(
            @"INSERT INTO dbo.SP_ImpLiquido (CI, CodEmpresa, Fechaingreso, IdTrabaja, Mes, Anio, AnioMes, Importe, Usr, Ts)
              SELECT t.CI, t.CodEmpresa, t.FechaIngreso, t.IdTrabaja, @mes, @anio, (@anio*100+@mes), @imp, @usr, @ts
              FROM dbo.SP_Trabaja t WHERE t.CI=@ci AND t.CodEmpresa=@cod",
            new { ci = (int)ci, cod = codEmpresa, mes, anio, imp = importe, usr = ClampUsr(_user.UserName), ts = DateTime.Now },
            cancellationToken: ct);

    /// <summary>
    /// Todas las empresas (para la carga automática de líquidos por empresa). Port del combo de frmCargaLiquido.
    /// </summary>
    public Task<IReadOnlyList<EmpresaTrabaja>> GetTodasEmpresasAsync(CancellationToken ct = default)
        => _db.QueryAsync<EmpresaTrabaja>(
            "SELECT CAST(CodEmpresa AS int) AS CodEmpresa, Nombre AS DescEmpresa FROM dbo.SP_Empresa ORDER BY Nombre",
            cancellationToken: ct);

    /// <summary>
    /// Carga automática de líquidos desde archivo (port de frmCargaLiquido.CargarExcel). Para una empresa/mes/año:
    /// borra el período completo de la empresa (modo Reemplazar, como 1015_Borrar_ImpLiquidoxEmpresaAnioMes) y, por
    /// cada cédula del archivo, inserta una fila por empleo en esa empresa tomando FechaIngreso/IdTrabaja de
    /// SP_Trabaja (1015). Las cédulas sin empleo en la empresa no generan filas y se reportan.
    /// </summary>
    public async Task<CargaLiquidoResult> CargarMasivoAsync(
        int codEmpresa, int mes, int anio, IReadOnlyList<LiquidoFila> filas, CancellationToken ct = default)
    {
        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            "DELETE FROM dbo.SP_ImpLiquido WHERE CodEmpresa=@cod AND Mes=@mes AND Anio=@anio",
            new { cod = codEmpresa, mes, anio }, cancellationToken: ct).ConfigureAwait(false);

        var usr = ClampUsr(_user.UserName);
        var ts = DateTime.Now;
        var cedulas = 0;
        var totalFilas = 0;
        var sinEmpleo = new List<long>();
        foreach (var f in filas)
        {
            var n = await uow.ExecuteAsync(
                @"INSERT INTO dbo.SP_ImpLiquido (CI, CodEmpresa, Fechaingreso, IdTrabaja, Mes, Anio, AnioMes, Importe, Usr, Ts)
                  SELECT t.CI, t.CodEmpresa, t.FechaIngreso, t.IdTrabaja, @mes, @anio, (@anio*100+@mes), @imp, @usr, @ts
                  FROM dbo.SP_Trabaja t WHERE t.CI=@ci AND t.CodEmpresa=@cod",
                new { ci = (int)f.CI, cod = codEmpresa, mes, anio, imp = f.Importe, usr, ts },
                cancellationToken: ct).ConfigureAwait(false);
            if (n > 0) { cedulas++; totalFilas += n; }
            else sinEmpleo.Add(f.CI);
        }

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new CargaLiquidoResult(cedulas, totalFilas, sinEmpleo);
    }

    /// <summary>
    /// Baja del importe líquido del período (port de 1007_Borrar_ImpLiquido): elimina las filas del empleo
    /// vigente (SP_Trabaja sin baja) para ese afiliado/empresa/mes/año.
    /// </summary>
    public Task BorrarAsync(long ci, int codEmpresa, int mes, int anio, CancellationToken ct = default)
        => _db.ExecuteAsync(
            @"DELETE il FROM dbo.SP_ImpLiquido il
              JOIN dbo.SP_Trabaja t ON il.Fechaingreso=t.FechaIngreso AND il.CodEmpresa=t.CodEmpresa AND il.CI=t.CI
              WHERE t.FechaBaja IS NULL AND il.CI=@ci AND il.CodEmpresa=@cod AND il.Mes=@mes AND il.Anio=@anio",
            new { ci = (int)ci, cod = codEmpresa, mes, anio }, cancellationToken: ct);
}
