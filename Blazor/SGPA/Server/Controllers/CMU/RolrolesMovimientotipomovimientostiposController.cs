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
    [Route("odata/CMU/RolrolesMovimientotipomovimientostipos")]
    public partial class RolrolesMovimientotipomovimientostiposController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RolrolesMovimientotipomovimientostiposController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> GetRolrolesMovimientotipomovimientostipos()
        {
            var items = this.context.RolrolesMovimientotipomovimientostipos.AsQueryable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>();
            this.OnRolrolesMovimientotipomovimientostiposRead(ref items);

            return items;
        }

        partial void OnRolrolesMovimientotipomovimientostiposRead(ref IQueryable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> items);

        partial void OnRolrolesMovimientotipomovimientostipoGet(ref SingleResult<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/RolrolesMovimientotipomovimientostipos(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> GetRolrolesMovimientotipomovimientostipo(int key)
        {
            var items = this.context.RolrolesMovimientotipomovimientostipos.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnRolrolesMovimientotipomovimientostipoGet(ref result);

            return result;
        }
        partial void OnRolrolesMovimientotipomovimientostipoDeleted(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoDeleted(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        [HttpDelete("/odata/CMU/RolrolesMovimientotipomovimientostipos(OID={OID})")]
        public IActionResult DeleteRolrolesMovimientotipomovimientostipo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RolrolesMovimientotipomovimientostipos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRolrolesMovimientotipomovimientostipoDeleted(item);
                this.context.RolrolesMovimientotipomovimientostipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRolrolesMovimientotipomovimientostipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRolrolesMovimientotipomovimientostipoUpdated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoUpdated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        [HttpPut("/odata/CMU/RolrolesMovimientotipomovimientostipos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRolrolesMovimientotipomovimientostipo(int key, [FromBody]SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RolrolesMovimientotipomovimientostipos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRolrolesMovimientotipomovimientostipoUpdated(item);
                this.context.RolrolesMovimientotipomovimientostipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RolrolesMovimientotipomovimientostipos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoTipo,Rol");
                this.OnAfterRolrolesMovimientotipomovimientostipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/RolrolesMovimientotipomovimientostipos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRolrolesMovimientotipomovimientostipo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RolrolesMovimientotipomovimientostipos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRolrolesMovimientotipomovimientostipoUpdated(item);
                this.context.RolrolesMovimientotipomovimientostipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RolrolesMovimientotipomovimientostipos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoTipo,Rol");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRolrolesMovimientotipomovimientostipoCreated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoCreated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item)
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

                this.OnRolrolesMovimientotipomovimientostipoCreated(item);
                this.context.RolrolesMovimientotipomovimientostipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RolrolesMovimientotipomovimientostipos.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoTipo,Rol");

                this.OnAfterRolrolesMovimientotipomovimientostipoCreated(item);

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
