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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/Especialidads")]
    public partial class EspecialidadsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public EspecialidadsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Especialidad> GetEspecialidads()
        {
            var items = this.context.Especialidads.AsQueryable<SgpaNew.Server.Models.Sgpa.Especialidad>();
            this.OnEspecialidadsRead(ref items);

            return items;
        }

        partial void OnEspecialidadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Especialidad> items);

        partial void OnEspecialidadGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Especialidad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Especialidads(CodEspecialidad={CodEspecialidad})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Especialidad> GetEspecialidad(int key)
        {
            var items = this.context.Especialidads.Where(i => i.CodEspecialidad == key);
            var result = SingleResult.Create(items);

            OnEspecialidadGet(ref result);

            return result;
        }
        partial void OnEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.Especialidad item);

        [HttpDelete("/odata/Sgpa/Especialidads(CodEspecialidad={CodEspecialidad})")]
        public IActionResult DeleteEspecialidad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Especialidads
                    .Where(i => i.CodEspecialidad == key)
                    .Include(i => i.AfiliadoEspecialidads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Especialidad>(Request, items);

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

        partial void OnEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.Especialidad item);

        [HttpPut("/odata/Sgpa/Especialidads(CodEspecialidad={CodEspecialidad})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEspecialidad(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Especialidad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Especialidads
                    .Where(i => i.CodEspecialidad == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Especialidad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEspecialidadUpdated(item);
                this.context.Especialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Especialidads.Where(i => i.CodEspecialidad == key);
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

        [HttpPatch("/odata/Sgpa/Especialidads(CodEspecialidad={CodEspecialidad})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEspecialidad(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Especialidad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Especialidads
                    .Where(i => i.CodEspecialidad == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Especialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEspecialidadUpdated(item);
                this.context.Especialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Especialidads.Where(i => i.CodEspecialidad == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEspecialidadCreated(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadCreated(SgpaNew.Server.Models.Sgpa.Especialidad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Especialidad item)
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

                var itemToReturn = this.context.Especialidads.Where(i => i.CodEspecialidad == item.CodEspecialidad);

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
