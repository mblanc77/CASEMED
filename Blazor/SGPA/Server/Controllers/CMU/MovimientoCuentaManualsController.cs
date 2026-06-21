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
    [Route("odata/CMU/MovimientoCuentaManuals")]
    public partial class MovimientoCuentaManualsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MovimientoCuentaManualsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentaManual> GetMovimientoCuentaManuals()
        {
            var items = this.context.MovimientoCuentaManuals.AsQueryable<SGPA.Server.Models.CMU.MovimientoCuentaManual>();
            this.OnMovimientoCuentaManualsRead(ref items);

            return items;
        }

        partial void OnMovimientoCuentaManualsRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaManual> items);

        partial void OnMovimientoCuentaManualGet(ref SingleResult<SGPA.Server.Models.CMU.MovimientoCuentaManual> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MovimientoCuentaManuals(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.MovimientoCuentaManual> GetMovimientoCuentaManual(int key)
        {
            var items = this.context.MovimientoCuentaManuals.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnMovimientoCuentaManualGet(ref result);

            return result;
        }
        partial void OnMovimientoCuentaManualDeleted(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualDeleted(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        [HttpDelete("/odata/CMU/MovimientoCuentaManuals(Id={Id})")]
        public IActionResult DeleteMovimientoCuentaManual(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MovimientoCuentaManuals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaManual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentaManualDeleted(item);
                this.context.MovimientoCuentaManuals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMovimientoCuentaManualDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentaManualUpdated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualUpdated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        [HttpPut("/odata/CMU/MovimientoCuentaManuals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMovimientoCuentaManual(int key, [FromBody]SGPA.Server.Models.CMU.MovimientoCuentaManual item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuentaManuals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaManual>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentaManualUpdated(item);
                this.context.MovimientoCuentaManuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaManuals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoCuentum");
                this.OnAfterMovimientoCuentaManualUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MovimientoCuentaManuals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMovimientoCuentaManual(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MovimientoCuentaManual> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuentaManuals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaManual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMovimientoCuentaManualUpdated(item);
                this.context.MovimientoCuentaManuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaManuals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoCuentum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentaManualCreated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualCreated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MovimientoCuentaManual item)
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

                this.OnMovimientoCuentaManualCreated(item);
                this.context.MovimientoCuentaManuals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaManuals.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "MovimientoCuentum");

                this.OnAfterMovimientoCuentaManualCreated(item);

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
