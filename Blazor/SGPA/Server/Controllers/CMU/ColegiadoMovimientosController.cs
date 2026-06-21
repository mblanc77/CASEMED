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
    [Route("odata/CMU/ColegiadoMovimientos")]
    public partial class ColegiadoMovimientosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoMovimientosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoMovimiento> GetColegiadoMovimientos()
        {
            var items = this.context.ColegiadoMovimientos.AsQueryable<SGPA.Server.Models.CMU.ColegiadoMovimiento>();
            this.OnColegiadoMovimientosRead(ref items);

            return items;
        }

        partial void OnColegiadoMovimientosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoMovimiento> items);

        partial void OnColegiadoMovimientoGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoMovimiento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoMovimientos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoMovimiento> GetColegiadoMovimiento(int key)
        {
            var items = this.context.ColegiadoMovimientos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoMovimientoGet(ref result);

            return result;
        }
        partial void OnColegiadoMovimientoDeleted(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoDeleted(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        [HttpDelete("/odata/CMU/ColegiadoMovimientos(Id={Id})")]
        public IActionResult DeleteColegiadoMovimiento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoMovimientos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoMovimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoMovimientoDeleted(item);
                this.context.ColegiadoMovimientos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoMovimientoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoMovimientoUpdated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoUpdated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        [HttpPut("/odata/CMU/ColegiadoMovimientos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoMovimiento(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoMovimiento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoMovimientos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoMovimiento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoMovimientoUpdated(item);
                this.context.ColegiadoMovimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoMovimientos.Where(i => i.Id == key);
                ;
                this.OnAfterColegiadoMovimientoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoMovimientos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoMovimiento(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoMovimiento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoMovimientos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoMovimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoMovimientoUpdated(item);
                this.context.ColegiadoMovimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoMovimientos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoMovimientoCreated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoCreated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoMovimiento item)
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

                this.OnColegiadoMovimientoCreated(item);
                this.context.ColegiadoMovimientos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoMovimientos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterColegiadoMovimientoCreated(item);

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
