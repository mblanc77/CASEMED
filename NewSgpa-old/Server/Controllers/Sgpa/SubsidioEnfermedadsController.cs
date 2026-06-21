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
    [Route("odata/Sgpa/SubsidioEnfermedads")]
    public partial class SubsidioEnfermedadsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioEnfermedadsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> GetSubsidioEnfermedads()
        {
            var items = this.context.SubsidioEnfermedads.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>();
            this.OnSubsidioEnfermedadsRead(ref items);

            return items;
        }

        partial void OnSubsidioEnfermedadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> items);

        partial void OnSubsidioEnfermedadGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioEnfermedads(SubsidioEnfermedadId={SubsidioEnfermedadId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> GetSubsidioEnfermedad(int key)
        {
            var items = this.context.SubsidioEnfermedads.Where(i => i.SubsidioEnfermedadId == key);
            var result = SingleResult.Create(items);

            OnSubsidioEnfermedadGet(ref result);

            return result;
        }
        partial void OnSubsidioEnfermedadDeleted(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadDeleted(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        [HttpDelete("/odata/Sgpa/SubsidioEnfermedads(SubsidioEnfermedadId={SubsidioEnfermedadId})")]
        public IActionResult DeleteSubsidioEnfermedad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioEnfermedads
                    .Where(i => i.SubsidioEnfermedadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioEnfermedadDeleted(item);
                this.context.SubsidioEnfermedads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioEnfermedadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioEnfermedadUpdated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadUpdated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        [HttpPut("/odata/Sgpa/SubsidioEnfermedads(SubsidioEnfermedadId={SubsidioEnfermedadId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioEnfermedad(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioEnfermedads
                    .Where(i => i.SubsidioEnfermedadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioEnfermedadUpdated(item);
                this.context.SubsidioEnfermedads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioEnfermedads.Where(i => i.SubsidioEnfermedadId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");
                this.OnAfterSubsidioEnfermedadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioEnfermedads(SubsidioEnfermedadId={SubsidioEnfermedadId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioEnfermedad(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioEnfermedads
                    .Where(i => i.SubsidioEnfermedadId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioEnfermedadUpdated(item);
                this.context.SubsidioEnfermedads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioEnfermedads.Where(i => i.SubsidioEnfermedadId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioEnfermedadCreated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadCreated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item)
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

                this.OnSubsidioEnfermedadCreated(item);
                this.context.SubsidioEnfermedads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioEnfermedads.Where(i => i.SubsidioEnfermedadId == item.SubsidioEnfermedadId);

                Request.QueryString = Request.QueryString.Add("$expand", "SubsidioCabezal");

                this.OnAfterSubsidioEnfermedadCreated(item);

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
