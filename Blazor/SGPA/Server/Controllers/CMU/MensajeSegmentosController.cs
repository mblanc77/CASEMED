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
    [Route("odata/CMU/MensajeSegmentos")]
    public partial class MensajeSegmentosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MensajeSegmentosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MensajeSegmento> GetMensajeSegmentos()
        {
            var items = this.context.MensajeSegmentos.AsQueryable<SGPA.Server.Models.CMU.MensajeSegmento>();
            this.OnMensajeSegmentosRead(ref items);

            return items;
        }

        partial void OnMensajeSegmentosRead(ref IQueryable<SGPA.Server.Models.CMU.MensajeSegmento> items);

        partial void OnMensajeSegmentoGet(ref SingleResult<SGPA.Server.Models.CMU.MensajeSegmento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MensajeSegmentos(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.MensajeSegmento> GetMensajeSegmento(int key)
        {
            var items = this.context.MensajeSegmentos.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnMensajeSegmentoGet(ref result);

            return result;
        }
        partial void OnMensajeSegmentoDeleted(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoDeleted(SGPA.Server.Models.CMU.MensajeSegmento item);

        [HttpDelete("/odata/CMU/MensajeSegmentos(OID={OID})")]
        public IActionResult DeleteMensajeSegmento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MensajeSegmentos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajeSegmento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajeSegmentoDeleted(item);
                this.context.MensajeSegmentos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMensajeSegmentoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajeSegmentoUpdated(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoUpdated(SGPA.Server.Models.CMU.MensajeSegmento item);

        [HttpPut("/odata/CMU/MensajeSegmentos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMensajeSegmento(int key, [FromBody]SGPA.Server.Models.CMU.MensajeSegmento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajeSegmentos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajeSegmento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajeSegmentoUpdated(item);
                this.context.MensajeSegmentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajeSegmentos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");
                this.OnAfterMensajeSegmentoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MensajeSegmentos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMensajeSegmento(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MensajeSegmento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajeSegmentos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajeSegmento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMensajeSegmentoUpdated(item);
                this.context.MensajeSegmentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajeSegmentos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajeSegmentoCreated(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoCreated(SGPA.Server.Models.CMU.MensajeSegmento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MensajeSegmento item)
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

                this.OnMensajeSegmentoCreated(item);
                this.context.MensajeSegmentos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajeSegmentos.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");

                this.OnAfterMensajeSegmentoCreated(item);

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
