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
    [Route("odata/CMU/AgenteGrupos")]
    public partial class AgenteGruposController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AgenteGruposController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AgenteGrupo> GetAgenteGrupos()
        {
            var items = this.context.AgenteGrupos.AsQueryable<SGPA.Server.Models.CMU.AgenteGrupo>();
            this.OnAgenteGruposRead(ref items);

            return items;
        }

        partial void OnAgenteGruposRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteGrupo> items);

        partial void OnAgenteGrupoGet(ref SingleResult<SGPA.Server.Models.CMU.AgenteGrupo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AgenteGrupos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AgenteGrupo> GetAgenteGrupo(int key)
        {
            var items = this.context.AgenteGrupos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAgenteGrupoGet(ref result);

            return result;
        }
        partial void OnAgenteGrupoDeleted(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoDeleted(SGPA.Server.Models.CMU.AgenteGrupo item);

        [HttpDelete("/odata/CMU/AgenteGrupos(Id={Id})")]
        public IActionResult DeleteAgenteGrupo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AgenteGrupos
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteGrupo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteGrupoDeleted(item);
                this.context.AgenteGrupos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAgenteGrupoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteGrupoUpdated(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoUpdated(SGPA.Server.Models.CMU.AgenteGrupo item);

        [HttpPut("/odata/CMU/AgenteGrupos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAgenteGrupo(int key, [FromBody]SGPA.Server.Models.CMU.AgenteGrupo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteGrupos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteGrupo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteGrupoUpdated(item);
                this.context.AgenteGrupos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteGrupos.Where(i => i.Id == key);
                ;
                this.OnAfterAgenteGrupoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AgenteGrupos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAgenteGrupo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AgenteGrupo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteGrupos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteGrupo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAgenteGrupoUpdated(item);
                this.context.AgenteGrupos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteGrupos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteGrupoCreated(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoCreated(SGPA.Server.Models.CMU.AgenteGrupo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AgenteGrupo item)
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

                this.OnAgenteGrupoCreated(item);
                this.context.AgenteGrupos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteGrupos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterAgenteGrupoCreated(item);

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
