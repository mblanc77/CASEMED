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
    [Route("odata/CMU/Cjps")]
    public partial class CjpsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CjpsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Cjp> GetCjps()
        {
            var items = this.context.Cjps.AsQueryable<SGPA.Server.Models.CMU.Cjp>();
            this.OnCjpsRead(ref items);

            return items;
        }

        partial void OnCjpsRead(ref IQueryable<SGPA.Server.Models.CMU.Cjp> items);

        partial void OnCjpGet(ref SingleResult<SGPA.Server.Models.CMU.Cjp> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Cjps(CI={CI})")]
        public SingleResult<SGPA.Server.Models.CMU.Cjp> GetCjp(int key)
        {
            var items = this.context.Cjps.Where(i => i.CI == key);
            var result = SingleResult.Create(items);

            OnCjpGet(ref result);

            return result;
        }
        partial void OnCjpDeleted(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpDeleted(SGPA.Server.Models.CMU.Cjp item);

        [HttpDelete("/odata/CMU/Cjps(CI={CI})")]
        public IActionResult DeleteCjp(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Cjps
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cjp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpDeleted(item);
                this.context.Cjps.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCjpDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpUpdated(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpUpdated(SGPA.Server.Models.CMU.Cjp item);

        [HttpPut("/odata/CMU/Cjps(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCjp(int key, [FromBody]SGPA.Server.Models.CMU.Cjp item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cjps
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cjp>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpUpdated(item);
                this.context.Cjps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cjps.Where(i => i.CI == key);
                ;
                this.OnAfterCjpUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Cjps(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCjp(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Cjp> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cjps
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Cjp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCjpUpdated(item);
                this.context.Cjps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cjps.Where(i => i.CI == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpCreated(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpCreated(SGPA.Server.Models.CMU.Cjp item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Cjp item)
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

                this.OnCjpCreated(item);
                this.context.Cjps.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cjps.Where(i => i.CI == item.CI);

                ;

                this.OnAfterCjpCreated(item);

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
