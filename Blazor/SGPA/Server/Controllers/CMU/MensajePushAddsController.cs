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
    [Route("odata/CMU/MensajePushAdds")]
    public partial class MensajePushAddsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MensajePushAddsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MensajePushAdd> GetMensajePushAdds()
        {
            var items = this.context.MensajePushAdds.AsQueryable<SGPA.Server.Models.CMU.MensajePushAdd>();
            this.OnMensajePushAddsRead(ref items);

            return items;
        }

        partial void OnMensajePushAddsRead(ref IQueryable<SGPA.Server.Models.CMU.MensajePushAdd> items);

        partial void OnMensajePushAddGet(ref SingleResult<SGPA.Server.Models.CMU.MensajePushAdd> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MensajePushAdds(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.MensajePushAdd> GetMensajePushAdd(int key)
        {
            var items = this.context.MensajePushAdds.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnMensajePushAddGet(ref result);

            return result;
        }
        partial void OnMensajePushAddDeleted(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddDeleted(SGPA.Server.Models.CMU.MensajePushAdd item);

        [HttpDelete("/odata/CMU/MensajePushAdds(OID={OID})")]
        public IActionResult DeleteMensajePushAdd(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MensajePushAdds
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePushAdd>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajePushAddDeleted(item);
                this.context.MensajePushAdds.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMensajePushAddDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajePushAddUpdated(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddUpdated(SGPA.Server.Models.CMU.MensajePushAdd item);

        [HttpPut("/odata/CMU/MensajePushAdds(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMensajePushAdd(int key, [FromBody]SGPA.Server.Models.CMU.MensajePushAdd item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajePushAdds
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePushAdd>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMensajePushAddUpdated(item);
                this.context.MensajePushAdds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushAdds.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");
                this.OnAfterMensajePushAddUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MensajePushAdds(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMensajePushAdd(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MensajePushAdd> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MensajePushAdds
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MensajePushAdd>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMensajePushAddUpdated(item);
                this.context.MensajePushAdds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushAdds.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMensajePushAddCreated(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddCreated(SGPA.Server.Models.CMU.MensajePushAdd item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MensajePushAdd item)
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

                this.OnMensajePushAddCreated(item);
                this.context.MensajePushAdds.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MensajePushAdds.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "MensajePush");

                this.OnAfterMensajePushAddCreated(item);

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
