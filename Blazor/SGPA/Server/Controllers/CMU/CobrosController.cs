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
    [Route("odata/CMU/Cobros")]
    public partial class CobrosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CobrosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Cobro> GetCobros()
        {
            var items = this.context.Cobros.AsQueryable<SGPA.Server.Models.CMU.Cobro>();
            this.OnCobrosRead(ref items);

            return items;
        }

        partial void OnCobrosRead(ref IQueryable<SGPA.Server.Models.CMU.Cobro> items);

        partial void OnCobroGet(ref SingleResult<SGPA.Server.Models.CMU.Cobro> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Cobros(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Cobro> GetCobro(int key)
        {
            var items = this.context.Cobros.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCobroGet(ref result);

            return result;
        }
        partial void OnCobroDeleted(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroDeleted(SGPA.Server.Models.CMU.Cobro item);

        [HttpDelete("/odata/CMU/Cobros(Id={Id})")]
        public IActionResult DeleteCobro(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Cobros
                    .Where(i => i.Id == key)
                    .Include(i => i.CobroNominas)
                    .Include(i => i.Debitos)
                    .Include(i => i.Depositos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cobro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCobroDeleted(item);
                this.context.Cobros.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCobroDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCobroUpdated(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroUpdated(SGPA.Server.Models.CMU.Cobro item);

        [HttpPut("/odata/CMU/Cobros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCobro(int key, [FromBody]SGPA.Server.Models.CMU.Cobro item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cobros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cobro>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCobroUpdated(item);
                this.context.Cobros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cobros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza1,CuentaBancarium,XpobjectType");
                this.OnAfterCobroUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Cobros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCobro(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Cobro> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cobros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cobro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCobroUpdated(item);
                this.context.Cobros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cobros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza1,CuentaBancarium,XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCobroCreated(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroCreated(SGPA.Server.Models.CMU.Cobro item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Cobro item)
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

                this.OnCobroCreated(item);
                this.context.Cobros.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cobros.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza1,CuentaBancarium,XpobjectType");

                this.OnAfterCobroCreated(item);

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
