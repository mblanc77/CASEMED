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
    [Route("odata/CMU/KpiScorecards")]
    public partial class KpiScorecardsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public KpiScorecardsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.KpiScorecard> GetKpiScorecards()
        {
            var items = this.context.KpiScorecards.AsQueryable<SGPA.Server.Models.CMU.KpiScorecard>();
            this.OnKpiScorecardsRead(ref items);

            return items;
        }

        partial void OnKpiScorecardsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiScorecard> items);

        partial void OnKpiScorecardGet(ref SingleResult<SGPA.Server.Models.CMU.KpiScorecard> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/KpiScorecards(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.KpiScorecard> GetKpiScorecard(Guid key)
        {
            var items = this.context.KpiScorecards.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnKpiScorecardGet(ref result);

            return result;
        }
        partial void OnKpiScorecardDeleted(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardDeleted(SGPA.Server.Models.CMU.KpiScorecard item);

        [HttpDelete("/odata/CMU/KpiScorecards(Oid={Oid})")]
        public IActionResult DeleteKpiScorecard(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.KpiScorecards
                    .Where(i => i.Oid == key)
                    .Include(i => i.KpiscorecardscorecardsKpiinstanceindicators)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiScorecard>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiScorecardDeleted(item);
                this.context.KpiScorecards.Remove(item);
                this.context.SaveChanges();
                this.OnAfterKpiScorecardDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiScorecardUpdated(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardUpdated(SGPA.Server.Models.CMU.KpiScorecard item);

        [HttpPut("/odata/CMU/KpiScorecards(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutKpiScorecard(Guid key, [FromBody]SGPA.Server.Models.CMU.KpiScorecard item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiScorecards
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiScorecard>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiScorecardUpdated(item);
                this.context.KpiScorecards.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiScorecards.Where(i => i.Oid == key);
                ;
                this.OnAfterKpiScorecardUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/KpiScorecards(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchKpiScorecard(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.KpiScorecard> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiScorecards
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiScorecard>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnKpiScorecardUpdated(item);
                this.context.KpiScorecards.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiScorecards.Where(i => i.Oid == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiScorecardCreated(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardCreated(SGPA.Server.Models.CMU.KpiScorecard item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.KpiScorecard item)
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

                this.OnKpiScorecardCreated(item);
                this.context.KpiScorecards.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiScorecards.Where(i => i.Oid == item.Oid);

                ;

                this.OnAfterKpiScorecardCreated(item);

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
