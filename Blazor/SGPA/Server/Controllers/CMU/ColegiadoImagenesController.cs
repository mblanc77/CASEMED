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
    [Route("odata/CMU/ColegiadoImagenes")]
    public partial class ColegiadoImagenesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoImagenesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoImagene> GetColegiadoImagenes()
        {
            var items = this.context.ColegiadoImagenes.AsQueryable<SGPA.Server.Models.CMU.ColegiadoImagene>();
            this.OnColegiadoImagenesRead(ref items);

            return items;
        }

        partial void OnColegiadoImagenesRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoImagene> items);

        partial void OnColegiadoImageneGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoImagene> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoImagenes(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoImagene> GetColegiadoImagene(int key)
        {
            var items = this.context.ColegiadoImagenes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoImageneGet(ref result);

            return result;
        }
        partial void OnColegiadoImageneDeleted(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneDeleted(SGPA.Server.Models.CMU.ColegiadoImagene item);

        [HttpDelete("/odata/CMU/ColegiadoImagenes(Id={Id})")]
        public IActionResult DeleteColegiadoImagene(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoImagenes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoImagene>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoImageneDeleted(item);
                this.context.ColegiadoImagenes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoImageneDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoImageneUpdated(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneUpdated(SGPA.Server.Models.CMU.ColegiadoImagene item);

        [HttpPut("/odata/CMU/ColegiadoImagenes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoImagene(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoImagene item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoImagenes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoImagene>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoImageneUpdated(item);
                this.context.ColegiadoImagenes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoImagenes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                this.OnAfterColegiadoImageneUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoImagenes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoImagene(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoImagene> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoImagenes
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoImagene>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoImageneUpdated(item);
                this.context.ColegiadoImagenes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoImagenes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoImageneCreated(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneCreated(SGPA.Server.Models.CMU.ColegiadoImagene item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoImagene item)
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

                this.OnColegiadoImageneCreated(item);
                this.context.ColegiadoImagenes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoImagenes.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");

                this.OnAfterColegiadoImageneCreated(item);

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
