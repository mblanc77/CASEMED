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
    [Route("odata/CMU/XpoStateMachines")]
    public partial class XpoStateMachinesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpoStateMachinesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpoStateMachine> GetXpoStateMachines()
        {
            var items = this.context.XpoStateMachines.AsQueryable<SGPA.Server.Models.CMU.XpoStateMachine>();
            this.OnXpoStateMachinesRead(ref items);

            return items;
        }

        partial void OnXpoStateMachinesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoStateMachine> items);

        partial void OnXpoStateMachineGet(ref SingleResult<SGPA.Server.Models.CMU.XpoStateMachine> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpoStateMachines(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.XpoStateMachine> GetXpoStateMachine(Guid key)
        {
            var items = this.context.XpoStateMachines.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnXpoStateMachineGet(ref result);

            return result;
        }
        partial void OnXpoStateMachineDeleted(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineDeleted(SGPA.Server.Models.CMU.XpoStateMachine item);

        [HttpDelete("/odata/CMU/XpoStateMachines(Oid={Oid})")]
        public IActionResult DeleteXpoStateMachine(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpoStateMachines
                    .Where(i => i.Oid == key)
                    .Include(i => i.XpoStates)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateMachine>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateMachineDeleted(item);
                this.context.XpoStateMachines.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpoStateMachineDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateMachineUpdated(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineUpdated(SGPA.Server.Models.CMU.XpoStateMachine item);

        [HttpPut("/odata/CMU/XpoStateMachines(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpoStateMachine(Guid key, [FromBody]SGPA.Server.Models.CMU.XpoStateMachine item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStateMachines
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateMachine>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpoStateMachineUpdated(item);
                this.context.XpoStateMachines.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateMachines.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");
                this.OnAfterXpoStateMachineUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpoStateMachines(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpoStateMachine(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.XpoStateMachine> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpoStateMachines
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpoStateMachine>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpoStateMachineUpdated(item);
                this.context.XpoStateMachines.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateMachines.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpoStateMachineCreated(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineCreated(SGPA.Server.Models.CMU.XpoStateMachine item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpoStateMachine item)
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

                this.OnXpoStateMachineCreated(item);
                this.context.XpoStateMachines.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpoStateMachines.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpoState");

                this.OnAfterXpoStateMachineCreated(item);

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
