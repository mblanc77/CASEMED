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
    [Route("odata/CMU/XpoTransitions")]
    public partial class XpoTransitionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpoTransitionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpoTransition> GetXpoTransitions()
        {
            var items = this.context.XpoTransitions.AsQueryable<SGPA.Server.Models.CMU.XpoTransition>();
            this.OnXpoTransitionsRead(ref items);

            return items;
        }

        partial void OnXpoTransitionsRead(ref IQueryable<SGPA.Server.Models.CMU.XpoTransition> items);

        partial void OnXpoTransitionGet(ref SingleResult<SGPA.Server.Models.CMU.XpoTransition> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpoTransitions(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.XpoTransition> GetXpoTransition(Guid key)
        {
            var items = this.context.XpoTransitions.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnXpoTransitionGet(ref result);

            return result;
        }
        partial void OnXpoTransitionDeleted(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionDeleted(SGPA.Server.Models.CMU.XpoTransition item);

        [HttpDelete("/odata/CMU/XpoTransitions(Oid={Oid})")]
        public IActionResult DeleteXpoTransition(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpoTransitions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoTransition>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoTransitionDeleted(item);
                this.context.XpoTransitions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpoTransitionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoTransitionUpdated(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionUpdated(SGPA.Server.Models.CMU.XpoTransition item);

        [HttpPut("/odata/CMU/XpoTransitions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpoTransition(Guid key, [FromBody]SGPA.Server.Models.CMU.XpoTransition item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoTransitions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoTransition>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoTransitionUpdated(item);
                this.context.XpoTransitions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoTransitions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState,XpoState1");
                this.OnAfterXpoTransitionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpoTransitions(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpoTransition(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.XpoTransition> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoTransitions
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoTransition>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpoTransitionUpdated(item);
                this.context.XpoTransitions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoTransitions.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState,XpoState1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoTransitionCreated(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionCreated(SGPA.Server.Models.CMU.XpoTransition item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpoTransition item)
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

                this.OnXpoTransitionCreated(item);
                this.context.XpoTransitions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoTransitions.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpoState,XpoState1");

                this.OnAfterXpoTransitionCreated(item);

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
