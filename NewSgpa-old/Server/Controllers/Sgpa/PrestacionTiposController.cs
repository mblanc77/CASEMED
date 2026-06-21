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
    [Route("odata/Sgpa/PrestacionTipos")]
    public partial class PrestacionTiposController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public PrestacionTiposController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.PrestacionTipo> GetPrestacionTipos()
        {
            var items = this.context.PrestacionTipos.AsQueryable<SgpaNew.Server.Models.Sgpa.PrestacionTipo>();
            this.OnPrestacionTiposRead(ref items);

            return items;
        }

        partial void OnPrestacionTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrestacionTipo> items);

        partial void OnPrestacionTipoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.PrestacionTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/PrestacionTipos(CodPrestacionTipo={CodPrestacionTipo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.PrestacionTipo> GetPrestacionTipo(short key)
        {
            var items = this.context.PrestacionTipos.Where(i => i.CodPrestacionTipo == key);
            var result = SingleResult.Create(items);

            OnPrestacionTipoGet(ref result);

            return result;
        }
        partial void OnPrestacionTipoDeleted(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoDeleted(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        [HttpDelete("/odata/Sgpa/PrestacionTipos(CodPrestacionTipo={CodPrestacionTipo})")]
        public IActionResult DeletePrestacionTipo(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.PrestacionTipos
                    .Where(i => i.CodPrestacionTipo == key)
                    .Include(i => i.Prestacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrestacionTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrestacionTipoDeleted(item);
                this.context.PrestacionTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPrestacionTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrestacionTipoUpdated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoUpdated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        [HttpPut("/odata/Sgpa/PrestacionTipos(CodPrestacionTipo={CodPrestacionTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPrestacionTipo(short key, [FromBody]SgpaNew.Server.Models.Sgpa.PrestacionTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PrestacionTipos
                    .Where(i => i.CodPrestacionTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrestacionTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrestacionTipoUpdated(item);
                this.context.PrestacionTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrestacionTipos.Where(i => i.CodPrestacionTipo == key);
                ;
                this.OnAfterPrestacionTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/PrestacionTipos(CodPrestacionTipo={CodPrestacionTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPrestacionTipo(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.PrestacionTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PrestacionTipos
                    .Where(i => i.CodPrestacionTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrestacionTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPrestacionTipoUpdated(item);
                this.context.PrestacionTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrestacionTipos.Where(i => i.CodPrestacionTipo == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrestacionTipoCreated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoCreated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.PrestacionTipo item)
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

                this.OnPrestacionTipoCreated(item);
                this.context.PrestacionTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrestacionTipos.Where(i => i.CodPrestacionTipo == item.CodPrestacionTipo);

                ;

                this.OnAfterPrestacionTipoCreated(item);

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
