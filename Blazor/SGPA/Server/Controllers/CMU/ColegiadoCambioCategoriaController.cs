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
    [Route("odata/CMU/ColegiadoCambioCategoria")]
    public partial class ColegiadoCambioCategoriaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoCambioCategoriaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> GetColegiadoCambioCategoria()
        {
            var items = this.context.ColegiadoCambioCategoria.AsQueryable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>();
            this.OnColegiadoCambioCategoriaRead(ref items);

            return items;
        }

        partial void OnColegiadoCambioCategoriaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> items);

        partial void OnColegiadoCambioCategoriumGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoCambioCategoria(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> GetColegiadoCambioCategorium(int key)
        {
            var items = this.context.ColegiadoCambioCategoria.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoCambioCategoriumGet(ref result);

            return result;
        }
        partial void OnColegiadoCambioCategoriumDeleted(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumDeleted(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        [HttpDelete("/odata/CMU/ColegiadoCambioCategoria(Id={Id})")]
        public IActionResult DeleteColegiadoCambioCategorium(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoCambioCategoria
                    .Where(i => i.Id == key)
                    .Include(i => i.AjusteRetroactivos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoCambioCategoriumDeleted(item);
                this.context.ColegiadoCambioCategoria.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoCambioCategoriumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoCambioCategoriumUpdated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumUpdated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        [HttpPut("/odata/CMU/ColegiadoCambioCategoria(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoCambioCategorium(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoCambioCategorium item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoCambioCategoria
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoCambioCategoriumUpdated(item);
                this.context.ColegiadoCambioCategoria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCambioCategoria.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,Colegiado1");
                this.OnAfterColegiadoCambioCategoriumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoCambioCategoria(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoCambioCategorium(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoCambioCategoria
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoCambioCategoriumUpdated(item);
                this.context.ColegiadoCambioCategoria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCambioCategoria.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,Colegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoCambioCategoriumCreated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumCreated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoCambioCategorium item)
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

                this.OnColegiadoCambioCategoriumCreated(item);
                this.context.ColegiadoCambioCategoria.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCambioCategoria.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,Colegiado1");

                this.OnAfterColegiadoCambioCategoriumCreated(item);

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
