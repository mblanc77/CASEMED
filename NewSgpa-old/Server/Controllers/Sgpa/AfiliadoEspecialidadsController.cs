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
    [Route("odata/Sgpa/AfiliadoEspecialidads")]
    public partial class AfiliadoEspecialidadsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AfiliadoEspecialidadsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> GetAfiliadoEspecialidads()
        {
            var items = this.context.AfiliadoEspecialidads.AsQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>();
            this.OnAfiliadoEspecialidadsRead(ref items);

            return items;
        }

        partial void OnAfiliadoEspecialidadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> items);

        partial void OnAfiliadoEspecialidadGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AfiliadoEspecialidads(AfiliadoEspecialidadId={AfiliadoEspecialidadId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> GetAfiliadoEspecialidad(int key)
        {
            var items = this.context.AfiliadoEspecialidads.Where(i => i.AfiliadoEspecialidadId == key);
            var result = SingleResult.Create(items);

            OnAfiliadoEspecialidadGet(ref result);

            return result;
        }
        partial void OnAfiliadoEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        [HttpDelete("/odata/Sgpa/AfiliadoEspecialidads(AfiliadoEspecialidadId={AfiliadoEspecialidadId})")]
        public IActionResult DeleteAfiliadoEspecialidad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AfiliadoEspecialidads
                    .Where(i => i.AfiliadoEspecialidadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoEspecialidadDeleted(item);
                this.context.AfiliadoEspecialidads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAfiliadoEspecialidadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        [HttpPut("/odata/Sgpa/AfiliadoEspecialidads(AfiliadoEspecialidadId={AfiliadoEspecialidadId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAfiliadoEspecialidad(int key, [FromBody]SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfiliadoEspecialidads
                    .Where(i => i.AfiliadoEspecialidadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoEspecialidadUpdated(item);
                this.context.AfiliadoEspecialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoEspecialidads.Where(i => i.AfiliadoEspecialidadId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Especialidad");
                this.OnAfterAfiliadoEspecialidadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AfiliadoEspecialidads(AfiliadoEspecialidadId={AfiliadoEspecialidadId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAfiliadoEspecialidad(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfiliadoEspecialidads
                    .Where(i => i.AfiliadoEspecialidadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAfiliadoEspecialidadUpdated(item);
                this.context.AfiliadoEspecialidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoEspecialidads.Where(i => i.AfiliadoEspecialidadId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Especialidad");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoEspecialidadCreated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadCreated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item)
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

                this.OnAfiliadoEspecialidadCreated(item);
                this.context.AfiliadoEspecialidads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoEspecialidads.Where(i => i.AfiliadoEspecialidadId == item.AfiliadoEspecialidadId);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Especialidad");

                this.OnAfterAfiliadoEspecialidadCreated(item);

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
