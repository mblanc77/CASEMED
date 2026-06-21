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
    [Route("odata/CMU/XpWeakReferences")]
    public partial class XpWeakReferencesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpWeakReferencesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpWeakReference> GetXpWeakReferences()
        {
            var items = this.context.XpWeakReferences.AsQueryable<SGPA.Server.Models.CMU.XpWeakReference>();
            this.OnXpWeakReferencesRead(ref items);

            return items;
        }

        partial void OnXpWeakReferencesRead(ref IQueryable<SGPA.Server.Models.CMU.XpWeakReference> items);

        partial void OnXpWeakReferenceGet(ref SingleResult<SGPA.Server.Models.CMU.XpWeakReference> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpWeakReferences(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.XpWeakReference> GetXpWeakReference(Guid key)
        {
            var items = this.context.XpWeakReferences.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnXpWeakReferenceGet(ref result);

            return result;
        }
        partial void OnXpWeakReferenceDeleted(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceDeleted(SGPA.Server.Models.CMU.XpWeakReference item);

        [HttpDelete("/odata/CMU/XpWeakReferences(Oid={Oid})")]
        public IActionResult DeleteXpWeakReference(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpWeakReferences
                    .Where(i => i.Oid == key)
                    .Include(i => i.AuditDataItemPersistents)
                    .Include(i => i.AuditDataItemPersistents1)
                    .Include(i => i.AuditedObjectWeakReferences)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpWeakReference>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpWeakReferenceDeleted(item);
                this.context.XpWeakReferences.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpWeakReferenceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpWeakReferenceUpdated(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceUpdated(SGPA.Server.Models.CMU.XpWeakReference item);

        [HttpPut("/odata/CMU/XpWeakReferences(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpWeakReference(Guid key, [FromBody]SGPA.Server.Models.CMU.XpWeakReference item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpWeakReferences
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpWeakReference>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpWeakReferenceUpdated(item);
                this.context.XpWeakReferences.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpWeakReferences.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,XpobjectType1");
                this.OnAfterXpWeakReferenceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpWeakReferences(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpWeakReference(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.XpWeakReference> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpWeakReferences
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpWeakReference>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpWeakReferenceUpdated(item);
                this.context.XpWeakReferences.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpWeakReferences.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,XpobjectType1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpWeakReferenceCreated(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceCreated(SGPA.Server.Models.CMU.XpWeakReference item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpWeakReference item)
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

                this.OnXpWeakReferenceCreated(item);
                this.context.XpWeakReferences.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpWeakReferences.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,XpobjectType1");

                this.OnAfterXpWeakReferenceCreated(item);

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
