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
    [Route("odata/CMU/DepositoNominaMultiBrous")]
    public partial class DepositoNominaMultiBrousController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepositoNominaMultiBrousController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> GetDepositoNominaMultiBrous()
        {
            var items = this.context.DepositoNominaMultiBrous.AsQueryable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>();
            this.OnDepositoNominaMultiBrousRead(ref items);

            return items;
        }

        partial void OnDepositoNominaMultiBrousRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> items);

        partial void OnDepositoNominaMultiBrouGet(ref SingleResult<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DepositoNominaMultiBrous(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> GetDepositoNominaMultiBrou(int key)
        {
            var items = this.context.DepositoNominaMultiBrous.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepositoNominaMultiBrouGet(ref result);

            return result;
        }
        partial void OnDepositoNominaMultiBrouDeleted(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouDeleted(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        [HttpDelete("/odata/CMU/DepositoNominaMultiBrous(Id={Id})")]
        public IActionResult DeleteDepositoNominaMultiBrou(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DepositoNominaMultiBrous
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaMultiBrouDeleted(item);
                this.context.DepositoNominaMultiBrous.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepositoNominaMultiBrouDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaMultiBrouUpdated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouUpdated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        [HttpPut("/odata/CMU/DepositoNominaMultiBrous(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepositoNominaMultiBrou(int key, [FromBody]SGPA.Server.Models.CMU.DepositoNominaMultiBrou item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaMultiBrous
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepositoNominaMultiBrouUpdated(item);
                this.context.DepositoNominaMultiBrous.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaMultiBrous.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");
                this.OnAfterDepositoNominaMultiBrouUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DepositoNominaMultiBrous(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepositoNominaMultiBrou(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DepositoNominaMultiBrous
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepositoNominaMultiBrouUpdated(item);
                this.context.DepositoNominaMultiBrous.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaMultiBrous.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepositoNominaMultiBrouCreated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouCreated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DepositoNominaMultiBrou item)
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

                this.OnDepositoNominaMultiBrouCreated(item);
                this.context.DepositoNominaMultiBrous.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DepositoNominaMultiBrous.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "DepositoNomina");

                this.OnAfterDepositoNominaMultiBrouCreated(item);

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
