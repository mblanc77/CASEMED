using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for Certificacion queries.
/// Replaces VB6 queries: Rs_Certificacion, Rs_Certificacion_Nombre, 102_DiasCertificados, etc.
/// </summary>
public class CertificacionQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>Rs_Certificacion: All certifications.</summary>
    public IQueryable<Certificacion> GetCertificaciones()
        => db.Certificaciones.AsNoTracking();

    /// <summary>Rs_Certificacion filtered by CI.</summary>
    public IQueryable<Certificacion> GetCertificaciones(long ci)
        => db.Certificaciones.AsNoTracking()
            .Include(c => c.AfeccionTipo)
            .Include(c => c.Certificador)
            .Include(c => c.SalidaTipo)
            .Where(c => c.CI == ci)
            .OrderByDescending(c => c.FechaIni);

    /// <summary>Rs_Certificacion_Nombre: Certifications with afiliado name.</summary>
    public IQueryable<object> GetCertificacionesConNombre()
        => db.Certificaciones.AsNoTracking()
            .Include(c => c.Afiliado)
            .Select(c => new
            {
                c.NroLlamado,
                c.CI,
                Nombre = c.Afiliado != null
                    ? c.Afiliado.Apellido1 + " " + (c.Afiliado.Apellido2 ?? "") + ", " + c.Afiliado.Nombres
                    : null,
                c.FechaRecibido,
                c.FechaCertificacion,
                c.FechaIni,
                c.FechaFin,
                c.CodAfeccionTipo,
                c.CodCertificador,
                c.CodSalidaTipo,
                c.Efectiva,
                c.Indicaciones,
                c.ImporteDeducible,
                c.TrabajaDuranteCertificacion
            });

    /// <summary>
    /// 102_DiasCertificados: Total certified days and count for a CI up to a NroLlamado.
    /// </summary>
    public async Task<(int Dias, int Cantidad)> GetDiasCertificadosAsync(long ci, int nroLlamado = 0)
    {
        var query = db.Certificaciones.AsNoTracking()
            .Where(c => c.CI == ci && c.Efectiva);

        if (nroLlamado > 0)
            query = query.Where(c => c.NroLlamado <= nroLlamado);

        var result = await query
            .Select(c => new
            {
                Dias = c.FechaFin != null && c.FechaIni != null
                    ? EF.Functions.DateDiffDay(c.FechaIni.Value, c.FechaFin.Value) + 1
                    : 0
            })
            .ToListAsync();

        return (result.Sum(r => r.Dias), result.Count);
    }

    /// <summary>
    /// Certifications for a subsidio period (month/year).
    /// Replaces queries like 200_Certificaciones, GenerarCertificaciones in VB6.
    /// </summary>
    public IQueryable<Certificacion> GetCertificacionesPorPeriodo(long ci, int mes, int anio)
    {
        var primerDia = new DateTime(anio, mes, 1);
        var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

        return db.Certificaciones.AsNoTracking()
            .Where(c => c.CI == ci && c.Efectiva &&
                         c.FechaIni <= ultimoDia && c.FechaFin >= primerDia)
            .OrderBy(c => c.FechaIni);
    }
}
