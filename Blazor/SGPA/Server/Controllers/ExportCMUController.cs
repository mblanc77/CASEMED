using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using SGPA.Server.Data;

namespace SGPA.Server.Controllers
{
    public partial class ExportCMUController : ExportController
    {
        private readonly CMUContext context;
        private readonly CMUService service;

        public ExportCMUController(CMUContext context, CMUService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/CMU/actaconsejos/csv")]
        [HttpGet("/export/CMU/actaconsejos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportActaConsejosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetActaConsejos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/actaconsejos/excel")]
        [HttpGet("/export/CMU/actaconsejos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportActaConsejosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetActaConsejos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzas/csv")]
        [HttpGet("/export/CMU/agentecobranzas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAgenteCobranzas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzas/excel")]
        [HttpGet("/export/CMU/agentecobranzas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAgenteCobranzas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzadebitos/csv")]
        [HttpGet("/export/CMU/agentecobranzadebitos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzaDebitosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAgenteCobranzaDebitos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzadebitos/excel")]
        [HttpGet("/export/CMU/agentecobranzadebitos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzaDebitosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAgenteCobranzaDebitos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzatipos/csv")]
        [HttpGet("/export/CMU/agentecobranzatipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzaTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAgenteCobranzaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentecobranzatipos/excel")]
        [HttpGet("/export/CMU/agentecobranzatipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteCobranzaTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAgenteCobranzaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentegrupos/csv")]
        [HttpGet("/export/CMU/agentegrupos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteGruposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAgenteGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/agentegrupos/excel")]
        [HttpGet("/export/CMU/agentegrupos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAgenteGruposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAgenteGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/ajustedetalles/csv")]
        [HttpGet("/export/CMU/ajustedetalles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAjusteDetallesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAjusteDetalles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/ajustedetalles/excel")]
        [HttpGet("/export/CMU/ajustedetalles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAjusteDetallesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAjusteDetalles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/ajusteretroactivos/csv")]
        [HttpGet("/export/CMU/ajusteretroactivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAjusteRetroactivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAjusteRetroactivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/ajusteretroactivos/excel")]
        [HttpGet("/export/CMU/ajusteretroactivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAjusteRetroactivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAjusteRetroactivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/analyses/csv")]
        [HttpGet("/export/CMU/analyses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAnalysesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAnalyses(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/analyses/excel")]
        [HttpGet("/export/CMU/analyses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAnalysesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAnalyses(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/areacontactos/csv")]
        [HttpGet("/export/CMU/areacontactos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAreaContactosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAreaContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/areacontactos/excel")]
        [HttpGet("/export/CMU/areacontactos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAreaContactosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAreaContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/auditdataitempersistents/csv")]
        [HttpGet("/export/CMU/auditdataitempersistents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAuditDataItemPersistentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAuditDataItemPersistents(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/auditdataitempersistents/excel")]
        [HttpGet("/export/CMU/auditdataitempersistents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAuditDataItemPersistentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAuditDataItemPersistents(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/auditedobjectweakreferences/csv")]
        [HttpGet("/export/CMU/auditedobjectweakreferences/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAuditedObjectWeakReferencesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAuditedObjectWeakReferences(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/auditedobjectweakreferences/excel")]
        [HttpGet("/export/CMU/auditedobjectweakreferences/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAuditedObjectWeakReferencesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAuditedObjectWeakReferences(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bajamotivos/csv")]
        [HttpGet("/export/CMU/bajamotivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaMotivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBajaMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bajamotivos/excel")]
        [HttpGet("/export/CMU/bajamotivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaMotivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBajaMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bajatemporalmotivos/csv")]
        [HttpGet("/export/CMU/bajatemporalmotivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaTemporalMotivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBajaTemporalMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bajatemporalmotivos/excel")]
        [HttpGet("/export/CMU/bajatemporalmotivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBajaTemporalMotivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBajaTemporalMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bancos/csv")]
        [HttpGet("/export/CMU/bancos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBancosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBancos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/bancos/excel")]
        [HttpGet("/export/CMU/bancos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBancosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBancos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cargocontactos/csv")]
        [HttpGet("/export/CMU/cargocontactos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCargoContactosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCargoContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cargocontactos/excel")]
        [HttpGet("/export/CMU/cargocontactos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCargoContactosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCargoContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/categoriacolegiados/csv")]
        [HttpGet("/export/CMU/categoriacolegiados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoriaColegiadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategoriaColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/categoriacolegiados/excel")]
        [HttpGet("/export/CMU/categoriacolegiados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoriaColegiadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategoriaColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/categoriacolegiadovalors/csv")]
        [HttpGet("/export/CMU/categoriacolegiadovalors/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoriaColegiadoValorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategoriaColegiadoValors(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/categoriacolegiadovalors/excel")]
        [HttpGet("/export/CMU/categoriacolegiadovalors/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCategoriaColegiadoValorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategoriaColegiadoValors(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjps/csv")]
        [HttpGet("/export/CMU/cjps/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCjps(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjps/excel")]
        [HttpGet("/export/CMU/cjps/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCjps(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjpmats/csv")]
        [HttpGet("/export/CMU/cjpmats/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpMatsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCjpMats(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjpmats/excel")]
        [HttpGet("/export/CMU/cjpmats/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpMatsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCjpMats(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjpolds/csv")]
        [HttpGet("/export/CMU/cjpolds/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpOldsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCjpOlds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cjpolds/excel")]
        [HttpGet("/export/CMU/cjpolds/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCjpOldsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCjpOlds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cobros/csv")]
        [HttpGet("/export/CMU/cobros/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCobrosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCobros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cobros/excel")]
        [HttpGet("/export/CMU/cobros/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCobrosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCobros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cobronominas/csv")]
        [HttpGet("/export/CMU/cobronominas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCobroNominasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCobroNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cobronominas/excel")]
        [HttpGet("/export/CMU/cobronominas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCobroNominasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCobroNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiados/csv")]
        [HttpGet("/export/CMU/colegiados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiados/excel")]
        [HttpGet("/export/CMU/colegiados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadoactualizaciondps/csv")]
        [HttpGet("/export/CMU/colegiadoactualizaciondps/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoActualizacionDpsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoActualizacionDps(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadoactualizaciondps/excel")]
        [HttpGet("/export/CMU/colegiadoactualizaciondps/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoActualizacionDpsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoActualizacionDps(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoras/csv")]
        [HttpGet("/export/CMU/colegiadobitacoras/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacorasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoBitacoras(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoras/excel")]
        [HttpGet("/export/CMU/colegiadobitacoras/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacorasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoBitacoras(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoraemailenvios/csv")]
        [HttpGet("/export/CMU/colegiadobitacoraemailenvios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraEMailEnviosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoBitacoraEMailEnvios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoraemailenvios/excel")]
        [HttpGet("/export/CMU/colegiadobitacoraemailenvios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraEMailEnviosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoBitacoraEMailEnvios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoraemailrecepcions/csv")]
        [HttpGet("/export/CMU/colegiadobitacoraemailrecepcions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraEMailRecepcionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoBitacoraEMailRecepcions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoraemailrecepcions/excel")]
        [HttpGet("/export/CMU/colegiadobitacoraemailrecepcions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraEMailRecepcionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoBitacoraEMailRecepcions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoranota/csv")]
        [HttpGet("/export/CMU/colegiadobitacoranota/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraNotaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoBitacoraNota(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadobitacoranota/excel")]
        [HttpGet("/export/CMU/colegiadobitacoranota/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoBitacoraNotaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoBitacoraNota(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadocambiocategoria/csv")]
        [HttpGet("/export/CMU/colegiadocambiocategoria/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoCambioCategoriaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoCambioCategoria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadocambiocategoria/excel")]
        [HttpGet("/export/CMU/colegiadocambiocategoria/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoCambioCategoriaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoCambioCategoria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadocertificadoexpedidos/csv")]
        [HttpGet("/export/CMU/colegiadocertificadoexpedidos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoCertificadoExpedidosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoCertificadoExpedidos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadocertificadoexpedidos/excel")]
        [HttpGet("/export/CMU/colegiadocertificadoexpedidos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoCertificadoExpedidosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoCertificadoExpedidos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadodebitobancarioasociados/csv")]
        [HttpGet("/export/CMU/colegiadodebitobancarioasociados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoDebitoBancarioAsociadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoDebitoBancarioAsociados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadodebitobancarioasociados/excel")]
        [HttpGet("/export/CMU/colegiadodebitobancarioasociados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoDebitoBancarioAsociadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoDebitoBancarioAsociados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadodeclaracionjurada/csv")]
        [HttpGet("/export/CMU/colegiadodeclaracionjurada/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoDeclaracionJuradaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoDeclaracionJurada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadodeclaracionjurada/excel")]
        [HttpGet("/export/CMU/colegiadodeclaracionjurada/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoDeclaracionJuradaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoDeclaracionJurada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadoimagenes/csv")]
        [HttpGet("/export/CMU/colegiadoimagenes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoImagenesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoImagenes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadoimagenes/excel")]
        [HttpGet("/export/CMU/colegiadoimagenes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoImagenesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoImagenes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadomovimientos/csv")]
        [HttpGet("/export/CMU/colegiadomovimientos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoMovimientosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoMovimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadomovimientos/excel")]
        [HttpGet("/export/CMU/colegiadomovimientos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoMovimientosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoMovimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiados2011s/csv")]
        [HttpGet("/export/CMU/colegiados2011s/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiados2011SToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiados2011S(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiados2011s/excel")]
        [HttpGet("/export/CMU/colegiados2011s/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiados2011SToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiados2011S(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadotarjetadebitoasociada/csv")]
        [HttpGet("/export/CMU/colegiadotarjetadebitoasociada/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoTarjetaDebitoAsociadaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColegiadoTarjetaDebitoAsociada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/colegiadotarjetadebitoasociada/excel")]
        [HttpGet("/export/CMU/colegiadotarjetadebitoasociada/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColegiadoTarjetaDebitoAsociadaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColegiadoTarjetaDebitoAsociada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/contactos/csv")]
        [HttpGet("/export/CMU/contactos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/contactos/excel")]
        [HttpGet("/export/CMU/contactos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/contactoinfoadicionals/csv")]
        [HttpGet("/export/CMU/contactoinfoadicionals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactoInfoAdicionalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactoInfoAdicionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/contactoinfoadicionals/excel")]
        [HttpGet("/export/CMU/contactoinfoadicionals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactoInfoAdicionalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactoInfoAdicionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/convenios/csv")]
        [HttpGet("/export/CMU/convenios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConveniosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetConvenios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/convenios/excel")]
        [HttpGet("/export/CMU/convenios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConveniosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetConvenios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/conveniofinanciacions/csv")]
        [HttpGet("/export/CMU/conveniofinanciacions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConvenioFinanciacionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetConvenioFinanciacions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/conveniofinanciacions/excel")]
        [HttpGet("/export/CMU/conveniofinanciacions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConvenioFinanciacionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetConvenioFinanciacions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cuentabancaria/csv")]
        [HttpGet("/export/CMU/cuentabancaria/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaBancariaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuentaBancaria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/cuentabancaria/excel")]
        [HttpGet("/export/CMU/cuentabancaria/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaBancariaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuentaBancaria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitos/csv")]
        [HttpGet("/export/CMU/debitos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDebitos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitos/excel")]
        [HttpGet("/export/CMU/debitos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDebitos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitoadjuntos/csv")]
        [HttpGet("/export/CMU/debitoadjuntos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitoAdjuntosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDebitoAdjuntos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitoadjuntos/excel")]
        [HttpGet("/export/CMU/debitoadjuntos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitoAdjuntosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDebitoAdjuntos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitonominas/csv")]
        [HttpGet("/export/CMU/debitonominas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitoNominasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDebitoNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/debitonominas/excel")]
        [HttpGet("/export/CMU/debitonominas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDebitoNominasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDebitoNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/declaracionjuradaadjuntos/csv")]
        [HttpGet("/export/CMU/declaracionjuradaadjuntos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDeclaracionJuradaAdjuntosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDeclaracionJuradaAdjuntos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/declaracionjuradaadjuntos/excel")]
        [HttpGet("/export/CMU/declaracionjuradaadjuntos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDeclaracionJuradaAdjuntosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDeclaracionJuradaAdjuntos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/declaracionjuradatipos/csv")]
        [HttpGet("/export/CMU/declaracionjuradatipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDeclaracionJuradaTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDeclaracionJuradaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/declaracionjuradatipos/excel")]
        [HttpGet("/export/CMU/declaracionjuradatipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDeclaracionJuradaTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDeclaracionJuradaTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/departamentos/csv")]
        [HttpGet("/export/CMU/departamentos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartamentosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepartamentos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/departamentos/excel")]
        [HttpGet("/export/CMU/departamentos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepartamentosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepartamentos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositos/csv")]
        [HttpGet("/export/CMU/depositos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepositos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositos/excel")]
        [HttpGet("/export/CMU/depositos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepositos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominas/csv")]
        [HttpGet("/export/CMU/depositonominas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepositoNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominas/excel")]
        [HttpGet("/export/CMU/depositonominas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepositoNominas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominamultibrous/csv")]
        [HttpGet("/export/CMU/depositonominamultibrous/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaMultiBrousToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepositoNominaMultiBrous(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominamultibrous/excel")]
        [HttpGet("/export/CMU/depositonominamultibrous/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaMultiBrousToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepositoNominaMultiBrous(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominanoidentificada/csv")]
        [HttpGet("/export/CMU/depositonominanoidentificada/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaNoIdentificadaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepositoNominaNoIdentificada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominanoidentificada/excel")]
        [HttpGet("/export/CMU/depositonominanoidentificada/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaNoIdentificadaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepositoNominaNoIdentificada(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominaredpagos/csv")]
        [HttpGet("/export/CMU/depositonominaredpagos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaRedPagosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepositoNominaRedPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/depositonominaredpagos/excel")]
        [HttpGet("/export/CMU/depositonominaredpagos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepositoNominaRedPagosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepositoNominaRedPagos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/djinactividadmotivos/csv")]
        [HttpGet("/export/CMU/djinactividadmotivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDjInactividadMotivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDjInactividadMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/djinactividadmotivos/excel")]
        [HttpGet("/export/CMU/djinactividadmotivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDjInactividadMotivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDjInactividadMotivos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/dynamiclistviewfilters/csv")]
        [HttpGet("/export/CMU/dynamiclistviewfilters/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDynamicListViewFiltersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDynamicListViewFilters(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/dynamiclistviewfilters/excel")]
        [HttpGet("/export/CMU/dynamiclistviewfilters/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDynamicListViewFiltersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDynamicListViewFilters(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/emailenvios/csv")]
        [HttpGet("/export/CMU/emailenvios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailEnviosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmailEnvios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/emailenvios/excel")]
        [HttpGet("/export/CMU/emailenvios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailEnviosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmailEnvios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/especialidads/csv")]
        [HttpGet("/export/CMU/especialidads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEspecialidadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/especialidads/excel")]
        [HttpGet("/export/CMU/especialidads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEspecialidadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEspecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/facultadtitulos/csv")]
        [HttpGet("/export/CMU/facultadtitulos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacultadTitulosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFacultadTitulos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/facultadtitulos/excel")]
        [HttpGet("/export/CMU/facultadtitulos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacultadTitulosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFacultadTitulos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/filedata/csv")]
        [HttpGet("/export/CMU/filedata/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFileDataToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFileData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/filedata/excel")]
        [HttpGet("/export/CMU/filedata/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFileDataToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFileData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/grupocontactos/csv")]
        [HttpGet("/export/CMU/grupocontactos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGrupoContactosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGrupoContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/grupocontactos/excel")]
        [HttpGet("/export/CMU/grupocontactos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGrupoContactosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGrupoContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/grupolugarretirocarnes/csv")]
        [HttpGet("/export/CMU/grupolugarretirocarnes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGrupoLugarRetiroCarnesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGrupoLugarRetiroCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/grupolugarretirocarnes/excel")]
        [HttpGet("/export/CMU/grupolugarretirocarnes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGrupoLugarRetiroCarnesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGrupoLugarRetiroCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpidefinitions/csv")]
        [HttpGet("/export/CMU/kpidefinitions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiDefinitionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKpiDefinitions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpidefinitions/excel")]
        [HttpGet("/export/CMU/kpidefinitions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiDefinitionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKpiDefinitions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpihistoryitems/csv")]
        [HttpGet("/export/CMU/kpihistoryitems/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiHistoryItemsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKpiHistoryItems(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpihistoryitems/excel")]
        [HttpGet("/export/CMU/kpihistoryitems/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiHistoryItemsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKpiHistoryItems(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiinstances/csv")]
        [HttpGet("/export/CMU/kpiinstances/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiInstancesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKpiInstances(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiinstances/excel")]
        [HttpGet("/export/CMU/kpiinstances/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiInstancesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKpiInstances(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiscorecards/csv")]
        [HttpGet("/export/CMU/kpiscorecards/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiScorecardsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKpiScorecards(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiscorecards/excel")]
        [HttpGet("/export/CMU/kpiscorecards/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiScorecardsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKpiScorecards(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiscorecardscorecardskpiinstanceindicators/csv")]
        [HttpGet("/export/CMU/kpiscorecardscorecardskpiinstanceindicators/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiscorecardscorecardsKpiinstanceindicatorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetKpiscorecardscorecardsKpiinstanceindicators(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/kpiscorecardscorecardskpiinstanceindicators/excel")]
        [HttpGet("/export/CMU/kpiscorecardscorecardskpiinstanceindicators/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportKpiscorecardscorecardsKpiinstanceindicatorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetKpiscorecardscorecardsKpiinstanceindicators(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/lugarretirocarnes/csv")]
        [HttpGet("/export/CMU/lugarretirocarnes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLugarRetiroCarnesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLugarRetiroCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/lugarretirocarnes/excel")]
        [HttpGet("/export/CMU/lugarretirocarnes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLugarRetiroCarnesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLugarRetiroCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajepushes/csv")]
        [HttpGet("/export/CMU/mensajepushes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajePushesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMensajePushes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajepushes/excel")]
        [HttpGet("/export/CMU/mensajepushes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajePushesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMensajePushes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajepushadds/csv")]
        [HttpGet("/export/CMU/mensajepushadds/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajePushAddsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMensajePushAdds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajepushadds/excel")]
        [HttpGet("/export/CMU/mensajepushadds/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajePushAddsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMensajePushAdds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajesegmentos/csv")]
        [HttpGet("/export/CMU/mensajesegmentos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajeSegmentosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMensajeSegmentos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/mensajesegmentos/excel")]
        [HttpGet("/export/CMU/mensajesegmentos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMensajeSegmentosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMensajeSegmentos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/moduleinfos/csv")]
        [HttpGet("/export/CMU/moduleinfos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportModuleInfosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetModuleInfos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/moduleinfos/excel")]
        [HttpGet("/export/CMU/moduleinfos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportModuleInfosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetModuleInfos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuenta/csv")]
        [HttpGet("/export/CMU/movimientocuenta/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMovimientoCuenta(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuenta/excel")]
        [HttpGet("/export/CMU/movimientocuenta/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMovimientoCuenta(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuentacuota/csv")]
        [HttpGet("/export/CMU/movimientocuentacuota/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaCuotaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMovimientoCuentaCuota(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuentacuota/excel")]
        [HttpGet("/export/CMU/movimientocuentacuota/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaCuotaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMovimientoCuentaCuota(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuentamanuals/csv")]
        [HttpGet("/export/CMU/movimientocuentamanuals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaManualsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMovimientoCuentaManuals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientocuentamanuals/excel")]
        [HttpGet("/export/CMU/movimientocuentamanuals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoCuentaManualsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMovimientoCuentaManuals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientotipos/csv")]
        [HttpGet("/export/CMU/movimientotipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoTiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMovimientoTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/movimientotipos/excel")]
        [HttpGet("/export/CMU/movimientotipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMovimientoTiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMovimientoTipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/myfiledata/csv")]
        [HttpGet("/export/CMU/myfiledata/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMyFileDataToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMyFileData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/myfiledata/excel")]
        [HttpGet("/export/CMU/myfiledata/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMyFileDataToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMyFileData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/origenmovimientos/csv")]
        [HttpGet("/export/CMU/origenmovimientos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOrigenMovimientosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOrigenMovimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/origenmovimientos/excel")]
        [HttpGet("/export/CMU/origenmovimientos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOrigenMovimientosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOrigenMovimientos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/pais/csv")]
        [HttpGet("/export/CMU/pais/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPais(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/pais/excel")]
        [HttpGet("/export/CMU/pais/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPais(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/parametros/csv")]
        [HttpGet("/export/CMU/parametros/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportParametrosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetParametros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/parametros/excel")]
        [HttpGet("/export/CMU/parametros/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportParametrosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetParametros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regions/csv")]
        [HttpGet("/export/CMU/regions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regions/excel")]
        [HttpGet("/export/CMU/regions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regionals/csv")]
        [HttpGet("/export/CMU/regionals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regionals/excel")]
        [HttpGet("/export/CMU/regionals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regionalregionalescuentabancariacuentabancaria/csv")]
        [HttpGet("/export/CMU/regionalregionalescuentabancariacuentabancaria/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionalregionalesCuentabancariacuentabancariaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegionalregionalesCuentabancariacuentabancaria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/regionalregionalescuentabancariacuentabancaria/excel")]
        [HttpGet("/export/CMU/regionalregionalescuentabancariacuentabancaria/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegionalregionalesCuentabancariacuentabancariaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegionalregionalesCuentabancariacuentabancaria(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiados/csv")]
        [HttpGet("/export/CMU/registrocolegiados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegistroColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiados/excel")]
        [HttpGet("/export/CMU/registrocolegiados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegistroColegiados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiadonotificacions/csv")]
        [HttpGet("/export/CMU/registrocolegiadonotificacions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadoNotificacionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegistroColegiadoNotificacions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiadonotificacions/excel")]
        [HttpGet("/export/CMU/registrocolegiadonotificacions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadoNotificacionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegistroColegiadoNotificacions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiadorechazoparams/csv")]
        [HttpGet("/export/CMU/registrocolegiadorechazoparams/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadoRechazoParamsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRegistroColegiadoRechazoParams(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/registrocolegiadorechazoparams/excel")]
        [HttpGet("/export/CMU/registrocolegiadorechazoparams/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRegistroColegiadoRechazoParamsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRegistroColegiadoRechazoParams(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/reportdata/csv")]
        [HttpGet("/export/CMU/reportdata/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReportDataToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetReportData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/reportdata/excel")]
        [HttpGet("/export/CMU/reportdata/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReportDataToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetReportData(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/reportdatav2s/csv")]
        [HttpGet("/export/CMU/reportdatav2s/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReportDataV2SToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetReportDataV2S(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/reportdatav2s/excel")]
        [HttpGet("/export/CMU/reportdatav2s/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportReportDataV2SToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetReportDataV2S(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/rols/csv")]
        [HttpGet("/export/CMU/rols/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRols(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/rols/excel")]
        [HttpGet("/export/CMU/rols/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRols(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/rolrolesmovimientotipomovimientostipos/csv")]
        [HttpGet("/export/CMU/rolrolesmovimientotipomovimientostipos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolrolesMovimientotipomovimientostiposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRolrolesMovimientotipomovimientostipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/rolrolesmovimientotipomovimientostipos/excel")]
        [HttpGet("/export/CMU/rolrolesmovimientotipomovimientostipos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolrolesMovimientotipomovimientostiposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRolrolesMovimientotipomovimientostipos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salacmus/csv")]
        [HttpGet("/export/CMU/salacmus/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaCmusToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalaCmus(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salacmus/excel")]
        [HttpGet("/export/CMU/salacmus/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaCmusToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalaCmus(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salaorganizadors/csv")]
        [HttpGet("/export/CMU/salaorganizadors/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaOrganizadorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalaOrganizadors(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salaorganizadors/excel")]
        [HttpGet("/export/CMU/salaorganizadors/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaOrganizadorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalaOrganizadors(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salareservas/csv")]
        [HttpGet("/export/CMU/salareservas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaReservasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalaReservas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salareservas/excel")]
        [HttpGet("/export/CMU/salareservas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaReservasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalaReservas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salareservaregistros/csv")]
        [HttpGet("/export/CMU/salareservaregistros/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaReservaRegistrosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSalaReservaRegistros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/salareservaregistros/excel")]
        [HttpGet("/export/CMU/salareservaregistros/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSalaReservaRegistrosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSalaReservaRegistros(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemmemberpermissionsobjects/csv")]
        [HttpGet("/export/CMU/securitysystemmemberpermissionsobjects/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemMemberPermissionsObjectsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritySystemMemberPermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemmemberpermissionsobjects/excel")]
        [HttpGet("/export/CMU/securitysystemmemberpermissionsobjects/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemMemberPermissionsObjectsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritySystemMemberPermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemobjectpermissionsobjects/csv")]
        [HttpGet("/export/CMU/securitysystemobjectpermissionsobjects/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemObjectPermissionsObjectsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritySystemObjectPermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemobjectpermissionsobjects/excel")]
        [HttpGet("/export/CMU/securitysystemobjectpermissionsobjects/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemObjectPermissionsObjectsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritySystemObjectPermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemroles/csv")]
        [HttpGet("/export/CMU/securitysystemroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritySystemRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemroles/excel")]
        [HttpGet("/export/CMU/securitysystemroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritySystemRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemroleparentrolessecuritysystemrolechildroles/csv")]
        [HttpGet("/export/CMU/securitysystemroleparentrolessecuritysystemrolechildroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemroleparentrolessecuritysystemrolechildroles/excel")]
        [HttpGet("/export/CMU/securitysystemroleparentrolessecuritysystemrolechildroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemtypepermissionsobjects/csv")]
        [HttpGet("/export/CMU/securitysystemtypepermissionsobjects/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemTypePermissionsObjectsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritySystemTypePermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemtypepermissionsobjects/excel")]
        [HttpGet("/export/CMU/securitysystemtypepermissionsobjects/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemTypePermissionsObjectsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritySystemTypePermissionsObjects(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemusers/csv")]
        [HttpGet("/export/CMU/securitysystemusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritySystemUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemusers/excel")]
        [HttpGet("/export/CMU/securitysystemusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritySystemUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritySystemUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemuseruserssecuritysystemroleroles/csv")]
        [HttpGet("/export/CMU/securitysystemuseruserssecuritysystemroleroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritysystemuserusersSecuritysystemrolerolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSecuritysystemuserusersSecuritysystemroleroles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/securitysystemuseruserssecuritysystemroleroles/excel")]
        [HttpGet("/export/CMU/securitysystemuseruserssecuritysystemroleroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSecuritysystemuserusersSecuritysystemrolerolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSecuritysystemuserusersSecuritysystemroleroles(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/solicitudbajas/csv")]
        [HttpGet("/export/CMU/solicitudbajas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSolicitudBajasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSolicitudBajas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/solicitudbajas/excel")]
        [HttpGet("/export/CMU/solicitudbajas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSolicitudBajasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSolicitudBajas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/solicitudbajafileattachments/csv")]
        [HttpGet("/export/CMU/solicitudbajafileattachments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSolicitudBajaFileAttachmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSolicitudBajaFileAttachments(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/solicitudbajafileattachments/excel")]
        [HttpGet("/export/CMU/solicitudbajafileattachments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSolicitudBajaFileAttachmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSolicitudBajaFileAttachments(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpcarneentregados/csv")]
        [HttpGet("/export/CMU/tmpcarneentregados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpCarneEntregadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTmpCarneEntregados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpcarneentregados/excel")]
        [HttpGet("/export/CMU/tmpcarneentregados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpCarneEntregadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTmpCarneEntregados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpcarneretirars/csv")]
        [HttpGet("/export/CMU/tmpcarneretirars/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpCarneRetirarsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTmpCarneRetirars(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpcarneretirars/excel")]
        [HttpGet("/export/CMU/tmpcarneretirars/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpCarneRetirarsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTmpCarneRetirars(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpfechas/csv")]
        [HttpGet("/export/CMU/tmpfechas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpFechasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTmpFechas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpfechas/excel")]
        [HttpGet("/export/CMU/tmpfechas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpFechasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTmpFechas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpmeses/csv")]
        [HttpGet("/export/CMU/tmpmeses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpMesesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTmpMeses(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tmpmeses/excel")]
        [HttpGet("/export/CMU/tmpmeses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmpMesesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTmpMeses(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntabases/csv")]
        [HttpGet("/export/CMU/tramiteinfoadjuntabases/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntabasesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteInfoadjuntabases(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntabases/excel")]
        [HttpGet("/export/CMU/tramiteinfoadjuntabases/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntabasesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteInfoadjuntabases(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntacedulas/csv")]
        [HttpGet("/export/CMU/tramiteinfoadjuntacedulas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntacedulasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteInfoadjuntacedulas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntacedulas/excel")]
        [HttpGet("/export/CMU/tramiteinfoadjuntacedulas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntacedulasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteInfoadjuntacedulas(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntaespecialidads/csv")]
        [HttpGet("/export/CMU/tramiteinfoadjuntaespecialidads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntaespecialidadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteInfoadjuntaespecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntaespecialidads/excel")]
        [HttpGet("/export/CMU/tramiteinfoadjuntaespecialidads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntaespecialidadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteInfoadjuntaespecialidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntafotocarnes/csv")]
        [HttpGet("/export/CMU/tramiteinfoadjuntafotocarnes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntafotocarnesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteInfoadjuntafotocarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntafotocarnes/excel")]
        [HttpGet("/export/CMU/tramiteinfoadjuntafotocarnes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntafotocarnesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteInfoadjuntafotocarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntatitulos/csv")]
        [HttpGet("/export/CMU/tramiteinfoadjuntatitulos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntatitulosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteInfoadjuntatitulos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramiteinfoadjuntatitulos/excel")]
        [HttpGet("/export/CMU/tramiteinfoadjuntatitulos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteInfoadjuntatitulosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteInfoadjuntatitulos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarnes/csv")]
        [HttpGet("/export/CMU/tramitecarnes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarnesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarnes/excel")]
        [HttpGet("/export/CMU/tramitecarnes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarnesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteCarnes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestados/csv")]
        [HttpGet("/export/CMU/tramitecarneestados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteCarneEstados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestados/excel")]
        [HttpGet("/export/CMU/tramitecarneestados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteCarneEstados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestadocodigos/csv")]
        [HttpGet("/export/CMU/tramitecarneestadocodigos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadoCodigosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteCarneEstadoCodigos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestadocodigos/excel")]
        [HttpGet("/export/CMU/tramitecarneestadocodigos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadoCodigosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteCarneEstadoCodigos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestadoworkflows/csv")]
        [HttpGet("/export/CMU/tramitecarneestadoworkflows/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadoWorkFlowsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTramiteCarneEstadoWorkFlows(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/tramitecarneestadoworkflows/excel")]
        [HttpGet("/export/CMU/tramitecarneestadoworkflows/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTramiteCarneEstadoWorkFlowsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTramiteCarneEstadoWorkFlows(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/universidads/csv")]
        [HttpGet("/export/CMU/universidads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUniversidadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUniversidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/universidads/excel")]
        [HttpGet("/export/CMU/universidads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUniversidadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUniversidads(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/universidadtitulogrados/csv")]
        [HttpGet("/export/CMU/universidadtitulogrados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUniversidadTituloGradosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUniversidadTituloGrados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/universidadtitulogrados/excel")]
        [HttpGet("/export/CMU/universidadtitulogrados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUniversidadTituloGradosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUniversidadTituloGrados(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/userresetpasswordrequests/csv")]
        [HttpGet("/export/CMU/userresetpasswordrequests/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUserResetPasswordRequestsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUserResetPasswordRequests(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/userresetpasswordrequests/excel")]
        [HttpGet("/export/CMU/userresetpasswordrequests/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUserResetPasswordRequestsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUserResetPasswordRequests(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarios/csv")]
        [HttpGet("/export/CMU/usuarios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuariosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsuarios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarios/excel")]
        [HttpGet("/export/CMU/usuarios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuariosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsuarios(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioaccesos/csv")]
        [HttpGet("/export/CMU/usuarioaccesos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioAccesosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsuarioAccesos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioaccesos/excel")]
        [HttpGet("/export/CMU/usuarioaccesos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioAccesosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsuarioAccesos(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioinstitucions/csv")]
        [HttpGet("/export/CMU/usuarioinstitucions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioInstitucionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsuarioInstitucions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioinstitucions/excel")]
        [HttpGet("/export/CMU/usuarioinstitucions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioInstitucionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsuarioInstitucions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioregionals/csv")]
        [HttpGet("/export/CMU/usuarioregionals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioRegionalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsuarioRegionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/usuarioregionals/excel")]
        [HttpGet("/export/CMU/usuarioregionals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuarioRegionalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsuarioRegionals(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpobjectmodifieds/csv")]
        [HttpGet("/export/CMU/xpobjectmodifieds/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpObjectModifiedsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpObjectModifieds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpobjectmodifieds/excel")]
        [HttpGet("/export/CMU/xpobjectmodifieds/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpObjectModifiedsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpObjectModifieds(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpobjecttypes/csv")]
        [HttpGet("/export/CMU/xpobjecttypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpObjectTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpObjectTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpobjecttypes/excel")]
        [HttpGet("/export/CMU/xpobjecttypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpObjectTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpObjectTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostates/csv")]
        [HttpGet("/export/CMU/xpostates/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStatesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpoStates(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostates/excel")]
        [HttpGet("/export/CMU/xpostates/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStatesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpoStates(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostateappearances/csv")]
        [HttpGet("/export/CMU/xpostateappearances/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStateAppearancesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpoStateAppearances(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostateappearances/excel")]
        [HttpGet("/export/CMU/xpostateappearances/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStateAppearancesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpoStateAppearances(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostatemachines/csv")]
        [HttpGet("/export/CMU/xpostatemachines/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStateMachinesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpoStateMachines(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpostatemachines/excel")]
        [HttpGet("/export/CMU/xpostatemachines/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoStateMachinesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpoStateMachines(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpotransitions/csv")]
        [HttpGet("/export/CMU/xpotransitions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoTransitionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpoTransitions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpotransitions/excel")]
        [HttpGet("/export/CMU/xpotransitions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpoTransitionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpoTransitions(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpweakreferences/csv")]
        [HttpGet("/export/CMU/xpweakreferences/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpWeakReferencesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetXpWeakReferences(), Request.Query), fileName);
        }

        [HttpGet("/export/CMU/xpweakreferences/excel")]
        [HttpGet("/export/CMU/xpweakreferences/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportXpWeakReferencesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetXpWeakReferences(), Request.Query), fileName);
        }
    }
}
