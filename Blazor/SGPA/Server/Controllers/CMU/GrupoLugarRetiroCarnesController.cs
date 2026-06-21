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
    [Route("odata/CMU/GrupoLugarRetiroCarnes")]
    public partial class GrupoLugarRetiroCarnesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public GrupoLugarRetiroCarnesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> GetGrupoLugarRetiroCarnes()
        {
            var items = this.context.GrupoLugarRetiroCarnes.AsQueryable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>();
            this.OnGrupoLugarRetiroCarnesRead(ref items);

            return items;
        }

        partial void OnGrupoLugarRetiroCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> items);

        partial void OnGrupoLugarRetiroCarneGet(ref SingleResult<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/GrupoLugarRetiroCarnes(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> GetGrupoLugarRetiroCarne(int key)
        {
            var items = this.context.GrupoLugarRetiroCarnes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnGrupoLugarRetiroCarneGet(ref result);

            return result;
        }
        partial void OnGrupoLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        [HttpDelete("/odata/CMU/GrupoLugarRetiroCarnes(Id={Id})")]
        public IActionResult DeleteGrupoLugarRetiroCarne(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.GrupoLugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .Include(i => i.LugarRetiroCarnes)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGrupoLugarRetiroCarneDeleted(item);
                this.context.GrupoLugarRetiroCarnes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterGrupoLugarRetiroCarneDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGrupoLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        [HttpPut("/odata/CMU/GrupoLugarRetiroCarnes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutGrupoLugarRetiroCarne(int key, [FromBody]SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.GrupoLugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGrupoLugarRetiroCarneUpdated(item);
                this.context.GrupoLugarRetiroCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoLugarRetiroCarnes.Where(i => i.Id == key);
                ;
                this.OnAfterGrupoLugarRetiroCarneUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/GrupoLugarRetiroCarnes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchGrupoLugarRetiroCarne(int key, [FromBody]Delta<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.GrupoLugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnGrupoLugarRetiroCarneUpdated(item);
                this.context.GrupoLugarRetiroCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoLugarRetiroCarnes.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGrupoLugarRetiroCarneCreated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneCreated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item)
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

                this.OnGrupoLugarRetiroCarneCreated(item);
                this.context.GrupoLugarRetiroCarnes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoLugarRetiroCarnes.Where(i => i.Id == item.Id);

                ;

                this.OnAfterGrupoLugarRetiroCarneCreated(item);

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
