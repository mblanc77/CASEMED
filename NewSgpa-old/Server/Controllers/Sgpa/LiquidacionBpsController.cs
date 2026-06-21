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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/LiquidacionBps")]
    public partial class LiquidacionBpsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public LiquidacionBpsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.LiquidacionBp> GetLiquidacionBps()
        {
            var items = this.context.LiquidacionBps.AsQueryable<SgpaNew.Server.Models.Sgpa.LiquidacionBp>();
            this.OnLiquidacionBpsRead(ref items);

            return items;
        }

        partial void OnLiquidacionBpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.LiquidacionBp> items);

        partial void OnLiquidacionBpGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.LiquidacionBp> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/LiquidacionBps(LiquidacionBPSId={LiquidacionBPSId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.LiquidacionBp> GetLiquidacionBp(int key)
        {
            var items = this.context.LiquidacionBps.Where(i => i.LiquidacionBPSId == key);
            var result = SingleResult.Create(items);

            OnLiquidacionBpGet(ref result);

            return result;
        }
        partial void OnLiquidacionBpDeleted(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpDeleted(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        [HttpDelete("/odata/Sgpa/LiquidacionBps(LiquidacionBPSId={LiquidacionBPSId})")]
        public IActionResult DeleteLiquidacionBp(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.LiquidacionBps
                    .Where(i => i.LiquidacionBPSId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.LiquidacionBp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLiquidacionBpDeleted(item);
                this.context.LiquidacionBps.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLiquidacionBpDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLiquidacionBpUpdated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpUpdated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        [HttpPut("/odata/Sgpa/LiquidacionBps(LiquidacionBPSId={LiquidacionBPSId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLiquidacionBp(int key, [FromBody]SgpaNew.Server.Models.Sgpa.LiquidacionBp item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LiquidacionBps
                    .Where(i => i.LiquidacionBPSId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.LiquidacionBp>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLiquidacionBpUpdated(item);
                this.context.LiquidacionBps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LiquidacionBps.Where(i => i.LiquidacionBPSId == key);
                ;
                this.OnAfterLiquidacionBpUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/LiquidacionBps(LiquidacionBPSId={LiquidacionBPSId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLiquidacionBp(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.LiquidacionBp> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.LiquidacionBps
                    .Where(i => i.LiquidacionBPSId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.LiquidacionBp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLiquidacionBpUpdated(item);
                this.context.LiquidacionBps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LiquidacionBps.Where(i => i.LiquidacionBPSId == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLiquidacionBpCreated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpCreated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.LiquidacionBp item)
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

                this.OnLiquidacionBpCreated(item);
                this.context.LiquidacionBps.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.LiquidacionBps.Where(i => i.LiquidacionBPSId == item.LiquidacionBPSId);

                ;

                this.OnAfterLiquidacionBpCreated(item);

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
