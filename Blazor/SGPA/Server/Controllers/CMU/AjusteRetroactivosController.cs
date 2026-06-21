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
    [Route("odata/CMU/AjusteRetroactivos")]
    public partial class AjusteRetroactivosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AjusteRetroactivosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AjusteRetroactivo> GetAjusteRetroactivos()
        {
            var items = this.context.AjusteRetroactivos.AsQueryable<SGPA.Server.Models.CMU.AjusteRetroactivo>();
            this.OnAjusteRetroactivosRead(ref items);

            return items;
        }

        partial void OnAjusteRetroactivosRead(ref IQueryable<SGPA.Server.Models.CMU.AjusteRetroactivo> items);

        partial void OnAjusteRetroactivoGet(ref SingleResult<SGPA.Server.Models.CMU.AjusteRetroactivo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AjusteRetroactivos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AjusteRetroactivo> GetAjusteRetroactivo(int key)
        {
            var items = this.context.AjusteRetroactivos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAjusteRetroactivoGet(ref result);

            return result;
        }
        partial void OnAjusteRetroactivoDeleted(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoDeleted(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        [HttpDelete("/odata/CMU/AjusteRetroactivos(Id={Id})")]
        public IActionResult DeleteAjusteRetroactivo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AjusteRetroactivos
                    .Where(i => i.Id == key)
                    .Include(i => i.AjusteDetalles)
                    .Include(i => i.AjusteRetroactivos1)
                    .Include(i => i.MovimientoCuenta)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteRetroactivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAjusteRetroactivoDeleted(item);
                this.context.AjusteRetroactivos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAjusteRetroactivoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAjusteRetroactivoUpdated(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoUpdated(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        [HttpPut("/odata/CMU/AjusteRetroactivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAjusteRetroactivo(int key, [FromBody]SGPA.Server.Models.CMU.AjusteRetroactivo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AjusteRetroactivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteRetroactivo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAjusteRetroactivoUpdated(item);
                this.context.AjusteRetroactivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteRetroactivos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo1,ColegiadoCambioCategorium,Colegiado1,ColegiadoDeclaracionJuradum");
                this.OnAfterAjusteRetroactivoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AjusteRetroactivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAjusteRetroactivo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AjusteRetroactivo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AjusteRetroactivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteRetroactivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAjusteRetroactivoUpdated(item);
                this.context.AjusteRetroactivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteRetroactivos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo1,ColegiadoCambioCategorium,Colegiado1,ColegiadoDeclaracionJuradum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAjusteRetroactivoCreated(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoCreated(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AjusteRetroactivo item)
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

                this.OnAjusteRetroactivoCreated(item);
                this.context.AjusteRetroactivos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteRetroactivos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo1,ColegiadoCambioCategorium,Colegiado1,ColegiadoDeclaracionJuradum");

                this.OnAfterAjusteRetroactivoCreated(item);

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
