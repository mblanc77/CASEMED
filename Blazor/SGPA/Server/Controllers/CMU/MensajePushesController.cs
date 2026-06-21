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
    [Route("odata/CMU/MensajePushes")]
    public partial class MensajePushesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MensajePushesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MensajePush> GetMensajePushes()
        {
            var items = this.context.MensajePushes.AsQueryable<SGPA.Server.Models.CMU.MensajePush>();
            this.OnMensajePushesRead(ref items);

            return items;
        }

        partial void OnMensajePushesRead(ref IQueryable<SGPA.Server.Models.CMU.MensajePush> items);

        partial void OnMensajePushGet(ref SingleResult<SGPA.Server.Models.CMU.MensajePush> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MensajePushes(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.MensajePush> GetMensajePush(int key)
        {
            var items = this.context.MensajePushes.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnMensajePushGet(ref result);

            return result;
        }
        partial void OnMensajePushDeleted(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushDeleted(SGPA.Server.Models.CMU.MensajePush item);

        [HttpDelete("/odata/CMU/MensajePushes(OID={OID})")]
        public IActionResult DeleteMensajePush(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MensajePushes
                    .Where(i => i.OID == key)
                    .Include(i => i.MensajePushAdds)
                    .Include(i => i.MensajeSegmentos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePush>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajePushDeleted(item);
                this.context.MensajePushes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMensajePushDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajePushUpdated(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushUpdated(SGPA.Server.Models.CMU.MensajePush item);

        [HttpPut("/odata/CMU/MensajePushes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMensajePush(int key, [FromBody]SGPA.Server.Models.CMU.MensajePush item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajePushes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePush>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajePushUpdated(item);
                this.context.MensajePushes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushes.Where(i => i.OID == key);
                ;
                this.OnAfterMensajePushUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MensajePushes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMensajePush(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MensajePush> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajePushes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePush>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMensajePushUpdated(item);
                this.context.MensajePushes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushes.Where(i => i.OID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajePushCreated(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushCreated(SGPA.Server.Models.CMU.MensajePush item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MensajePush item)
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

                this.OnMensajePushCreated(item);
                this.context.MensajePushes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushes.Where(i => i.OID == item.OID);

                ;

                this.OnAfterMensajePushCreated(item);

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
