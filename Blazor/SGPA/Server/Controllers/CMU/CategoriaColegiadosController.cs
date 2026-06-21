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
    [Route("odata/CMU/CategoriaColegiados")]
    public partial class CategoriaColegiadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CategoriaColegiadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CategoriaColegiado> GetCategoriaColegiados()
        {
            var items = this.context.CategoriaColegiados.AsQueryable<SGPA.Server.Models.CMU.CategoriaColegiado>();
            this.OnCategoriaColegiadosRead(ref items);

            return items;
        }

        partial void OnCategoriaColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiado> items);

        partial void OnCategoriaColegiadoGet(ref SingleResult<SGPA.Server.Models.CMU.CategoriaColegiado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CategoriaColegiados(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.CategoriaColegiado> GetCategoriaColegiado(int key)
        {
            var items = this.context.CategoriaColegiados.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCategoriaColegiadoGet(ref result);

            return result;
        }
        partial void OnCategoriaColegiadoDeleted(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoDeleted(SGPA.Server.Models.CMU.CategoriaColegiado item);

        [HttpDelete("/odata/CMU/CategoriaColegiados(Id={Id})")]
        public IActionResult DeleteCategoriaColegiado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CategoriaColegiados
                    .Where(i => i.Id == key)
                    .Include(i => i.CategoriaColegiados1)
                    .Include(i => i.CategoriaColegiadoValors)
                    .Include(i => i.Colegiados)
                    .Include(i => i.ColegiadoCambioCategoria)
                    .Include(i => i.DeclaracionJuradaTipos)
                    .Include(i => i.MovimientoCuentaCuota)
                    .Include(i => i.Parametros)
                    .Include(i => i.Parametros1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoriaColegiadoDeleted(item);
                this.context.CategoriaColegiados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCategoriaColegiadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoriaColegiadoUpdated(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoUpdated(SGPA.Server.Models.CMU.CategoriaColegiado item);

        [HttpPut("/odata/CMU/CategoriaColegiados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCategoriaColegiado(int key, [FromBody]SGPA.Server.Models.CMU.CategoriaColegiado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoriaColegiados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCategoriaColegiadoUpdated(item);
                this.context.CategoriaColegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiados.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");
                this.OnAfterCategoriaColegiadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CategoriaColegiados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCategoriaColegiado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CategoriaColegiado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CategoriaColegiados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CategoriaColegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCategoriaColegiadoUpdated(item);
                this.context.CategoriaColegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiados.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCategoriaColegiadoCreated(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoCreated(SGPA.Server.Models.CMU.CategoriaColegiado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CategoriaColegiado item)
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

                this.OnCategoriaColegiadoCreated(item);
                this.context.CategoriaColegiados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CategoriaColegiados.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado1");

                this.OnAfterCategoriaColegiadoCreated(item);

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
