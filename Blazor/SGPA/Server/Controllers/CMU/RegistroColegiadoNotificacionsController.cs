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
    [Route("odata/CMU/RegistroColegiadoNotificacions")]
    public partial class RegistroColegiadoNotificacionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RegistroColegiadoNotificacionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> GetRegistroColegiadoNotificacions()
        {
            var items = this.context.RegistroColegiadoNotificacions.AsQueryable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>();
            this.OnRegistroColegiadoNotificacionsRead(ref items);

            return items;
        }

        partial void OnRegistroColegiadoNotificacionsRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> items);

        partial void OnRegistroColegiadoNotificacionGet(ref SingleResult<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/RegistroColegiadoNotificacions(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> GetRegistroColegiadoNotificacion(int key)
        {
            var items = this.context.RegistroColegiadoNotificacions.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnRegistroColegiadoNotificacionGet(ref result);

            return result;
        }
        partial void OnRegistroColegiadoNotificacionDeleted(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionDeleted(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        [HttpDelete("/odata/CMU/RegistroColegiadoNotificacions(Id={Id})")]
        public IActionResult DeleteRegistroColegiadoNotificacion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegistroColegiadoNotificacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoNotificacionDeleted(item);
                this.context.RegistroColegiadoNotificacions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegistroColegiadoNotificacionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoNotificacionUpdated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionUpdated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        [HttpPut("/odata/CMU/RegistroColegiadoNotificacions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegistroColegiadoNotificacion(int key, [FromBody]SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiadoNotificacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoNotificacionUpdated(item);
                this.context.RegistroColegiadoNotificacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoNotificacions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RegistroColegiado1");
                this.OnAfterRegistroColegiadoNotificacionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/RegistroColegiadoNotificacions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegistroColegiadoNotificacion(int key, [FromBody]Delta<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiadoNotificacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegistroColegiadoNotificacionUpdated(item);
                this.context.RegistroColegiadoNotificacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoNotificacions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RegistroColegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoNotificacionCreated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionCreated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item)
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

                this.OnRegistroColegiadoNotificacionCreated(item);
                this.context.RegistroColegiadoNotificacions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoNotificacions.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "RegistroColegiado1");

                this.OnAfterRegistroColegiadoNotificacionCreated(item);

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
