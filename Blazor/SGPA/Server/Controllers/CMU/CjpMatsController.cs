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
    [Route("odata/CMU/CjpMats")]
    public partial class CjpMatsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CjpMatsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CjpMat> GetCjpMats()
        {
            var items = this.context.CjpMats.AsQueryable<SGPA.Server.Models.CMU.CjpMat>();
            this.OnCjpMatsRead(ref items);

            return items;
        }

        partial void OnCjpMatsRead(ref IQueryable<SGPA.Server.Models.CMU.CjpMat> items);

        partial void OnCjpMatGet(ref SingleResult<SGPA.Server.Models.CMU.CjpMat> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CjpMats(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.CjpMat> GetCjpMat(int key)
        {
            var items = this.context.CjpMats.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnCjpMatGet(ref result);

            return result;
        }
        partial void OnCjpMatDeleted(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatDeleted(SGPA.Server.Models.CMU.CjpMat item);

        [HttpDelete("/odata/CMU/CjpMats(Documento={Documento})")]
        public IActionResult DeleteCjpMat(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CjpMats
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpMat>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpMatDeleted(item);
                this.context.CjpMats.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCjpMatDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpMatUpdated(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatUpdated(SGPA.Server.Models.CMU.CjpMat item);

        [HttpPut("/odata/CMU/CjpMats(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCjpMat(int key, [FromBody]SGPA.Server.Models.CMU.CjpMat item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CjpMats
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpMat>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCjpMatUpdated(item);
                this.context.CjpMats.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpMats.Where(i => i.Documento == key);
                ;
                this.OnAfterCjpMatUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CjpMats(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCjpMat(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CjpMat> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CjpMats
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CjpMat>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCjpMatUpdated(item);
                this.context.CjpMats.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpMats.Where(i => i.Documento == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCjpMatCreated(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatCreated(SGPA.Server.Models.CMU.CjpMat item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CjpMat item)
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

                this.OnCjpMatCreated(item);
                this.context.CjpMats.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CjpMats.Where(i => i.Documento == item.Documento);

                ;

                this.OnAfterCjpMatCreated(item);

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
