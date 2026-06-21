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
    [Route("odata/Sgpa/Trabajas")]
    public partial class TrabajasController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public TrabajasController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Trabaja> GetTrabajas()
        {
            var items = this.context.Trabajas.AsQueryable<SgpaNew.Server.Models.Sgpa.Trabaja>();
            this.OnTrabajasRead(ref items);

            return items;
        }

        partial void OnTrabajasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Trabaja> items);

        partial void OnTrabajaGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Trabaja> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Trabajas(IdTrabaja={IdTrabaja})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Trabaja> GetTrabaja(int key)
        {
            var items = this.context.Trabajas.Where(i => i.IdTrabaja == key);
            var result = SingleResult.Create(items);

            OnTrabajaGet(ref result);

            return result;
        }
        partial void OnTrabajaDeleted(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaDeleted(SgpaNew.Server.Models.Sgpa.Trabaja item);

        [HttpDelete("/odata/Sgpa/Trabajas(IdTrabaja={IdTrabaja})")]
        public IActionResult DeleteTrabaja(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Trabajas
                    .Where(i => i.IdTrabaja == key)
                    .Include(i => i.Imponibles)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Trabaja>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTrabajaDeleted(item);
                this.context.Trabajas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTrabajaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTrabajaUpdated(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaUpdated(SgpaNew.Server.Models.Sgpa.Trabaja item);

        [HttpPut("/odata/Sgpa/Trabajas(IdTrabaja={IdTrabaja})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTrabaja(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Trabaja item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Trabajas
                    .Where(i => i.IdTrabaja == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Trabaja>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTrabajaUpdated(item);
                this.context.Trabajas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trabajas.Where(i => i.IdTrabaja == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,BajaMotivo,Empresa");
                this.OnAfterTrabajaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Trabajas(IdTrabaja={IdTrabaja})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTrabaja(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Trabaja> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Trabajas
                    .Where(i => i.IdTrabaja == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Trabaja>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTrabajaUpdated(item);
                this.context.Trabajas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trabajas.Where(i => i.IdTrabaja == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,BajaMotivo,Empresa");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTrabajaCreated(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaCreated(SgpaNew.Server.Models.Sgpa.Trabaja item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Trabaja item)
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

                this.OnTrabajaCreated(item);
                this.context.Trabajas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Trabajas.Where(i => i.IdTrabaja == item.IdTrabaja);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,BajaMotivo,Empresa");

                this.OnAfterTrabajaCreated(item);

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
