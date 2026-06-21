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
    [Route("odata/CMU/ColegiadoBitacoraEMailEnvios")]
    public partial class ColegiadoBitacoraEMailEnviosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoBitacoraEMailEnviosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> GetColegiadoBitacoraEMailEnvios()
        {
            var items = this.context.ColegiadoBitacoraEMailEnvios.AsQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>();
            this.OnColegiadoBitacoraEMailEnviosRead(ref items);

            return items;
        }

        partial void OnColegiadoBitacoraEMailEnviosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> items);

        partial void OnColegiadoBitacoraEMailEnvioGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoBitacoraEMailEnvios(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> GetColegiadoBitacoraEMailEnvio(int key)
        {
            var items = this.context.ColegiadoBitacoraEMailEnvios.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoBitacoraEMailEnvioGet(ref result);

            return result;
        }
        partial void OnColegiadoBitacoraEMailEnvioDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        [HttpDelete("/odata/CMU/ColegiadoBitacoraEMailEnvios(Id={Id})")]
        public IActionResult DeleteColegiadoBitacoraEMailEnvio(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoBitacoraEMailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraEMailEnvioDeleted(item);
                this.context.ColegiadoBitacoraEMailEnvios.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoBitacoraEMailEnvioDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraEMailEnvioUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        [HttpPut("/odata/CMU/ColegiadoBitacoraEMailEnvios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoBitacoraEMailEnvio(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraEMailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraEMailEnvioUpdated(item);
                this.context.ColegiadoBitacoraEMailEnvios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailEnvios.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");
                this.OnAfterColegiadoBitacoraEMailEnvioUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoBitacoraEMailEnvios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoBitacoraEMailEnvio(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraEMailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoBitacoraEMailEnvioUpdated(item);
                this.context.ColegiadoBitacoraEMailEnvios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailEnvios.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraEMailEnvioCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item)
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

                this.OnColegiadoBitacoraEMailEnvioCreated(item);
                this.context.ColegiadoBitacoraEMailEnvios.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailEnvios.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");

                this.OnAfterColegiadoBitacoraEMailEnvioCreated(item);

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
