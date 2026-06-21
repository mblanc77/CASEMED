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
    [Route("odata/Sgpa/Receta")]
    public partial class RecetaController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public RecetaController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Recetum> GetReceta()
        {
            var items = this.context.Receta.AsQueryable<SgpaNew.Server.Models.Sgpa.Recetum>();
            this.OnRecetaRead(ref items);

            return items;
        }

        partial void OnRecetaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Recetum> items);

        partial void OnRecetumGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Recetum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Receta(RecetaId={RecetaId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Recetum> GetRecetum(int key)
        {
            var items = this.context.Receta.Where(i => i.RecetaId == key);
            var result = SingleResult.Create(items);

            OnRecetumGet(ref result);

            return result;
        }
        partial void OnRecetumDeleted(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumDeleted(SgpaNew.Server.Models.Sgpa.Recetum item);

        [HttpDelete("/odata/Sgpa/Receta(RecetaId={RecetaId})")]
        public IActionResult DeleteRecetum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Receta
                    .Where(i => i.RecetaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Recetum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRecetumDeleted(item);
                this.context.Receta.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRecetumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRecetumUpdated(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumUpdated(SgpaNew.Server.Models.Sgpa.Recetum item);

        [HttpPut("/odata/Sgpa/Receta(RecetaId={RecetaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRecetum(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Recetum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Receta
                    .Where(i => i.RecetaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Recetum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRecetumUpdated(item);
                this.context.Receta.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Receta.Where(i => i.RecetaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RecetaDistancium,Prestacion");
                this.OnAfterRecetumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Receta(RecetaId={RecetaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRecetum(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Recetum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Receta
                    .Where(i => i.RecetaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Recetum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRecetumUpdated(item);
                this.context.Receta.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Receta.Where(i => i.RecetaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RecetaDistancium,Prestacion");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRecetumCreated(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumCreated(SgpaNew.Server.Models.Sgpa.Recetum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Recetum item)
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

                this.OnRecetumCreated(item);
                this.context.Receta.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Receta.Where(i => i.RecetaId == item.RecetaId);

                Request.QueryString = Request.QueryString.Add("$expand", "RecetaDistancium,Prestacion");

                this.OnAfterRecetumCreated(item);

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
