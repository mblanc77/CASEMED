using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SGPA.Server.Controllers.CMU
{
    [Route("odata/CMU/Colegiados")]
    public partial class ColegiadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Colegiado> GetColegiados()
        {
            var items = this.context.Colegiados.AsQueryable<SGPA.Server.Models.CMU.Colegiado>();
            this.OnColegiadosRead(ref items);

            return items;
        }

        partial void OnColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.Colegiado> items);

        partial void OnColegiadoGet(ref SingleResult<SGPA.Server.Models.CMU.Colegiado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Colegiados(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.Colegiado> GetColegiado(int key)
        {
            var items = this.context.Colegiados.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnColegiadoGet(ref result);

            return result;
        }
        partial void OnColegiadoDeleted(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoDeleted(SGPA.Server.Models.CMU.Colegiado item);

        [HttpDelete("/odata/CMU/Colegiados(Documento={Documento})")]
        public IActionResult DeleteColegiado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Colegiados
                    .Where(i => i.Documento == key)
                    .Include(i => i.AjusteRetroactivos)
                    .Include(i => i.CobroNominas)
                    .Include(i => i.ColegiadoActualizacionDps)
                    .Include(i => i.ColegiadoBitacoras)
                    .Include(i => i.ColegiadoCambioCategoria)
                    .Include(i => i.ColegiadoCertificadoExpedidos)
                    .Include(i => i.ColegiadoDebitoBancarioAsociados)
                    .Include(i => i.ColegiadoDeclaracionJurada)
                    .Include(i => i.ColegiadoImagenes)
                    .Include(i => i.ColegiadoTarjetaDebitoAsociada)
                    .Include(i => i.Convenios)
                    .Include(i => i.MovimientoCuenta)
                    .Include(i => i.SolicitudBajas)
                    .Include(i => i.TramiteCarnes)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoDeleted(item);
                this.context.Colegiados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoUpdated(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoUpdated(SGPA.Server.Models.CMU.Colegiado item);

        [HttpPut("/odata/CMU/Colegiados(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiado(int key, [FromBody]SGPA.Server.Models.CMU.Colegiado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Colegiados
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoUpdated(item);
                this.context.Colegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados.Where(i => i.Documento == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,BajaMotivo1,CategoriaColegiado1,Departamento1,Pai,Regional,AgenteCobranza1,UniversidadTituloGrado1");
                this.OnAfterColegiadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Colegiados(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Colegiado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Colegiados
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoUpdated(item);
                this.context.Colegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados.Where(i => i.Documento == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,BajaMotivo1,CategoriaColegiado1,Departamento1,Pai,Regional,AgenteCobranza1,UniversidadTituloGrado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoCreated(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoCreated(SGPA.Server.Models.CMU.Colegiado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Colegiado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnColegiadoCreated(item);
                this.context.Colegiados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados.Where(i => i.Documento == item.Documento);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,BajaMotivo1,CategoriaColegiado1,Departamento1,Pai,Regional,AgenteCobranza1,UniversidadTituloGrado1");

                this.OnAfterColegiadoCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
