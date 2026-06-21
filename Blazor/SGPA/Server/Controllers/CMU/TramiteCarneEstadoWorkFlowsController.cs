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
    [Route("odata/CMU/TramiteCarneEstadoWorkFlows")]
    public partial class TramiteCarneEstadoWorkFlowsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteCarneEstadoWorkFlowsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> GetTramiteCarneEstadoWorkFlows()
        {
            var items = this.context.TramiteCarneEstadoWorkFlows.AsQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>();
            this.OnTramiteCarneEstadoWorkFlowsRead(ref items);

            return items;
        }

        partial void OnTramiteCarneEstadoWorkFlowsRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> items);

        partial void OnTramiteCarneEstadoWorkFlowGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteCarneEstadoWorkFlows(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> GetTramiteCarneEstadoWorkFlow(int key)
        {
            var items = this.context.TramiteCarneEstadoWorkFlows.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteCarneEstadoWorkFlowGet(ref result);

            return result;
        }
        partial void OnTramiteCarneEstadoWorkFlowDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        [HttpDelete("/odata/CMU/TramiteCarneEstadoWorkFlows(OID={OID})")]
        public IActionResult DeleteTramiteCarneEstadoWorkFlow(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteCarneEstadoWorkFlows
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoWorkFlowDeleted(item);
                this.context.TramiteCarneEstadoWorkFlows.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteCarneEstadoWorkFlowDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoWorkFlowUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        [HttpPut("/odata/CMU/TramiteCarneEstadoWorkFlows(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteCarneEstadoWorkFlow(int key, [FromBody]SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstadoWorkFlows
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneEstadoWorkFlowUpdated(item);
                this.context.TramiteCarneEstadoWorkFlows.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoWorkFlows.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarneEstadoCodigo1");
                this.OnAfterTramiteCarneEstadoWorkFlowUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteCarneEstadoWorkFlows(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteCarneEstadoWorkFlow(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarneEstadoWorkFlows
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteCarneEstadoWorkFlowUpdated(item);
                this.context.TramiteCarneEstadoWorkFlows.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoWorkFlows.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarneEstadoCodigo1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneEstadoWorkFlowCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item)
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

                this.OnTramiteCarneEstadoWorkFlowCreated(item);
                this.context.TramiteCarneEstadoWorkFlows.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarneEstadoWorkFlows.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "TramiteCarneEstadoCodigo,TramiteCarneEstadoCodigo1");

                this.OnAfterTramiteCarneEstadoWorkFlowCreated(item);

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
