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
    [Route("odata/CMU/ColegiadoBitacoraNota")]
    public partial class ColegiadoBitacoraNotaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoBitacoraNotaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> GetColegiadoBitacoraNota()
        {
            var items = this.context.ColegiadoBitacoraNota.AsQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>();
            this.OnColegiadoBitacoraNotaRead(ref items);

            return items;
        }

        partial void OnColegiadoBitacoraNotaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> items);

        partial void OnColegiadoBitacoraNotumGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoBitacoraNota(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> GetColegiadoBitacoraNotum(int key)
        {
            var items = this.context.ColegiadoBitacoraNota.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoBitacoraNotumGet(ref result);

            return result;
        }
        partial void OnColegiadoBitacoraNotumDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        [HttpDelete("/odata/CMU/ColegiadoBitacoraNota(Id={Id})")]
        public IActionResult DeleteColegiadoBitacoraNotum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoBitacoraNota
                    .Where(i => i.Id == key)
                    .Include(i => i.ColegiadoBitacoraEMailEnvios)
                    .Include(i => i.ColegiadoBitacoraEMailRecepcions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraNotumDeleted(item);
                this.context.ColegiadoBitacoraNota.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoBitacoraNotumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraNotumUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        [HttpPut("/odata/CMU/ColegiadoBitacoraNota(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoBitacoraNotum(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraNota
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraNotumUpdated(item);
                this.context.ColegiadoBitacoraNota.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraNota.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacora");
                this.OnAfterColegiadoBitacoraNotumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoBitacoraNota(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoBitacoraNotum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraNota
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoBitacoraNotumUpdated(item);
                this.context.ColegiadoBitacoraNota.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraNota.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacora");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraNotumCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item)
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

                this.OnColegiadoBitacoraNotumCreated(item);
                this.context.ColegiadoBitacoraNota.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraNota.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacora");

                this.OnAfterColegiadoBitacoraNotumCreated(item);

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
