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
    [Route("odata/CMU/AgenteCobranzaDebitos")]
    public partial class AgenteCobranzaDebitosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AgenteCobranzaDebitosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> GetAgenteCobranzaDebitos()
        {
            var items = this.context.AgenteCobranzaDebitos.AsQueryable<SGPA.Server.Models.CMU.AgenteCobranzaDebito>();
            this.OnAgenteCobranzaDebitosRead(ref items);

            return items;
        }

        partial void OnAgenteCobranzaDebitosRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> items);

        partial void OnAgenteCobranzaDebitoGet(ref SingleResult<SGPA.Server.Models.CMU.AgenteCobranzaDebito> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AgenteCobranzaDebitos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AgenteCobranzaDebito> GetAgenteCobranzaDebito(int key)
        {
            var items = this.context.AgenteCobranzaDebitos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAgenteCobranzaDebitoGet(ref result);

            return result;
        }
        partial void OnAgenteCobranzaDebitoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        [HttpDelete("/odata/CMU/AgenteCobranzaDebitos(Id={Id})")]
        public IActionResult DeleteAgenteCobranzaDebito(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AgenteCobranzaDebitos
                    .Where(i => i.Id == key)
                    .Include(i => i.ColegiadoDebitoBancarioAsociados)
                    .Include(i => i.ColegiadoTarjetaDebitoAsociada)
                    .Include(i => i.Debitos)
                    .Include(i => i.Parametros)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaDebito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaDebitoDeleted(item);
                this.context.AgenteCobranzaDebitos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAgenteCobranzaDebitoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaDebitoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        [HttpPut("/odata/CMU/AgenteCobranzaDebitos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAgenteCobranzaDebito(int key, [FromBody]SGPA.Server.Models.CMU.AgenteCobranzaDebito item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzaDebitos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaDebito>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaDebitoUpdated(item);
                this.context.AgenteCobranzaDebitos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaDebitos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza");
                this.OnAfterAgenteCobranzaDebitoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AgenteCobranzaDebitos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAgenteCobranzaDebito(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AgenteCobranzaDebito> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzaDebitos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranzaDebito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAgenteCobranzaDebitoUpdated(item);
                this.context.AgenteCobranzaDebitos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaDebitos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaDebitoCreated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoCreated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AgenteCobranzaDebito item)
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

                this.OnAgenteCobranzaDebitoCreated(item);
                this.context.AgenteCobranzaDebitos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzaDebitos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranza");

                this.OnAfterAgenteCobranzaDebitoCreated(item);

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
