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
    [Route("odata/CMU/UsuarioRegionals")]
    public partial class UsuarioRegionalsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UsuarioRegionalsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.UsuarioRegional> GetUsuarioRegionals()
        {
            var items = this.context.UsuarioRegionals.AsQueryable<SGPA.Server.Models.CMU.UsuarioRegional>();
            this.OnUsuarioRegionalsRead(ref items);

            return items;
        }

        partial void OnUsuarioRegionalsRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioRegional> items);

        partial void OnUsuarioRegionalGet(ref SingleResult<SGPA.Server.Models.CMU.UsuarioRegional> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/UsuarioRegionals(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.UsuarioRegional> GetUsuarioRegional(Guid key)
        {
            var items = this.context.UsuarioRegionals.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnUsuarioRegionalGet(ref result);

            return result;
        }
        partial void OnUsuarioRegionalDeleted(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalDeleted(SGPA.Server.Models.CMU.UsuarioRegional item);

        [HttpDelete("/odata/CMU/UsuarioRegionals(Oid={Oid})")]
        public IActionResult DeleteUsuarioRegional(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.UsuarioRegionals
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioRegional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioRegionalDeleted(item);
                this.context.UsuarioRegionals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUsuarioRegionalDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioRegionalUpdated(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalUpdated(SGPA.Server.Models.CMU.UsuarioRegional item);

        [HttpPut("/odata/CMU/UsuarioRegionals(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUsuarioRegional(Guid key, [FromBody]SGPA.Server.Models.CMU.UsuarioRegional item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioRegionals
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioRegional>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUsuarioRegionalUpdated(item);
                this.context.UsuarioRegionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioRegionals.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Usuario,Regional1");
                this.OnAfterUsuarioRegionalUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/UsuarioRegionals(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUsuarioRegional(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.UsuarioRegional> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UsuarioRegionals
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UsuarioRegional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUsuarioRegionalUpdated(item);
                this.context.UsuarioRegionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioRegionals.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Usuario,Regional1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUsuarioRegionalCreated(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalCreated(SGPA.Server.Models.CMU.UsuarioRegional item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.UsuarioRegional item)
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

                this.OnUsuarioRegionalCreated(item);
                this.context.UsuarioRegionals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UsuarioRegionals.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "Usuario,Regional1");

                this.OnAfterUsuarioRegionalCreated(item);

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
