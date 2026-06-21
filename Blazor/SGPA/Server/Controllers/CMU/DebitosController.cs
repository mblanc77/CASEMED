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
    [Route("odata/CMU/Debitos")]
    public partial class DebitosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DebitosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Debito> GetDebitos()
        {
            var items = this.context.Debitos.AsQueryable<SGPA.Server.Models.CMU.Debito>();
            this.OnDebitosRead(ref items);

            return items;
        }

        partial void OnDebitosRead(ref IQueryable<SGPA.Server.Models.CMU.Debito> items);

        partial void OnDebitoGet(ref SingleResult<SGPA.Server.Models.CMU.Debito> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Debitos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Debito> GetDebito(int key)
        {
            var items = this.context.Debitos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDebitoGet(ref result);

            return result;
        }
        partial void OnDebitoDeleted(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoDeleted(SGPA.Server.Models.CMU.Debito item);

        [HttpDelete("/odata/CMU/Debitos(Id={Id})")]
        public IActionResult DeleteDebito(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Debitos
                    .Where(i => i.Id == key)
                    .Include(i => i.DebitoAdjuntos)
                    .Include(i => i.DebitoNominas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Debito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoDeleted(item);
                this.context.Debitos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDebitoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoUpdated(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoUpdated(SGPA.Server.Models.CMU.Debito item);

        [HttpPut("/odata/CMU/Debitos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDebito(int key, [FromBody]SGPA.Server.Models.CMU.Debito item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Debitos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Debito>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoUpdated(item);
                this.context.Debitos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Debitos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Cobro");
                this.OnAfterDebitoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Debitos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDebito(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Debito> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Debitos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Debito>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDebitoUpdated(item);
                this.context.Debitos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Debitos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Cobro");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoCreated(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoCreated(SGPA.Server.Models.CMU.Debito item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Debito item)
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

                this.OnDebitoCreated(item);
                this.context.Debitos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Debitos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaDebito,Cobro");

                this.OnAfterDebitoCreated(item);

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
