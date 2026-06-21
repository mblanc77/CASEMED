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
    [Route("odata/Sgpa/Prestacions")]
    public partial class PrestacionsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public PrestacionsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Prestacion> GetPrestacions()
        {
            var items = this.context.Prestacions.AsQueryable<SgpaNew.Server.Models.Sgpa.Prestacion>();
            this.OnPrestacionsRead(ref items);

            return items;
        }

        partial void OnPrestacionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Prestacion> items);

        partial void OnPrestacionGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Prestacion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Prestacions(PrestacionId={PrestacionId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Prestacion> GetPrestacion(int key)
        {
            var items = this.context.Prestacions.Where(i => i.PrestacionId == key);
            var result = SingleResult.Create(items);

            OnPrestacionGet(ref result);

            return result;
        }
        partial void OnPrestacionDeleted(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionDeleted(SgpaNew.Server.Models.Sgpa.Prestacion item);

        [HttpDelete("/odata/Sgpa/Prestacions(PrestacionId={PrestacionId})")]
        public IActionResult DeletePrestacion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Prestacions
                    .Where(i => i.PrestacionId == key)
                    .Include(i => i.Receta)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Prestacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrestacionDeleted(item);
                this.context.Prestacions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPrestacionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrestacionUpdated(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionUpdated(SgpaNew.Server.Models.Sgpa.Prestacion item);

        [HttpPut("/odata/Sgpa/Prestacions(PrestacionId={PrestacionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPrestacion(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Prestacion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Prestacions
                    .Where(i => i.PrestacionId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Prestacion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrestacionUpdated(item);
                this.context.Prestacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Prestacions.Where(i => i.PrestacionId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,PrestacionTipo,Monedum");
                this.OnAfterPrestacionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Prestacions(PrestacionId={PrestacionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPrestacion(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Prestacion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Prestacions
                    .Where(i => i.PrestacionId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Prestacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPrestacionUpdated(item);
                this.context.Prestacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Prestacions.Where(i => i.PrestacionId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,PrestacionTipo,Monedum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrestacionCreated(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionCreated(SgpaNew.Server.Models.Sgpa.Prestacion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Prestacion item)
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

                this.OnPrestacionCreated(item);
                this.context.Prestacions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Prestacions.Where(i => i.PrestacionId == item.PrestacionId);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,PrestacionTipo,Monedum");

                this.OnAfterPrestacionCreated(item);

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
