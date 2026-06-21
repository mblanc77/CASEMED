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
    [Route("odata/CMU/KpiHistoryItems")]
    public partial class KpiHistoryItemsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public KpiHistoryItemsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.KpiHistoryItem> GetKpiHistoryItems()
        {
            var items = this.context.KpiHistoryItems.AsQueryable<SGPA.Server.Models.CMU.KpiHistoryItem>();
            this.OnKpiHistoryItemsRead(ref items);

            return items;
        }

        partial void OnKpiHistoryItemsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiHistoryItem> items);

        partial void OnKpiHistoryItemGet(ref SingleResult<SGPA.Server.Models.CMU.KpiHistoryItem> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/KpiHistoryItems(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.KpiHistoryItem> GetKpiHistoryItem(Guid key)
        {
            var items = this.context.KpiHistoryItems.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnKpiHistoryItemGet(ref result);

            return result;
        }
        partial void OnKpiHistoryItemDeleted(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemDeleted(SGPA.Server.Models.CMU.KpiHistoryItem item);

        [HttpDelete("/odata/CMU/KpiHistoryItems(Oid={Oid})")]
        public IActionResult DeleteKpiHistoryItem(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.KpiHistoryItems
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiHistoryItem>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiHistoryItemDeleted(item);
                this.context.KpiHistoryItems.Remove(item);
                this.context.SaveChanges();
                this.OnAfterKpiHistoryItemDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiHistoryItemUpdated(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemUpdated(SGPA.Server.Models.CMU.KpiHistoryItem item);

        [HttpPut("/odata/CMU/KpiHistoryItems(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutKpiHistoryItem(Guid key, [FromBody]SGPA.Server.Models.CMU.KpiHistoryItem item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiHistoryItems
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiHistoryItem>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiHistoryItemUpdated(item);
                this.context.KpiHistoryItems.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiHistoryItems.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");
                this.OnAfterKpiHistoryItemUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/KpiHistoryItems(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchKpiHistoryItem(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.KpiHistoryItem> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiHistoryItems
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiHistoryItem>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnKpiHistoryItemUpdated(item);
                this.context.KpiHistoryItems.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiHistoryItems.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiHistoryItemCreated(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemCreated(SGPA.Server.Models.CMU.KpiHistoryItem item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.KpiHistoryItem item)
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

                this.OnKpiHistoryItemCreated(item);
                this.context.KpiHistoryItems.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiHistoryItems.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance1");

                this.OnAfterKpiHistoryItemCreated(item);

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
