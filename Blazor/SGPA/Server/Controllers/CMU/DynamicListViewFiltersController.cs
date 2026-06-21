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
    [Route("odata/CMU/DynamicListViewFilters")]
    public partial class DynamicListViewFiltersController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DynamicListViewFiltersController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DynamicListViewFilter> GetDynamicListViewFilters()
        {
            var items = this.context.DynamicListViewFilters.AsQueryable<SGPA.Server.Models.CMU.DynamicListViewFilter>();
            this.OnDynamicListViewFiltersRead(ref items);

            return items;
        }

        partial void OnDynamicListViewFiltersRead(ref IQueryable<SGPA.Server.Models.CMU.DynamicListViewFilter> items);

        partial void OnDynamicListViewFilterGet(ref SingleResult<SGPA.Server.Models.CMU.DynamicListViewFilter> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DynamicListViewFilters(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DynamicListViewFilter> GetDynamicListViewFilter(int key)
        {
            var items = this.context.DynamicListViewFilters.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDynamicListViewFilterGet(ref result);

            return result;
        }
        partial void OnDynamicListViewFilterDeleted(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterDeleted(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        [HttpDelete("/odata/CMU/DynamicListViewFilters(Id={Id})")]
        public IActionResult DeleteDynamicListViewFilter(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DynamicListViewFilters
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DynamicListViewFilter>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDynamicListViewFilterDeleted(item);
                this.context.DynamicListViewFilters.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDynamicListViewFilterDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDynamicListViewFilterUpdated(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterUpdated(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        [HttpPut("/odata/CMU/DynamicListViewFilters(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDynamicListViewFilter(int key, [FromBody]SGPA.Server.Models.CMU.DynamicListViewFilter item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DynamicListViewFilters
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DynamicListViewFilter>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDynamicListViewFilterUpdated(item);
                this.context.DynamicListViewFilters.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DynamicListViewFilters.Where(i => i.Id == key);
                ;
                this.OnAfterDynamicListViewFilterUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DynamicListViewFilters(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDynamicListViewFilter(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DynamicListViewFilter> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DynamicListViewFilters
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DynamicListViewFilter>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDynamicListViewFilterUpdated(item);
                this.context.DynamicListViewFilters.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DynamicListViewFilters.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDynamicListViewFilterCreated(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterCreated(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DynamicListViewFilter item)
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

                this.OnDynamicListViewFilterCreated(item);
                this.context.DynamicListViewFilters.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DynamicListViewFilters.Where(i => i.Id == item.Id);

                ;

                this.OnAfterDynamicListViewFilterCreated(item);

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
