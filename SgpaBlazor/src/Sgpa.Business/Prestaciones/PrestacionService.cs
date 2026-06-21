using Sgpa.Business.Afiliados;
using Sgpa.Data;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Prestaciones;

/// <summary>
/// Reglas de negocio de las prestaciones (port de AbmPrest.frm). Avisos no bloqueantes al grabar:
/// período de renovación (otra prestación del mismo tipo demasiado reciente) y elegibilidad 1,25 SMN.
/// </summary>
public sealed class PrestacionService
{
    private readonly IDbExecutor _db;
    private readonly AfiliadoService _afiliado;

    public PrestacionService(IDbExecutor db, AfiliadoService afiliado)
    {
        _db = db;
        _afiliado = afiliado;
    }

    /// <summary>
    /// Avisos de la prestación (port de la parte informativa de DatosOk):
    /// 1) período de renovación — existe una prestación anterior del mismo tipo a menos de
    ///    <c>PrestacionTipo.PeriodoRenovacion</c> meses. OJO: la query migrada 230_PrestacionAnterior
    ///    devuelve PeriodoRenovacion=0 (se migró sin esa columna); acá se lee el valor real de la tabla.
    /// 2) elegibilidad — el afiliado no llega a 1,25 SMN (regla compartida con reintegros).
    /// </summary>
    public async Task<IReadOnlyList<string>> GetAvisosAsync(Prestacion p, CancellationToken ct = default)
    {
        var avisos = new List<string>();
        if (p.CI <= 0) return avisos;

        // Prestación anterior del mismo tipo, excluyendo la fecha recién grabada (la PK es CI+Fecha+Tipo).
        var fechaAnterior = await _db.ExecuteScalarAsync<DateTime?>(
            "SELECT MAX(Fecha) FROM dbo.Prestacion WHERE CI=@ci AND CodPrestacionTipo=@tipo AND Fecha<>@fecha",
            new { ci = p.CI, tipo = p.CodPrestacionTipo, fecha = p.Fecha }, cancellationToken: ct).ConfigureAwait(false);
        var periodoRenovacion = await _db.ExecuteScalarAsync<int?>(
            "SELECT PeriodoRenovacion FROM dbo.PrestacionTipo WHERE CodPrestacionTipo=@tipo",
            new { tipo = p.CodPrestacionTipo }, cancellationToken: ct).ConfigureAwait(false) ?? 0;

        if (fechaAnterior is not null && periodoRenovacion > 0 && MesesEntre(fechaAnterior.Value, p.Fecha) < periodoRenovacion)
            avisos.Add($"Existe una prestación del {fechaAnterior.Value:dd/MM/yyyy} en un período menor al de " +
                       $"renovación ({periodoRenovacion} meses). Verificá antes de ingresarla.");

        if (await _afiliado.NoLlegaA125SmnAsync(p.CI, p.Fecha.Month, p.Fecha.Year, ct).ConfigureAwait(false))
            avisos.Add("El afiliado no llega a 1,25 SMN de promedio en los últimos 6 meses ni en el último mes.");

        return avisos;
    }

    /// <summary>Diferencia en meses calendario (port de DateDiff("m", desde, hasta)).</summary>
    public static int MesesEntre(DateTime desde, DateTime hasta)
        => (hasta.Year - desde.Year) * 12 + (hasta.Month - desde.Month);
}
