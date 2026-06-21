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
    [Route("odata/CMU/DebitoAdjuntos")]
    public partial class DebitoAdjuntosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DebitoAdjuntosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DebitoAdjunto> GetDebitoAdjuntos()
        {
            var items = this.context.DebitoAdjuntos.AsQueryable<SGPA.Server.Models.CMU.DebitoAdjunto>();
            this.OnDebitoAdjuntosRead(ref items);

            return items;
        }

        partial void OnDebitoAdjuntosRead(ref IQueryable<SGPA.Server.Models.CMU.DebitoAdjunto> items);

        partial void OnDebitoAdjuntoGet(ref SingleResult<SGPA.Server.Models.CMU.DebitoAdjunto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DebitoAdjuntos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DebitoAdjunto> GetDebitoAdjunto(int key)
        {
            var items = this.context.DebitoAdjuntos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDebitoAdjuntoGet(ref result);

            return result;
        }
        partial void OnDebitoAdjuntoDeleted(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoDeleted(SGPA.Server.Models.CMU.DebitoAdjunto item);

        [HttpDelete("/odata/CMU/DebitoAdjuntos(Id={Id})")]
        public IActionResult DeleteDebitoAdjunto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DebitoAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoAdjunto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoAdjuntoDeleted(item);
                this.context.DebitoAdjuntos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDebitoAdjuntoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoAdjuntoUpdated(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoUpdated(SGPA.Server.Models.CMU.DebitoAdjunto item);

        [HttpPut("/odata/CMU/DebitoAdjuntos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDebitoAdjunto(int key, [FromBody]SGPA.Server.Models.CMU.DebitoAdjunto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DebitoAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoAdjunto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoAdjuntoUpdated(item);
                this.context.DebitoAdjuntos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoAdjuntos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,FileDatum");
                this.OnAfterDebitoAdjuntoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DebitoAdjuntos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDebitoAdjunto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DebitoAdjunto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DebitoAdjuntos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoAdjunto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDebitoAdjuntoUpdated(item);
                this.context.DebitoAdjuntos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoAdjuntos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,FileDatum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoAdjuntoCreated(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoCreated(SGPA.Server.Models.CMU.DebitoAdjunto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DebitoAdjunto item)
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

                this.OnDebitoAdjuntoCreated(item);
                this.context.DebitoAdjuntos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoAdjuntos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,FileDatum");

                this.OnAfterDebitoAdjuntoCreated(item);

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
