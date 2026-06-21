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
    [Route("odata/CMU/Parametros")]
    public partial class ParametrosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ParametrosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Parametro> GetParametros()
        {
            var items = this.context.Parametros.AsQueryable<SGPA.Server.Models.CMU.Parametro>();
            this.OnParametrosRead(ref items);

            return items;
        }

        partial void OnParametrosRead(ref IQueryable<SGPA.Server.Models.CMU.Parametro> items);

        partial void OnParametroGet(ref SingleResult<SGPA.Server.Models.CMU.Parametro> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Parametros(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Parametro> GetParametro(int key)
        {
            var items = this.context.Parametros.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnParametroGet(ref result);

            return result;
        }
        partial void OnParametroDeleted(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroDeleted(SGPA.Server.Models.CMU.Parametro item);

        [HttpDelete("/odata/CMU/Parametros(Id={Id})")]
        public IActionResult DeleteParametro(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Parametros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Parametro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnParametroDeleted(item);
                this.context.Parametros.Remove(item);
                this.context.SaveChanges();
                this.OnAfterParametroDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnParametroUpdated(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroUpdated(SGPA.Server.Models.CMU.Parametro item);

        [HttpPut("/odata/CMU/Parametros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutParametro(int key, [FromBody]SGPA.Server.Models.CMU.Parametro item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Parametros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Parametro>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnParametroUpdated(item);
                this.context.Parametros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Parametros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,AgenteCobranzaDebito,CategoriaColegiado,CategoriaColegiado1,DeclaracionJuradaTipo,DeclaracionJuradaTipo1,MovimientoTipo,MovimientoTipo1,MovimientoTipo2,MovimientoTipo3");
                this.OnAfterParametroUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Parametros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchParametro(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Parametro> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Parametros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Parametro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnParametroUpdated(item);
                this.context.Parametros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Parametros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,AgenteCobranzaDebito,CategoriaColegiado,CategoriaColegiado1,DeclaracionJuradaTipo,DeclaracionJuradaTipo1,MovimientoTipo,MovimientoTipo1,MovimientoTipo2,MovimientoTipo3");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnParametroCreated(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroCreated(SGPA.Server.Models.CMU.Parametro item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Parametro item)
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

                this.OnParametroCreated(item);
                this.context.Parametros.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Parametros.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,AgenteCobranzaDebito,CategoriaColegiado,CategoriaColegiado1,DeclaracionJuradaTipo,DeclaracionJuradaTipo1,MovimientoTipo,MovimientoTipo1,MovimientoTipo2,MovimientoTipo3");

                this.OnAfterParametroCreated(item);

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
