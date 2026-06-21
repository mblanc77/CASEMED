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
    [Route("odata/CMU/TramiteCarneEstados")]
    public partial class TramiteCarneEstadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteCarneEstadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstado> GetTramiteCarneEstados()
        {
            var items = this.context.TramiteCarneEstados.AsQueryable<SGPA.Server.Models.CMU.TramiteCarneEstado>();
            this.OnTramiteCarneEstadosRead(ref items);

            return items;
        }

        partial void OnTramiteCarneEstadosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstado> items);

        partial void OnTramiteCarneEstadoGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteCarneEstados(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstado> GetTramiteCarneEstado(int key)
        {
            var items = this.context.TramiteCarneEstados.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteCarneEstadoGet(ref result);

            return result;
        }
        partial void OnTramiteCarneEstadoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        [HttpDelete("/odata/CMU/TramiteCarneEstados(OID={OID})")]
        public IActionResult DeleteTramiteCarneEstado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteCarneEstados
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoDeleted(item);
                this.context.TramiteCarneEstados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteCarneEstadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        [HttpPut("/odata/CMU/TramiteCarneEstados(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteCarneEstado(int key, [FromBody]SGPA.Server.Models.CMU.TramiteCarneEstado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstados
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoUpdated(item);
                this.context.TramiteCarneEstados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstados.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarne");
                this.OnAfterTramiteCarneEstadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteCarneEstados(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteCarneEstado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteCarneEstado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstados
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteCarneEstadoUpdated(item);
                this.context.TramiteCarneEstados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstados.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarne");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoCreated(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoCreated(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteCarneEstado item)
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

                this.OnTramiteCarneEstadoCreated(item);
                this.context.TramiteCarneEstados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstados.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarne");

                this.OnAfterTramiteCarneEstadoCreated(item);

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
