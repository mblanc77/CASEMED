using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for statistical report queries.
/// Replaces 800_* series (Informes Estadísticos) and report generation queries.
/// </summary>
public class ReportQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>
    /// 810_Certificados_Sexo_Cantidad: Certifications count by sex for a period.
    /// </summary>
    public IQueryable<object> GetCertificadosPorSexo(DateTime desde, DateTime hasta)
        => from c in db.Certificaciones.AsNoTracking()
           join a in db.Afiliados.AsNoTracking() on c.CI equals a.CI
           where c.Efectiva && c.FechaIni >= desde && c.FechaFin <= hasta
           group c by a.Sexo into g
           select new { Sexo = g.Key, Cantidad = g.Count() };

    /// <summary>
    /// 818_Certificados_Patologia: Certifications count by pathology.
    /// </summary>
    public IQueryable<object> GetCertificadosPorPatologia(DateTime desde, DateTime hasta)
        => from c in db.Certificaciones.AsNoTracking()
           join at in db.AfeccionTipos.AsNoTracking() on c.CodAfeccionTipo equals at.CodAfeccionTipo
           join ag in db.AfeccionGrupos.AsNoTracking() on at.CodAfeccionGrupo equals ag.CodAfeccionGrupo
           join p in db.Patologias.AsNoTracking() on ag.CodPatologia equals p.CodPatologia
           where c.Efectiva && c.FechaIni >= desde && c.FechaFin <= hasta
           group c by new { p.CodPatologia, p.Descrip } into g
           select new
           {
               Codigo = g.Key.CodPatologia,
               Descrip = g.Key.Descrip,
               Cantidad = g.Count()
           };

    /// <summary>
    /// 819_Certificados_AfeccionGrupo: By afeccion group.
    /// </summary>
    public IQueryable<object> GetCertificadosPorAfeccionGrupo(DateTime desde, DateTime hasta)
        => from c in db.Certificaciones.AsNoTracking()
           join at in db.AfeccionTipos.AsNoTracking() on c.CodAfeccionTipo equals at.CodAfeccionTipo
           join ag in db.AfeccionGrupos.AsNoTracking() on at.CodAfeccionGrupo equals ag.CodAfeccionGrupo
           where c.Efectiva && c.FechaIni >= desde && c.FechaFin <= hasta
           group c by new { ag.CodAfeccionGrupo, ag.Descrip } into g
           select new
           {
               Codigo = g.Key.CodAfeccionGrupo,
               Descrip = g.Key.Descrip,
               Cantidad = g.Count()
           };

    /// <summary>
    /// 824_PrestacionesCantidad: Prestaciones count by type in a period.
    /// </summary>
    public IQueryable<object> GetPrestacionesPorTipo(DateTime desde, DateTime hasta)
        => from p in db.Prestaciones.AsNoTracking()
           join pt in db.PrestacionTipos.AsNoTracking() on p.CodPrestacionTipo equals pt.CodPrestacionTipo
           where p.Fecha >= desde && p.Fecha <= hasta
           group p by new { pt.CodPrestacionTipo, pt.Descrip } into g
           select new
           {
               Codigo = g.Key.CodPrestacionTipo,
               Descrip = g.Key.Descrip,
               Cantidad = g.Count()
           };

    /// <summary>
    /// 828_Cantidad_Empresa: Employee count per empresa.
    /// </summary>
    public IQueryable<object> GetCantidadPorEmpresa()
        => from t in db.Trabajos.AsNoTracking()
           join e in db.Empresas.AsNoTracking() on t.CodEmpresa equals e.CodEmpresa
           where t.FechaBaja == null
           group t by new { e.CodEmpresa, e.Nombre } into g
           orderby g.Count() descending
           select new
           {
               CodEmpresa = g.Key.CodEmpresa,
               Nombre = g.Key.Nombre,
               Cantidad = g.Count()
           };

    /// <summary>
    /// 827_AfiliadosActivos_GE_Sexo: Active afiliados by age group and sex.
    /// </summary>
    public IQueryable<object> GetAfiliadosActivosPorGrupoEtarioYSexo()
    {
        var today = DateTime.Today;
        return from a in db.Afiliados.AsNoTracking()
               where db.Trabajos.Any(t => t.CI == a.CI && t.FechaBaja == null)
                     && a.FechaNacimiento != null
               let edad = EF.Functions.DateDiffYear(a.FechaNacimiento!.Value, today)
               let grupo = edad < 20 ? "< 20" :
                           edad < 30 ? "20-29" :
                           edad < 40 ? "30-39" :
                           edad < 50 ? "40-49" :
                           edad < 60 ? "50-59" :
                           edad < 70 ? "60-69" : "70+"
               group a by new { Grupo = grupo, a.Sexo } into g
               select new
               {
                   GrupoEtario = g.Key.Grupo,
                   Sexo = g.Key.Sexo,
                   Cantidad = g.Count()
               };
    }

    /// <summary>
    /// Rpt_SubsidioCabezal: Subsidio summary report data.
    /// </summary>
    public IQueryable<object> GetSubsidioResumen(int mes, int anio)
        => from s in db.SubsidioCabezales.AsNoTracking()
           join a in db.Afiliados.AsNoTracking() on s.CI equals a.CI
           where s.Mes == mes && s.Anio == anio
           orderby a.Apellido1, a.Apellido2, a.Nombres
           select new
           {
               s.Anio,
               s.Mes,
               s.CI,
               DescAfiliado = a.Apellido1 + " " + (a.Apellido2 ?? "") + ", " + a.Nombres,
               s.Dias,
               a.Nombres,
               a.Apellido1,
               a.Apellido2,
               s.ImpNominal,
               s.ImpAguinaldo,
               s.ImpLiquido,
               s.Liquidar,
               a.FechaNacimiento
           };
}
