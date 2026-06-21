using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using SgpaNew.Server.Data;

namespace SgpaNew.Server.Controllers
{
    public partial class ExportSgpaController : ExportController
    {
        private readonly SgpaContext context;
        private readonly SgpaService service;

        public ExportSgpaController(SgpaContext context, SgpaService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Sgpa/adprejubs/csv")]
        [HttpGet("/export/Sgpa/adprejubs/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdPreJubsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAdPreJubs(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/adprejubs/excel")]
        [HttpGet("/export/Sgpa/adprejubs/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdPreJubsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAdPreJubs(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/adprejubpagos/csv")]
        [HttpGet("/export/Sgpa/adprejubpagos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdPreJubPagosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAdPreJubPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/adprejubpagos/excel")]
        [HttpGet("/export/Sgpa/adprejubpagos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdPreJubPagosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAdPreJubPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afecciongrupos/csv")]
        [HttpGet("/export/Sgpa/afecciongrupos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfeccionGruposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAfeccionGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afecciongrupos/excel")]
        [HttpGet("/export/Sgpa/afecciongrupos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfeccionGruposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAfeccionGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afecciontipos/csv")]
        [HttpGet("/export/Sgpa/afecciontipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfeccionTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAfeccionTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afecciontipos/excel")]
        [HttpGet("/export/Sgpa/afecciontipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfeccionTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAfeccionTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliados/csv")]
        [HttpGet("/export/Sgpa/afiliados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAfiliados(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliados/excel")]
        [HttpGet("/export/Sgpa/afiliados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAfiliados(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliadoapuntes/csv")]
        [HttpGet("/export/Sgpa/afiliadoapuntes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadoApuntesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAfiliadoApuntes(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliadoapuntes/excel")]
        [HttpGet("/export/Sgpa/afiliadoapuntes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadoApuntesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAfiliadoApuntes(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliadoespecialidads/csv")]
        [HttpGet("/export/Sgpa/afiliadoespecialidads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadoEspecialidadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAfiliadoEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/afiliadoespecialidads/excel")]
        [HttpGet("/export/Sgpa/afiliadoespecialidads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAfiliadoEspecialidadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAfiliadoEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/aportetipos/csv")]
        [HttpGet("/export/Sgpa/aportetipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAporteTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAporteTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/aportetipos/excel")]
        [HttpGet("/export/Sgpa/aportetipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAporteTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAporteTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/bajamotivos/csv")]
        [HttpGet("/export/Sgpa/bajamotivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaMotivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBajaMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/bajamotivos/excel")]
        [HttpGet("/export/Sgpa/bajamotivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaMotivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBajaMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/bancos/csv")]
        [HttpGet("/export/Sgpa/bancos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBancosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBancos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/bancos/excel")]
        [HttpGet("/export/Sgpa/bancos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBancosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBancos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificacions/csv")]
        [HttpGet("/export/Sgpa/certificacions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificacionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCertificacions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificacions/excel")]
        [HttpGet("/export/Sgpa/certificacions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificacionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCertificacions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificacionprorrogas/csv")]
        [HttpGet("/export/Sgpa/certificacionprorrogas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificacionProrrogasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCertificacionProrrogas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificacionprorrogas/excel")]
        [HttpGet("/export/Sgpa/certificacionprorrogas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificacionProrrogasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCertificacionProrrogas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificadors/csv")]
        [HttpGet("/export/Sgpa/certificadors/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificadorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCertificadors(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/certificadors/excel")]
        [HttpGet("/export/Sgpa/certificadors/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCertificadorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCertificadors(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/ctasbrous/csv")]
        [HttpGet("/export/Sgpa/ctasbrous/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCtasbrousToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCtasbrous(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/ctasbrous/excel")]
        [HttpGet("/export/Sgpa/ctasbrous/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCtasbrousToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCtasbrous(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/cuenta/csv")]
        [HttpGet("/export/Sgpa/cuenta/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuenta(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/cuenta/excel")]
        [HttpGet("/export/Sgpa/cuenta/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuenta(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/departamentos/csv")]
        [HttpGet("/export/Sgpa/departamentos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartamentosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepartamentos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/departamentos/excel")]
        [HttpGet("/export/Sgpa/departamentos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartamentosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepartamentos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/discounts/csv")]
        [HttpGet("/export/Sgpa/discounts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDiscountsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDiscounts(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/discounts/excel")]
        [HttpGet("/export/Sgpa/discounts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDiscountsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDiscounts(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/empresas/csv")]
        [HttpGet("/export/Sgpa/empresas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmpresasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/empresas/excel")]
        [HttpGet("/export/Sgpa/empresas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmpresasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/empresapagos/csv")]
        [HttpGet("/export/Sgpa/empresapagos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmpresaPagosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmpresaPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/empresapagos/excel")]
        [HttpGet("/export/Sgpa/empresapagos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmpresaPagosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmpresaPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/especialidads/csv")]
        [HttpGet("/export/Sgpa/especialidads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEspecialidadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/especialidads/excel")]
        [HttpGet("/export/Sgpa/especialidads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEspecialidadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/formapagos/csv")]
        [HttpGet("/export/Sgpa/formapagos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFormaPagosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFormaPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/formapagos/excel")]
        [HttpGet("/export/Sgpa/formapagos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFormaPagosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFormaPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/franjairpfs/csv")]
        [HttpGet("/export/Sgpa/franjairpfs/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFranjaIrpfsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFranjaIrpfs(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/franjairpfs/excel")]
        [HttpGet("/export/Sgpa/franjairpfs/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFranjaIrpfsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFranjaIrpfs(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/imps/csv")]
        [HttpGet("/export/Sgpa/imps/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportImpsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetImps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/imps/excel")]
        [HttpGet("/export/Sgpa/imps/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportImpsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetImps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/imponibles/csv")]
        [HttpGet("/export/Sgpa/imponibles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportImponiblesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetImponibles(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/imponibles/excel")]
        [HttpGet("/export/Sgpa/imponibles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportImponiblesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetImponibles(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/informeestadisticos/csv")]
        [HttpGet("/export/Sgpa/informeestadisticos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportInformeEstadisticosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetInformeEstadisticos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/informeestadisticos/excel")]
        [HttpGet("/export/Sgpa/informeestadisticos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportInformeEstadisticosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetInformeEstadisticos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/liquidacionbps/csv")]
        [HttpGet("/export/Sgpa/liquidacionbps/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLiquidacionBpsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLiquidacionBps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/liquidacionbps/excel")]
        [HttpGet("/export/Sgpa/liquidacionbps/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLiquidacionBpsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLiquidacionBps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/maefuns/csv")]
        [HttpGet("/export/Sgpa/maefuns/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMaeFunsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMaeFuns(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/maefuns/excel")]
        [HttpGet("/export/Sgpa/maefuns/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMaeFunsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMaeFuns(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/moneda/csv")]
        [HttpGet("/export/Sgpa/moneda/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMonedaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMoneda(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/moneda/excel")]
        [HttpGet("/export/Sgpa/moneda/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMonedaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMoneda(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/mutualista/csv")]
        [HttpGet("/export/Sgpa/mutualista/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMutualistaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMutualista(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/mutualista/excel")]
        [HttpGet("/export/Sgpa/mutualista/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMutualistaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMutualista(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/nocargadohls/csv")]
        [HttpGet("/export/Sgpa/nocargadohls/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNoCargadoHlsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNoCargadoHls(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/nocargadohls/excel")]
        [HttpGet("/export/Sgpa/nocargadohls/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNoCargadoHlsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNoCargadoHls(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/parametros/csv")]
        [HttpGet("/export/Sgpa/parametros/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportParametrosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetParametros(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/parametros/excel")]
        [HttpGet("/export/Sgpa/parametros/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportParametrosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetParametros(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/patologia/csv")]
        [HttpGet("/export/Sgpa/patologia/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPatologiaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPatologia(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/patologia/excel")]
        [HttpGet("/export/Sgpa/patologia/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPatologiaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPatologia(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/prestacions/csv")]
        [HttpGet("/export/Sgpa/prestacions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrestacionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPrestacions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/prestacions/excel")]
        [HttpGet("/export/Sgpa/prestacions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrestacionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPrestacions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/prestaciontipos/csv")]
        [HttpGet("/export/Sgpa/prestaciontipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrestacionTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPrestacionTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/prestaciontipos/excel")]
        [HttpGet("/export/Sgpa/prestaciontipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrestacionTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPrestacionTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/primafallecimientos/csv")]
        [HttpGet("/export/Sgpa/primafallecimientos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrimaFallecimientosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPrimaFallecimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/primafallecimientos/excel")]
        [HttpGet("/export/Sgpa/primafallecimientos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPrimaFallecimientosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPrimaFallecimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/receta/csv")]
        [HttpGet("/export/Sgpa/receta/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRecetaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetReceta(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/receta/excel")]
        [HttpGet("/export/Sgpa/receta/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRecetaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetReceta(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/recetadistancia/csv")]
        [HttpGet("/export/Sgpa/recetadistancia/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRecetaDistanciaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRecetaDistancia(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/recetadistancia/excel")]
        [HttpGet("/export/Sgpa/recetadistancia/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRecetaDistanciaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRecetaDistancia(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/regimenaportes/csv")]
        [HttpGet("/export/Sgpa/regimenaportes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegimenAportesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegimenAportes(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/regimenaportes/excel")]
        [HttpGet("/export/Sgpa/regimenaportes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegimenAportesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegimenAportes(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/regimenjubilatorios/csv")]
        [HttpGet("/export/Sgpa/regimenjubilatorios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegimenJubilatoriosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegimenJubilatorios(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/regimenjubilatorios/excel")]
        [HttpGet("/export/Sgpa/regimenjubilatorios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegimenJubilatoriosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegimenJubilatorios(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/reintegromutuals/csv")]
        [HttpGet("/export/Sgpa/reintegromutuals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReintegroMutualsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetReintegroMutuals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/reintegromutuals/excel")]
        [HttpGet("/export/Sgpa/reintegromutuals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReintegroMutualsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetReintegroMutuals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/salidatipos/csv")]
        [HttpGet("/export/Sgpa/salidatipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalidaTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalidaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/salidatipos/excel")]
        [HttpGet("/export/Sgpa/salidatipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalidaTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalidaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/seleccions/csv")]
        [HttpGet("/export/Sgpa/seleccions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSeleccionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSeleccions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/seleccions/excel")]
        [HttpGet("/export/Sgpa/seleccions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSeleccionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSeleccions(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/situacionmutuals/csv")]
        [HttpGet("/export/Sgpa/situacionmutuals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSituacionMutualsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSituacionMutuals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/situacionmutuals/excel")]
        [HttpGet("/export/Sgpa/situacionmutuals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSituacionMutualsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSituacionMutuals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/situacionpagos/csv")]
        [HttpGet("/export/Sgpa/situacionpagos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSituacionPagosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSituacionPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/situacionpagos/excel")]
        [HttpGet("/export/Sgpa/situacionpagos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSituacionPagosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSituacionPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezals/csv")]
        [HttpGet("/export/Sgpa/subsidiocabezals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioCabezalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioCabezals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezals/excel")]
        [HttpGet("/export/Sgpa/subsidiocabezals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioCabezalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioCabezals(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezalbps/csv")]
        [HttpGet("/export/Sgpa/subsidiocabezalbps/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidiocabezalBpsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidiocabezalBps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezalbps/excel")]
        [HttpGet("/export/Sgpa/subsidiocabezalbps/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidiocabezalBpsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidiocabezalBps(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezalempresas/csv")]
        [HttpGet("/export/Sgpa/subsidiocabezalempresas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioCabezalEmpresasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioCabezalEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidiocabezalempresas/excel")]
        [HttpGet("/export/Sgpa/subsidiocabezalempresas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioCabezalEmpresasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioCabezalEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioenfermedads/csv")]
        [HttpGet("/export/Sgpa/subsidioenfermedads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioEnfermedadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioEnfermedads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioenfermedads/excel")]
        [HttpGet("/export/Sgpa/subsidioenfermedads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioEnfermedadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioEnfermedads(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioimponibles/csv")]
        [HttpGet("/export/Sgpa/subsidioimponibles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioImponiblesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioImponibles(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioimponibles/excel")]
        [HttpGet("/export/Sgpa/subsidioimponibles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioImponiblesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioImponibles(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitems/csv")]
        [HttpGet("/export/Sgpa/subsidioitems/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioItems(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitems/excel")]
        [HttpGet("/export/Sgpa/subsidioitems/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioItems(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemcods/csv")]
        [HttpGet("/export/Sgpa/subsidioitemcods/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemCodsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioItemCods(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemcods/excel")]
        [HttpGet("/export/Sgpa/subsidioitemcods/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemCodsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioItemCods(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemcodafiliados/csv")]
        [HttpGet("/export/Sgpa/subsidioitemcodafiliados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioitemcodAfiliadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioitemcodAfiliados(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemcodafiliados/excel")]
        [HttpGet("/export/Sgpa/subsidioitemcodafiliados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioitemcodAfiliadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioitemcodAfiliados(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemempresas/csv")]
        [HttpGet("/export/Sgpa/subsidioitemempresas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemEmpresasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubsidioItemEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/subsidioitemempresas/excel")]
        [HttpGet("/export/Sgpa/subsidioitemempresas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubsidioItemEmpresasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubsidioItemEmpresas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/trabajas/csv")]
        [HttpGet("/export/Sgpa/trabajas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTrabajasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTrabajas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/trabajas/excel")]
        [HttpGet("/export/Sgpa/trabajas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTrabajasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTrabajas(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/xusrparams/csv")]
        [HttpGet("/export/Sgpa/xusrparams/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXUsrParamsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXUsrParams(), Request.Query), fileName);
        }

        [HttpGet("/export/Sgpa/xusrparams/excel")]
        [HttpGet("/export/Sgpa/xusrparams/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXUsrParamsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXUsrParams(), Request.Query), fileName);
        }
    }
}
