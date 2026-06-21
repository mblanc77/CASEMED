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
    [Route("odata/CMU/AgenteCobranzaTipos")]
    public partial class AgenteCobranzaTiposController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AgenteCobranzaTiposController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaTipo> GetAgenteCobranzaTipos()
        {
            var items = this.context.AgenteCobranzaTipos.AsQueryable<SGPA.Server.Models.CMU.AgenteCobranzaTipo>();
            this.OnAgenteCobranzaTiposRead(ref items);

            return items;
        }

        partial void OnAgenteCobranzaTiposRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaTipo> items);

        partial void OnAgenteCobranzaTipoGet(ref SingleResult<SGPA.Server.Models.CMU.AgenteCobranzaTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AgenteCobranzaTipos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AgenteCobranzaTipo> GetAgenteCobranzaTipo(int key)
        {
            var items = this.context.AgenteCobranzaTipos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAgenteCobranzaTipoGet(ref result);

            return result;
        }
        partial void OnAgenteCobranzaTipoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        [HttpDelete("/odata/CMU/AgenteCobranzaTipos(Id={Id})")]
        public IActionResult DeleteAgenteCobranzaTipo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AgenteCobranzaTipos
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaTipoDeleted(item);
                this.context.AgenteCobranzaTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAgenteCobranzaTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaTipoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        [HttpPut("/odata/CMU/AgenteCobranzaTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAgenteCobranzaTipo(int key, [FromBody]SGPA.Server.Models.CMU.AgenteCobranzaTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzaTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaTipoUpdated(item);
                this.context.AgenteCobranzaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaTipos.Where(i => i.Id == key);
                ;
                this.OnAfterAgenteCobranzaTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AgenteCobranzaTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAgenteCobranzaTipo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AgenteCobranzaTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzaTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAgenteCobranzaTipoUpdated(item);
                this.context.AgenteCobranzaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaTipos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaTipoCreated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoCreated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AgenteCobranzaTipo item)
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

                this.OnAgenteCobranzaTipoCreated(item);
                this.context.AgenteCobranzaTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaTipos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterAgenteCobranzaTipoCreated(item);

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
