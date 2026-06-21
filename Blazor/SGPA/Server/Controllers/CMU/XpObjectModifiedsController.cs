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
    [Route("odata/CMU/XpObjectModifieds")]
    public partial class XpObjectModifiedsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpObjectModifiedsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpObjectModified> GetXpObjectModifieds()
        {
            var items = this.context.XpObjectModifieds.AsQueryable<SGPA.Server.Models.CMU.XpObjectModified>();
            this.OnXpObjectModifiedsRead(ref items);

            return items;
        }

        partial void OnXpObjectModifiedsRead(ref IQueryable<SGPA.Server.Models.CMU.XpObjectModified> items);

        partial void OnXpObjectModifiedGet(ref SingleResult<SGPA.Server.Models.CMU.XpObjectModified> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpObjectModifieds(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.XpObjectModified> GetXpObjectModified(int key)
        {
            var items = this.context.XpObjectModifieds.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnXpObjectModifiedGet(ref result);

            return result;
        }
        partial void OnXpObjectModifiedDeleted(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedDeleted(SGPA.Server.Models.CMU.XpObjectModified item);

        [HttpDelete("/odata/CMU/XpObjectModifieds(Id={Id})")]
        public IActionResult DeleteXpObjectModified(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpObjectModifieds
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectModified>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpObjectModifiedDeleted(item);
                this.context.XpObjectModifieds.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpObjectModifiedDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpObjectModifiedUpdated(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedUpdated(SGPA.Server.Models.CMU.XpObjectModified item);

        [HttpPut("/odata/CMU/XpObjectModifieds(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpObjectModified(int key, [FromBody]SGPA.Server.Models.CMU.XpObjectModified item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpObjectModifieds
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectModified>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpObjectModifiedUpdated(item);
                this.context.XpObjectModifieds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectModifieds.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                this.OnAfterXpObjectModifiedUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpObjectModifieds(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpObjectModified(int key, [FromBody]Delta<SGPA.Server.Models.CMU.XpObjectModified> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpObjectModifieds
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectModified>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpObjectModifiedUpdated(item);
                this.context.XpObjectModifieds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectModifieds.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpObjectModifiedCreated(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedCreated(SGPA.Server.Models.CMU.XpObjectModified item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpObjectModified item)
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

                this.OnXpObjectModifiedCreated(item);
                this.context.XpObjectModifieds.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectModifieds.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");

                this.OnAfterXpObjectModifiedCreated(item);

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
