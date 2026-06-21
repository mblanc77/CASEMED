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
    [Route("odata/Sgpa/SituacionMutuals")]
    public partial class SituacionMutualsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SituacionMutualsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SituacionMutual> GetSituacionMutuals()
        {
            var items = this.context.SituacionMutuals.AsQueryable<SgpaNew.Server.Models.Sgpa.SituacionMutual>();
            this.OnSituacionMutualsRead(ref items);

            return items;
        }

        partial void OnSituacionMutualsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionMutual> items);

        partial void OnSituacionMutualGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SituacionMutual> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SituacionMutuals(CodSituacionMutual={CodSituacionMutual})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SituacionMutual> GetSituacionMutual(string key)
        {
            var items = this.context.SituacionMutuals.Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnSituacionMutualGet(ref result);

            return result;
        }
        partial void OnSituacionMutualDeleted(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualDeleted(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        [HttpDelete("/odata/Sgpa/SituacionMutuals(CodSituacionMutual={CodSituacionMutual})")]
        public IActionResult DeleteSituacionMutual(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SituacionMutuals
                    .Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionMutual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSituacionMutualDeleted(item);
                this.context.SituacionMutuals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSituacionMutualDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSituacionMutualUpdated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualUpdated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        [HttpPut("/odata/Sgpa/SituacionMutuals(CodSituacionMutual={CodSituacionMutual})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSituacionMutual(string key, [FromBody]SgpaNew.Server.Models.Sgpa.SituacionMutual item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SituacionMutuals
                    .Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionMutual>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSituacionMutualUpdated(item);
                this.context.SituacionMutuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionMutuals.Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key));
                ;
                this.OnAfterSituacionMutualUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SituacionMutuals(CodSituacionMutual={CodSituacionMutual})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSituacionMutual(string key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SituacionMutual> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SituacionMutuals
                    .Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionMutual>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSituacionMutualUpdated(item);
                this.context.SituacionMutuals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionMutuals.Where(i => i.CodSituacionMutual == Uri.UnescapeDataString(key));
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSituacionMutualCreated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualCreated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SituacionMutual item)
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

                this.OnSituacionMutualCreated(item);
                this.context.SituacionMutuals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionMutuals.Where(i => i.CodSituacionMutual == item.CodSituacionMutual);

                ;

                this.OnAfterSituacionMutualCreated(item);

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
