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
    [Route("odata/CMU/SecuritySystemUsers")]
    public partial class SecuritySystemUsersController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritySystemUsersController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritySystemUser> GetSecuritySystemUsers()
        {
            var items = this.context.SecuritySystemUsers.AsQueryable<SGPA.Server.Models.CMU.SecuritySystemUser>();
            this.OnSecuritySystemUsersRead(ref items);

            return items;
        }

        partial void OnSecuritySystemUsersRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemUser> items);

        partial void OnSecuritySystemUserGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritySystemUser> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritySystemUsers(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritySystemUser> GetSecuritySystemUser(Guid key)
        {
            var items = this.context.SecuritySystemUsers.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnSecuritySystemUserGet(ref result);

            return result;
        }
        partial void OnSecuritySystemUserDeleted(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserDeleted(SGPA.Server.Models.CMU.SecuritySystemUser item);

        [HttpDelete("/odata/CMU/SecuritySystemUsers(Oid={Oid})")]
        public IActionResult DeleteSecuritySystemUser(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritySystemUsers
                    .Where(i => i.Oid == key)
                    .Include(i => i.SecuritysystemuserusersSecuritysystemroleroles)
                    .Include(i => i.Usuarios)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemUser>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemUserDeleted(item);
                this.context.SecuritySystemUsers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritySystemUserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemUserUpdated(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserUpdated(SGPA.Server.Models.CMU.SecuritySystemUser item);

        [HttpPut("/odata/CMU/SecuritySystemUsers(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritySystemUser(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritySystemUser item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemUsers
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemUser>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemUserUpdated(item);
                this.context.SecuritySystemUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemUsers.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                this.OnAfterSecuritySystemUserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritySystemUsers(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritySystemUser(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritySystemUser> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemUsers
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemUser>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritySystemUserUpdated(item);
                this.context.SecuritySystemUsers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemUsers.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemUserCreated(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserCreated(SGPA.Server.Models.CMU.SecuritySystemUser item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritySystemUser item)
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

                this.OnSecuritySystemUserCreated(item);
                this.context.SecuritySystemUsers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemUsers.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType");

                this.OnAfterSecuritySystemUserCreated(item);

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
