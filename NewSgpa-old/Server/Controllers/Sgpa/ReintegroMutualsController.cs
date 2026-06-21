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
    [Route("odata/Sgpa/ReintegroMutuals")]
    public partial class ReintegroMutualsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public ReintegroMutualsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.ReintegroMutual> GetReintegroMutuals()
        {
            var items = this.context.ReintegroMutuals.AsQueryable<SgpaNew.Server.Models.Sgpa.ReintegroMutual>();
            this.OnReintegroMutualsRead(ref items);

            return items;
        }

        partial void OnReintegroMutualsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.ReintegroMutual> items);

        partial void OnReintegroMutualGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.ReintegroMutual> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/ReintegroMutuals(ReintegroMutualId={ReintegroMutualId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.ReintegroMutual> GetReintegroMutual(int key)
        {
            var items = this.context.ReintegroMutuals.Where(i => i.ReintegroMutualId == key);
            var result = SingleResult.Create(items);

            OnReintegroMutualGet(ref result);

            return result;
        }
        partial void OnReintegroMutualDeleted(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualDeleted(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        [HttpDelete("/odata/Sgpa/ReintegroMutuals(ReintegroMutualId={ReintegroMutualId})")]
        public IActionResult DeleteReintegroMutual(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ReintegroMutuals
                    .Where(i => i.ReintegroMutualId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.ReintegroMutual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReintegroMutualDeleted(item);
                this.context.ReintegroMutuals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterReintegroMutualDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReintegroMutualUpdated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualUpdated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        [HttpPut("/odata/Sgpa/ReintegroMutuals(ReintegroMutualId={ReintegroMutualId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutReintegroMutual(int key, [FromBody]SgpaNew.Server.Models.Sgpa.ReintegroMutual item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReintegroMutuals
                    .Where(i => i.ReintegroMutualId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.ReintegroMutual>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnReintegroMutualUpdated(item);
                this.context.ReintegroMutuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReintegroMutuals.Where(i => i.ReintegroMutualId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Mutualistum");
                this.OnAfterReintegroMutualUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/ReintegroMutuals(ReintegroMutualId={ReintegroMutualId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchReintegroMutual(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.ReintegroMutual> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ReintegroMutuals
                    .Where(i => i.ReintegroMutualId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.ReintegroMutual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnReintegroMutualUpdated(item);
                this.context.ReintegroMutuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReintegroMutuals.Where(i => i.ReintegroMutualId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Mutualistum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnReintegroMutualCreated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualCreated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.ReintegroMutual item)
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

                this.OnReintegroMutualCreated(item);
                this.context.ReintegroMutuals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ReintegroMutuals.Where(i => i.ReintegroMutualId == item.ReintegroMutualId);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,Mutualistum");

                this.OnAfterReintegroMutualCreated(item);

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
