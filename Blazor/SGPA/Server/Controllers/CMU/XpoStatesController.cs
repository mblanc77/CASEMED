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
    [Route("odata/CMU/XpoStates")]
    public partial class XpoStatesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpoStatesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpoState> GetXpoStates()
        {
            var items = this.context.XpoStates.AsQueryable<SGPA.Server.Models.CMU.XpoState>();
            this.OnXpoStatesRead(ref items);

            return items;
        }

        partial void OnXpoStatesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoState> items);

        partial void OnXpoStateGet(ref SingleResult<SGPA.Server.Models.CMU.XpoState> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpoStates(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.XpoState> GetXpoState(Guid key)
        {
            var items = this.context.XpoStates.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnXpoStateGet(ref result);

            return result;
        }
        partial void OnXpoStateDeleted(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateDeleted(SGPA.Server.Models.CMU.XpoState item);

        [HttpDelete("/odata/CMU/XpoStates(Oid={Oid})")]
        public IActionResult DeleteXpoState(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpoStates
                    .Where(i => i.Oid == key)
                    .Include(i => i.XpoStateAppearances)
                    .Include(i => i.XpoStateMachines)
                    .Include(i => i.XpoTransitions)
                    .Include(i => i.XpoTransitions1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoState>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateDeleted(item);
                this.context.XpoStates.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpoStateDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateUpdated(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateUpdated(SGPA.Server.Models.CMU.XpoState item);

        [HttpPut("/odata/CMU/XpoStates(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpoState(Guid key, [FromBody]SGPA.Server.Models.CMU.XpoState item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStates
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoState>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateUpdated(item);
                this.context.XpoStates.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStates.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoStateMachine");
                this.OnAfterXpoStateUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpoStates(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpoState(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.XpoState> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStates
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoState>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpoStateUpdated(item);
                this.context.XpoStates.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStates.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoStateMachine");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateCreated(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateCreated(SGPA.Server.Models.CMU.XpoState item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpoState item)
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

                this.OnXpoStateCreated(item);
                this.context.XpoStates.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStates.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpoStateMachine");

                this.OnAfterXpoStateCreated(item);

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
