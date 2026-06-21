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
    [Route("odata/CMU/AreaContactos")]
    public partial class AreaContactosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AreaContactosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AreaContacto> GetAreaContactos()
        {
            var items = this.context.AreaContactos.AsQueryable<SGPA.Server.Models.CMU.AreaContacto>();
            this.OnAreaContactosRead(ref items);

            return items;
        }

        partial void OnAreaContactosRead(ref IQueryable<SGPA.Server.Models.CMU.AreaContacto> items);

        partial void OnAreaContactoGet(ref SingleResult<SGPA.Server.Models.CMU.AreaContacto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AreaContactos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AreaContacto> GetAreaContacto(int key)
        {
            var items = this.context.AreaContactos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAreaContactoGet(ref result);

            return result;
        }
        partial void OnAreaContactoDeleted(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoDeleted(SGPA.Server.Models.CMU.AreaContacto item);

        [HttpDelete("/odata/CMU/AreaContactos(Id={Id})")]
        public IActionResult DeleteAreaContacto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AreaContactos
                    .Where(i => i.Id == key)
                    .Include(i => i.Contactos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AreaContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAreaContactoDeleted(item);
                this.context.AreaContactos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAreaContactoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAreaContactoUpdated(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoUpdated(SGPA.Server.Models.CMU.AreaContacto item);

        [HttpPut("/odata/CMU/AreaContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAreaContacto(int key, [FromBody]SGPA.Server.Models.CMU.AreaContacto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AreaContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AreaContacto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAreaContactoUpdated(item);
                this.context.AreaContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AreaContactos.Where(i => i.Id == key);
                ;
                this.OnAfterAreaContactoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AreaContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAreaContacto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AreaContacto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AreaContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AreaContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAreaContactoUpdated(item);
                this.context.AreaContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AreaContactos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAreaContactoCreated(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoCreated(SGPA.Server.Models.CMU.AreaContacto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AreaContacto item)
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

                this.OnAreaContactoCreated(item);
                this.context.AreaContactos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AreaContactos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterAreaContactoCreated(item);

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
