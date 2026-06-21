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
    [Route("odata/CMU/TramiteInfoadjuntaespecialidads")]
    public partial class TramiteInfoadjuntaespecialidadsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteInfoadjuntaespecialidadsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> GetTramiteInfoadjuntaespecialidads()
        {
            var items = this.context.TramiteInfoadjuntaespecialidads.AsQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>();
            this.OnTramiteInfoadjuntaespecialidadsRead(ref items);

            return items;
        }

        partial void OnTramiteInfoadjuntaespecialidadsRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> items);

        partial void OnTramiteInfoadjuntaespecialidadGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteInfoadjuntaespecialidads(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> GetTramiteInfoadjuntaespecialidad(int key)
        {
            var items = this.context.TramiteInfoadjuntaespecialidads.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteInfoadjuntaespecialidadGet(ref result);

            return result;
        }
        partial void OnTramiteInfoadjuntaespecialidadDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        [HttpDelete("/odata/CMU/TramiteInfoadjuntaespecialidads(OID={OID})")]
        public IActionResult DeleteTramiteInfoadjuntaespecialidad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteInfoadjuntaespecialidads
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntaespecialidadDeleted(item);
                this.context.TramiteInfoadjuntaespecialidads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteInfoadjuntaespecialidadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntaespecialidadUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        [HttpPut("/odata/CMU/TramiteInfoadjuntaespecialidads(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteInfoadjuntaespecialidad(int key, [FromBody]SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntaespecialidads
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntaespecialidadUpdated(item);
                this.context.TramiteInfoadjuntaespecialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntaespecialidads.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Especialidad1,TramiteInfoAdjuntaTitulo");
                this.OnAfterTramiteInfoadjuntaespecialidadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteInfoadjuntaespecialidads(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteInfoadjuntaespecialidad(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntaespecialidads
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteInfoadjuntaespecialidadUpdated(item);
                this.context.TramiteInfoadjuntaespecialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntaespecialidads.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Especialidad1,TramiteInfoAdjuntaTitulo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntaespecialidadCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item)
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

                this.OnTramiteInfoadjuntaespecialidadCreated(item);
                this.context.TramiteInfoadjuntaespecialidads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntaespecialidads.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "Especialidad1,TramiteInfoAdjuntaTitulo");

                this.OnAfterTramiteInfoadjuntaespecialidadCreated(item);

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
