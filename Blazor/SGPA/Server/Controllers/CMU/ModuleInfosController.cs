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
    [Route("odata/CMU/ModuleInfos")]
    public partial class ModuleInfosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ModuleInfosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ModuleInfo> GetModuleInfos()
        {
            var items = this.context.ModuleInfos.AsQueryable<SGPA.Server.Models.CMU.ModuleInfo>();
            this.OnModuleInfosRead(ref items);

            return items;
        }

        partial void OnModuleInfosRead(ref IQueryable<SGPA.Server.Models.CMU.ModuleInfo> items);

        partial void OnModuleInfoGet(ref SingleResult<SGPA.Server.Models.CMU.ModuleInfo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ModuleInfos(ID={ID})")]
        public SingleResult<SGPA.Server.Models.CMU.ModuleInfo> GetModuleInfo(int key)
        {
            var items = this.context.ModuleInfos.Where(i => i.ID == key);
            var result = SingleResult.Create(items);

            OnModuleInfoGet(ref result);

            return result;
        }
        partial void OnModuleInfoDeleted(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoDeleted(SGPA.Server.Models.CMU.ModuleInfo item);

        [HttpDelete("/odata/CMU/ModuleInfos(ID={ID})")]
        public IActionResult DeleteModuleInfo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ModuleInfos
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ModuleInfo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnModuleInfoDeleted(item);
                this.context.ModuleInfos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterModuleInfoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnModuleInfoUpdated(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoUpdated(SGPA.Server.Models.CMU.ModuleInfo item);

        [HttpPut("/odata/CMU/ModuleInfos(ID={ID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutModuleInfo(int key, [FromBody]SGPA.Server.Models.CMU.ModuleInfo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ModuleInfos
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ModuleInfo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnModuleInfoUpdated(item);
                this.context.ModuleInfos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ModuleInfos.Where(i => i.ID == key);
                ;
                this.OnAfterModuleInfoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ModuleInfos(ID={ID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchModuleInfo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ModuleInfo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ModuleInfos
                    .Where(i => i.ID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ModuleInfo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnModuleInfoUpdated(item);
                this.context.ModuleInfos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ModuleInfos.Where(i => i.ID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnModuleInfoCreated(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoCreated(SGPA.Server.Models.CMU.ModuleInfo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ModuleInfo item)
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

                this.OnModuleInfoCreated(item);
                this.context.ModuleInfos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ModuleInfos.Where(i => i.ID == item.ID);

                ;

                this.OnAfterModuleInfoCreated(item);

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
