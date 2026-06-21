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
    [Route("odata/CMU/SecuritySystemMemberPermissionsObjects")]
    public partial class SecuritySystemMemberPermissionsObjectsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritySystemMemberPermissionsObjectsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> GetSecuritySystemMemberPermissionsObjects()
        {
            var items = this.context.SecuritySystemMemberPermissionsObjects.AsQueryable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>();
            this.OnSecuritySystemMemberPermissionsObjectsRead(ref items);

            return items;
        }

        partial void OnSecuritySystemMemberPermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> items);

        partial void OnSecuritySystemMemberPermissionsObjectGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritySystemMemberPermissionsObjects(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> GetSecuritySystemMemberPermissionsObject(Guid key)
        {
            var items = this.context.SecuritySystemMemberPermissionsObjects.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnSecuritySystemMemberPermissionsObjectGet(ref result);

            return result;
        }
        partial void OnSecuritySystemMemberPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        [HttpDelete("/odata/CMU/SecuritySystemMemberPermissionsObjects(Oid={Oid})")]
        public IActionResult DeleteSecuritySystemMemberPermissionsObject(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritySystemMemberPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemMemberPermissionsObjectDeleted(item);
                this.context.SecuritySystemMemberPermissionsObjects.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritySystemMemberPermissionsObjectDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemMemberPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        [HttpPut("/odata/CMU/SecuritySystemMemberPermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritySystemMemberPermissionsObject(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemMemberPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemMemberPermissionsObjectUpdated(item);
                this.context.SecuritySystemMemberPermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemMemberPermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");
                this.OnAfterSecuritySystemMemberPermissionsObjectUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritySystemMemberPermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritySystemMemberPermissionsObject(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemMemberPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritySystemMemberPermissionsObjectUpdated(item);
                this.context.SecuritySystemMemberPermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemMemberPermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemMemberPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item)
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

                this.OnSecuritySystemMemberPermissionsObjectCreated(item);
                this.context.SecuritySystemMemberPermissionsObjects.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemMemberPermissionsObjects.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");

                this.OnAfterSecuritySystemMemberPermissionsObjectCreated(item);

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
