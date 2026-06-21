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
    [Route("odata/CMU/AuditedObjectWeakReferences")]
    public partial class AuditedObjectWeakReferencesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AuditedObjectWeakReferencesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AuditedObjectWeakReference> GetAuditedObjectWeakReferences()
        {
            var items = this.context.AuditedObjectWeakReferences.AsQueryable<SGPA.Server.Models.CMU.AuditedObjectWeakReference>();
            this.OnAuditedObjectWeakReferencesRead(ref items);

            return items;
        }

        partial void OnAuditedObjectWeakReferencesRead(ref IQueryable<SGPA.Server.Models.CMU.AuditedObjectWeakReference> items);

        partial void OnAuditedObjectWeakReferenceGet(ref SingleResult<SGPA.Server.Models.CMU.AuditedObjectWeakReference> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AuditedObjectWeakReferences(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.AuditedObjectWeakReference> GetAuditedObjectWeakReference(Guid key)
        {
            var items = this.context.AuditedObjectWeakReferences.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnAuditedObjectWeakReferenceGet(ref result);

            return result;
        }
        partial void OnAuditedObjectWeakReferenceDeleted(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceDeleted(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        [HttpDelete("/odata/CMU/AuditedObjectWeakReferences(Oid={Oid})")]
        public IActionResult DeleteAuditedObjectWeakReference(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AuditedObjectWeakReferences
                    .Where(i => i.Oid == key)
                    .Include(i => i.AuditDataItemPersistents)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditedObjectWeakReference>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAuditedObjectWeakReferenceDeleted(item);
                this.context.AuditedObjectWeakReferences.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAuditedObjectWeakReferenceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAuditedObjectWeakReferenceUpdated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceUpdated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        [HttpPut("/odata/CMU/AuditedObjectWeakReferences(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAuditedObjectWeakReference(Guid key, [FromBody]SGPA.Server.Models.CMU.AuditedObjectWeakReference item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AuditedObjectWeakReferences
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditedObjectWeakReference>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAuditedObjectWeakReferenceUpdated(item);
                this.context.AuditedObjectWeakReferences.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditedObjectWeakReferences.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpweakReference");
                this.OnAfterAuditedObjectWeakReferenceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AuditedObjectWeakReferences(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAuditedObjectWeakReference(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.AuditedObjectWeakReference> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AuditedObjectWeakReferences
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditedObjectWeakReference>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAuditedObjectWeakReferenceUpdated(item);
                this.context.AuditedObjectWeakReferences.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditedObjectWeakReferences.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpweakReference");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAuditedObjectWeakReferenceCreated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceCreated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AuditedObjectWeakReference item)
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

                this.OnAuditedObjectWeakReferenceCreated(item);
                this.context.AuditedObjectWeakReferences.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditedObjectWeakReferences.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpweakReference");

                this.OnAfterAuditedObjectWeakReferenceCreated(item);

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
