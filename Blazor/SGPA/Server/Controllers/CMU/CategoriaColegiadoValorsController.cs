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
    [Route("odata/CMU/CategoriaColegiadoValors")]
    public partial class CategoriaColegiadoValorsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CategoriaColegiadoValorsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiadoValor> GetCategoriaColegiadoValors()
        {
            var items = this.context.CategoriaColegiadoValors.AsQueryable<SGPA.Server.Models.CMU.CategoriaColegiadoValor>();
            this.OnCategoriaColegiadoValorsRead(ref items);

            return items;
        }

        partial void OnCategoriaColegiadoValorsRead(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiadoValor> items);

        partial void OnCategoriaColegiadoValorGet(ref SingleResult<SGPA.Server.Models.CMU.CategoriaColegiadoValor> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CategoriaColegiadoValors(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.CategoriaColegiadoValor> GetCategoriaColegiadoValor(int key)
        {
            var items = this.context.CategoriaColegiadoValors.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCategoriaColegiadoValorGet(ref result);

            return result;
        }
        partial void OnCategoriaColegiadoValorDeleted(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorDeleted(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        [HttpDelete("/odata/CMU/CategoriaColegiadoValors(Id={Id})")]
        public IActionResult DeleteCategoriaColegiadoValor(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CategoriaColegiadoValors
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiadoValor>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoriaColegiadoValorDeleted(item);
                this.context.CategoriaColegiadoValors.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCategoriaColegiadoValorDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoriaColegiadoValorUpdated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorUpdated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        [HttpPut("/odata/CMU/CategoriaColegiadoValors(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCategoriaColegiadoValor(int key, [FromBody]SGPA.Server.Models.CMU.CategoriaColegiadoValor item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoriaColegiadoValors
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiadoValor>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoriaColegiadoValorUpdated(item);
                this.context.CategoriaColegiadoValors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiadoValors.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");
                this.OnAfterCategoriaColegiadoValorUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CategoriaColegiadoValors(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCategoriaColegiadoValor(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CategoriaColegiadoValor> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoriaColegiadoValors
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiadoValor>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCategoriaColegiadoValorUpdated(item);
                this.context.CategoriaColegiadoValors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiadoValors.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoriaColegiadoValorCreated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorCreated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CategoriaColegiadoValor item)
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

                this.OnCategoriaColegiadoValorCreated(item);
                this.context.CategoriaColegiadoValors.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiadoValors.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");

                this.OnAfterCategoriaColegiadoValorCreated(item);

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
