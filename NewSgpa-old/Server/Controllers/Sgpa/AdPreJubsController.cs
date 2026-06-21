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
    [Route("odata/Sgpa/AdPreJubs")]
    public partial class AdPreJubsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AdPreJubsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AdPreJub> GetAdPreJubs()
        {
            var items = this.context.AdPreJubs.AsQueryable<SgpaNew.Server.Models.Sgpa.AdPreJub>();
            this.OnAdPreJubsRead(ref items);

            return items;
        }

        partial void OnAdPreJubsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJub> items);

        partial void OnAdPreJubGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AdPreJub> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AdPreJubs(CI={CI})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AdPreJub> GetAdPreJub(int key)
        {
            var items = this.context.AdPreJubs.Where(i => i.CI == key);
            var result = SingleResult.Create(items);

            OnAdPreJubGet(ref result);

            return result;
        }
        partial void OnAdPreJubDeleted(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubDeleted(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        [HttpDelete("/odata/Sgpa/AdPreJubs(CI={CI})")]
        public IActionResult DeleteAdPreJub(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AdPreJubs
                    .Where(i => i.CI == key)
                    .Include(i => i.AdPreJubPagos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJub>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAdPreJubDeleted(item);
                this.context.AdPreJubs.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAdPreJubDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdPreJubUpdated(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubUpdated(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        [HttpPut("/odata/Sgpa/AdPreJubs(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAdPreJub(int key, [FromBody]SgpaNew.Server.Models.Sgpa.AdPreJub item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AdPreJubs
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJub>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAdPreJubUpdated(item);
                this.context.AdPreJubs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubs.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                this.OnAfterAdPreJubUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AdPreJubs(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAdPreJub(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AdPreJub> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AdPreJubs
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AdPreJub>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAdPreJubUpdated(item);
                this.context.AdPreJubs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubs.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdPreJubCreated(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubCreated(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AdPreJub item)
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

                this.OnAdPreJubCreated(item);
                this.context.AdPreJubs.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdPreJubs.Where(i => i.CI == item.CI);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");

                this.OnAfterAdPreJubCreated(item);

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
