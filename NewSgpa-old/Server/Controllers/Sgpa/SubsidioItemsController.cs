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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/SubsidioItems")]
    public partial class SubsidioItemsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioItemsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioItem> GetSubsidioItems()
        {
            var items = this.context.SubsidioItems.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItem>();
            this.OnSubsidioItemsRead(ref items);

            return items;
        }

        partial void OnSubsidioItemsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItem> items);

        partial void OnSubsidioItemGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItem> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioItems(SubsidioItemId={SubsidioItemId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItem> GetSubsidioItem(int key)
        {
            var items = this.context.SubsidioItems.Where(i => i.SubsidioItemId == key);
            var result = SingleResult.Create(items);

            OnSubsidioItemGet(ref result);

            return result;
        }
        partial void OnSubsidioItemDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        [HttpDelete("/odata/Sgpa/SubsidioItems(SubsidioItemId={SubsidioItemId})")]
        public IActionResult DeleteSubsidioItem(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioItems
                    .Where(i => i.SubsidioItemId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItem>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemDeleted(item);
                this.context.SubsidioItems.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioItemDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        [HttpPut("/odata/Sgpa/SubsidioItems(SubsidioItemId={SubsidioItemId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioItem(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioItem item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItems
                    .Where(i => i.SubsidioItemId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItem>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemUpdated(item);
                this.context.SubsidioItems.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItems.Where(i => i.SubsidioItemId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioItemCod,SubsidioCabezal");
                this.OnAfterSubsidioItemUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioItems(SubsidioItemId={SubsidioItemId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioItem(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioItem> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItems
                    .Where(i => i.SubsidioItemId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItem>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioItemUpdated(item);
                this.context.SubsidioItems.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItems.Where(i => i.SubsidioItemId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioItemCod,SubsidioCabezal");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemCreated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemCreated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioItem item)
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

                this.OnSubsidioItemCreated(item);
                this.context.SubsidioItems.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItems.Where(i => i.SubsidioItemId == item.SubsidioItemId);

                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioItemCod,SubsidioCabezal");

                this.OnAfterSubsidioItemCreated(item);

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
