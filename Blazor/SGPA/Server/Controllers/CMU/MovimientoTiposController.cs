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
    [Route("odata/CMU/MovimientoTipos")]
    public partial class MovimientoTiposController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MovimientoTiposController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MovimientoTipo> GetMovimientoTipos()
        {
            var items = this.context.MovimientoTipos.AsQueryable<SGPA.Server.Models.CMU.MovimientoTipo>();
            this.OnMovimientoTiposRead(ref items);

            return items;
        }

        partial void OnMovimientoTiposRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoTipo> items);

        partial void OnMovimientoTipoGet(ref SingleResult<SGPA.Server.Models.CMU.MovimientoTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MovimientoTipos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.MovimientoTipo> GetMovimientoTipo(int key)
        {
            var items = this.context.MovimientoTipos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnMovimientoTipoGet(ref result);

            return result;
        }
        partial void OnMovimientoTipoDeleted(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoDeleted(SGPA.Server.Models.CMU.MovimientoTipo item);

        [HttpDelete("/odata/CMU/MovimientoTipos(Id={Id})")]
        public IActionResult DeleteMovimientoTipo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MovimientoTipos
                    .Where(i => i.Id == key)
                    .Include(i => i.MovimientoCuenta)
                    .Include(i => i.Parametros)
                    .Include(i => i.Parametros1)
                    .Include(i => i.Parametros2)
                    .Include(i => i.Parametros3)
                    .Include(i => i.RolrolesMovimientotipomovimientostipos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoTipoDeleted(item);
                this.context.MovimientoTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMovimientoTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoTipoUpdated(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoUpdated(SGPA.Server.Models.CMU.MovimientoTipo item);

        [HttpPut("/odata/CMU/MovimientoTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMovimientoTipo(int key, [FromBody]SGPA.Server.Models.CMU.MovimientoTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoTipoUpdated(item);
                this.context.MovimientoTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoTipos.Where(i => i.Id == key);
                ;
                this.OnAfterMovimientoTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MovimientoTipos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMovimientoTipo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MovimientoTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoTipos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMovimientoTipoUpdated(item);
                this.context.MovimientoTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoTipos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoTipoCreated(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoCreated(SGPA.Server.Models.CMU.MovimientoTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MovimientoTipo item)
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

                this.OnMovimientoTipoCreated(item);
                this.context.MovimientoTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoTipos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterMovimientoTipoCreated(item);

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
