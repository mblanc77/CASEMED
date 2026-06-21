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
    [Route("odata/CMU/UserResetPasswordRequests")]
    public partial class UserResetPasswordRequestsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UserResetPasswordRequestsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.UserResetPasswordRequest> GetUserResetPasswordRequests()
        {
            var items = this.context.UserResetPasswordRequests.AsQueryable<SGPA.Server.Models.CMU.UserResetPasswordRequest>();
            this.OnUserResetPasswordRequestsRead(ref items);

            return items;
        }

        partial void OnUserResetPasswordRequestsRead(ref IQueryable<SGPA.Server.Models.CMU.UserResetPasswordRequest> items);

        partial void OnUserResetPasswordRequestGet(ref SingleResult<SGPA.Server.Models.CMU.UserResetPasswordRequest> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/UserResetPasswordRequests(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.UserResetPasswordRequest> GetUserResetPasswordRequest(int key)
        {
            var items = this.context.UserResetPasswordRequests.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnUserResetPasswordRequestGet(ref result);

            return result;
        }
        partial void OnUserResetPasswordRequestDeleted(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestDeleted(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        [HttpDelete("/odata/CMU/UserResetPasswordRequests(Id={Id})")]
        public IActionResult DeleteUserResetPasswordRequest(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.UserResetPasswordRequests
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UserResetPasswordRequest>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUserResetPasswordRequestDeleted(item);
                this.context.UserResetPasswordRequests.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUserResetPasswordRequestDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserResetPasswordRequestUpdated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestUpdated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        [HttpPut("/odata/CMU/UserResetPasswordRequests(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUserResetPasswordRequest(int key, [FromBody]SGPA.Server.Models.CMU.UserResetPasswordRequest item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UserResetPasswordRequests
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UserResetPasswordRequest>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUserResetPasswordRequestUpdated(item);
                this.context.UserResetPasswordRequests.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserResetPasswordRequests.Where(i => i.Id == key);
                ;
                this.OnAfterUserResetPasswordRequestUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/UserResetPasswordRequests(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUserResetPasswordRequest(int key, [FromBody]Delta<SGPA.Server.Models.CMU.UserResetPasswordRequest> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UserResetPasswordRequests
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UserResetPasswordRequest>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUserResetPasswordRequestUpdated(item);
                this.context.UserResetPasswordRequests.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserResetPasswordRequests.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserResetPasswordRequestCreated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestCreated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.UserResetPasswordRequest item)
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

                this.OnUserResetPasswordRequestCreated(item);
                this.context.UserResetPasswordRequests.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserResetPasswordRequests.Where(i => i.Id == item.Id);

                ;

                this.OnAfterUserResetPasswordRequestCreated(item);

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
