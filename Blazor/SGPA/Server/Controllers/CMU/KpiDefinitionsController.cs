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
    [Route("odata/CMU/KpiDefinitions")]
    public partial class KpiDefinitionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public KpiDefinitionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.KpiDefinition> GetKpiDefinitions()
        {
            var items = this.context.KpiDefinitions.AsQueryable<SGPA.Server.Models.CMU.KpiDefinition>();
            this.OnKpiDefinitionsRead(ref items);

            return items;
        }

        partial void OnKpiDefinitionsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiDefinition> items);

        partial void OnKpiDefinitionGet(ref SingleResult<SGPA.Server.Models.CMU.KpiDefinition> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/KpiDefinitions(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.KpiDefinition> GetKpiDefinition(Guid key)
        {
            var items = this.context.KpiDefinitions.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnKpiDefinitionGet(ref result);

            return result;
        }
        partial void OnKpiDefinitionDeleted(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionDeleted(SGPA.Server.Models.CMU.KpiDefinition item);

        [HttpDelete("/odata/CMU/KpiDefinitions(Oid={Oid})")]
        public IActionResult DeleteKpiDefinition(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.KpiDefinitions
                    .Where(i => i.Oid == key)
                    .Include(i => i.KpiInstances)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiDefinition>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiDefinitionDeleted(item);
                this.context.KpiDefinitions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterKpiDefinitionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiDefinitionUpdated(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionUpdated(SGPA.Server.Models.CMU.KpiDefinition item);

        [HttpPut("/odata/CMU/KpiDefinitions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutKpiDefinition(Guid key, [FromBody]SGPA.Server.Models.CMU.KpiDefinition item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiDefinitions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiDefinition>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiDefinitionUpdated(item);
                this.context.KpiDefinitions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiDefinitions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");
                this.OnAfterKpiDefinitionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/KpiDefinitions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchKpiDefinition(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.KpiDefinition> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiDefinitions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiDefinition>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnKpiDefinitionUpdated(item);
                this.context.KpiDefinitions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiDefinitions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiDefinitionCreated(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionCreated(SGPA.Server.Models.CMU.KpiDefinition item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.KpiDefinition item)
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

                this.OnKpiDefinitionCreated(item);
                this.context.KpiDefinitions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiDefinitions.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");

                this.OnAfterKpiDefinitionCreated(item);

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
