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
    [Route("odata/CMU/XpoStateAppearances")]
    public partial class XpoStateAppearancesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpoStateAppearancesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpoStateAppearance> GetXpoStateAppearances()
        {
            var items = this.context.XpoStateAppearances.AsQueryable<SGPA.Server.Models.CMU.XpoStateAppearance>();
            this.OnXpoStateAppearancesRead(ref items);

            return items;
        }

        partial void OnXpoStateAppearancesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoStateAppearance> items);

        partial void OnXpoStateAppearanceGet(ref SingleResult<SGPA.Server.Models.CMU.XpoStateAppearance> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpoStateAppearances(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.XpoStateAppearance> GetXpoStateAppearance(Guid key)
        {
            var items = this.context.XpoStateAppearances.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnXpoStateAppearanceGet(ref result);

            return result;
        }
        partial void OnXpoStateAppearanceDeleted(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceDeleted(SGPA.Server.Models.CMU.XpoStateAppearance item);

        [HttpDelete("/odata/CMU/XpoStateAppearances(Oid={Oid})")]
        public IActionResult DeleteXpoStateAppearance(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpoStateAppearances
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateAppearance>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateAppearanceDeleted(item);
                this.context.XpoStateAppearances.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpoStateAppearanceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateAppearanceUpdated(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceUpdated(SGPA.Server.Models.CMU.XpoStateAppearance item);

        [HttpPut("/odata/CMU/XpoStateAppearances(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpoStateAppearance(Guid key, [FromBody]SGPA.Server.Models.CMU.XpoStateAppearance item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStateAppearances
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateAppearance>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateAppearanceUpdated(item);
                this.context.XpoStateAppearances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateAppearances.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");
                this.OnAfterXpoStateAppearanceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpoStateAppearances(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpoStateAppearance(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.XpoStateAppearance> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStateAppearances
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateAppearance>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpoStateAppearanceUpdated(item);
                this.context.XpoStateAppearances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateAppearances.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateAppearanceCreated(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceCreated(SGPA.Server.Models.CMU.XpoStateAppearance item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpoStateAppearance item)
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

                this.OnXpoStateAppearanceCreated(item);
                this.context.XpoStateAppearances.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateAppearances.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");

                this.OnAfterXpoStateAppearanceCreated(item);

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
