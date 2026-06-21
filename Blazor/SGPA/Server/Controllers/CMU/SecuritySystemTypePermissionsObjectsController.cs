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
    [Route("odata/CMU/SecuritySystemTypePermissionsObjects")]
    public partial class SecuritySystemTypePermissionsObjectsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritySystemTypePermissionsObjectsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> GetSecuritySystemTypePermissionsObjects()
        {
            var items = this.context.SecuritySystemTypePermissionsObjects.AsQueryable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>();
            this.OnSecuritySystemTypePermissionsObjectsRead(ref items);

            return items;
        }

        partial void OnSecuritySystemTypePermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> items);

        partial void OnSecuritySystemTypePermissionsObjectGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritySystemTypePermissionsObjects(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> GetSecuritySystemTypePermissionsObject(Guid key)
        {
            var items = this.context.SecuritySystemTypePermissionsObjects.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnSecuritySystemTypePermissionsObjectGet(ref result);

            return result;
        }
        partial void OnSecuritySystemTypePermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        [HttpDelete("/odata/CMU/SecuritySystemTypePermissionsObjects(Oid={Oid})")]
        public IActionResult DeleteSecuritySystemTypePermissionsObject(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritySystemTypePermissionsObjects
                    .Where(i => i.Oid == key)
                    .Include(i => i.SecuritySystemMemberPermissionsObjects)
                    .Include(i => i.SecuritySystemObjectPermissionsObjects)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemTypePermissionsObjectDeleted(item);
                this.context.SecuritySystemTypePermissionsObjects.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritySystemTypePermissionsObjectDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemTypePermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        [HttpPut("/odata/CMU/SecuritySystemTypePermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritySystemTypePermissionsObject(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemTypePermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemTypePermissionsObjectUpdated(item);
                this.context.SecuritySystemTypePermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemTypePermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,SecuritySystemRole");
                this.OnAfterSecuritySystemTypePermissionsObjectUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritySystemTypePermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritySystemTypePermissionsObject(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemTypePermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritySystemTypePermissionsObjectUpdated(item);
                this.context.SecuritySystemTypePermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemTypePermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,SecuritySystemRole");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemTypePermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item)
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

                this.OnSecuritySystemTypePermissionsObjectCreated(item);
                this.context.SecuritySystemTypePermissionsObjects.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemTypePermissionsObjects.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,SecuritySystemRole");

                this.OnAfterSecuritySystemTypePermissionsObjectCreated(item);

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
