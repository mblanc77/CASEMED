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
    [Route("odata/Sgpa/Mutualista")]
    public partial class MutualistaController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public MutualistaController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Mutualistum> GetMutualista()
        {
            var items = this.context.Mutualista.AsQueryable<SgpaNew.Server.Models.Sgpa.Mutualistum>();
            this.OnMutualistaRead(ref items);

            return items;
        }

        partial void OnMutualistaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Mutualistum> items);

        partial void OnMutualistumGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Mutualistum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Mutualista(CodMutualista={CodMutualista})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Mutualistum> GetMutualistum(short key)
        {
            var items = this.context.Mutualista.Where(i => i.CodMutualista == key);
            var result = SingleResult.Create(items);

            OnMutualistumGet(ref result);

            return result;
        }
        partial void OnMutualistumDeleted(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumDeleted(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        [HttpDelete("/odata/Sgpa/Mutualista(CodMutualista={CodMutualista})")]
        public IActionResult DeleteMutualistum(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Mutualista
                    .Where(i => i.CodMutualista == key)
                    .Include(i => i.Afiliados)
                    .Include(i => i.ReintegroMutuals)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Mutualistum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMutualistumDeleted(item);
                this.context.Mutualista.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMutualistumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMutualistumUpdated(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumUpdated(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        [HttpPut("/odata/Sgpa/Mutualista(CodMutualista={CodMutualista})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMutualistum(short key, [FromBody]SgpaNew.Server.Models.Sgpa.Mutualistum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Mutualista
                    .Where(i => i.CodMutualista == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Mutualistum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMutualistumUpdated(item);
                this.context.Mutualista.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mutualista.Where(i => i.CodMutualista == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FormaPago");
                this.OnAfterMutualistumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Mutualista(CodMutualista={CodMutualista})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMutualistum(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Mutualistum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Mutualista
                    .Where(i => i.CodMutualista == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Mutualistum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMutualistumUpdated(item);
                this.context.Mutualista.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mutualista.Where(i => i.CodMutualista == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FormaPago");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMutualistumCreated(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumCreated(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Mutualistum item)
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

                this.OnMutualistumCreated(item);
                this.context.Mutualista.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Mutualista.Where(i => i.CodMutualista == item.CodMutualista);

                Request.QueryString = Request.QueryString.Add("$expand", "FormaPago");

                this.OnAfterMutualistumCreated(item);

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
