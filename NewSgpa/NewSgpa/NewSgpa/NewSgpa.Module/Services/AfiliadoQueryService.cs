using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service encapsulating all Afiliado-related queries migrated from Access sgpa.mdb.
/// Replaces VB6 queries: Rs_Afiliado, Rs_Afiliado_Desc, Rs_Empleo, Rs_TrabajaActivo,
/// Rs_TrabajaUltimo, Rs_AfiliadoEspecialidadDesc, Rs_AfiliadoImponibleMes, etc.
/// </summary>
public class AfiliadoQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>Rs_Afiliado: Full afiliado list.</summary>
    public IQueryable<Afiliado> GetAfiliados()
        => db.Afiliados.AsNoTracking();

    /// <summary>Rs_Afiliado_Desc: Afiliado description sorted by Apellido.</summary>
    public IQueryable<Afiliado> GetAfiliadosDesc()
        => db.Afiliados.AsNoTracking()
            .OrderBy(a => a.Apellido1).ThenBy(a => a.Apellido2).ThenBy(a => a.Nombres);

    /// <summary>1000_AfiladoCI2Nombre: Lookup afiliado by CI.</summary>
    public async Task<Afiliado?> GetAfiliadoByCiAsync(long ci)
        => await db.Afiliados.AsNoTracking().FirstOrDefaultAsync(a => a.CI == ci);

    /// <summary>Rs_Afiliado_FechaNacimiento: Afiliados with birth date.</summary>
    public IQueryable<Afiliado> GetAfiliadosConFechaNacimiento()
        => db.Afiliados.AsNoTracking().Where(a => a.FechaNacimiento != null);

    /// <summary>Rs_AfiliadoCristalin: Afiliados in empresa 7 (Cristalin).</summary>
    public IQueryable<Afiliado> GetAfiliadosCristalin()
        => db.Afiliados.AsNoTracking()
            .Where(a => db.Trabajos.Any(t => t.CI == a.CI && t.CodEmpresa == 7));

    /// <summary>rsAfiliadoActivo: Afiliados with at least one active employment.</summary>
    public IQueryable<Afiliado> GetAfiliadosActivos()
        => db.Afiliados.AsNoTracking()
            .Where(a => db.Trabajos.Any(t => t.CI == a.CI && t.FechaBaja == null));

    /// <summary>Rs_Empleo: Employment records with empresa description (used in ABM Afiliados).</summary>
    public IQueryable<object> GetEmpleos(long ci)
        => db.Trabajos.AsNoTracking()
            .Include(t => t.Empresa)
            .Include(t => t.BajaMotivo)
            .Where(t => t.CI == ci)
            .Select(t => new
            {
                t.CI,
                t.CodEmpresa,
                DescEmpresa = t.Empresa != null ? t.Empresa.Nombre : null,
                t.FechaIngreso,
                t.FechaIngCasemed,
                t.FechaBaja,
                DescBajaMotivo = t.BajaMotivo != null ? t.BajaMotivo.Descrip : null,
                t.NroFichaEmpresa,
                t.IdTrabaja
            });

    /// <summary>Rs_TrabajaActivo: Active employments (no FechaBaja).</summary>
    public IQueryable<Trabaja> GetTrabajosActivos()
        => db.Trabajos.AsNoTracking().Where(t => t.FechaBaja == null);

    /// <summary>100_TrabajaActivo: Active at a given month/year.</summary>
    public IQueryable<Trabaja> GetTrabajosActivosAlMes(int mes, int anio)
    {
        var fechaCorte = new DateTime(anio, mes, 1);
        return db.Trabajos.AsNoTracking()
            .Where(t => t.FechaBaja == null || t.FechaBaja >= fechaCorte);
    }

    /// <summary>Rs_TrabajaUltimo / tmp_ult_empleo: Last employment per CI+Empresa.</summary>
    public IQueryable<Trabaja> GetUltimoEmpleo()
        => db.Trabajos.AsNoTracking()
            .Where(t1 => !db.Trabajos.Any(t2 =>
                t2.CI == t1.CI &&
                t2.CodEmpresa == t1.CodEmpresa &&
                t2.FechaIngreso > t1.FechaIngreso));

    /// <summary>Rs_AfiliadoEspecialidadDesc: Specialties with description for a CI.</summary>
    public IQueryable<object> GetEspecialidadesAfiliado(long ci)
        => db.AfiliadoEspecialidades.AsNoTracking()
            .Include(ae => ae.Especialidad)
            .Where(ae => ae.CI == ci)
            .Select(ae => new
            {
                ae.CI,
                ae.CodEspecialidad,
                Descrip = ae.Especialidad != null ? ae.Especialidad.Descrip : null
            });

    /// <summary>Rs_AfiliadoApunte: Notes for an afiliado.</summary>
    public IQueryable<AfiliadoApunte> GetApuntes(long ci)
        => db.AfiliadoApuntes.AsNoTracking()
            .Where(a => a.CI == ci)
            .OrderByDescending(a => a.Fecha);

    /// <summary>Rs_AfiliadoApunteFromPeriodo: Notes in a date range.</summary>
    public IQueryable<object> GetApuntesEnPeriodo(DateTime desde, DateTime hasta)
        => db.AfiliadoApuntes.AsNoTracking()
            .Include(a => a.Afiliado)
            .Where(a => a.Fecha >= desde && a.Fecha <= hasta)
            .Select(a => new
            {
                a.Afiliado!.CI,
                a.Afiliado.Nombres,
                a.Afiliado.Apellido1,
                a.Afiliado.Apellido2,
                a.Fecha,
                a.Descrip
            });

    /// <summary>Rs_AfiliadoImponibleMes: Imponible totals per CI per month.</summary>
    public IQueryable<object> GetImponiblePorMes(long ci)
        => db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci)
            .GroupBy(i => new { i.CI, i.Mes, i.Anio })
            .Select(g => new
            {
                CI = g.Key.CI,
                Mes = g.Key.Mes,
                Anio = g.Key.Anio,
                Importe = g.Sum(x => x.Importe ?? 0),
                AnioMes = g.Min(x => x.AnioMes)
            });

    /// <summary>Rs_MaxImp_Afiliado: Max imponible per afiliado.</summary>
    public IQueryable<object> GetMaxImponible()
        => db.Imponibles.AsNoTracking()
            .GroupBy(i => i.CI)
            .Select(g => new
            {
                CI = g.Key,
                MaxImporte = g.Max(x => x.Importe)
            });

    /// <summary>110_Imponible_Periodo: Sum of imponible in a period for a CI.</summary>
    public async Task<double> GetImponiblePeriodoAsync(long ci, int mesIni, int mesFin)
        => await db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci && i.AnioMes >= mesIni && i.AnioMes <= mesFin)
            .SumAsync(i => i.Importe ?? 0);
}
