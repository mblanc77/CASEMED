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
    [Route("odata/CMU/SalaCmus")]
    public partial class SalaCmusController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SalaCmusController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SalaCmu> GetSalaCmus()
        {
            var items = this.context.SalaCmus.AsQueryable<SGPA.Server.Models.CMU.SalaCmu>();
            this.OnSalaCmusRead(ref items);

            return items;
        }

        partial void OnSalaCmusRead(ref IQueryable<SGPA.Server.Models.CMU.SalaCmu> items);

        partial void OnSalaCmuGet(ref SingleResult<SGPA.Server.Models.CMU.SalaCmu> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SalaCmus(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.SalaCmu> GetSalaCmu(int key)
        {
            var items = this.context.SalaCmus.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnSalaCmuGet(ref result);

            return result;
        }
        partial void OnSalaCmuDeleted(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuDeleted(SGPA.Server.Models.CMU.SalaCmu item);

        [HttpDelete("/odata/CMU/SalaCmus(Id={Id})")]
        public IActionResult DeleteSalaCmu(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SalaCmus
                    .Where(i => i.Id == key)
                    .Include(i => i.SalaReservas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaCmu>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaCmuDeleted(item);
                this.context.SalaCmus.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalaCmuDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaCmuUpdated(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuUpdated(SGPA.Server.Models.CMU.SalaCmu item);

        [HttpPut("/odata/CMU/SalaCmus(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalaCmu(int key, [FromBody]SGPA.Server.Models.CMU.SalaCmu item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaCmus
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaCmu>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaCmuUpdated(item);
                this.context.SalaCmus.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaCmus.Where(i => i.Id == key);
                ;
                this.OnAfterSalaCmuUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SalaCmus(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalaCmu(int key, [FromBody]Delta<SGPA.Server.Models.CMU.SalaCmu> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaCmus
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaCmu>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSalaCmuUpdated(item);
                this.context.SalaCmus.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaCmus.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaCmuCreated(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuCreated(SGPA.Server.Models.CMU.SalaCmu item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SalaCmu item)
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

                this.OnSalaCmuCreated(item);
                this.context.SalaCmus.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaCmus.Where(i => i.Id == item.Id);

                ;

                this.OnAfterSalaCmuCreated(item);

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
