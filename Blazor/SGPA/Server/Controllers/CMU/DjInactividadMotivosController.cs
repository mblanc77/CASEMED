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
    [Route("odata/CMU/DjInactividadMotivos")]
    public partial class DjInactividadMotivosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DjInactividadMotivosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DjInactividadMotivo> GetDjInactividadMotivos()
        {
            var items = this.context.DjInactividadMotivos.AsQueryable<SGPA.Server.Models.CMU.DjInactividadMotivo>();
            this.OnDjInactividadMotivosRead(ref items);

            return items;
        }

        partial void OnDjInactividadMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.DjInactividadMotivo> items);

        partial void OnDjInactividadMotivoGet(ref SingleResult<SGPA.Server.Models.CMU.DjInactividadMotivo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DjInactividadMotivos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DjInactividadMotivo> GetDjInactividadMotivo(int key)
        {
            var items = this.context.DjInactividadMotivos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDjInactividadMotivoGet(ref result);

            return result;
        }
        partial void OnDjInactividadMotivoDeleted(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoDeleted(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        [HttpDelete("/odata/CMU/DjInactividadMotivos(Id={Id})")]
        public IActionResult DeleteDjInactividadMotivo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DjInactividadMotivos
                    .Where(i => i.Id == key)
                    .Include(i => i.ColegiadoDeclaracionJurada)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DjInactividadMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDjInactividadMotivoDeleted(item);
                this.context.DjInactividadMotivos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDjInactividadMotivoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDjInactividadMotivoUpdated(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoUpdated(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        [HttpPut("/odata/CMU/DjInactividadMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDjInactividadMotivo(int key, [FromBody]SGPA.Server.Models.CMU.DjInactividadMotivo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DjInactividadMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DjInactividadMotivo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDjInactividadMotivoUpdated(item);
                this.context.DjInactividadMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DjInactividadMotivos.Where(i => i.Id == key);
                ;
                this.OnAfterDjInactividadMotivoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DjInactividadMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDjInactividadMotivo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DjInactividadMotivo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DjInactividadMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DjInactividadMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDjInactividadMotivoUpdated(item);
                this.context.DjInactividadMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DjInactividadMotivos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDjInactividadMotivoCreated(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoCreated(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DjInactividadMotivo item)
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

                this.OnDjInactividadMotivoCreated(item);
                this.context.DjInactividadMotivos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DjInactividadMotivos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterDjInactividadMotivoCreated(item);

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
