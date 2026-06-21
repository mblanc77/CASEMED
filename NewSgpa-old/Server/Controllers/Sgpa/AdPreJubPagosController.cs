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
    [Route("odata/Sgpa/AdPreJubPagos")]
    public partial class AdPreJubPagosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AdPreJubPagosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AdPreJubPago> GetAdPreJubPagos()
        {
            var items = this.context.AdPreJubPagos.AsQueryable<SgpaNew.Server.Models.Sgpa.AdPreJubPago>();
            this.OnAdPreJubPagosRead(ref items);

            return items;
        }

        partial void OnAdPreJubPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJubPago> items);

        partial void OnAdPreJubPagoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AdPreJubPago> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AdPreJubPagos(AdPreJubPagoId={AdPreJubPagoId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AdPreJubPago> GetAdPreJubPago(int key)
        {
            var items = this.context.AdPreJubPagos.Where(i => i.AdPreJubPagoId == key);
            var result = SingleResult.Create(items);

            OnAdPreJubPagoGet(ref result);

            return result;
        }
        partial void OnAdPreJubPagoDeleted(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoDeleted(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        [HttpDelete("/odata/Sgpa/AdPreJubPagos(AdPreJubPagoId={AdPreJubPagoId})")]
        public IActionResult DeleteAdPreJubPago(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AdPreJubPagos
                    .Where(i => i.AdPreJubPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJubPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAdPreJubPagoDeleted(item);
                this.context.AdPreJubPagos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAdPreJubPagoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdPreJubPagoUpdated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoUpdated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        [HttpPut("/odata/Sgpa/AdPreJubPagos(AdPreJubPagoId={AdPreJubPagoId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAdPreJubPago(int key, [FromBody]SgpaNew.Server.Models.Sgpa.AdPreJubPago item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AdPreJubPagos
                    .Where(i => i.AdPreJubPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJubPago>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAdPreJubPagoUpdated(item);
                this.context.AdPreJubPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubPagos.Where(i => i.AdPreJubPagoId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AdPreJub");
                this.OnAfterAdPreJubPagoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AdPreJubPagos(AdPreJubPagoId={AdPreJubPagoId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAdPreJubPago(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AdPreJubPago> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AdPreJubPagos
                    .Where(i => i.AdPreJubPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJubPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAdPreJubPagoUpdated(item);
                this.context.AdPreJubPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubPagos.Where(i => i.AdPreJubPagoId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AdPreJub");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdPreJubPagoCreated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoCreated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AdPreJubPago item)
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

                this.OnAdPreJubPagoCreated(item);
                this.context.AdPreJubPagos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubPagos.Where(i => i.AdPreJubPagoId == item.AdPreJubPagoId);

                Request.QueryString = Request.QueryString.Add("$expand", "AdPreJub");

                this.OnAfterAdPreJubPagoCreated(item);

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
