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
    [Route("odata/CMU/Rols")]
    public partial class RolsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RolsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Rol> GetRols()
        {
            var items = this.context.Rols.AsQueryable<SGPA.Server.Models.CMU.Rol>();
            this.OnRolsRead(ref items);

            return items;
        }

        partial void OnRolsRead(ref IQueryable<SGPA.Server.Models.CMU.Rol> items);

        partial void OnRolGet(ref SingleResult<SGPA.Server.Models.CMU.Rol> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Rols(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.Rol> GetRol(Guid key)
        {
            var items = this.context.Rols.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnRolGet(ref result);

            return result;
        }
        partial void OnRolDeleted(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolDeleted(SGPA.Server.Models.CMU.Rol item);

        [HttpDelete("/odata/CMU/Rols(Oid={Oid})")]
        public IActionResult DeleteRol(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Rols
                    .Where(i => i.Oid == key)
                    .Include(i => i.RolrolesMovimientotipomovimientostipos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Rol>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRolDeleted(item);
                this.context.Rols.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRolDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRolUpdated(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolUpdated(SGPA.Server.Models.CMU.Rol item);

        [HttpPut("/odata/CMU/Rols(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRol(Guid key, [FromBody]SGPA.Server.Models.CMU.Rol item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Rols
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Rol>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRolUpdated(item);
                this.context.Rols.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Rols.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole");
                this.OnAfterRolUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Rols(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRol(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.Rol> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Rols
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Rol>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRolUpdated(item);
                this.context.Rols.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Rols.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRolCreated(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolCreated(SGPA.Server.Models.CMU.Rol item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Rol item)
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

                this.OnRolCreated(item);
                this.context.Rols.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Rols.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole");

                this.OnAfterRolCreated(item);

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
