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
    [Route("odata/CMU/MovimientoCuenta")]
    public partial class MovimientoCuentaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MovimientoCuentaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentum> GetMovimientoCuenta()
        {
            var items = this.context.MovimientoCuenta.AsQueryable<SGPA.Server.Models.CMU.MovimientoCuentum>();
            this.OnMovimientoCuentaRead(ref items);

            return items;
        }

        partial void OnMovimientoCuentaRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentum> items);

        partial void OnMovimientoCuentumGet(ref SingleResult<SGPA.Server.Models.CMU.MovimientoCuentum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MovimientoCuenta(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.MovimientoCuentum> GetMovimientoCuentum(int key)
        {
            var items = this.context.MovimientoCuenta.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnMovimientoCuentumGet(ref result);

            return result;
        }
        partial void OnMovimientoCuentumDeleted(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumDeleted(SGPA.Server.Models.CMU.MovimientoCuentum item);

        [HttpDelete("/odata/CMU/MovimientoCuenta(Id={Id})")]
        public IActionResult DeleteMovimientoCuentum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MovimientoCuenta
                    .Where(i => i.Id == key)
                    .Include(i => i.CobroNominas)
                    .Include(i => i.MovimientoCuenta1)
                    .Include(i => i.MovimientoCuentaCuota)
                    .Include(i => i.MovimientoCuentaManuals)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentumDeleted(item);
                this.context.MovimientoCuenta.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMovimientoCuentumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentumUpdated(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumUpdated(SGPA.Server.Models.CMU.MovimientoCuentum item);

        [HttpPut("/odata/CMU/MovimientoCuenta(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMovimientoCuentum(int key, [FromBody]SGPA.Server.Models.CMU.MovimientoCuentum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuenta
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentumUpdated(item);
                this.context.MovimientoCuenta.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuenta.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo,Colegiado1,MovimientoCuentum1,MovimientoTipo1,XpobjectType,OrigenMovimiento");
                this.OnAfterMovimientoCuentumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MovimientoCuenta(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMovimientoCuentum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MovimientoCuentum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuenta
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMovimientoCuentumUpdated(item);
                this.context.MovimientoCuenta.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuenta.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo,Colegiado1,MovimientoCuentum1,MovimientoTipo1,XpobjectType,OrigenMovimiento");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentumCreated(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumCreated(SGPA.Server.Models.CMU.MovimientoCuentum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MovimientoCuentum item)
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

                this.OnMovimientoCuentumCreated(item);
                this.context.MovimientoCuenta.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuenta.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo,Colegiado1,MovimientoCuentum1,MovimientoTipo1,XpobjectType,OrigenMovimiento");

                this.OnAfterMovimientoCuentumCreated(item);

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
