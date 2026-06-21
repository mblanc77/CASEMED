using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for Imponible/Aporte related queries.
/// Replaces: Rs_Imponible, 101_ImponibleMes, Rs_ImpMax, Rpt_Imponible, 
/// xEmpresaCantidad, xEmpresaPromedio, etc.
/// </summary>
public class AporteQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>101_ImponibleMes: Imponible records for a specific month/year.</summary>
    public IQueryable<Imponible> GetImponibleMes(int mes, int anio)
        => db.Imponibles.AsNoTracking()
            .Where(i => i.Mes == mes && i.Anio == anio);

    /// <summary>Rs_Imponible: All imponible records.</summary>
    public IQueryable<Imponible> GetImponibles()
        => db.Imponibles.AsNoTracking();

    /// <summary>Rs_Imponible for a specific empleo (CI + Empresa + FechaIngreso).</summary>
    public IQueryable<Imponible> GetImponiblePorEmpleo(long ci, int codEmpresa, DateTime fechaIngreso)
        => db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci && i.CodEmpresa == codEmpresa && i.Fechaingreso == fechaIngreso)
            .OrderByDescending(i => i.AnioMes);

    /// <summary>Rs_ImpMax: Max AnioMes per CI.</summary>
    public IQueryable<object> GetMaxImponiblePorAfiliado()
        => db.Imponibles.AsNoTracking()
            .GroupBy(i => i.CI)
            .Select(g => new
            {
                CI = g.Key,
                MaxAnioMes = g.Max(x => x.AnioMes)
            });

    /// <summary>xEmpresaCantidad: Count of employees per empresa (active).</summary>
    public IQueryable<object> GetCantidadPorEmpresa()
        => db.Trabajos.AsNoTracking()
            .GroupBy(t => t.CodEmpresa)
            .Select(g => new
            {
                CodEmpresa = g.Key,
                Cantidad = g.Count()
            });

    /// <summary>xEmpresaPromedio: Average imponible per empresa in a period.</summary>
    public IQueryable<object> GetPromedioImponiblePorEmpresa(int mesIni, int mesFin)
        => db.Imponibles.AsNoTracking()
            .Where(i => i.AnioMes >= mesIni && i.AnioMes <= mesFin)
            .GroupBy(i => i.CodEmpresa)
            .Select(g => new
            {
                CodEmpresa = g.Key,
                PromedioImporte = g.Average(x => x.Importe ?? 0)
            });

    /// <summary>xMutualistaCantidad: Count of afiliados per mutualista.</summary>
    public IQueryable<object> GetCantidadPorMutualista()
        => db.Afiliados.AsNoTracking()
            .GroupBy(a => a.CodMutualista)
            .Select(g => new
            {
                CodMutualista = g.Key,
                Cantidad = g.Count()
            });

    /// <summary>460_IMSxAnioMes: IMS value for a given period.</summary>
    public async Task<float?> GetImsValorAsync(int anioMes)
        => await db.ImsRegistros.AsNoTracking()
            .Where(i => i.AnioMes == anioMes)
            .Select(i => i.Importe)
            .FirstOrDefaultAsync();

    /// <summary>
    /// Consulta5: Imponible summary by empresa and concepto for a month/year.
    /// </summary>
    public IQueryable<object> GetImponibleResumen(int mes, int anio)
        => db.Imponibles.AsNoTracking()
            .Where(i => i.Mes == mes && i.Anio == anio)
            .GroupBy(i => new { i.CodEmpresa, i.Concepto })
            .Select(g => new
            {
                CodEmpresa = g.Key.CodEmpresa,
                Concepto = g.Key.Concepto,
                SumaImporte = g.Sum(x => x.Importe ?? 0)
            });

    /// <summary>
    /// 250_Control_Aporte: Aporte verification report.
    /// Replaces VB6 frmDBG_Control_Aporte.cargarDatos (query 250_Control_Aporte).
    /// Cross-references afiliados with active employment at a date against
    /// their imponible records for that period.
    /// </summary>
    public async Task<List<ControlAporteDto>> GetControlAporteAsync(int anioMes, DateTime fecha)
    {
        int anio = anioMes / 100;
        int mes = anioMes % 100;

        var result = await (
            from t in db.Trabajos.AsNoTracking()
            join a in db.Afiliados.AsNoTracking() on t.CI equals a.CI
            join e in db.Empresas.AsNoTracking() on t.CodEmpresa equals e.CodEmpresa
            where (t.FechaBaja == null || t.FechaBaja >= fecha)
                  && e.Liquidar
            select new
            {
                CI = a.CI,
                Nombre = a.Apellido1 + " " + (a.Apellido2 ?? "") + ", " + a.Nombres,
                CodEmpresa = t.CodEmpresa,
                NomEmpresa = e.Nombre,
                FechaIngreso = t.FechaIngreso,
                TieneImponible = db.Imponibles.Any(i =>
                    i.CI == a.CI && i.CodEmpresa == t.CodEmpresa
                    && i.AnioMes == anioMes)
            }
        ).ToListAsync();

        return result.Select(r => new ControlAporteDto
        {
            CI = r.CI,
            Nombre = r.Nombre,
            CodEmpresa = r.CodEmpresa ?? 0,
            NomEmpresa = r.NomEmpresa,
            FechaIngreso = r.FechaIngreso,
            TieneImponible = r.TieneImponible
        }).ToList();
    }

    /// <summary>
    /// EmpresaPago summary for a month/year.
    /// Replaces VB6 Rs_EmpresaPago query.
    /// </summary>
    public IQueryable<EmpresaPago> GetEmpresaPagos(int mes, int anio)
        => db.EmpresaPagos.AsNoTracking()
            .Where(ep => ep.Mes == mes && ep.Anio == anio);
}

public class ControlAporteDto
{
    public long CI { get; set; }
    public string? Nombre { get; set; }
    public int CodEmpresa { get; set; }
    public string? NomEmpresa { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public bool TieneImponible { get; set; }
}
