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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/AfeccionTipos")]
    public partial class AfeccionTiposController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AfeccionTiposController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AfeccionTipo> GetAfeccionTipos()
        {
            var items = this.context.AfeccionTipos.AsQueryable<SgpaNew.Server.Models.Sgpa.AfeccionTipo>();
            this.OnAfeccionTiposRead(ref items);

            return items;
        }

        partial void OnAfeccionTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionTipo> items);

        partial void OnAfeccionTipoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AfeccionTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AfeccionTipos(CodAfeccionTipo={CodAfeccionTipo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AfeccionTipo> GetAfeccionTipo(short key)
        {
            var items = this.context.AfeccionTipos.Where(i => i.CodAfeccionTipo == key);
            var result = SingleResult.Create(items);

            OnAfeccionTipoGet(ref result);

            return result;
        }
        partial void OnAfeccionTipoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        [HttpDelete("/odata/Sgpa/AfeccionTipos(CodAfeccionTipo={CodAfeccionTipo})")]
        public IActionResult DeleteAfeccionTipo(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AfeccionTipos
                    .Where(i => i.CodAfeccionTipo == key)
                    .Include(i => i.Certificacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfeccionTipoDeleted(item);
                this.context.AfeccionTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAfeccionTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfeccionTipoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        [HttpPut("/odata/Sgpa/AfeccionTipos(CodAfeccionTipo={CodAfeccionTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAfeccionTipo(short key, [FromBody]SgpaNew.Server.Models.Sgpa.AfeccionTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfeccionTipos
                    .Where(i => i.CodAfeccionTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfeccionTipoUpdated(item);
                this.context.AfeccionTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionTipos.Where(i => i.CodAfeccionTipo == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AfeccionGrupo");
                this.OnAfterAfeccionTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AfeccionTipos(CodAfeccionTipo={CodAfeccionTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAfeccionTipo(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AfeccionTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfeccionTipos
                    .Where(i => i.CodAfeccionTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAfeccionTipoUpdated(item);
                this.context.AfeccionTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionTipos.Where(i => i.CodAfeccionTipo == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AfeccionGrupo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfeccionTipoCreated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoCreated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AfeccionTipo item)
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

                this.OnAfeccionTipoCreated(item);
                this.context.AfeccionTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionTipos.Where(i => i.CodAfeccionTipo == item.CodAfeccionTipo);

                Request.QueryString = Request.QueryString.Add("$expand", "AfeccionGrupo");

                this.OnAfterAfeccionTipoCreated(item);

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
