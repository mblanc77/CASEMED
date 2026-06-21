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
    [Route("odata/Sgpa/SubsidiocabezalBps")]
    public partial class SubsidiocabezalBpsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidiocabezalBpsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> GetSubsidiocabezalBps()
        {
            var items = this.context.SubsidiocabezalBps.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>();
            this.OnSubsidiocabezalBpsRead(ref items);

            return items;
        }

        partial void OnSubsidiocabezalBpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> items);

        partial void OnSubsidiocabezalBpGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidiocabezalBps(IdSubsidio={IdSubsidio})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> GetSubsidiocabezalBp(int key)
        {
            var items = this.context.SubsidiocabezalBps.Where(i => i.IdSubsidio == key);
            var result = SingleResult.Create(items);

            OnSubsidiocabezalBpGet(ref result);

            return result;
        }
        partial void OnSubsidiocabezalBpDeleted(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpDeleted(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        [HttpDelete("/odata/Sgpa/SubsidiocabezalBps(IdSubsidio={IdSubsidio})")]
        public IActionResult DeleteSubsidiocabezalBp(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidiocabezalBps
                    .Where(i => i.IdSubsidio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidiocabezalBpDeleted(item);
                this.context.SubsidiocabezalBps.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidiocabezalBpDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidiocabezalBpUpdated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpUpdated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        [HttpPut("/odata/Sgpa/SubsidiocabezalBps(IdSubsidio={IdSubsidio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidiocabezalBp(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidiocabezalBps
                    .Where(i => i.IdSubsidio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidiocabezalBpUpdated(item);
                this.context.SubsidiocabezalBps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidiocabezalBps.Where(i => i.IdSubsidio == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");
                this.OnAfterSubsidiocabezalBpUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidiocabezalBps(IdSubsidio={IdSubsidio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidiocabezalBp(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidiocabezalBps
                    .Where(i => i.IdSubsidio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidiocabezalBpUpdated(item);
                this.context.SubsidiocabezalBps.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidiocabezalBps.Where(i => i.IdSubsidio == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidiocabezalBpCreated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpCreated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item)
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

                this.OnSubsidiocabezalBpCreated(item);
                this.context.SubsidiocabezalBps.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidiocabezalBps.Where(i => i.IdSubsidio == item.IdSubsidio);

                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");

                this.OnAfterSubsidiocabezalBpCreated(item);

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
