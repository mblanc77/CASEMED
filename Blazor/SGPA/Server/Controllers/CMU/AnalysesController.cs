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
    [Route("odata/CMU/Analyses")]
    public partial class AnalysesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AnalysesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Analysis> GetAnalyses()
        {
            var items = this.context.Analyses.AsQueryable<SGPA.Server.Models.CMU.Analysis>();
            this.OnAnalysesRead(ref items);

            return items;
        }

        partial void OnAnalysesRead(ref IQueryable<SGPA.Server.Models.CMU.Analysis> items);

        partial void OnAnalysisGet(ref SingleResult<SGPA.Server.Models.CMU.Analysis> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Analyses(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.Analysis> GetAnalysis(Guid key)
        {
            var items = this.context.Analyses.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnAnalysisGet(ref result);

            return result;
        }
        partial void OnAnalysisDeleted(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisDeleted(SGPA.Server.Models.CMU.Analysis item);

        [HttpDelete("/odata/CMU/Analyses(Oid={Oid})")]
        public IActionResult DeleteAnalysis(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Analyses
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Analysis>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAnalysisDeleted(item);
                this.context.Analyses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAnalysisDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAnalysisUpdated(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisUpdated(SGPA.Server.Models.CMU.Analysis item);

        [HttpPut("/odata/CMU/Analyses(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAnalysis(Guid key, [FromBody]SGPA.Server.Models.CMU.Analysis item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Analyses
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Analysis>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAnalysisUpdated(item);
                this.context.Analyses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Analyses.Where(i => i.Oid == key);
                ;
                this.OnAfterAnalysisUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Analyses(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAnalysis(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.Analysis> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Analyses
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Analysis>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAnalysisUpdated(item);
                this.context.Analyses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Analyses.Where(i => i.Oid == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAnalysisCreated(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisCreated(SGPA.Server.Models.CMU.Analysis item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Analysis item)
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

                this.OnAnalysisCreated(item);
                this.context.Analyses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Analyses.Where(i => i.Oid == item.Oid);

                ;

                this.OnAfterAnalysisCreated(item);

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
