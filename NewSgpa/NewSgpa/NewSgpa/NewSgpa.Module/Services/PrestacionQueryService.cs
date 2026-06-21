using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for Prestacion/Receta queries.
/// Replaces: Rs_Prestacion, 103_PrestacionesAfiliado, 103_ReintegrosAfiliado,
/// 102_Prestacion_Cantidad, Rs_ReintegroMutual, etc.
/// </summary>
public class PrestacionQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>103_PrestacionesAfiliado: Prestaciones for a CI with type description.</summary>
    public IQueryable<object> GetPrestacionesAfiliado(long ci)
        => db.Prestaciones.AsNoTracking()
            .Include(p => p.PrestacionTipo)
            .Where(p => p.CI == ci)
            .OrderByDescending(p => p.Fecha)
            .Select(p => new
            {
                p.CI,
                p.Fecha,
                Tipo = p.PrestacionTipo != null ? p.PrestacionTipo.Descrip : null,
                p.Importe
            });

    /// <summary>102_Prestacion_Cantidad: Count of prestaciones by type for a CI.</summary>
    public async Task<int> GetPrestacionCantidadAsync(long ci, int codPrestacionTipo)
        => await db.Prestaciones.AsNoTracking()
            .CountAsync(p => p.CI == ci && p.CodPrestacionTipo == codPrestacionTipo);

    /// <summary>103_ReintegrosAfiliado: Reintegros for a CI.</summary>
    public IQueryable<object> GetReintegrosAfiliado(long ci)
        => db.ReintegrosMutuales.AsNoTracking()
            .Include(r => r.Mutualista)
            .Where(r => r.CI == ci)
            .OrderByDescending(r => r.Anio).ThenByDescending(r => r.Mes)
            .Select(r => new
            {
                r.CI,
                AnioMes = r.Anio + "/" + (r.Mes ?? 0).ToString("D2"),
                r.Fecha,
                Mutualista = r.Mutualista != null ? r.Mutualista.Descrip : null,
                r.Importe
            });

    /// <summary>Rs_Prestacion: All prestaciones.</summary>
    public IQueryable<Prestacion> GetPrestaciones()
        => db.Prestaciones.AsNoTracking();

    /// <summary>Rs_ReintegroMutual: All reintegros.</summary>
    public IQueryable<ReintegroMutual> GetReintegros()
        => db.ReintegrosMutuales.AsNoTracking();
}
