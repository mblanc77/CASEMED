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
    [Route("odata/CMU/OrigenMovimientos")]
    public partial class OrigenMovimientosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public OrigenMovimientosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.OrigenMovimiento> GetOrigenMovimientos()
        {
            var items = this.context.OrigenMovimientos.AsQueryable<SGPA.Server.Models.CMU.OrigenMovimiento>();
            this.OnOrigenMovimientosRead(ref items);

            return items;
        }

        partial void OnOrigenMovimientosRead(ref IQueryable<SGPA.Server.Models.CMU.OrigenMovimiento> items);

        partial void OnOrigenMovimientoGet(ref SingleResult<SGPA.Server.Models.CMU.OrigenMovimiento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/OrigenMovimientos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.OrigenMovimiento> GetOrigenMovimiento(int key)
        {
            var items = this.context.OrigenMovimientos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnOrigenMovimientoGet(ref result);

            return result;
        }
        partial void OnOrigenMovimientoDeleted(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoDeleted(SGPA.Server.Models.CMU.OrigenMovimiento item);

        [HttpDelete("/odata/CMU/OrigenMovimientos(Id={Id})")]
        public IActionResult DeleteOrigenMovimiento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.OrigenMovimientos
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzas)
                    .Include(i => i.MovimientoCuenta)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.OrigenMovimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnOrigenMovimientoDeleted(item);
                this.context.OrigenMovimientos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterOrigenMovimientoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOrigenMovimientoUpdated(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoUpdated(SGPA.Server.Models.CMU.OrigenMovimiento item);

        [HttpPut("/odata/CMU/OrigenMovimientos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutOrigenMovimiento(int key, [FromBody]SGPA.Server.Models.CMU.OrigenMovimiento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.OrigenMovimientos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.OrigenMovimiento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnOrigenMovimientoUpdated(item);
                this.context.OrigenMovimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OrigenMovimientos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                this.OnAfterOrigenMovimientoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/OrigenMovimientos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchOrigenMovimiento(int key, [FromBody]Delta<SGPA.Server.Models.CMU.OrigenMovimiento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.OrigenMovimientos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.OrigenMovimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnOrigenMovimientoUpdated(item);
                this.context.OrigenMovimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OrigenMovimientos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOrigenMovimientoCreated(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoCreated(SGPA.Server.Models.CMU.OrigenMovimiento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.OrigenMovimiento item)
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

                this.OnOrigenMovimientoCreated(item);
                this.context.OrigenMovimientos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OrigenMovimientos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");

                this.OnAfterOrigenMovimientoCreated(item);

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
