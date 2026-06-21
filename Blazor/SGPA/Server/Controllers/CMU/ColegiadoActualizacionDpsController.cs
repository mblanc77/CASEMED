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
    [Route("odata/CMU/ColegiadoActualizacionDps")]
    public partial class ColegiadoActualizacionDpsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoActualizacionDpsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> GetColegiadoActualizacionDps()
        {
            var items = this.context.ColegiadoActualizacionDps.AsQueryable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>();
            this.OnColegiadoActualizacionDpsRead(ref items);

            return items;
        }

        partial void OnColegiadoActualizacionDpsRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> items);

        partial void OnColegiadoActualizacionDpGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoActualizacionDps(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> GetColegiadoActualizacionDp(int key)
        {
            var items = this.context.ColegiadoActualizacionDps.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoActualizacionDpGet(ref result);

            return result;
        }
        partial void OnColegiadoActualizacionDpDeleted(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpDeleted(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        [HttpDelete("/odata/CMU/ColegiadoActualizacionDps(Id={Id})")]
        public IActionResult DeleteColegiadoActualizacionDp(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoActualizacionDps
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoActualizacionDpDeleted(item);
                this.context.ColegiadoActualizacionDps.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoActualizacionDpDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoActualizacionDpUpdated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpUpdated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        [HttpPut("/odata/CMU/ColegiadoActualizacionDps(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoActualizacionDp(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoActualizacionDp item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoActualizacionDps
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoActualizacionDpUpdated(item);
                this.context.ColegiadoActualizacionDps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoActualizacionDps.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                this.OnAfterColegiadoActualizacionDpUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoActualizacionDps(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoActualizacionDp(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoActualizacionDps
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoActualizacionDpUpdated(item);
                this.context.ColegiadoActualizacionDps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoActualizacionDps.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoActualizacionDpCreated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpCreated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoActualizacionDp item)
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

                this.OnColegiadoActualizacionDpCreated(item);
                this.context.ColegiadoActualizacionDps.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoActualizacionDps.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");

                this.OnAfterColegiadoActualizacionDpCreated(item);

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
