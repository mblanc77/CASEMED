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
    [Route("odata/Sgpa/MaeFuns")]
    public partial class MaeFunsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public MaeFunsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.MaeFun> GetMaeFuns()
        {
            var items = this.context.MaeFuns.AsQueryable<SgpaNew.Server.Models.Sgpa.MaeFun>();
            this.OnMaeFunsRead(ref items);

            return items;
        }

        partial void OnMaeFunsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.MaeFun> items);

        partial void OnMaeFunGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.MaeFun> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/MaeFuns(NroFun={NroFun})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.MaeFun> GetMaeFun(int key)
        {
            var items = this.context.MaeFuns.Where(i => i.NroFun == key);
            var result = SingleResult.Create(items);

            OnMaeFunGet(ref result);

            return result;
        }
        partial void OnMaeFunDeleted(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunDeleted(SgpaNew.Server.Models.Sgpa.MaeFun item);

        [HttpDelete("/odata/Sgpa/MaeFuns(NroFun={NroFun})")]
        public IActionResult DeleteMaeFun(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MaeFuns
                    .Where(i => i.NroFun == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.MaeFun>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMaeFunDeleted(item);
                this.context.MaeFuns.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMaeFunDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMaeFunUpdated(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunUpdated(SgpaNew.Server.Models.Sgpa.MaeFun item);

        [HttpPut("/odata/Sgpa/MaeFuns(NroFun={NroFun})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMaeFun(int key, [FromBody]SgpaNew.Server.Models.Sgpa.MaeFun item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MaeFuns
                    .Where(i => i.NroFun == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.MaeFun>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMaeFunUpdated(item);
                this.context.MaeFuns.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaeFuns.Where(i => i.NroFun == key);
                ;
                this.OnAfterMaeFunUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/MaeFuns(NroFun={NroFun})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMaeFun(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.MaeFun> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MaeFuns
                    .Where(i => i.NroFun == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.MaeFun>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMaeFunUpdated(item);
                this.context.MaeFuns.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaeFuns.Where(i => i.NroFun == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMaeFunCreated(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunCreated(SgpaNew.Server.Models.Sgpa.MaeFun item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.MaeFun item)
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

                this.OnMaeFunCreated(item);
                this.context.MaeFuns.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaeFuns.Where(i => i.NroFun == item.NroFun);

                ;

                this.OnAfterMaeFunCreated(item);

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
