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
    [Route("odata/Sgpa/SubsidioCabezalEmpresas")]
    public partial class SubsidioCabezalEmpresasController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioCabezalEmpresasController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> GetSubsidioCabezalEmpresas()
        {
            var items = this.context.SubsidioCabezalEmpresas.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>();
            this.OnSubsidioCabezalEmpresasRead(ref items);

            return items;
        }

        partial void OnSubsidioCabezalEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> items);

        partial void OnSubsidioCabezalEmpresaGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioCabezalEmpresas(SubsidioCabezalempresaId={SubsidioCabezalempresaId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> GetSubsidioCabezalEmpresa(int key)
        {
            var items = this.context.SubsidioCabezalEmpresas.Where(i => i.SubsidioCabezalempresaId == key);
            var result = SingleResult.Create(items);

            OnSubsidioCabezalEmpresaGet(ref result);

            return result;
        }
        partial void OnSubsidioCabezalEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        [HttpDelete("/odata/Sgpa/SubsidioCabezalEmpresas(SubsidioCabezalempresaId={SubsidioCabezalempresaId})")]
        public IActionResult DeleteSubsidioCabezalEmpresa(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioCabezalEmpresas
                    .Where(i => i.SubsidioCabezalempresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioCabezalEmpresaDeleted(item);
                this.context.SubsidioCabezalEmpresas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioCabezalEmpresaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioCabezalEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        [HttpPut("/odata/Sgpa/SubsidioCabezalEmpresas(SubsidioCabezalempresaId={SubsidioCabezalempresaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioCabezalEmpresa(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioCabezalEmpresas
                    .Where(i => i.SubsidioCabezalempresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioCabezalEmpresaUpdated(item);
                this.context.SubsidioCabezalEmpresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezalEmpresas.Where(i => i.SubsidioCabezalempresaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa,SubsidioCabezal");
                this.OnAfterSubsidioCabezalEmpresaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioCabezalEmpresas(SubsidioCabezalempresaId={SubsidioCabezalempresaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioCabezalEmpresa(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioCabezalEmpresas
                    .Where(i => i.SubsidioCabezalempresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioCabezalEmpresaUpdated(item);
                this.context.SubsidioCabezalEmpresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezalEmpresas.Where(i => i.SubsidioCabezalempresaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa,SubsidioCabezal");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioCabezalEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item)
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

                this.OnSubsidioCabezalEmpresaCreated(item);
                this.context.SubsidioCabezalEmpresas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioCabezalEmpresas.Where(i => i.SubsidioCabezalempresaId == item.SubsidioCabezalempresaId);

                Request.QueryString = Request.QueryString.Add("$expand", "Empresa,SubsidioCabezal");

                this.OnAfterSubsidioCabezalEmpresaCreated(item);

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
