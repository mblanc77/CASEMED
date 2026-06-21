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
    [Route("odata/CMU/ColegiadoDebitoBancarioAsociados")]
    public partial class ColegiadoDebitoBancarioAsociadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoDebitoBancarioAsociadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> GetColegiadoDebitoBancarioAsociados()
        {
            var items = this.context.ColegiadoDebitoBancarioAsociados.AsQueryable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>();
            this.OnColegiadoDebitoBancarioAsociadosRead(ref items);

            return items;
        }

        partial void OnColegiadoDebitoBancarioAsociadosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> items);

        partial void OnColegiadoDebitoBancarioAsociadoGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoDebitoBancarioAsociados(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> GetColegiadoDebitoBancarioAsociado(int key)
        {
            var items = this.context.ColegiadoDebitoBancarioAsociados.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoDebitoBancarioAsociadoGet(ref result);

            return result;
        }
        partial void OnColegiadoDebitoBancarioAsociadoDeleted(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoDeleted(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        [HttpDelete("/odata/CMU/ColegiadoDebitoBancarioAsociados(Id={Id})")]
        public IActionResult DeleteColegiadoDebitoBancarioAsociado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoDebitoBancarioAsociados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoDebitoBancarioAsociadoDeleted(item);
                this.context.ColegiadoDebitoBancarioAsociados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoDebitoBancarioAsociadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoDebitoBancarioAsociadoUpdated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoUpdated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        [HttpPut("/odata/CMU/ColegiadoDebitoBancarioAsociados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoDebitoBancarioAsociado(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoDebitoBancarioAsociados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoDebitoBancarioAsociadoUpdated(item);
                this.context.ColegiadoDebitoBancarioAsociados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDebitoBancarioAsociados.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Colegiado1");
                this.OnAfterColegiadoDebitoBancarioAsociadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoDebitoBancarioAsociados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoDebitoBancarioAsociado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoDebitoBancarioAsociados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoDebitoBancarioAsociadoUpdated(item);
                this.context.ColegiadoDebitoBancarioAsociados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDebitoBancarioAsociados.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Colegiado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoDebitoBancarioAsociadoCreated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoCreated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item)
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

                this.OnColegiadoDebitoBancarioAsociadoCreated(item);
                this.context.ColegiadoDebitoBancarioAsociados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDebitoBancarioAsociados.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Colegiado1");

                this.OnAfterColegiadoDebitoBancarioAsociadoCreated(item);

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
