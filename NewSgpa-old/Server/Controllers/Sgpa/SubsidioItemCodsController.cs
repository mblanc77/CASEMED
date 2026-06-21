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
    [Route("odata/Sgpa/SubsidioItemCods")]
    public partial class SubsidioItemCodsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioItemCodsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> GetSubsidioItemCods()
        {
            var items = this.context.SubsidioItemCods.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>();
            this.OnSubsidioItemCodsRead(ref items);

            return items;
        }

        partial void OnSubsidioItemCodsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> items);

        partial void OnSubsidioItemCodGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioItemCods(CodSubsidioItemCod={CodSubsidioItemCod})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> GetSubsidioItemCod(short key)
        {
            var items = this.context.SubsidioItemCods.Where(i => i.CodSubsidioItemCod == key);
            var result = SingleResult.Create(items);

            OnSubsidioItemCodGet(ref result);

            return result;
        }
        partial void OnSubsidioItemCodDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        [HttpDelete("/odata/Sgpa/SubsidioItemCods(CodSubsidioItemCod={CodSubsidioItemCod})")]
        public IActionResult DeleteSubsidioItemCod(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioItemCods
                    .Where(i => i.CodSubsidioItemCod == key)
                    .Include(i => i.SubsidioItems)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemCodDeleted(item);
                this.context.SubsidioItemCods.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioItemCodDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemCodUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        [HttpPut("/odata/Sgpa/SubsidioItemCods(CodSubsidioItemCod={CodSubsidioItemCod})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioItemCod(short key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioItemCod item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItemCods
                    .Where(i => i.CodSubsidioItemCod == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemCodUpdated(item);
                this.context.SubsidioItemCods.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemCods.Where(i => i.CodSubsidioItemCod == key);
                ;
                this.OnAfterSubsidioItemCodUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioItemCods(CodSubsidioItemCod={CodSubsidioItemCod})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioItemCod(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItemCods
                    .Where(i => i.CodSubsidioItemCod == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioItemCodUpdated(item);
                this.context.SubsidioItemCods.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemCods.Where(i => i.CodSubsidioItemCod == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemCodCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioItemCod item)
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

                this.OnSubsidioItemCodCreated(item);
                this.context.SubsidioItemCods.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemCods.Where(i => i.CodSubsidioItemCod == item.CodSubsidioItemCod);

                ;

                this.OnAfterSubsidioItemCodCreated(item);

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
