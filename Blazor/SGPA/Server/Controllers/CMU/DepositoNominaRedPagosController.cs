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
    [Route("odata/CMU/DepositoNominaRedPagos")]
    public partial class DepositoNominaRedPagosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepositoNominaRedPagosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DepositoNominaRedPago> GetDepositoNominaRedPagos()
        {
            var items = this.context.DepositoNominaRedPagos.AsQueryable<SGPA.Server.Models.CMU.DepositoNominaRedPago>();
            this.OnDepositoNominaRedPagosRead(ref items);

            return items;
        }

        partial void OnDepositoNominaRedPagosRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaRedPago> items);

        partial void OnDepositoNominaRedPagoGet(ref SingleResult<SGPA.Server.Models.CMU.DepositoNominaRedPago> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DepositoNominaRedPagos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DepositoNominaRedPago> GetDepositoNominaRedPago(int key)
        {
            var items = this.context.DepositoNominaRedPagos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepositoNominaRedPagoGet(ref result);

            return result;
        }
        partial void OnDepositoNominaRedPagoDeleted(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoDeleted(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        [HttpDelete("/odata/CMU/DepositoNominaRedPagos(Id={Id})")]
        public IActionResult DeleteDepositoNominaRedPago(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DepositoNominaRedPagos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaRedPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaRedPagoDeleted(item);
                this.context.DepositoNominaRedPagos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepositoNominaRedPagoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaRedPagoUpdated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoUpdated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        [HttpPut("/odata/CMU/DepositoNominaRedPagos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepositoNominaRedPago(int key, [FromBody]SGPA.Server.Models.CMU.DepositoNominaRedPago item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaRedPagos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaRedPago>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaRedPagoUpdated(item);
                this.context.DepositoNominaRedPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaRedPagos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");
                this.OnAfterDepositoNominaRedPagoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DepositoNominaRedPagos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepositoNominaRedPago(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DepositoNominaRedPago> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaRedPagos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaRedPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepositoNominaRedPagoUpdated(item);
                this.context.DepositoNominaRedPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaRedPagos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaRedPagoCreated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoCreated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DepositoNominaRedPago item)
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

                this.OnDepositoNominaRedPagoCreated(item);
                this.context.DepositoNominaRedPagos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaRedPagos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");

                this.OnAfterDepositoNominaRedPagoCreated(item);

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
