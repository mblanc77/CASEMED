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
    [Route("odata/Sgpa/SituacionPagos")]
    public partial class SituacionPagosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SituacionPagosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SituacionPago> GetSituacionPagos()
        {
            var items = this.context.SituacionPagos.AsQueryable<SgpaNew.Server.Models.Sgpa.SituacionPago>();
            this.OnSituacionPagosRead(ref items);

            return items;
        }

        partial void OnSituacionPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionPago> items);

        partial void OnSituacionPagoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SituacionPago> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SituacionPagos(CodSituacionPago={CodSituacionPago})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SituacionPago> GetSituacionPago(short key)
        {
            var items = this.context.SituacionPagos.Where(i => i.CodSituacionPago == key);
            var result = SingleResult.Create(items);

            OnSituacionPagoGet(ref result);

            return result;
        }
        partial void OnSituacionPagoDeleted(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoDeleted(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        [HttpDelete("/odata/Sgpa/SituacionPagos(CodSituacionPago={CodSituacionPago})")]
        public IActionResult DeleteSituacionPago(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SituacionPagos
                    .Where(i => i.CodSituacionPago == key)
                    .Include(i => i.Empresas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSituacionPagoDeleted(item);
                this.context.SituacionPagos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSituacionPagoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSituacionPagoUpdated(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoUpdated(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        [HttpPut("/odata/Sgpa/SituacionPagos(CodSituacionPago={CodSituacionPago})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSituacionPago(short key, [FromBody]SgpaNew.Server.Models.Sgpa.SituacionPago item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SituacionPagos
                    .Where(i => i.CodSituacionPago == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionPago>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSituacionPagoUpdated(item);
                this.context.SituacionPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionPagos.Where(i => i.CodSituacionPago == key);
                ;
                this.OnAfterSituacionPagoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SituacionPagos(CodSituacionPago={CodSituacionPago})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSituacionPago(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SituacionPago> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SituacionPagos
                    .Where(i => i.CodSituacionPago == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SituacionPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSituacionPagoUpdated(item);
                this.context.SituacionPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionPagos.Where(i => i.CodSituacionPago == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSituacionPagoCreated(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoCreated(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SituacionPago item)
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

                this.OnSituacionPagoCreated(item);
                this.context.SituacionPagos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SituacionPagos.Where(i => i.CodSituacionPago == item.CodSituacionPago);

                ;

                this.OnAfterSituacionPagoCreated(item);

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
