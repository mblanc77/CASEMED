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
    [Route("odata/CMU/Depositos")]
    public partial class DepositosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepositosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Deposito> GetDepositos()
        {
            var items = this.context.Depositos.AsQueryable<SGPA.Server.Models.CMU.Deposito>();
            this.OnDepositosRead(ref items);

            return items;
        }

        partial void OnDepositosRead(ref IQueryable<SGPA.Server.Models.CMU.Deposito> items);

        partial void OnDepositoGet(ref SingleResult<SGPA.Server.Models.CMU.Deposito> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Depositos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Deposito> GetDeposito(int key)
        {
            var items = this.context.Depositos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepositoGet(ref result);

            return result;
        }
        partial void OnDepositoDeleted(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoDeleted(SGPA.Server.Models.CMU.Deposito item);

        [HttpDelete("/odata/CMU/Depositos(Id={Id})")]
        public IActionResult DeleteDeposito(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Depositos
                    .Where(i => i.Id == key)
                    .Include(i => i.DepositoNominas)
                    .Include(i => i.DepositoNominaNoIdentificada)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Deposito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoDeleted(item);
                this.context.Depositos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepositoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoUpdated(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoUpdated(SGPA.Server.Models.CMU.Deposito item);

        [HttpPut("/odata/CMU/Depositos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDeposito(int key, [FromBody]SGPA.Server.Models.CMU.Deposito item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Depositos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Deposito>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoUpdated(item);
                this.context.Depositos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depositos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Cobro");
                this.OnAfterDepositoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Depositos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDeposito(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Deposito> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Depositos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Deposito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepositoUpdated(item);
                this.context.Depositos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depositos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Cobro");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoCreated(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoCreated(SGPA.Server.Models.CMU.Deposito item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Deposito item)
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

                this.OnDepositoCreated(item);
                this.context.Depositos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depositos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Cobro");

                this.OnAfterDepositoCreated(item);

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
