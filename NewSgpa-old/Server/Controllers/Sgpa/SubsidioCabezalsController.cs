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
    [Route("odata/Sgpa/SubsidioCabezals")]
    public partial class SubsidioCabezalsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioCabezalsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> GetSubsidioCabezals()
        {
            var items = this.context.SubsidioCabezals.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>();
            this.OnSubsidioCabezalsRead(ref items);

            return items;
        }

        partial void OnSubsidioCabezalsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> items);

        partial void OnSubsidioCabezalGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioCabezals(IdSubsidio={IdSubsidio})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> GetSubsidioCabezal(int key)
        {
            var items = this.context.SubsidioCabezals.Where(i => i.IdSubsidio == key);
            var result = SingleResult.Create(items);

            OnSubsidioCabezalGet(ref result);

            return result;
        }
        partial void OnSubsidioCabezalDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        [HttpDelete("/odata/Sgpa/SubsidioCabezals(IdSubsidio={IdSubsidio})")]
        public IActionResult DeleteSubsidioCabezal(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioCabezals
                    .Where(i => i.IdSubsidio == key)
                    .Include(i => i.SubsidiocabezalBps)
                    .Include(i => i.SubsidioCabezalEmpresas)
                    .Include(i => i.SubsidioEnfermedads)
                    .Include(i => i.SubsidioItems)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioCabezalDeleted(item);
                this.context.SubsidioCabezals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioCabezalDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioCabezalUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        [HttpPut("/odata/Sgpa/SubsidioCabezals(IdSubsidio={IdSubsidio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioCabezal(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioCabezal item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioCabezals
                    .Where(i => i.IdSubsidio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioCabezalUpdated(item);
                this.context.SubsidioCabezals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezals.Where(i => i.IdSubsidio == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                this.OnAfterSubsidioCabezalUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioCabezals(IdSubsidio={IdSubsidio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioCabezal(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioCabezals
                    .Where(i => i.IdSubsidio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioCabezalUpdated(item);
                this.context.SubsidioCabezals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezals.Where(i => i.IdSubsidio == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioCabezalCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioCabezal item)
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

                this.OnSubsidioCabezalCreated(item);
                this.context.SubsidioCabezals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezals.Where(i => i.IdSubsidio == item.IdSubsidio);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");

                this.OnAfterSubsidioCabezalCreated(item);

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
