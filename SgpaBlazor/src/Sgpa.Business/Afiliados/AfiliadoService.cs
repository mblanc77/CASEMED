using Sgpa.Data;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Cálculos de negocio a nivel afiliado (port de funciones de AbmAfili.frm). Por ahora: el promedio de
/// ingresos (cmdPromedio / Promedio), que promedia los imponibles mensuales (concepto 1, empleos activos)
/// de los 6 meses que terminan 2 meses atrás. No depende de IMS.
/// </summary>
/// <summary>Una coincidencia de la búsqueda de afiliados (cédula + nombre).</summary>
public sealed record AfiliadoBusqueda(long CI, string? Apellido1, string? Apellido2, string? Nombres)
{
    public string NombreCompleto => $"{Apellido1} {Apellido2}, {Nombres}".Replace("  ", " ").Trim().Trim(',').Trim();
}

public sealed class AfiliadoService
{
    private readonly IDbExecutor _db;
    public AfiliadoService(IDbExecutor db) => _db = db;

    /// <summary>
    /// Búsqueda "full-text" de afiliados estilo XAF (port de frmBuscarxNombre): una sola caja donde el usuario
    /// escribe cédula o nombre. El término se parte en palabras; CADA palabra debe matchear ALGÚN campo
    /// (cédula por prefijo, o nombre / 1er / 2do apellido por contiene), así "perez juan" encuentra al afiliado
    /// sin importar el orden. Devuelve hasta <paramref name="max"/> coincidencias.
    /// </summary>
    public Task<IReadOnlyList<AfiliadoBusqueda>> BuscarAsync(string termino, int max = 50, CancellationToken ct = default)
    {
        var palabras = (termino ?? string.Empty).Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        if (palabras.Length == 0) return Task.FromResult<IReadOnlyList<AfiliadoBusqueda>>(Array.Empty<AfiliadoBusqueda>());

        var p = new Dapper.DynamicParameters();
        p.Add("max", max);
        var conds = new List<string>();
        for (var i = 0; i < palabras.Length; i++)
        {
            p.Add($"w{i}", "%" + palabras[i] + "%");
            p.Add($"c{i}", palabras[i] + "%");
            conds.Add($"(Nombres LIKE @w{i} OR Apellido1 LIKE @w{i} OR Apellido2 LIKE @w{i} OR CAST(CI AS nvarchar(20)) LIKE @c{i})");
        }

        var sql = $@"SELECT TOP (@max) CI, Apellido1, Apellido2, Nombres
                     FROM dbo.Afiliado
                     WHERE {string.Join(" AND ", conds)}
                     ORDER BY Apellido1, Apellido2, Nombres";
        return _db.QueryAsync<AfiliadoBusqueda>(sql, p, cancellationToken: ct);
    }

    /// <summary>
    /// Promedio de ingresos del afiliado (port de Promedio / 220_AfiliadoPromedio): media de los imponibles
    /// mensuales en la ventana [mes−5, mes], donde mes = aaaamm de (hoy − 2 meses). Null si no hay datos.
    /// </summary>
    public Task<double?> GetPromedioIngresosAsync(long ci, CancellationToken ct = default)
    {
        var fecha = DateTime.Today.AddMonths(-2);
        var mes = fecha.Year * 100 + fecha.Month;
        var ini = fecha.AddMonths(-5);
        var mesIni = ini.Year * 100 + ini.Month;

        return _db.ExecuteScalarAsync<double?>(
            "SELECT Promedio FROM dbo.acc_sgpa_220_AfiliadoPromedio_q(@ci, @mes, @mesIni)",
            new { ci = (int)ci, mes, mesIni }, cancellationToken: ct);
    }

    /// <summary>True si la cédula corresponde a un afiliado existente (port de la verificación 100_Afiliado_CI
    /// que el VB6 hacía en txtCI_LostFocus antes de aceptar una certificación/prestación/reintegro).</summary>
    public async Task<bool> ExisteAsync(long ci, CancellationToken ct = default)
    {
        if (ci <= 0) return false;
        return await _db.ExecuteScalarAsync<int?>(
            "SELECT 1 FROM dbo.Afiliado WHERE CI=@ci", new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) is not null;
    }

    /// <summary>
    /// True si el afiliado está "activo" según el VB6 (Bcpart.AfiliadoActivo): al menos 3 aportes mensuales
    /// (concepto 1, empleo vigente) en los últimos 12 meses (403_AportesUlt12xCI). Si no existe o no aportó, false.
    /// </summary>
    public async Task<bool> EsActivoAsync(long ci, CancellationToken ct = default)
    {
        if (ci <= 0) return false;
        var aportes = await _db.ExecuteScalarAsync<int?>(
            "SELECT COUNT(*) FROM dbo.acc_sgpa_403_AportesUlt12xCI_q(@ci)",
            new { ci = (int)ci }, cancellationToken: ct).ConfigureAwait(false) ?? 0;
        return aportes >= 3;
    }

    // Umbral de elegibilidad compartido (reintegros, prestaciones): 1,25 SMN.
    private const double FactorElegibilidad = 1.25;

    /// <summary>
    /// True si el afiliado NO llega a 1,25 SMN ni en el promedio de los 6 meses (terminando 2 antes del
    /// período) ni en el último mes (port de la regla de elegibilidad común a AbmReint y AbmPrest, IMS-free).
    /// </summary>
    public async Task<bool> NoLlegaA125SmnAsync(long ci, int mes, int anio, CancellationToken ct = default)
    {
        if (ci <= 0 || mes < 1 || mes > 12 || anio <= 0) return false;

        var smn = await _db.ExecuteScalarAsync<double?>(
            "SELECT TOP 1 CAST(ISNULL(SMN,0) AS float) FROM dbo.Parametros", cancellationToken: ct).ConfigureAwait(false) ?? 0d;
        var fin = AddMonth(anio * 100 + mes, -2);
        var ini = AddMonth(fin, -5);
        var promedio = await _db.ExecuteScalarAsync<double?>(
            "SELECT Importe FROM dbo.acc_sgpa_320_AfiliadoPromedio_q(@ci, @ini, @fin)",
            new { ci = (int)ci, ini, fin }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;
        var ultMes = await _db.ExecuteScalarAsync<double?>(
            "SELECT Importe FROM dbo.acc_sgpa_320_AfiliadoUltMes_q(@ci, @m)",
            new { ci = (int)ci, m = fin }, cancellationToken: ct).ConfigureAwait(false) ?? 0d;

        return promedio < FactorElegibilidad * smn && ultMes < FactorElegibilidad * smn;
    }

    /// <summary>Suma n meses a un AnioMes (yyyymm).</summary>
    internal static int AddMonth(int anioMes, int n)
    {
        var d = new DateTime(anioMes / 100, anioMes % 100, 1).AddMonths(n);
        return d.Year * 100 + d.Month;
    }

    /// <summary>
    /// Avisos no bloqueantes del afiliado (port de la validación de DatosOk). Por ahora: dígito verificador
    /// de la cédula incorrecto (en el VB6 era un aviso con override; acá se informa sin impedir guardar).
    /// </summary>
    public Task<IReadOnlyList<string>> GetAvisosAsync(Afiliado a, CancellationToken ct = default)
    {
        var avisos = new List<string>();
        if (a.CI > 0 && !CedulaValidator.EsValida(a.CI))
            avisos.Add("El dígito verificador de la cédula no es correcto; verificá que el número esté bien ingresado.");
        return Task.FromResult<IReadOnlyList<string>>(avisos);
    }
}
