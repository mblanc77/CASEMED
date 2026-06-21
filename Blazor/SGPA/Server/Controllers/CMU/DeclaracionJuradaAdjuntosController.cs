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
    [Route("odata/CMU/DeclaracionJuradaAdjuntos")]
    public partial class DeclaracionJuradaAdjuntosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DeclaracionJuradaAdjuntosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> GetDeclaracionJuradaAdjuntos()
        {
            var items = this.context.DeclaracionJuradaAdjuntos.AsQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>();
            this.OnDeclaracionJuradaAdjuntosRead(ref items);

            return items;
        }

        partial void OnDeclaracionJuradaAdjuntosRead(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> items);

        partial void OnDeclaracionJuradaAdjuntoGet(ref SingleResult<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DeclaracionJuradaAdjuntos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> GetDeclaracionJuradaAdjunto(int key)
        {
            var items = this.context.DeclaracionJuradaAdjuntos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDeclaracionJuradaAdjuntoGet(ref result);

            return result;
        }
        partial void OnDeclaracionJuradaAdjuntoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        [HttpDelete("/odata/CMU/DeclaracionJuradaAdjuntos(Id={Id})")]
        public IActionResult DeleteDeclaracionJuradaAdjunto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DeclaracionJuradaAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDeclaracionJuradaAdjuntoDeleted(item);
                this.context.DeclaracionJuradaAdjuntos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDeclaracionJuradaAdjuntoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDeclaracionJuradaAdjuntoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        [HttpPut("/odata/CMU/DeclaracionJuradaAdjuntos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDeclaracionJuradaAdjunto(int key, [FromBody]SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DeclaracionJuradaAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDeclaracionJuradaAdjuntoUpdated(item);
                this.context.DeclaracionJuradaAdjuntos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaAdjuntos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoDeclaracionJuradum,FileDatum");
                this.OnAfterDeclaracionJuradaAdjuntoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DeclaracionJuradaAdjuntos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDeclaracionJuradaAdjunto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DeclaracionJuradaAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDeclaracionJuradaAdjuntoUpdated(item);
                this.context.DeclaracionJuradaAdjuntos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaAdjuntos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoDeclaracionJuradum,FileDatum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDeclaracionJuradaAdjuntoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item)
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

                this.OnDeclaracionJuradaAdjuntoCreated(item);
                this.context.DeclaracionJuradaAdjuntos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DeclaracionJuradaAdjuntos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "ColegiadoDeclaracionJuradum,FileDatum");

                this.OnAfterDeclaracionJuradaAdjuntoCreated(item);

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
