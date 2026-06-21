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
    [Route("odata/Sgpa/Empresas")]
    public partial class EmpresasController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public EmpresasController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Empresa> GetEmpresas()
        {
            var items = this.context.Empresas.AsQueryable<SgpaNew.Server.Models.Sgpa.Empresa>();
            this.OnEmpresasRead(ref items);

            return items;
        }

        partial void OnEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Empresa> items);

        partial void OnEmpresaGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Empresa> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Empresas(CodEmpresa={CodEmpresa})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Empresa> GetEmpresa(short key)
        {
            var items = this.context.Empresas.Where(i => i.CodEmpresa == key);
            var result = SingleResult.Create(items);

            OnEmpresaGet(ref result);

            return result;
        }
        partial void OnEmpresaDeleted(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaDeleted(SgpaNew.Server.Models.Sgpa.Empresa item);

        [HttpDelete("/odata/Sgpa/Empresas(CodEmpresa={CodEmpresa})")]
        public IActionResult DeleteEmpresa(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Empresas
                    .Where(i => i.CodEmpresa == key)
                    .Include(i => i.EmpresaPagos)
                    .Include(i => i.SubsidioCabezalEmpresas)
                    .Include(i => i.SubsidioItemEmpresas)
                    .Include(i => i.Trabajas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Empresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmpresaDeleted(item);
                this.context.Empresas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmpresaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmpresaUpdated(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaUpdated(SgpaNew.Server.Models.Sgpa.Empresa item);

        [HttpPut("/odata/Sgpa/Empresas(CodEmpresa={CodEmpresa})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmpresa(short key, [FromBody]SgpaNew.Server.Models.Sgpa.Empresa item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Empresas
                    .Where(i => i.CodEmpresa == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Empresa>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmpresaUpdated(item);
                this.context.Empresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Empresas.Where(i => i.CodEmpresa == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RegimenAporte,SituacionPago");
                this.OnAfterEmpresaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Empresas(CodEmpresa={CodEmpresa})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmpresa(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Empresa> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Empresas
                    .Where(i => i.CodEmpresa == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Empresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmpresaUpdated(item);
                this.context.Empresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Empresas.Where(i => i.CodEmpresa == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RegimenAporte,SituacionPago");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmpresaCreated(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaCreated(SgpaNew.Server.Models.Sgpa.Empresa item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Empresa item)
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

                this.OnEmpresaCreated(item);
                this.context.Empresas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Empresas.Where(i => i.CodEmpresa == item.CodEmpresa);

                Request.QueryString = Request.QueryString.Add("$expand", "RegimenAporte,SituacionPago");

                this.OnAfterEmpresaCreated(item);

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
