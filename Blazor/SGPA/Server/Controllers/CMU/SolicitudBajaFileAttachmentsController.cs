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
    [Route("odata/CMU/SolicitudBajaFileAttachments")]
    public partial class SolicitudBajaFileAttachmentsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SolicitudBajaFileAttachmentsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> GetSolicitudBajaFileAttachments()
        {
            var items = this.context.SolicitudBajaFileAttachments.AsQueryable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>();
            this.OnSolicitudBajaFileAttachmentsRead(ref items);

            return items;
        }

        partial void OnSolicitudBajaFileAttachmentsRead(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> items);

        partial void OnSolicitudBajaFileAttachmentGet(ref SingleResult<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SolicitudBajaFileAttachments(Guid={Guid})")]
        public SingleResult<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> GetSolicitudBajaFileAttachment(Guid key)
        {
            var items = this.context.SolicitudBajaFileAttachments.Where(i => i.Guid == key);
            var result = SingleResult.Create(items);

            OnSolicitudBajaFileAttachmentGet(ref result);

            return result;
        }
        partial void OnSolicitudBajaFileAttachmentDeleted(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentDeleted(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        [HttpDelete("/odata/CMU/SolicitudBajaFileAttachments(Guid={Guid})")]
        public IActionResult DeleteSolicitudBajaFileAttachment(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SolicitudBajaFileAttachments
                    .Where(i => i.Guid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSolicitudBajaFileAttachmentDeleted(item);
                this.context.SolicitudBajaFileAttachments.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSolicitudBajaFileAttachmentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSolicitudBajaFileAttachmentUpdated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentUpdated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        [HttpPut("/odata/CMU/SolicitudBajaFileAttachments(Guid={Guid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSolicitudBajaFileAttachment(Guid key, [FromBody]SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SolicitudBajaFileAttachments
                    .Where(i => i.Guid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSolicitudBajaFileAttachmentUpdated(item);
                this.context.SolicitudBajaFileAttachments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajaFileAttachments.Where(i => i.Guid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum,SolicitudBaja");
                this.OnAfterSolicitudBajaFileAttachmentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SolicitudBajaFileAttachments(Guid={Guid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSolicitudBajaFileAttachment(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SolicitudBajaFileAttachments
                    .Where(i => i.Guid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSolicitudBajaFileAttachmentUpdated(item);
                this.context.SolicitudBajaFileAttachments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajaFileAttachments.Where(i => i.Guid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum,SolicitudBaja");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSolicitudBajaFileAttachmentCreated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentCreated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item)
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

                this.OnSolicitudBajaFileAttachmentCreated(item);
                this.context.SolicitudBajaFileAttachments.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajaFileAttachments.Where(i => i.Guid == item.Guid);

                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum,SolicitudBaja");

                this.OnAfterSolicitudBajaFileAttachmentCreated(item);

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
