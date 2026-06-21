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
    [Route("odata/CMU/RegionalregionalesCuentabancariacuentabancaria")]
    public partial class RegionalregionalesCuentabancariacuentabancariaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RegionalregionalesCuentabancariacuentabancariaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> GetRegionalregionalesCuentabancariacuentabancaria()
        {
            var items = this.context.RegionalregionalesCuentabancariacuentabancaria.AsQueryable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>();
            this.OnRegionalregionalesCuentabancariacuentabancariaRead(ref items);

            return items;
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaRead(ref IQueryable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> items);

        partial void OnRegionalregionalesCuentabancariacuentabancariaGet(ref SingleResult<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/RegionalregionalesCuentabancariacuentabancaria(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> GetRegionalregionalesCuentabancariacuentabancaria(int key)
        {
            var items = this.context.RegionalregionalesCuentabancariacuentabancaria.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnRegionalregionalesCuentabancariacuentabancariaGet(ref result);

            return result;
        }
        partial void OnRegionalregionalesCuentabancariacuentabancariaDeleted(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaDeleted(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        [HttpDelete("/odata/CMU/RegionalregionalesCuentabancariacuentabancaria(OID={OID})")]
        public IActionResult DeleteRegionalregionalesCuentabancariacuentabancaria(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegionalregionalesCuentabancariacuentabancaria
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionalregionalesCuentabancariacuentabancariaDeleted(item);
                this.context.RegionalregionalesCuentabancariacuentabancaria.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegionalregionalesCuentabancariacuentabancariaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaUpdated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaUpdated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        [HttpPut("/odata/CMU/RegionalregionalesCuentabancariacuentabancaria(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegionalregionalesCuentabancariacuentabancaria(int key, [FromBody]SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegionalregionalesCuentabancariacuentabancaria
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionalregionalesCuentabancariacuentabancariaUpdated(item);
                this.context.RegionalregionalesCuentabancariacuentabancaria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegionalregionalesCuentabancariacuentabancaria.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CuentaBancarium,Regional");
                this.OnAfterRegionalregionalesCuentabancariacuentabancariaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/RegionalregionalesCuentabancariacuentabancaria(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegionalregionalesCuentabancariacuentabancaria(int key, [FromBody]Delta<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegionalregionalesCuentabancariacuentabancaria
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegionalregionalesCuentabancariacuentabancariaUpdated(item);
                this.context.RegionalregionalesCuentabancariacuentabancaria.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegionalregionalesCuentabancariacuentabancaria.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CuentaBancarium,Regional");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaCreated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaCreated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item)
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

                this.OnRegionalregionalesCuentabancariacuentabancariaCreated(item);
                this.context.RegionalregionalesCuentabancariacuentabancaria.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegionalregionalesCuentabancariacuentabancaria.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "CuentaBancarium,Regional");

                this.OnAfterRegionalregionalesCuentabancariacuentabancariaCreated(item);

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
