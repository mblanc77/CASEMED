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
    [Route("odata/CMU/DepositoNominas")]
    public partial class DepositoNominasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepositoNominasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DepositoNomina> GetDepositoNominas()
        {
            var items = this.context.DepositoNominas.AsQueryable<SGPA.Server.Models.CMU.DepositoNomina>();
            this.OnDepositoNominasRead(ref items);

            return items;
        }

        partial void OnDepositoNominasRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNomina> items);

        partial void OnDepositoNominaGet(ref SingleResult<SGPA.Server.Models.CMU.DepositoNomina> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DepositoNominas(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DepositoNomina> GetDepositoNomina(int key)
        {
            var items = this.context.DepositoNominas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepositoNominaGet(ref result);

            return result;
        }
        partial void OnDepositoNominaDeleted(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaDeleted(SGPA.Server.Models.CMU.DepositoNomina item);

        [HttpDelete("/odata/CMU/DepositoNominas(Id={Id})")]
        public IActionResult DeleteDepositoNomina(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DepositoNominas
                    .Where(i => i.Id == key)
                    .Include(i => i.DepositoNominaMultiBrous)
                    .Include(i => i.DepositoNominaRedPagos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaDeleted(item);
                this.context.DepositoNominas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepositoNominaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaUpdated(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaUpdated(SGPA.Server.Models.CMU.DepositoNomina item);

        [HttpPut("/odata/CMU/DepositoNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepositoNomina(int key, [FromBody]SGPA.Server.Models.CMU.DepositoNomina item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNomina>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaUpdated(item);
                this.context.DepositoNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1,CobroNomina");
                this.OnAfterDepositoNominaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DepositoNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepositoNomina(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DepositoNomina> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepositoNominaUpdated(item);
                this.context.DepositoNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1,CobroNomina");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaCreated(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaCreated(SGPA.Server.Models.CMU.DepositoNomina item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DepositoNomina item)
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

                this.OnDepositoNominaCreated(item);
                this.context.DepositoNominas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Deposito1,CobroNomina");

                this.OnAfterDepositoNominaCreated(item);

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
