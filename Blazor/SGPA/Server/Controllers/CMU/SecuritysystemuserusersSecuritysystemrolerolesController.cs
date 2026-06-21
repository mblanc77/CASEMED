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
    [Route("odata/CMU/SecuritysystemuserusersSecuritysystemroleroles")]
    public partial class SecuritysystemuserusersSecuritysystemrolerolesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SecuritysystemuserusersSecuritysystemrolerolesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> GetSecuritysystemuserusersSecuritysystemroleroles()
        {
            var items = this.context.SecuritysystemuserusersSecuritysystemroleroles.AsQueryable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>();
            this.OnSecuritysystemuserusersSecuritysystemrolerolesRead(ref items);

            return items;
        }

        partial void OnSecuritysystemuserusersSecuritysystemrolerolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> items);

        partial void OnSecuritysystemuserusersSecuritysystemroleroleGet(ref SingleResult<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SecuritysystemuserusersSecuritysystemroleroles(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> GetSecuritysystemuserusersSecuritysystemrolerole(Guid key)
        {
            var items = this.context.SecuritysystemuserusersSecuritysystemroleroles.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnSecuritysystemuserusersSecuritysystemroleroleGet(ref result);

            return result;
        }
        partial void OnSecuritysystemuserusersSecuritysystemroleroleDeleted(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleDeleted(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        [HttpDelete("/odata/CMU/SecuritysystemuserusersSecuritysystemroleroles(OID={OID})")]
        public IActionResult DeleteSecuritysystemuserusersSecuritysystemrolerole(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SecuritysystemuserusersSecuritysystemroleroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritysystemuserusersSecuritysystemroleroleDeleted(item);
                this.context.SecuritysystemuserusersSecuritysystemroleroles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSecuritysystemuserusersSecuritysystemroleroleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleUpdated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleUpdated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        [HttpPut("/odata/CMU/SecuritysystemuserusersSecuritysystemroleroles(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSecuritysystemuserusersSecuritysystemrolerole(Guid key, [FromBody]SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritysystemuserusersSecuritysystemroleroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSecuritysystemuserusersSecuritysystemroleroleUpdated(item);
                this.context.SecuritysystemuserusersSecuritysystemroleroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemuserusersSecuritysystemroleroles.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemUser");
                this.OnAfterSecuritysystemuserusersSecuritysystemroleroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SecuritysystemuserusersSecuritysystemroleroles(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSecuritysystemuserusersSecuritysystemrolerole(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SecuritysystemuserusersSecuritysystemroleroles
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSecuritysystemuserusersSecuritysystemroleroleUpdated(item);
                this.context.SecuritysystemuserusersSecuritysystemroleroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemuserusersSecuritysystemroleroles.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemUser");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleCreated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleCreated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item)
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

                this.OnSecuritysystemuserusersSecuritysystemroleroleCreated(item);
                this.context.SecuritysystemuserusersSecuritysystemroleroles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SecuritysystemuserusersSecuritysystemroleroles.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "SecuritySystemRole,SecuritySystemUser");

                this.OnAfterSecuritysystemuserusersSecuritysystemroleroleCreated(item);

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
