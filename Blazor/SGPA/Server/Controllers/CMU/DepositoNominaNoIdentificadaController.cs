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
    [Route("odata/CMU/DepositoNominaNoIdentificada")]
    public partial class DepositoNominaNoIdentificadaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepositoNominaNoIdentificadaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> GetDepositoNominaNoIdentificada()
        {
            var items = this.context.DepositoNominaNoIdentificada.AsQueryable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>();
            this.OnDepositoNominaNoIdentificadaRead(ref items);

            return items;
        }

        partial void OnDepositoNominaNoIdentificadaRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> items);

        partial void OnDepositoNominaNoIdentificadumGet(ref SingleResult<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DepositoNominaNoIdentificada(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> GetDepositoNominaNoIdentificadum(int key)
        {
            var items = this.context.DepositoNominaNoIdentificada.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepositoNominaNoIdentificadumGet(ref result);

            return result;
        }
        partial void OnDepositoNominaNoIdentificadumDeleted(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumDeleted(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        [HttpDelete("/odata/CMU/DepositoNominaNoIdentificada(Id={Id})")]
        public IActionResult DeleteDepositoNominaNoIdentificadum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DepositoNominaNoIdentificada
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaNoIdentificadumDeleted(item);
                this.context.DepositoNominaNoIdentificada.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepositoNominaNoIdentificadumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaNoIdentificadumUpdated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumUpdated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        [HttpPut("/odata/CMU/DepositoNominaNoIdentificada(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepositoNominaNoIdentificadum(int key, [FromBody]SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaNoIdentificada
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaNoIdentificadumUpdated(item);
                this.context.DepositoNominaNoIdentificada.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaNoIdentificada.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1");
                this.OnAfterDepositoNominaNoIdentificadumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DepositoNominaNoIdentificada(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepositoNominaNoIdentificadum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaNoIdentificada
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepositoNominaNoIdentificadumUpdated(item);
                this.context.DepositoNominaNoIdentificada.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaNoIdentificada.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaNoIdentificadumCreated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumCreated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item)
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

                this.OnDepositoNominaNoIdentificadumCreated(item);
                this.context.DepositoNominaNoIdentificada.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaNoIdentificada.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1");

                this.OnAfterDepositoNominaNoIdentificadumCreated(item);

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
