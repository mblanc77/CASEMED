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
    [Route("odata/CMU/Usuarios")]
    public partial class UsuariosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UsuariosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Usuario> GetUsuarios()
        {
            var items = this.context.Usuarios.AsQueryable<SGPA.Server.Models.CMU.Usuario>();
            this.OnUsuariosRead(ref items);

            return items;
        }

        partial void OnUsuariosRead(ref IQueryable<SGPA.Server.Models.CMU.Usuario> items);

        partial void OnUsuarioGet(ref SingleResult<SGPA.Server.Models.CMU.Usuario> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Usuarios(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.Usuario> GetUsuario(Guid key)
        {
            var items = this.context.Usuarios.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnUsuarioGet(ref result);

            return result;
        }
        partial void OnUsuarioDeleted(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioDeleted(SGPA.Server.Models.CMU.Usuario item);

        [HttpDelete("/odata/CMU/Usuarios(Oid={Oid})")]
        public IActionResult DeleteUsuario(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Usuarios
                    .Where(i => i.Oid == key)
                    .Include(i => i.UsuarioAccesos)
                    .Include(i => i.UsuarioInstitucions)
                    .Include(i => i.UsuarioRegionals)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Usuario>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioDeleted(item);
                this.context.Usuarios.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUsuarioDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioUpdated(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioUpdated(SGPA.Server.Models.CMU.Usuario item);

        [HttpPut("/odata/CMU/Usuarios(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUsuario(Guid key, [FromBody]SGPA.Server.Models.CMU.Usuario item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Usuarios
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Usuario>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioUpdated(item);
                this.context.Usuarios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Usuarios.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemUser");
                this.OnAfterUsuarioUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Usuarios(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUsuario(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.Usuario> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Usuarios
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Usuario>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUsuarioUpdated(item);
                this.context.Usuarios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Usuarios.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemUser");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioCreated(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioCreated(SGPA.Server.Models.CMU.Usuario item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Usuario item)
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

                this.OnUsuarioCreated(item);
                this.context.Usuarios.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Usuarios.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemUser");

                this.OnAfterUsuarioCreated(item);

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
