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
    [Route("odata/CMU/SecuritysystemroleparentrolesSecuritysystemrolechildroles")]
    public partial class SecuritysystemroleparentrolesSecuritysystemrolechildrolesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritysystemroleparentrolesSecuritysystemrolechildrolesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> GetSecuritysystemroleparentrolesSecuritysystemrolechildroles()
        {
            var items = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.AsQueryable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>();
            this.OnSecuritysystemroleparentrolesSecuritysystemrolechildrolesRead(ref items);

            return items;
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildrolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> items);

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritysystemroleparentrolesSecuritysystemrolechildroles(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> GetSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid key)
        {
            var items = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnSecuritysystemroleparentrolesSecuritysystemrolechildroleGet(ref result);

            return result;
        }
        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        [HttpDelete("/odata/CMU/SecuritysystemroleparentrolesSecuritysystemrolechildroles(OID={OID})")]
        public IActionResult DeleteSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(item);
                this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        [HttpPut("/odata/CMU/SecuritysystemroleparentrolesSecuritysystemrolechildroles(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(item);
                this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemRole1");
                this.OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritysystemroleparentrolesSecuritysystemrolechildroles(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(item);
                this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemRole1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item)
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

                this.OnSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(item);
                this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemRole1");

                this.OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(item);

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
