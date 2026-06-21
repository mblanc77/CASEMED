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
    [Route("odata/CMU/LugarRetiroCarnes")]
    public partial class LugarRetiroCarnesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public LugarRetiroCarnesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.LugarRetiroCarne> GetLugarRetiroCarnes()
        {
            var items = this.context.LugarRetiroCarnes.AsQueryable<SGPA.Server.Models.CMU.LugarRetiroCarne>();
            this.OnLugarRetiroCarnesRead(ref items);

            return items;
        }

        partial void OnLugarRetiroCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.LugarRetiroCarne> items);

        partial void OnLugarRetiroCarneGet(ref SingleResult<SGPA.Server.Models.CMU.LugarRetiroCarne> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/LugarRetiroCarnes(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.LugarRetiroCarne> GetLugarRetiroCarne(int key)
        {
            var items = this.context.LugarRetiroCarnes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnLugarRetiroCarneGet(ref result);

            return result;
        }
        partial void OnLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        [HttpDelete("/odata/CMU/LugarRetiroCarnes(Id={Id})")]
        public IActionResult DeleteLugarRetiroCarne(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.LugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .Include(i => i.TramiteCarnes)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.LugarRetiroCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLugarRetiroCarneDeleted(item);
                this.context.LugarRetiroCarnes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLugarRetiroCarneDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        [HttpPut("/odata/CMU/LugarRetiroCarnes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLugarRetiroCarne(int key, [FromBody]SGPA.Server.Models.CMU.LugarRetiroCarne item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.LugarRetiroCarne>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLugarRetiroCarneUpdated(item);
                this.context.LugarRetiroCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LugarRetiroCarnes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,GrupoLugarRetiroCarne");
                this.OnAfterLugarRetiroCarneUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/LugarRetiroCarnes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLugarRetiroCarne(int key, [FromBody]Delta<SGPA.Server.Models.CMU.LugarRetiroCarne> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LugarRetiroCarnes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.LugarRetiroCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLugarRetiroCarneUpdated(item);
                this.context.LugarRetiroCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LugarRetiroCarnes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,GrupoLugarRetiroCarne");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLugarRetiroCarneCreated(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneCreated(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.LugarRetiroCarne item)
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

                this.OnLugarRetiroCarneCreated(item);
                this.context.LugarRetiroCarnes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LugarRetiroCarnes.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,GrupoLugarRetiroCarne");

                this.OnAfterLugarRetiroCarneCreated(item);

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
