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
    [Route("odata/CMU/ColegiadoCertificadoExpedidos")]
    public partial class ColegiadoCertificadoExpedidosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoCertificadoExpedidosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> GetColegiadoCertificadoExpedidos()
        {
            var items = this.context.ColegiadoCertificadoExpedidos.AsQueryable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>();
            this.OnColegiadoCertificadoExpedidosRead(ref items);

            return items;
        }

        partial void OnColegiadoCertificadoExpedidosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> items);

        partial void OnColegiadoCertificadoExpedidoGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoCertificadoExpedidos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> GetColegiadoCertificadoExpedido(int key)
        {
            var items = this.context.ColegiadoCertificadoExpedidos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoCertificadoExpedidoGet(ref result);

            return result;
        }
        partial void OnColegiadoCertificadoExpedidoDeleted(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoDeleted(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        [HttpDelete("/odata/CMU/ColegiadoCertificadoExpedidos(Id={Id})")]
        public IActionResult DeleteColegiadoCertificadoExpedido(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoCertificadoExpedidos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoCertificadoExpedidoDeleted(item);
                this.context.ColegiadoCertificadoExpedidos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoCertificadoExpedidoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoCertificadoExpedidoUpdated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoUpdated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        [HttpPut("/odata/CMU/ColegiadoCertificadoExpedidos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoCertificadoExpedido(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoCertificadoExpedidos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoCertificadoExpedidoUpdated(item);
                this.context.ColegiadoCertificadoExpedidos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCertificadoExpedidos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                this.OnAfterColegiadoCertificadoExpedidoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoCertificadoExpedidos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoCertificadoExpedido(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoCertificadoExpedidos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoCertificadoExpedidoUpdated(item);
                this.context.ColegiadoCertificadoExpedidos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCertificadoExpedidos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoCertificadoExpedidoCreated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoCreated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item)
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

                this.OnColegiadoCertificadoExpedidoCreated(item);
                this.context.ColegiadoCertificadoExpedidos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoCertificadoExpedidos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1");

                this.OnAfterColegiadoCertificadoExpedidoCreated(item);

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
