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
    [Route("odata/CMU/Regionals")]
    public partial class RegionalsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RegionalsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Regional> GetRegionals()
        {
            var items = this.context.Regionals.AsQueryable<SGPA.Server.Models.CMU.Regional>();
            this.OnRegionalsRead(ref items);

            return items;
        }

        partial void OnRegionalsRead(ref IQueryable<SGPA.Server.Models.CMU.Regional> items);

        partial void OnRegionalGet(ref SingleResult<SGPA.Server.Models.CMU.Regional> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Regionals(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Regional> GetRegional(int key)
        {
            var items = this.context.Regionals.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnRegionalGet(ref result);

            return result;
        }
        partial void OnRegionalDeleted(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalDeleted(SGPA.Server.Models.CMU.Regional item);

        [HttpDelete("/odata/CMU/Regionals(Id={Id})")]
        public IActionResult DeleteRegional(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Regionals
                    .Where(i => i.Id == key)
                    .Include(i => i.Colegiados)
                    .Include(i => i.Departamentos)
                    .Include(i => i.RegionalregionalesCuentabancariacuentabancaria)
                    .Include(i => i.UsuarioRegionals)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Regional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionalDeleted(item);
                this.context.Regionals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegionalDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionalUpdated(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalUpdated(SGPA.Server.Models.CMU.Regional item);

        [HttpPut("/odata/CMU/Regionals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegional(int key, [FromBody]SGPA.Server.Models.CMU.Regional item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Regionals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Regional>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegionalUpdated(item);
                this.context.Regionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regionals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Region1");
                this.OnAfterRegionalUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Regionals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegional(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Regional> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Regionals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Regional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegionalUpdated(item);
                this.context.Regionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regionals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Region1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegionalCreated(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalCreated(SGPA.Server.Models.CMU.Regional item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Regional item)
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

                this.OnRegionalCreated(item);
                this.context.Regionals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Regionals.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Region1");

                this.OnAfterRegionalCreated(item);

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
