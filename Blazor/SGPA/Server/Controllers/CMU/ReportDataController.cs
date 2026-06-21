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
    [Route("odata/CMU/ReportData")]
    public partial class ReportDataController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ReportDataController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ReportDatum> GetReportData()
        {
            var items = this.context.ReportData.AsQueryable<SGPA.Server.Models.CMU.ReportDatum>();
            this.OnReportDataRead(ref items);

            return items;
        }

        partial void OnReportDataRead(ref IQueryable<SGPA.Server.Models.CMU.ReportDatum> items);

        partial void OnReportDatumGet(ref SingleResult<SGPA.Server.Models.CMU.ReportDatum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ReportData(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.ReportDatum> GetReportDatum(int key)
        {
            var items = this.context.ReportData.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnReportDatumGet(ref result);

            return result;
        }
        partial void OnReportDatumDeleted(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumDeleted(SGPA.Server.Models.CMU.ReportDatum item);

        [HttpDelete("/odata/CMU/ReportData(OID={OID})")]
        public IActionResult DeleteReportDatum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ReportData
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReportDatumDeleted(item);
                this.context.ReportData.Remove(item);
                this.context.SaveChanges();
                this.OnAfterReportDatumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReportDatumUpdated(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumUpdated(SGPA.Server.Models.CMU.ReportDatum item);

        [HttpPut("/odata/CMU/ReportData(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutReportDatum(int key, [FromBody]SGPA.Server.Models.CMU.ReportDatum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReportData
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDatum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReportDatumUpdated(item);
                this.context.ReportData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportData.Where(i => i.OID == key);
                ;
                this.OnAfterReportDatumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ReportData(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchReportDatum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ReportDatum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReportData
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ReportDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnReportDatumUpdated(item);
                this.context.ReportData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportData.Where(i => i.OID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReportDatumCreated(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumCreated(SGPA.Server.Models.CMU.ReportDatum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ReportDatum item)
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

                this.OnReportDatumCreated(item);
                this.context.ReportData.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReportData.Where(i => i.OID == item.OID);

                ;

                this.OnAfterReportDatumCreated(item);

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
