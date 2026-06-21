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
    [Route("odata/CMU/Especialidads")]
    public partial class EspecialidadsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public EspecialidadsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Especialidad> GetEspecialidads()
        {
            var items = this.context.Especialidads.AsQueryable<SGPA.Server.Models.CMU.Especialidad>();
            this.OnEspecialidadsRead(ref items);

            return items;
        }

        partial void OnEspecialidadsRead(ref IQueryable<SGPA.Server.Models.CMU.Especialidad> items);

        partial void OnEspecialidadGet(ref SingleResult<SGPA.Server.Models.CMU.Especialidad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Especialidads(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Especialidad> GetEspecialidad(int key)
        {
            var items = this.context.Especialidads.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnEspecialidadGet(ref result);

            return result;
        }
        partial void OnEspecialidadDeleted(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadDeleted(SGPA.Server.Models.CMU.Especialidad item);

        [HttpDelete("/odata/CMU/Especialidads(Id={Id})")]
        public IActionResult DeleteEspecialidad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Especialidads
                    .Where(i => i.Id == key)
                    .Include(i => i.TramiteInfoadjuntaespecialidads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Especialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEspecialidadDeleted(item);
                this.context.Especialidads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEspecialidadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEspecialidadUpdated(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadUpdated(SGPA.Server.Models.CMU.Especialidad item);

        [HttpPut("/odata/CMU/Especialidads(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEspecialidad(int key, [FromBody]SGPA.Server.Models.CMU.Especialidad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Especialidads
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Especialidad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEspecialidadUpdated(item);
                this.context.Especialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Especialidads.Where(i => i.Id == key);
                ;
                this.OnAfterEspecialidadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Especialidads(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEspecialidad(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Especialidad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Especialidads
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Especialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEspecialidadUpdated(item);
                this.context.Especialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Especialidads.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEspecialidadCreated(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadCreated(SGPA.Server.Models.CMU.Especialidad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Especialidad item)
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

                this.OnEspecialidadCreated(item);
                this.context.Especialidads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Especialidads.Where(i => i.Id == item.Id);

                ;

                this.OnAfterEspecialidadCreated(item);

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
