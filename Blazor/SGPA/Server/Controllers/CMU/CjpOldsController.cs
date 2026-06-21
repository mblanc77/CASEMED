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
    [Route("odata/CMU/CjpOlds")]
    public partial class CjpOldsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CjpOldsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CjpOld> GetCjpOlds()
        {
            var items = this.context.CjpOlds.AsQueryable<SGPA.Server.Models.CMU.CjpOld>();
            this.OnCjpOldsRead(ref items);

            return items;
        }

        partial void OnCjpOldsRead(ref IQueryable<SGPA.Server.Models.CMU.CjpOld> items);

        partial void OnCjpOldGet(ref SingleResult<SGPA.Server.Models.CMU.CjpOld> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CjpOlds(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.CjpOld> GetCjpOld(int key)
        {
            var items = this.context.CjpOlds.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnCjpOldGet(ref result);

            return result;
        }
        partial void OnCjpOldDeleted(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldDeleted(SGPA.Server.Models.CMU.CjpOld item);

        [HttpDelete("/odata/CMU/CjpOlds(Documento={Documento})")]
        public IActionResult DeleteCjpOld(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CjpOlds
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpOld>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpOldDeleted(item);
                this.context.CjpOlds.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCjpOldDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpOldUpdated(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldUpdated(SGPA.Server.Models.CMU.CjpOld item);

        [HttpPut("/odata/CMU/CjpOlds(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCjpOld(int key, [FromBody]SGPA.Server.Models.CMU.CjpOld item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CjpOlds
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpOld>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpOldUpdated(item);
                this.context.CjpOlds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpOlds.Where(i => i.Documento == key);
                ;
                this.OnAfterCjpOldUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CjpOlds(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCjpOld(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CjpOld> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CjpOlds
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpOld>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCjpOldUpdated(item);
                this.context.CjpOlds.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpOlds.Where(i => i.Documento == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpOldCreated(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldCreated(SGPA.Server.Models.CMU.CjpOld item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CjpOld item)
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

                this.OnCjpOldCreated(item);
                this.context.CjpOlds.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpOlds.Where(i => i.Documento == item.Documento);

                ;

                this.OnAfterCjpOldCreated(item);

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
