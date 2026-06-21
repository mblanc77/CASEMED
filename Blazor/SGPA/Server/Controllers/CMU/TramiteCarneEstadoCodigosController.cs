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
    [Route("odata/CMU/TramiteCarneEstadoCodigos")]
    public partial class TramiteCarneEstadoCodigosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteCarneEstadoCodigosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> GetTramiteCarneEstadoCodigos()
        {
            var items = this.context.TramiteCarneEstadoCodigos.AsQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>();
            this.OnTramiteCarneEstadoCodigosRead(ref items);

            return items;
        }

        partial void OnTramiteCarneEstadoCodigosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> items);

        partial void OnTramiteCarneEstadoCodigoGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteCarneEstadoCodigos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> GetTramiteCarneEstadoCodigo(int key)
        {
            var items = this.context.TramiteCarneEstadoCodigos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnTramiteCarneEstadoCodigoGet(ref result);

            return result;
        }
        partial void OnTramiteCarneEstadoCodigoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        [HttpDelete("/odata/CMU/TramiteCarneEstadoCodigos(Id={Id})")]
        public IActionResult DeleteTramiteCarneEstadoCodigo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteCarneEstadoCodigos
                    .Where(i => i.Id == key)
                    .Include(i => i.TramiteCarneEstados)
                    .Include(i => i.TramiteCarneEstadoWorkFlows)
                    .Include(i => i.TramiteCarneEstadoWorkFlows1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoCodigoDeleted(item);
                this.context.TramiteCarneEstadoCodigos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteCarneEstadoCodigoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoCodigoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        [HttpPut("/odata/CMU/TramiteCarneEstadoCodigos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteCarneEstadoCodigo(int key, [FromBody]SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstadoCodigos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoCodigoUpdated(item);
                this.context.TramiteCarneEstadoCodigos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoCodigos.Where(i => i.Id == key);
                ;
                this.OnAfterTramiteCarneEstadoCodigoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteCarneEstadoCodigos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteCarneEstadoCodigo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstadoCodigos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteCarneEstadoCodigoUpdated(item);
                this.context.TramiteCarneEstadoCodigos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoCodigos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoCodigoCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item)
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

                this.OnTramiteCarneEstadoCodigoCreated(item);
                this.context.TramiteCarneEstadoCodigos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoCodigos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterTramiteCarneEstadoCodigoCreated(item);

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
