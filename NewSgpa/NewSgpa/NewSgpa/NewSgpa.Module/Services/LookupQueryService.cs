using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for lookup/catalog queries used in combos and dropdowns.
/// Replaces: Rs_*_Desc queries (Rs_Mutualista_Desc, Rs_Empresa_Desc, Rs_BajaMotivo_Desc, etc.)
/// </summary>
public class LookupQueryService(NewSgpaEFCoreDbContext db)
{
    public IQueryable<Mutualista> GetMutualistas()
        => db.Mutualistas.OrderBy(m => m.Descrip);

    public IQueryable<Empresa> GetEmpresas()
        => db.Empresas.OrderBy(e => e.Nombre);

    public IQueryable<Empresa> GetEmpresasReales()
        => db.Empresas.Where(e => !e.Ficticia).OrderBy(e => e.Nombre);

    public IQueryable<AfeccionGrupo> GetAfeccionGrupos()
        => db.AfeccionGrupos.OrderBy(a => a.Descrip);

    public IQueryable<AfeccionTipo> GetAfeccionTipos()
        => db.AfeccionTipos.OrderBy(a => a.Descrip);

    public IQueryable<Patologia> GetPatologias()
        => db.Patologias.OrderBy(p => p.Descrip);

    public IQueryable<Certificador> GetCertificadores()
        => db.Certificadores.OrderBy(c => c.Descrip);

    public IQueryable<SalidaTipo> GetSalidaTipos()
        => db.SalidaTipos.OrderBy(s => s.Descrip);

    public IQueryable<BajaMotivo> GetBajaMotivos()
        => db.BajaMotivos.OrderBy(b => b.Descrip);

    public IQueryable<Banco> GetBancos()
        => db.Bancos.OrderBy(b => b.Descripcion);

    public IQueryable<FormaPago> GetFormasPago()
        => db.FormasPago.OrderBy(f => f.Descrip);

    public IQueryable<RegimenAporte> GetRegimenesAporte()
        => db.RegimenesAporte.OrderBy(r => r.Descrip);

    public IQueryable<RegimenJubilatorio> GetRegimenesJubilatorio()
        => db.RegimenesJubilatorio.OrderBy(r => r.Descrip);

    public IQueryable<AporteTipo> GetAporteTipos()
        => db.AporteTipos.OrderBy(a => a.Descrip);

    public IQueryable<SituacionPago> GetSituacionesPago()
        => db.SituacionesPago.OrderBy(s => s.Descrip);

    public IQueryable<SituacionMutual> GetSituacionesMutual()
        => db.SituacionesMutual.OrderBy(s => s.Descrip);

    public IQueryable<Departamento> GetDepartamentos()
        => db.Departamentos.OrderBy(d => d.Descrip);

    public IQueryable<Especialidad> GetEspecialidades()
        => db.Especialidades.OrderBy(e => e.Descrip);

    public IQueryable<PrestacionTipo> GetPrestacionTipos()
        => db.PrestacionTipos.OrderBy(p => p.Descrip);

    public IQueryable<RecetaDistancia> GetRecetaDistancias()
        => db.RecetaDistancias.OrderBy(r => r.Descrip);
}
