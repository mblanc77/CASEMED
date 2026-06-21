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
    [Route("odata/CMU/DeclaracionJuradaTipos")]
    public partial class DeclaracionJuradaTiposController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DeclaracionJuradaTiposController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> GetDeclaracionJuradaTipos()
        {
            var items = this.context.DeclaracionJuradaTipos.AsQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>();
            this.OnDeclaracionJuradaTiposRead(ref items);

            return items;
        }

        partial void OnDeclaracionJuradaTiposRead(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> items);

        partial void OnDeclaracionJuradaTipoGet(ref SingleResult<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DeclaracionJuradaTipos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> GetDeclaracionJuradaTipo(int key)
        {
            var items = this.context.DeclaracionJuradaTipos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDeclaracionJuradaTipoGet(ref result);

            return result;
        }
        partial void OnDeclaracionJuradaTipoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        [HttpDelete("/odata/CMU/DeclaracionJuradaTipos(Id={Id})")]
        public IActionResult DeleteDeclaracionJuradaTipo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DeclaracionJuradaTipos
                    .Where(i => i.Id == key)
                    .Include(i => i.ColegiadoDeclaracionJurada)
                    .Include(i => i.Parametros)
                    .Include(i => i.Parametros1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDeclaracionJuradaTipoDeleted(item);
                this.context.DeclaracionJuradaTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDeclaracionJuradaTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDeclaracionJuradaTipoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        [HttpPut("/odata/CMU/DeclaracionJuradaTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDeclaracionJuradaTipo(int key, [FromBody]SGPA.Server.Models.CMU.DeclaracionJuradaTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DeclaracionJuradaTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDeclaracionJuradaTipoUpdated(item);
                this.context.DeclaracionJuradaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaTipos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado");
                this.OnAfterDeclaracionJuradaTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DeclaracionJuradaTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDeclaracionJuradaTipo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DeclaracionJuradaTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDeclaracionJuradaTipoUpdated(item);
                this.context.DeclaracionJuradaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaTipos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDeclaracionJuradaTipoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DeclaracionJuradaTipo item)
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

                this.OnDeclaracionJuradaTipoCreated(item);
                this.context.DeclaracionJuradaTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaTipos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado");

                this.OnAfterDeclaracionJuradaTipoCreated(item);

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
