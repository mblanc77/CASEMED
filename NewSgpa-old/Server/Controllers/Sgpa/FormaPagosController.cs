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
    [Route("odata/Sgpa/FormaPagos")]
    public partial class FormaPagosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public FormaPagosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.FormaPago> GetFormaPagos()
        {
            var items = this.context.FormaPagos.AsQueryable<SgpaNew.Server.Models.Sgpa.FormaPago>();
            this.OnFormaPagosRead(ref items);

            return items;
        }

        partial void OnFormaPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.FormaPago> items);

        partial void OnFormaPagoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.FormaPago> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/FormaPagos(CodFormaPago={CodFormaPago})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.FormaPago> GetFormaPago(short key)
        {
            var items = this.context.FormaPagos.Where(i => i.CodFormaPago == key);
            var result = SingleResult.Create(items);

            OnFormaPagoGet(ref result);

            return result;
        }
        partial void OnFormaPagoDeleted(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoDeleted(SgpaNew.Server.Models.Sgpa.FormaPago item);

        [HttpDelete("/odata/Sgpa/FormaPagos(CodFormaPago={CodFormaPago})")]
        public IActionResult DeleteFormaPago(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.FormaPagos
                    .Where(i => i.CodFormaPago == key)
                    .Include(i => i.Mutualista)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FormaPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFormaPagoDeleted(item);
                this.context.FormaPagos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterFormaPagoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFormaPagoUpdated(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoUpdated(SgpaNew.Server.Models.Sgpa.FormaPago item);

        [HttpPut("/odata/Sgpa/FormaPagos(CodFormaPago={CodFormaPago})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutFormaPago(short key, [FromBody]SgpaNew.Server.Models.Sgpa.FormaPago item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FormaPagos
                    .Where(i => i.CodFormaPago == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FormaPago>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFormaPagoUpdated(item);
                this.context.FormaPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FormaPagos.Where(i => i.CodFormaPago == key);
                ;
                this.OnAfterFormaPagoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/FormaPagos(CodFormaPago={CodFormaPago})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchFormaPago(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.FormaPago> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FormaPagos
                    .Where(i => i.CodFormaPago == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.FormaPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnFormaPagoUpdated(item);
                this.context.FormaPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FormaPagos.Where(i => i.CodFormaPago == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFormaPagoCreated(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoCreated(SgpaNew.Server.Models.Sgpa.FormaPago item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.FormaPago item)
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

                this.OnFormaPagoCreated(item);
                this.context.FormaPagos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FormaPagos.Where(i => i.CodFormaPago == item.CodFormaPago);

                ;

                this.OnAfterFormaPagoCreated(item);

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
