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
    [Route("odata/CMU/UsuarioInstitucions")]
    public partial class UsuarioInstitucionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UsuarioInstitucionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.UsuarioInstitucion> GetUsuarioInstitucions()
        {
            var items = this.context.UsuarioInstitucions.AsQueryable<SGPA.Server.Models.CMU.UsuarioInstitucion>();
            this.OnUsuarioInstitucionsRead(ref items);

            return items;
        }

        partial void OnUsuarioInstitucionsRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioInstitucion> items);

        partial void OnUsuarioInstitucionGet(ref SingleResult<SGPA.Server.Models.CMU.UsuarioInstitucion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/UsuarioInstitucions(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.UsuarioInstitucion> GetUsuarioInstitucion(Guid key)
        {
            var items = this.context.UsuarioInstitucions.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnUsuarioInstitucionGet(ref result);

            return result;
        }
        partial void OnUsuarioInstitucionDeleted(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionDeleted(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        [HttpDelete("/odata/CMU/UsuarioInstitucions(Oid={Oid})")]
        public IActionResult DeleteUsuarioInstitucion(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.UsuarioInstitucions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioInstitucion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioInstitucionDeleted(item);
                this.context.UsuarioInstitucions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUsuarioInstitucionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioInstitucionUpdated(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionUpdated(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        [HttpPut("/odata/CMU/UsuarioInstitucions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUsuarioInstitucion(Guid key, [FromBody]SGPA.Server.Models.CMU.UsuarioInstitucion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioInstitucions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioInstitucion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioInstitucionUpdated(item);
                this.context.UsuarioInstitucions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioInstitucions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,Usuario");
                this.OnAfterUsuarioInstitucionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/UsuarioInstitucions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUsuarioInstitucion(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.UsuarioInstitucion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioInstitucions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioInstitucion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUsuarioInstitucionUpdated(item);
                this.context.UsuarioInstitucions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioInstitucions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,Usuario");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioInstitucionCreated(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionCreated(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.UsuarioInstitucion item)
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

                this.OnUsuarioInstitucionCreated(item);
                this.context.UsuarioInstitucions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioInstitucions.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza,Usuario");

                this.OnAfterUsuarioInstitucionCreated(item);

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
