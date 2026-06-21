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
    [Route("odata/Sgpa/EmpresaPagos")]
    public partial class EmpresaPagosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public EmpresaPagosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.EmpresaPago> GetEmpresaPagos()
        {
            var items = this.context.EmpresaPagos.AsQueryable<SgpaNew.Server.Models.Sgpa.EmpresaPago>();
            this.OnEmpresaPagosRead(ref items);

            return items;
        }

        partial void OnEmpresaPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.EmpresaPago> items);

        partial void OnEmpresaPagoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.EmpresaPago> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/EmpresaPagos(EmpresaPagoId={EmpresaPagoId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.EmpresaPago> GetEmpresaPago(int key)
        {
            var items = this.context.EmpresaPagos.Where(i => i.EmpresaPagoId == key);
            var result = SingleResult.Create(items);

            OnEmpresaPagoGet(ref result);

            return result;
        }
        partial void OnEmpresaPagoDeleted(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoDeleted(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        [HttpDelete("/odata/Sgpa/EmpresaPagos(EmpresaPagoId={EmpresaPagoId})")]
        public IActionResult DeleteEmpresaPago(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.EmpresaPagos
                    .Where(i => i.EmpresaPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.EmpresaPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmpresaPagoDeleted(item);
                this.context.EmpresaPagos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmpresaPagoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmpresaPagoUpdated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoUpdated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        [HttpPut("/odata/Sgpa/EmpresaPagos(EmpresaPagoId={EmpresaPagoId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmpresaPago(int key, [FromBody]SgpaNew.Server.Models.Sgpa.EmpresaPago item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.EmpresaPagos
                    .Where(i => i.EmpresaPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.EmpresaPago>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmpresaPagoUpdated(item);
                this.context.EmpresaPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmpresaPagos.Where(i => i.EmpresaPagoId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");
                this.OnAfterEmpresaPagoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/EmpresaPagos(EmpresaPagoId={EmpresaPagoId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmpresaPago(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.EmpresaPago> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.EmpresaPagos
                    .Where(i => i.EmpresaPagoId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.EmpresaPago>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmpresaPagoUpdated(item);
                this.context.EmpresaPagos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmpresaPagos.Where(i => i.EmpresaPagoId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmpresaPagoCreated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoCreated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.EmpresaPago item)
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

                this.OnEmpresaPagoCreated(item);
                this.context.EmpresaPagos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmpresaPagos.Where(i => i.EmpresaPagoId == item.EmpresaPagoId);

                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");

                this.OnAfterEmpresaPagoCreated(item);

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
