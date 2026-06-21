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
    [Route("odata/CMU/GrupoContactos")]
    public partial class GrupoContactosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public GrupoContactosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.GrupoContacto> GetGrupoContactos()
        {
            var items = this.context.GrupoContactos.AsQueryable<SGPA.Server.Models.CMU.GrupoContacto>();
            this.OnGrupoContactosRead(ref items);

            return items;
        }

        partial void OnGrupoContactosRead(ref IQueryable<SGPA.Server.Models.CMU.GrupoContacto> items);

        partial void OnGrupoContactoGet(ref SingleResult<SGPA.Server.Models.CMU.GrupoContacto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/GrupoContactos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.GrupoContacto> GetGrupoContacto(int key)
        {
            var items = this.context.GrupoContactos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnGrupoContactoGet(ref result);

            return result;
        }
        partial void OnGrupoContactoDeleted(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoDeleted(SGPA.Server.Models.CMU.GrupoContacto item);

        [HttpDelete("/odata/CMU/GrupoContactos(Id={Id})")]
        public IActionResult DeleteGrupoContacto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.GrupoContactos
                    .Where(i => i.Id == key)
                    .Include(i => i.Contactos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGrupoContactoDeleted(item);
                this.context.GrupoContactos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterGrupoContactoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGrupoContactoUpdated(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoUpdated(SGPA.Server.Models.CMU.GrupoContacto item);

        [HttpPut("/odata/CMU/GrupoContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutGrupoContacto(int key, [FromBody]SGPA.Server.Models.CMU.GrupoContacto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.GrupoContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoContacto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnGrupoContactoUpdated(item);
                this.context.GrupoContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoContactos.Where(i => i.Id == key);
                ;
                this.OnAfterGrupoContactoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/GrupoContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchGrupoContacto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.GrupoContacto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.GrupoContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.GrupoContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnGrupoContactoUpdated(item);
                this.context.GrupoContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoContactos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnGrupoContactoCreated(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoCreated(SGPA.Server.Models.CMU.GrupoContacto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.GrupoContacto item)
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

                this.OnGrupoContactoCreated(item);
                this.context.GrupoContactos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.GrupoContactos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterGrupoContactoCreated(item);

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
