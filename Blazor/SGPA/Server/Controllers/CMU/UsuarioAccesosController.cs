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
    [Route("odata/CMU/UsuarioAccesos")]
    public partial class UsuarioAccesosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UsuarioAccesosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.UsuarioAcceso> GetUsuarioAccesos()
        {
            var items = this.context.UsuarioAccesos.AsQueryable<SGPA.Server.Models.CMU.UsuarioAcceso>();
            this.OnUsuarioAccesosRead(ref items);

            return items;
        }

        partial void OnUsuarioAccesosRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioAcceso> items);

        partial void OnUsuarioAccesoGet(ref SingleResult<SGPA.Server.Models.CMU.UsuarioAcceso> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/UsuarioAccesos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.UsuarioAcceso> GetUsuarioAcceso(int key)
        {
            var items = this.context.UsuarioAccesos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnUsuarioAccesoGet(ref result);

            return result;
        }
        partial void OnUsuarioAccesoDeleted(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoDeleted(SGPA.Server.Models.CMU.UsuarioAcceso item);

        [HttpDelete("/odata/CMU/UsuarioAccesos(Id={Id})")]
        public IActionResult DeleteUsuarioAcceso(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.UsuarioAccesos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioAcceso>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioAccesoDeleted(item);
                this.context.UsuarioAccesos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUsuarioAccesoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioAccesoUpdated(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoUpdated(SGPA.Server.Models.CMU.UsuarioAcceso item);

        [HttpPut("/odata/CMU/UsuarioAccesos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUsuarioAcceso(int key, [FromBody]SGPA.Server.Models.CMU.UsuarioAcceso item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioAccesos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioAcceso>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioAccesoUpdated(item);
                this.context.UsuarioAccesos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioAccesos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Usuario1");
                this.OnAfterUsuarioAccesoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/UsuarioAccesos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUsuarioAcceso(int key, [FromBody]Delta<SGPA.Server.Models.CMU.UsuarioAcceso> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioAccesos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioAcceso>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUsuarioAccesoUpdated(item);
                this.context.UsuarioAccesos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioAccesos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Usuario1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioAccesoCreated(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoCreated(SGPA.Server.Models.CMU.UsuarioAcceso item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.UsuarioAcceso item)
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

                this.OnUsuarioAccesoCreated(item);
                this.context.UsuarioAccesos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioAccesos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Usuario1");

                this.OnAfterUsuarioAccesoCreated(item);

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
