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
    [Route("odata/Sgpa/AfeccionGrupos")]
    public partial class AfeccionGruposController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AfeccionGruposController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> GetAfeccionGrupos()
        {
            var items = this.context.AfeccionGrupos.AsQueryable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>();
            this.OnAfeccionGruposRead(ref items);

            return items;
        }

        partial void OnAfeccionGruposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> items);

        partial void OnAfeccionGrupoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AfeccionGrupos(CodAfeccionGrupo={CodAfeccionGrupo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> GetAfeccionGrupo(int key)
        {
            var items = this.context.AfeccionGrupos.Where(i => i.CodAfeccionGrupo == key);
            var result = SingleResult.Create(items);

            OnAfeccionGrupoGet(ref result);

            return result;
        }
        partial void OnAfeccionGrupoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        [HttpDelete("/odata/Sgpa/AfeccionGrupos(CodAfeccionGrupo={CodAfeccionGrupo})")]
        public IActionResult DeleteAfeccionGrupo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AfeccionGrupos
                    .Where(i => i.CodAfeccionGrupo == key)
                    .Include(i => i.AfeccionTipos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfeccionGrupoDeleted(item);
                this.context.AfeccionGrupos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAfeccionGrupoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfeccionGrupoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        [HttpPut("/odata/Sgpa/AfeccionGrupos(CodAfeccionGrupo={CodAfeccionGrupo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAfeccionGrupo(int key, [FromBody]SgpaNew.Server.Models.Sgpa.AfeccionGrupo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfeccionGrupos
                    .Where(i => i.CodAfeccionGrupo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfeccionGrupoUpdated(item);
                this.context.AfeccionGrupos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionGrupos.Where(i => i.CodAfeccionGrupo == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Patologium");
                this.OnAfterAfeccionGrupoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AfeccionGrupos(CodAfeccionGrupo={CodAfeccionGrupo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAfeccionGrupo(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfeccionGrupos
                    .Where(i => i.CodAfeccionGrupo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAfeccionGrupoUpdated(item);
                this.context.AfeccionGrupos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionGrupos.Where(i => i.CodAfeccionGrupo == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Patologium");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfeccionGrupoCreated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoCreated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AfeccionGrupo item)
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

                this.OnAfeccionGrupoCreated(item);
                this.context.AfeccionGrupos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfeccionGrupos.Where(i => i.CodAfeccionGrupo == item.CodAfeccionGrupo);

                Request.QueryString = Request.QueryString.Add("$expand", "Patologium");

                this.OnAfterAfeccionGrupoCreated(item);

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
