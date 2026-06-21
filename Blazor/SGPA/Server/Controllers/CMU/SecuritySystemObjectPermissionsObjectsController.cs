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
    [Route("odata/CMU/SecuritySystemObjectPermissionsObjects")]
    public partial class SecuritySystemObjectPermissionsObjectsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritySystemObjectPermissionsObjectsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> GetSecuritySystemObjectPermissionsObjects()
        {
            var items = this.context.SecuritySystemObjectPermissionsObjects.AsQueryable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>();
            this.OnSecuritySystemObjectPermissionsObjectsRead(ref items);

            return items;
        }

        partial void OnSecuritySystemObjectPermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> items);

        partial void OnSecuritySystemObjectPermissionsObjectGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritySystemObjectPermissionsObjects(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> GetSecuritySystemObjectPermissionsObject(Guid key)
        {
            var items = this.context.SecuritySystemObjectPermissionsObjects.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnSecuritySystemObjectPermissionsObjectGet(ref result);

            return result;
        }
        partial void OnSecuritySystemObjectPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        [HttpDelete("/odata/CMU/SecuritySystemObjectPermissionsObjects(Oid={Oid})")]
        public IActionResult DeleteSecuritySystemObjectPermissionsObject(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritySystemObjectPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemObjectPermissionsObjectDeleted(item);
                this.context.SecuritySystemObjectPermissionsObjects.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritySystemObjectPermissionsObjectDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemObjectPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        [HttpPut("/odata/CMU/SecuritySystemObjectPermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritySystemObjectPermissionsObject(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemObjectPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritySystemObjectPermissionsObjectUpdated(item);
                this.context.SecuritySystemObjectPermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemObjectPermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");
                this.OnAfterSecuritySystemObjectPermissionsObjectUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritySystemObjectPermissionsObjects(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritySystemObjectPermissionsObject(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritySystemObjectPermissionsObjects
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritySystemObjectPermissionsObjectUpdated(item);
                this.context.SecuritySystemObjectPermissionsObjects.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemObjectPermissionsObjects.Where(i => i.Oid == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritySystemObjectPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item)
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

                this.OnSecuritySystemObjectPermissionsObjectCreated(item);
                this.context.SecuritySystemObjectPermissionsObjects.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritySystemObjectPermissionsObjects.Where(i => i.Oid == item.Oid);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemTypePermissionsObject");

                this.OnAfterSecuritySystemObjectPermissionsObjectCreated(item);

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
