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
    [Route("odata/CMU/KpiInstances")]
    public partial class KpiInstancesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public KpiInstancesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.KpiInstance> GetKpiInstances()
        {
            var items = this.context.KpiInstances.AsQueryable<SGPA.Server.Models.CMU.KpiInstance>();
            this.OnKpiInstancesRead(ref items);

            return items;
        }

        partial void OnKpiInstancesRead(ref IQueryable<SGPA.Server.Models.CMU.KpiInstance> items);

        partial void OnKpiInstanceGet(ref SingleResult<SGPA.Server.Models.CMU.KpiInstance> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/KpiInstances(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.KpiInstance> GetKpiInstance(Guid key)
        {
            var items = this.context.KpiInstances.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnKpiInstanceGet(ref result);

            return result;
        }
        partial void OnKpiInstanceDeleted(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceDeleted(SGPA.Server.Models.CMU.KpiInstance item);

        [HttpDelete("/odata/CMU/KpiInstances(Oid={Oid})")]
        public IActionResult DeleteKpiInstance(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.KpiInstances
                    .Where(i => i.Oid == key)
                    .Include(i => i.KpiDefinitions)
                    .Include(i => i.KpiHistoryItems)
                    .Include(i => i.KpiscorecardscorecardsKpiinstanceindicators)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiInstance>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiInstanceDeleted(item);
                this.context.KpiInstances.Remove(item);
                this.context.SaveChanges();
                this.OnAfterKpiInstanceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiInstanceUpdated(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceUpdated(SGPA.Server.Models.CMU.KpiInstance item);

        [HttpPut("/odata/CMU/KpiInstances(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutKpiInstance(Guid key, [FromBody]SGPA.Server.Models.CMU.KpiInstance item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiInstances
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiInstance>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiInstanceUpdated(item);
                this.context.KpiInstances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiInstances.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiDefinition1");
                this.OnAfterKpiInstanceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/KpiInstances(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchKpiInstance(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.KpiInstance> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiInstances
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiInstance>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnKpiInstanceUpdated(item);
                this.context.KpiInstances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiInstances.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiDefinition1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiInstanceCreated(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceCreated(SGPA.Server.Models.CMU.KpiInstance item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.KpiInstance item)
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

                this.OnKpiInstanceCreated(item);
                this.context.KpiInstances.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiInstances.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "KpiDefinition1");

                this.OnAfterKpiInstanceCreated(item);

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
