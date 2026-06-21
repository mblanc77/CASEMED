using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Service for Subsidio liquidation queries and business logic.
/// Replaces VB6 frmLiquidaSubsidio and queries: 200_*, 210_*, Rs_SubsidioXMes, etc.
/// </summary>
public class SubsidioQueryService(NewSgpaEFCoreDbContext db)
{
    /// <summary>Rs_SubsidioXMes: Subsidios for a given month/year.</summary>
    public IQueryable<SubsidioCabezal> GetSubsidiosPorMes(int mes, int anio)
        => db.SubsidioCabezales.AsNoTracking()
            .Include(s => s.Afiliado)
            .Include(s => s.Enfermedades)
            .Include(s => s.Empresas)
            .Include(s => s.Items)
            .Where(s => s.Mes == mes && s.Anio == anio);

    /// <summary>Subsidios for a specific afiliado.</summary>
    public IQueryable<SubsidioCabezal> GetSubsidiosPorCi(long ci)
        => db.SubsidioCabezales.AsNoTracking()
            .Include(s => s.Enfermedades)
            .Include(s => s.Items)
            .Where(s => s.CI == ci)
            .OrderByDescending(s => s.Anio).ThenByDescending(s => s.Mes);

    /// <summary>
    /// 200_PagarMutualista: Afiliados who qualify for mutual payment.
    /// Original: active employment, SituacionMutual='AF', CodMutualista not in (0,38,40), no baja.
    /// </summary>
    public IQueryable<object> GetPagarMutualista()
        => from a in db.Afiliados.AsNoTracking()
           join t in db.Trabajos.AsNoTracking() on a.CI equals t.CI
           join e in db.Empresas.AsNoTracking() on t.CodEmpresa equals e.CodEmpresa
           where a.CodSituacionMutual == "AF"
                 && a.CodMutualista != null
                 && a.CodMutualista != 0 && a.CodMutualista != 38 && a.CodMutualista != 40
                 && t.FechaBaja == null
           select new { a.CI, t.CodEmpresa };

    /// <summary>
    /// 250_AfiliadoConDerecho: Afiliados with subsidio rights.
    /// Active employment + real mutual + SituacionMutual='AF' + real empresa.
    /// </summary>
    public IQueryable<object> GetAfiliadosConDerecho(int mesIni, int mesFin)
        => from a in db.Afiliados.AsNoTracking()
           join t in db.Trabajos.AsNoTracking() on a.CI equals t.CI
           join e in db.Empresas.AsNoTracking() on t.CodEmpresa equals e.CodEmpresa
           join m in db.Mutualistas.AsNoTracking() on a.CodMutualista equals m.CodMutualista
           where t.FechaBaja == null
                 && !m.Ficticia
                 && a.CodSituacionMutual == "AF"
                 && !e.Ficticia
           select new { a.CI, t.CodEmpresa };

    /// <summary>
    /// 210_SubsidioCantidad: Count of subsidios for a period.
    /// </summary>
    public async Task<int> GetSubsidioCantidadAsync(int mes, int anio, bool liquidar)
        => await db.SubsidioCabezales.AsNoTracking()
            .CountAsync(s => s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar);

    /// <summary>
    /// 210_MontoGrabado: Total taxable amount (Nominal + Aguinaldo).
    /// </summary>
    public async Task<double> GetMontoGravadoAsync(int mes, int anio, bool liquidar)
        => await db.SubsidioCabezales.AsNoTracking()
            .Where(s => s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar)
            .SumAsync(s => (s.ImpNominal ?? 0) + (s.ImpAguinaldo ?? 0));

    /// <summary>
    /// 210_ImpRetObrero: Worker retention items (codes 1-7).
    /// </summary>
    public async Task<double> GetImpRetObreroAsync(int mes, int anio, bool liquidar)
    {
        int[] codigos = [1, 2, 3, 4, 5, 6, 7];
        return await (from s in db.SubsidioCabezales.AsNoTracking()
                      join i in db.SubsidioItems.AsNoTracking() on s.IdSubsidio equals i.IdSubsidio
                      where s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar
                            && codigos.Contains(i.CodSubsidioItemCod ?? 0)
                      select i.Importe ?? 0).SumAsync();
    }

    /// <summary>
    /// 210_ImpRetPatronal: Employer retention items (codes 101, 102).
    /// </summary>
    public async Task<double> GetImpRetPatronalAsync(int mes, int anio, bool liquidar)
    {
        int[] codigos = [101, 102];
        return await (from s in db.SubsidioCabezales.AsNoTracking()
                      join i in db.SubsidioItems.AsNoTracking() on s.IdSubsidio equals i.IdSubsidio
                      where s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar
                            && codigos.Contains(i.CodSubsidioItemCod ?? 0)
                      select i.Importe ?? 0).SumAsync();
    }

    /// <summary>
    /// 210_TotImpEmp: Total empresa payments for a period.
    /// </summary>
    public async Task<(double Importe, double Tributo)> GetTotalEmpresaPagoAsync(int mes, int anio)
    {
        var importe = await db.EmpresaPagos.AsNoTracking()
            .Where(e => e.Mes == mes && e.Anio == anio)
            .SumAsync(e => e.Importe ?? 0);
        return (importe, importe * 0.5 / 100);
    }

    /// <summary>
    /// xw_Suma_ValorJornal1: Sum of ValorJornal by IdSubsidio across empresas.
    /// </summary>
    public IQueryable<object> GetSumaValorJornalPorSubsidio()
        => db.SubsidioCabezalEmpresas.AsNoTracking()
            .GroupBy(e => e.IdSubsidio)
            .Select(g => new
            {
                IdSubsidio = g.Key,
                SumaDeValorJornal = g.Sum(x => x.ValorJornal ?? 0)
            });

    /// <summary>Rs_SubsidioItem: Item codes list.</summary>
    public IQueryable<SubsidioItemCod> GetSubsidioItemCods()
        => db.SubsidioItemCods.AsNoTracking().OrderBy(s => s.Orden);

    /// <summary>Rs_SubsidioItemCodXCI: Item code overrides for a specific afiliado.</summary>
    public IQueryable<SubsidioItemCodAfiliado> GetSubsidioItemCodPorCi(long ci)
        => db.SubsidioItemCodAfiliados.AsNoTracking()
            .Include(s => s.SubsidioItemCod)
            .Where(s => s.CI == ci);
}
