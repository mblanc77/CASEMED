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
    [Route("odata/Sgpa/FranjaIrpfs")]
    public partial class FranjaIrpfsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public FranjaIrpfsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.FranjaIrpf> GetFranjaIrpfs()
        {
            var items = this.context.FranjaIrpfs.AsQueryable<SgpaNew.Server.Models.Sgpa.FranjaIrpf>();
            this.OnFranjaIrpfsRead(ref items);

            return items;
        }

        partial void OnFranjaIrpfsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.FranjaIrpf> items);

        partial void OnFranjaIrpfGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.FranjaIrpf> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/FranjaIrpfs(Desde={Desde})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.FranjaIrpf> GetFranjaIrpf(double key)
        {
            var items = this.context.FranjaIrpfs.Where(i => i.Desde == key);
            var result = SingleResult.Create(items);

            OnFranjaIrpfGet(ref result);

            return result;
        }
        partial void OnFranjaIrpfDeleted(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfDeleted(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        [HttpDelete("/odata/Sgpa/FranjaIrpfs(Desde={Desde})")]
        public IActionResult DeleteFranjaIrpf(double key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.FranjaIrpfs
                    .Where(i => i.Desde == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FranjaIrpf>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFranjaIrpfDeleted(item);
                this.context.FranjaIrpfs.Remove(item);
                this.context.SaveChanges();
                this.OnAfterFranjaIrpfDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFranjaIrpfUpdated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfUpdated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        [HttpPut("/odata/Sgpa/FranjaIrpfs(Desde={Desde})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutFranjaIrpf(double key, [FromBody]SgpaNew.Server.Models.Sgpa.FranjaIrpf item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FranjaIrpfs
                    .Where(i => i.Desde == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FranjaIrpf>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFranjaIrpfUpdated(item);
                this.context.FranjaIrpfs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FranjaIrpfs.Where(i => i.Desde == key);
                ;
                this.OnAfterFranjaIrpfUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/FranjaIrpfs(Desde={Desde})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchFranjaIrpf(double key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.FranjaIrpf> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FranjaIrpfs
                    .Where(i => i.Desde == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FranjaIrpf>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnFranjaIrpfUpdated(item);
                this.context.FranjaIrpfs.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FranjaIrpfs.Where(i => i.Desde == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFranjaIrpfCreated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfCreated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.FranjaIrpf item)
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

                this.OnFranjaIrpfCreated(item);
                this.context.FranjaIrpfs.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FranjaIrpfs.Where(i => i.Desde == item.Desde);

                ;

                this.OnAfterFranjaIrpfCreated(item);

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
