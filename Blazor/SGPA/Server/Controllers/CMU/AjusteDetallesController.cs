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
    [Route("odata/CMU/AjusteDetalles")]
    public partial class AjusteDetallesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AjusteDetallesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AjusteDetalle> GetAjusteDetalles()
        {
            var items = this.context.AjusteDetalles.AsQueryable<SGPA.Server.Models.CMU.AjusteDetalle>();
            this.OnAjusteDetallesRead(ref items);

            return items;
        }

        partial void OnAjusteDetallesRead(ref IQueryable<SGPA.Server.Models.CMU.AjusteDetalle> items);

        partial void OnAjusteDetalleGet(ref SingleResult<SGPA.Server.Models.CMU.AjusteDetalle> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AjusteDetalles(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AjusteDetalle> GetAjusteDetalle(int key)
        {
            var items = this.context.AjusteDetalles.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAjusteDetalleGet(ref result);

            return result;
        }
        partial void OnAjusteDetalleDeleted(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleDeleted(SGPA.Server.Models.CMU.AjusteDetalle item);

        [HttpDelete("/odata/CMU/AjusteDetalles(Id={Id})")]
        public IActionResult DeleteAjusteDetalle(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AjusteDetalles
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteDetalle>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAjusteDetalleDeleted(item);
                this.context.AjusteDetalles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAjusteDetalleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAjusteDetalleUpdated(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleUpdated(SGPA.Server.Models.CMU.AjusteDetalle item);

        [HttpPut("/odata/CMU/AjusteDetalles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAjusteDetalle(int key, [FromBody]SGPA.Server.Models.CMU.AjusteDetalle item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AjusteDetalles
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteDetalle>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAjusteDetalleUpdated(item);
                this.context.AjusteDetalles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteDetalles.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo");
                this.OnAfterAjusteDetalleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AjusteDetalles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAjusteDetalle(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AjusteDetalle> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AjusteDetalles
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AjusteDetalle>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAjusteDetalleUpdated(item);
                this.context.AjusteDetalles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteDetalles.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAjusteDetalleCreated(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleCreated(SGPA.Server.Models.CMU.AjusteDetalle item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AjusteDetalle item)
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

                this.OnAjusteDetalleCreated(item);
                this.context.AjusteDetalles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AjusteDetalles.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AjusteRetroactivo");

                this.OnAfterAjusteDetalleCreated(item);

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
