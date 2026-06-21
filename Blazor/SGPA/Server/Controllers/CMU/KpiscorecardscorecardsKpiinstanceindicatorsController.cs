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
    [Route("odata/CMU/KpiscorecardscorecardsKpiinstanceindicators")]
    public partial class KpiscorecardscorecardsKpiinstanceindicatorsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public KpiscorecardscorecardsKpiinstanceindicatorsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> GetKpiscorecardscorecardsKpiinstanceindicators()
        {
            var items = this.context.KpiscorecardscorecardsKpiinstanceindicators.AsQueryable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>();
            this.OnKpiscorecardscorecardsKpiinstanceindicatorsRead(ref items);

            return items;
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> items);

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorGet(ref SingleResult<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/KpiscorecardscorecardsKpiinstanceindicators(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> GetKpiscorecardscorecardsKpiinstanceindicator(Guid key)
        {
            var items = this.context.KpiscorecardscorecardsKpiinstanceindicators.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnKpiscorecardscorecardsKpiinstanceindicatorGet(ref result);

            return result;
        }
        partial void OnKpiscorecardscorecardsKpiinstanceindicatorDeleted(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorDeleted(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        [HttpDelete("/odata/CMU/KpiscorecardscorecardsKpiinstanceindicators(OID={OID})")]
        public IActionResult DeleteKpiscorecardscorecardsKpiinstanceindicator(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.KpiscorecardscorecardsKpiinstanceindicators
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiscorecardscorecardsKpiinstanceindicatorDeleted(item);
                this.context.KpiscorecardscorecardsKpiinstanceindicators.Remove(item);
                this.context.SaveChanges();
                this.OnAfterKpiscorecardscorecardsKpiinstanceindicatorDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorUpdated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorUpdated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        [HttpPut("/odata/CMU/KpiscorecardscorecardsKpiinstanceindicators(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutKpiscorecardscorecardsKpiinstanceindicator(Guid key, [FromBody]SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiscorecardscorecardsKpiinstanceindicators
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnKpiscorecardscorecardsKpiinstanceindicatorUpdated(item);
                this.context.KpiscorecardscorecardsKpiinstanceindicators.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiscorecardscorecardsKpiinstanceindicators.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance,KpiScorecard");
                this.OnAfterKpiscorecardscorecardsKpiinstanceindicatorUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/KpiscorecardscorecardsKpiinstanceindicators(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchKpiscorecardscorecardsKpiinstanceindicator(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.KpiscorecardscorecardsKpiinstanceindicators
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnKpiscorecardscorecardsKpiinstanceindicatorUpdated(item);
                this.context.KpiscorecardscorecardsKpiinstanceindicators.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiscorecardscorecardsKpiinstanceindicators.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance,KpiScorecard");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorCreated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorCreated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item)
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

                this.OnKpiscorecardscorecardsKpiinstanceindicatorCreated(item);
                this.context.KpiscorecardscorecardsKpiinstanceindicators.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.KpiscorecardscorecardsKpiinstanceindicators.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "KpiInstance,KpiScorecard");

                this.OnAfterKpiscorecardscorecardsKpiinstanceindicatorCreated(item);

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
