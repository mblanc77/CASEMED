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
    [Route("odata/CMU/ReportDataV2S")]
    public partial class ReportDataV2SController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ReportDataV2SController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ReportDataV2> GetReportDataV2S()
        {
            var items = this.context.ReportDataV2S.AsQueryable<SGPA.Server.Models.CMU.ReportDataV2>();
            this.OnReportDataV2SRead(ref items);

            return items;
        }

        partial void OnReportDataV2SRead(ref IQueryable<SGPA.Server.Models.CMU.ReportDataV2> items);

        partial void OnReportDataV2Get(ref SingleResult<SGPA.Server.Models.CMU.ReportDataV2> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ReportDataV2S(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.ReportDataV2> GetReportDataV2(Guid key)
        {
            var items = this.context.ReportDataV2S.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnReportDataV2Get(ref result);

            return result;
        }
        partial void OnReportDataV2Deleted(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Deleted(SGPA.Server.Models.CMU.ReportDataV2 item);

        [HttpDelete("/odata/CMU/ReportDataV2S(Oid={Oid})")]
        public IActionResult DeleteReportDataV2(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ReportDataV2S
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDataV2>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReportDataV2Deleted(item);
                this.context.ReportDataV2S.Remove(item);
                this.context.SaveChanges();
                this.OnAfterReportDataV2Deleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReportDataV2Updated(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Updated(SGPA.Server.Models.CMU.ReportDataV2 item);

        [HttpPut("/odata/CMU/ReportDataV2S(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutReportDataV2(Guid key, [FromBody]SGPA.Server.Models.CMU.ReportDataV2 item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReportDataV2S
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDataV2>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReportDataV2Updated(item);
                this.context.ReportDataV2S.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportDataV2S.Where(i => i.Oid == key);
                ;
                this.OnAfterReportDataV2Updated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ReportDataV2S(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchReportDataV2(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.ReportDataV2> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReportDataV2S
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDataV2>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnReportDataV2Updated(item);
                this.context.ReportDataV2S.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportDataV2S.Where(i => i.Oid == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReportDataV2Created(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Created(SGPA.Server.Models.CMU.ReportDataV2 item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ReportDataV2 item)
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

                this.OnReportDataV2Created(item);
                this.context.ReportDataV2S.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportDataV2S.Where(i => i.Oid == item.Oid);

                ;

                this.OnAfterReportDataV2Created(item);

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
