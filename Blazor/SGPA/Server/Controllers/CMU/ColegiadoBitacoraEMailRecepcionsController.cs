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
    [Route("odata/CMU/ColegiadoBitacoraEMailRecepcions")]
    public partial class ColegiadoBitacoraEMailRecepcionsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoBitacoraEMailRecepcionsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> GetColegiadoBitacoraEMailRecepcions()
        {
            var items = this.context.ColegiadoBitacoraEMailRecepcions.AsQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>();
            this.OnColegiadoBitacoraEMailRecepcionsRead(ref items);

            return items;
        }

        partial void OnColegiadoBitacoraEMailRecepcionsRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> items);

        partial void OnColegiadoBitacoraEMailRecepcionGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoBitacoraEMailRecepcions(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> GetColegiadoBitacoraEMailRecepcion(int key)
        {
            var items = this.context.ColegiadoBitacoraEMailRecepcions.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoBitacoraEMailRecepcionGet(ref result);

            return result;
        }
        partial void OnColegiadoBitacoraEMailRecepcionDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        [HttpDelete("/odata/CMU/ColegiadoBitacoraEMailRecepcions(Id={Id})")]
        public IActionResult DeleteColegiadoBitacoraEMailRecepcion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoBitacoraEMailRecepcions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraEMailRecepcionDeleted(item);
                this.context.ColegiadoBitacoraEMailRecepcions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoBitacoraEMailRecepcionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraEMailRecepcionUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        [HttpPut("/odata/CMU/ColegiadoBitacoraEMailRecepcions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoBitacoraEMailRecepcion(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraEMailRecepcions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraEMailRecepcionUpdated(item);
                this.context.ColegiadoBitacoraEMailRecepcions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailRecepcions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");
                this.OnAfterColegiadoBitacoraEMailRecepcionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoBitacoraEMailRecepcions(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoBitacoraEMailRecepcion(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoraEMailRecepcions
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoBitacoraEMailRecepcionUpdated(item);
                this.context.ColegiadoBitacoraEMailRecepcions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailRecepcions.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraEMailRecepcionCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item)
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

                this.OnColegiadoBitacoraEMailRecepcionCreated(item);
                this.context.ColegiadoBitacoraEMailRecepcions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoraEMailRecepcions.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoBitacoraNotum");

                this.OnAfterColegiadoBitacoraEMailRecepcionCreated(item);

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
