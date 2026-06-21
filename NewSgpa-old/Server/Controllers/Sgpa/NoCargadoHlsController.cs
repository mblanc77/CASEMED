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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/NoCargadoHls")]
    public partial class NoCargadoHlsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public NoCargadoHlsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.NoCargadoHl> GetNoCargadoHls()
        {
            var items = this.context.NoCargadoHls.AsQueryable<SgpaNew.Server.Models.Sgpa.NoCargadoHl>();
            this.OnNoCargadoHlsRead(ref items);

            return items;
        }

        partial void OnNoCargadoHlsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.NoCargadoHl> items);

        partial void OnNoCargadoHlGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.NoCargadoHl> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/NoCargadoHls(NoCargadoHLId={NoCargadoHLId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.NoCargadoHl> GetNoCargadoHl(int key)
        {
            var items = this.context.NoCargadoHls.Where(i => i.NoCargadoHLId == key);
            var result = SingleResult.Create(items);

            OnNoCargadoHlGet(ref result);

            return result;
        }
        partial void OnNoCargadoHlDeleted(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlDeleted(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        [HttpDelete("/odata/Sgpa/NoCargadoHls(NoCargadoHLId={NoCargadoHLId})")]
        public IActionResult DeleteNoCargadoHl(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.NoCargadoHls
                    .Where(i => i.NoCargadoHLId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.NoCargadoHl>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnNoCargadoHlDeleted(item);
                this.context.NoCargadoHls.Remove(item);
                this.context.SaveChanges();
                this.OnAfterNoCargadoHlDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnNoCargadoHlUpdated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlUpdated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        [HttpPut("/odata/Sgpa/NoCargadoHls(NoCargadoHLId={NoCargadoHLId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutNoCargadoHl(int key, [FromBody]SgpaNew.Server.Models.Sgpa.NoCargadoHl item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.NoCargadoHls
                    .Where(i => i.NoCargadoHLId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.NoCargadoHl>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnNoCargadoHlUpdated(item);
                this.context.NoCargadoHls.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.NoCargadoHls.Where(i => i.NoCargadoHLId == key);
                ;
                this.OnAfterNoCargadoHlUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/NoCargadoHls(NoCargadoHLId={NoCargadoHLId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchNoCargadoHl(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.NoCargadoHl> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.NoCargadoHls
                    .Where(i => i.NoCargadoHLId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.NoCargadoHl>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnNoCargadoHlUpdated(item);
                this.context.NoCargadoHls.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.NoCargadoHls.Where(i => i.NoCargadoHLId == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnNoCargadoHlCreated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlCreated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.NoCargadoHl item)
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

                this.OnNoCargadoHlCreated(item);
                this.context.NoCargadoHls.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.NoCargadoHls.Where(i => i.NoCargadoHLId == item.NoCargadoHLId);

                ;

                this.OnAfterNoCargadoHlCreated(item);

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
