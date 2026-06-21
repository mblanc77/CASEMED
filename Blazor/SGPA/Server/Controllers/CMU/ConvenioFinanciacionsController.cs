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
    [Route("odata/CMU/ConvenioFinanciacions")]
    public partial class ConvenioFinanciacionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ConvenioFinanciacionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ConvenioFinanciacion> GetConvenioFinanciacions()
        {
            var items = this.context.ConvenioFinanciacions.AsQueryable<SGPA.Server.Models.CMU.ConvenioFinanciacion>();
            this.OnConvenioFinanciacionsRead(ref items);

            return items;
        }

        partial void OnConvenioFinanciacionsRead(ref IQueryable<SGPA.Server.Models.CMU.ConvenioFinanciacion> items);

        partial void OnConvenioFinanciacionGet(ref SingleResult<SGPA.Server.Models.CMU.ConvenioFinanciacion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ConvenioFinanciacions(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ConvenioFinanciacion> GetConvenioFinanciacion(int key)
        {
            var items = this.context.ConvenioFinanciacions.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnConvenioFinanciacionGet(ref result);

            return result;
        }
        partial void OnConvenioFinanciacionDeleted(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionDeleted(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        [HttpDelete("/odata/CMU/ConvenioFinanciacions(Id={Id})")]
        public IActionResult DeleteConvenioFinanciacion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ConvenioFinanciacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ConvenioFinanciacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnConvenioFinanciacionDeleted(item);
                this.context.ConvenioFinanciacions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterConvenioFinanciacionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnConvenioFinanciacionUpdated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionUpdated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        [HttpPut("/odata/CMU/ConvenioFinanciacions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutConvenioFinanciacion(int key, [FromBody]SGPA.Server.Models.CMU.ConvenioFinanciacion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ConvenioFinanciacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ConvenioFinanciacion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnConvenioFinanciacionUpdated(item);
                this.context.ConvenioFinanciacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ConvenioFinanciacions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Convenio");
                this.OnAfterConvenioFinanciacionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ConvenioFinanciacions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchConvenioFinanciacion(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ConvenioFinanciacion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ConvenioFinanciacions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ConvenioFinanciacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnConvenioFinanciacionUpdated(item);
                this.context.ConvenioFinanciacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ConvenioFinanciacions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Convenio");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnConvenioFinanciacionCreated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionCreated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ConvenioFinanciacion item)
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

                this.OnConvenioFinanciacionCreated(item);
                this.context.ConvenioFinanciacions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ConvenioFinanciacions.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Convenio");

                this.OnAfterConvenioFinanciacionCreated(item);

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
