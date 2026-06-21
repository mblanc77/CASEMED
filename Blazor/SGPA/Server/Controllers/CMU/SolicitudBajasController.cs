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
    [Route("odata/CMU/SolicitudBajas")]
    public partial class SolicitudBajasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SolicitudBajasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SolicitudBaja> GetSolicitudBajas()
        {
            var items = this.context.SolicitudBajas.AsQueryable<SGPA.Server.Models.CMU.SolicitudBaja>();
            this.OnSolicitudBajasRead(ref items);

            return items;
        }

        partial void OnSolicitudBajasRead(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBaja> items);

        partial void OnSolicitudBajaGet(ref SingleResult<SGPA.Server.Models.CMU.SolicitudBaja> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SolicitudBajas(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.SolicitudBaja> GetSolicitudBaja(int key)
        {
            var items = this.context.SolicitudBajas.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnSolicitudBajaGet(ref result);

            return result;
        }
        partial void OnSolicitudBajaDeleted(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaDeleted(SGPA.Server.Models.CMU.SolicitudBaja item);

        [HttpDelete("/odata/CMU/SolicitudBajas(OID={OID})")]
        public IActionResult DeleteSolicitudBaja(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SolicitudBajas
                    .Where(i => i.OID == key)
                    .Include(i => i.SolicitudBajaFileAttachments)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBaja>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSolicitudBajaDeleted(item);
                this.context.SolicitudBajas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSolicitudBajaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSolicitudBajaUpdated(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaUpdated(SGPA.Server.Models.CMU.SolicitudBaja item);

        [HttpPut("/odata/CMU/SolicitudBajas(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSolicitudBaja(int key, [FromBody]SGPA.Server.Models.CMU.SolicitudBaja item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SolicitudBajas
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBaja>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSolicitudBajaUpdated(item);
                this.context.SolicitudBajas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajas.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,ColegiadoDeclaracionJuradum");
                this.OnAfterSolicitudBajaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SolicitudBajas(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSolicitudBaja(int key, [FromBody]Delta<SGPA.Server.Models.CMU.SolicitudBaja> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SolicitudBajas
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SolicitudBaja>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSolicitudBajaUpdated(item);
                this.context.SolicitudBajas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajas.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,ColegiadoDeclaracionJuradum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSolicitudBajaCreated(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaCreated(SGPA.Server.Models.CMU.SolicitudBaja item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SolicitudBaja item)
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

                this.OnSolicitudBajaCreated(item);
                this.context.SolicitudBajas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SolicitudBajas.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,ColegiadoDeclaracionJuradum");

                this.OnAfterSolicitudBajaCreated(item);

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
