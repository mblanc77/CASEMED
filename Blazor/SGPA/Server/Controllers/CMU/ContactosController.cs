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
    [Route("odata/CMU/Contactos")]
    public partial class ContactosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ContactosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Contacto> GetContactos()
        {
            var items = this.context.Contactos.AsQueryable<SGPA.Server.Models.CMU.Contacto>();
            this.OnContactosRead(ref items);

            return items;
        }

        partial void OnContactosRead(ref IQueryable<SGPA.Server.Models.CMU.Contacto> items);

        partial void OnContactoGet(ref SingleResult<SGPA.Server.Models.CMU.Contacto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Contactos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Contacto> GetContacto(int key)
        {
            var items = this.context.Contactos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnContactoGet(ref result);

            return result;
        }
        partial void OnContactoDeleted(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoDeleted(SGPA.Server.Models.CMU.Contacto item);

        [HttpDelete("/odata/CMU/Contactos(Id={Id})")]
        public IActionResult DeleteContacto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Contactos
                    .Where(i => i.Id == key)
                    .Include(i => i.ContactoInfoAdicionals)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Contacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactoDeleted(item);
                this.context.Contactos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactoUpdated(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoUpdated(SGPA.Server.Models.CMU.Contacto item);

        [HttpPut("/odata/CMU/Contactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContacto(int key, [FromBody]SGPA.Server.Models.CMU.Contacto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Contacto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactoUpdated(item);
                this.context.Contactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AreaContacto,CargoContacto,GrupoContacto");
                this.OnAfterContactoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Contactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContacto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Contacto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Contacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactoUpdated(item);
                this.context.Contactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AreaContacto,CargoContacto,GrupoContacto");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactoCreated(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoCreated(SGPA.Server.Models.CMU.Contacto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Contacto item)
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

                this.OnContactoCreated(item);
                this.context.Contactos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AreaContacto,CargoContacto,GrupoContacto");

                this.OnAfterContactoCreated(item);

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
