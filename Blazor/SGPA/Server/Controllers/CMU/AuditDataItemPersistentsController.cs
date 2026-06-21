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
    [Route("odata/CMU/AuditDataItemPersistents")]
    public partial class AuditDataItemPersistentsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AuditDataItemPersistentsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AuditDataItemPersistent> GetAuditDataItemPersistents()
        {
            var items = this.context.AuditDataItemPersistents.AsQueryable<SGPA.Server.Models.CMU.AuditDataItemPersistent>();
            this.OnAuditDataItemPersistentsRead(ref items);

            return items;
        }

        partial void OnAuditDataItemPersistentsRead(ref IQueryable<SGPA.Server.Models.CMU.AuditDataItemPersistent> items);

        partial void OnAuditDataItemPersistentGet(ref SingleResult<SGPA.Server.Models.CMU.AuditDataItemPersistent> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AuditDataItemPersistents(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.AuditDataItemPersistent> GetAuditDataItemPersistent(Guid key)
        {
            var items = this.context.AuditDataItemPersistents.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnAuditDataItemPersistentGet(ref result);

            return result;
        }
        partial void OnAuditDataItemPersistentDeleted(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentDeleted(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        [HttpDelete("/odata/CMU/AuditDataItemPersistents(Oid={Oid})")]
        public IActionResult DeleteAuditDataItemPersistent(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AuditDataItemPersistents
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditDataItemPersistent>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAuditDataItemPersistentDeleted(item);
                this.context.AuditDataItemPersistents.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAuditDataItemPersistentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAuditDataItemPersistentUpdated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentUpdated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        [HttpPut("/odata/CMU/AuditDataItemPersistents(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAuditDataItemPersistent(Guid key, [FromBody]SGPA.Server.Models.CMU.AuditDataItemPersistent item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AuditDataItemPersistents
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditDataItemPersistent>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAuditDataItemPersistentUpdated(item);
                this.context.AuditDataItemPersistents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditDataItemPersistents.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AuditedObjectWeakReference,XpweakReference,XpweakReference1");
                this.OnAfterAuditDataItemPersistentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AuditDataItemPersistents(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAuditDataItemPersistent(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.AuditDataItemPersistent> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AuditDataItemPersistents
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AuditDataItemPersistent>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAuditDataItemPersistentUpdated(item);
                this.context.AuditDataItemPersistents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditDataItemPersistents.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AuditedObjectWeakReference,XpweakReference,XpweakReference1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAuditDataItemPersistentCreated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentCreated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AuditDataItemPersistent item)
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

                this.OnAuditDataItemPersistentCreated(item);
                this.context.AuditDataItemPersistents.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AuditDataItemPersistents.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "AuditedObjectWeakReference,XpweakReference,XpweakReference1");

                this.OnAfterAuditDataItemPersistentCreated(item);

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
