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
    [Route("odata/CMU/SecuritySystemRoles")]
    public partial class SecuritySystemRolesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritySystemRolesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritySystemRole> GetSecuritySystemRoles()
        {
            var items = this.context.SecuritySystemRoles.AsQueryable<SGPA.Server.Models.CMU.SecuritySystemRole>();
            this.OnSecuritySystemRolesRead(ref items);

            return items;
        }

        partial void OnSecuritySystemRolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemRole> items);

        partial void OnSecuritySystemRoleGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritySystemRole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritySystemRoles(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritySystemRole> GetSecuritySystemRole(Guid key)
        {
            var items = this.context.SecuritySystemRoles.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnSecuritySystemRoleGet(ref result);

            return result;
        }
        partial void OnSecuritySystemRoleDeleted(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleDeleted(SGPA.Server.Models.CMU.SecuritySystemRole item);

        [HttpDelete("/odata/CMU/SecuritySystemRoles(Oid={Oid})")]
        public IActionResult DeleteSecuritySystemRole(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritySystemRoles
                    .Where(i => i.Oid == key)
                    .Include(i => i.Rols)
                    .Include(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles)
                    .Include(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles1)
                    .Include(i => i.SecuritySystemTypePermissionsObjects)
                    .Include(i => i.SecuritysystemuserusersSecuritysystemroleroles)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemRole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemRoleDeleted(item);
                this.context.SecuritySystemRoles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritySystemRoleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemRoleUpdated(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleUpdated(SGPA.Server.Models.CMU.SecuritySystemRole item);

        [HttpPut("/odata/CMU/SecuritySystemRoles(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritySystemRole(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritySystemRole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemRoles
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemRole>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemRoleUpdated(item);
                this.context.SecuritySystemRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemRoles.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                this.OnAfterSecuritySystemRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritySystemRoles(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritySystemRole(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritySystemRole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemRoles
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemRole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritySystemRoleUpdated(item);
                this.context.SecuritySystemRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemRoles.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemRoleCreated(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleCreated(SGPA.Server.Models.CMU.SecuritySystemRole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritySystemRole item)
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

                this.OnSecuritySystemRoleCreated(item);
                this.context.SecuritySystemRoles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemRoles.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");

                this.OnAfterSecuritySystemRoleCreated(item);

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
