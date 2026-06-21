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
    [Route("odata/Sgpa/SubsidioItemEmpresas")]
    public partial class SubsidioItemEmpresasController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioItemEmpresasController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> GetSubsidioItemEmpresas()
        {
            var items = this.context.SubsidioItemEmpresas.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>();
            this.OnSubsidioItemEmpresasRead(ref items);

            return items;
        }

        partial void OnSubsidioItemEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> items);

        partial void OnSubsidioItemEmpresaGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioItemEmpresas(SubsidioItemEmpresaId={SubsidioItemEmpresaId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> GetSubsidioItemEmpresa(int key)
        {
            var items = this.context.SubsidioItemEmpresas.Where(i => i.SubsidioItemEmpresaId == key);
            var result = SingleResult.Create(items);

            OnSubsidioItemEmpresaGet(ref result);

            return result;
        }
        partial void OnSubsidioItemEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        [HttpDelete("/odata/Sgpa/SubsidioItemEmpresas(SubsidioItemEmpresaId={SubsidioItemEmpresaId})")]
        public IActionResult DeleteSubsidioItemEmpresa(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioItemEmpresas
                    .Where(i => i.SubsidioItemEmpresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemEmpresaDeleted(item);
                this.context.SubsidioItemEmpresas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioItemEmpresaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        [HttpPut("/odata/Sgpa/SubsidioItemEmpresas(SubsidioItemEmpresaId={SubsidioItemEmpresaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioItemEmpresa(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItemEmpresas
                    .Where(i => i.SubsidioItemEmpresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioItemEmpresaUpdated(item);
                this.context.SubsidioItemEmpresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemEmpresas.Where(i => i.SubsidioItemEmpresaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");
                this.OnAfterSubsidioItemEmpresaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioItemEmpresas(SubsidioItemEmpresaId={SubsidioItemEmpresaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioItemEmpresa(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioItemEmpresas
                    .Where(i => i.SubsidioItemEmpresaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioItemEmpresaUpdated(item);
                this.context.SubsidioItemEmpresas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemEmpresas.Where(i => i.SubsidioItemEmpresaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioItemEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item)
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

                this.OnSubsidioItemEmpresaCreated(item);
                this.context.SubsidioItemEmpresas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioItemEmpresas.Where(i => i.SubsidioItemEmpresaId == item.SubsidioItemEmpresaId);

                Request.QueryString = Request.QueryString.Add("$expand", "Empresa");

                this.OnAfterSubsidioItemEmpresaCreated(item);

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
