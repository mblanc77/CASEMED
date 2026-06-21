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
    [Route("odata/CMU/CuentaBancaria")]
    public partial class CuentaBancariaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CuentaBancariaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CuentaBancarium> GetCuentaBancaria()
        {
            var items = this.context.CuentaBancaria.AsQueryable<SGPA.Server.Models.CMU.CuentaBancarium>();
            this.OnCuentaBancariaRead(ref items);

            return items;
        }

        partial void OnCuentaBancariaRead(ref IQueryable<SGPA.Server.Models.CMU.CuentaBancarium> items);

        partial void OnCuentaBancariumGet(ref SingleResult<SGPA.Server.Models.CMU.CuentaBancarium> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CuentaBancaria(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.CuentaBancarium> GetCuentaBancarium(int key)
        {
            var items = this.context.CuentaBancaria.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCuentaBancariumGet(ref result);

            return result;
        }
        partial void OnCuentaBancariumDeleted(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumDeleted(SGPA.Server.Models.CMU.CuentaBancarium item);

        [HttpDelete("/odata/CMU/CuentaBancaria(Id={Id})")]
        public IActionResult DeleteCuentaBancarium(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CuentaBancaria
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzas)
                    .Include(i => i.Cobros)
                    .Include(i => i.RegionalregionalesCuentabancariacuentabancaria)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CuentaBancarium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCuentaBancariumDeleted(item);
                this.context.CuentaBancaria.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCuentaBancariumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCuentaBancariumUpdated(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumUpdated(SGPA.Server.Models.CMU.CuentaBancarium item);

        [HttpPut("/odata/CMU/CuentaBancaria(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCuentaBancarium(int key, [FromBody]SGPA.Server.Models.CMU.CuentaBancarium item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CuentaBancaria
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CuentaBancarium>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCuentaBancariumUpdated(item);
                this.context.CuentaBancaria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CuentaBancaria.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Banco1");
                this.OnAfterCuentaBancariumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CuentaBancaria(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCuentaBancarium(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CuentaBancarium> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CuentaBancaria
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CuentaBancarium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCuentaBancariumUpdated(item);
                this.context.CuentaBancaria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CuentaBancaria.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Banco1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCuentaBancariumCreated(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumCreated(SGPA.Server.Models.CMU.CuentaBancarium item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CuentaBancarium item)
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

                this.OnCuentaBancariumCreated(item);
                this.context.CuentaBancaria.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CuentaBancaria.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Banco1");

                this.OnAfterCuentaBancariumCreated(item);

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
