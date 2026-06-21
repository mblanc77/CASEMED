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
    [Route("odata/Sgpa/Afiliados")]
    public partial class AfiliadosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AfiliadosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Afiliado> GetAfiliados()
        {
            var items = this.context.Afiliados.AsQueryable<SgpaNew.Server.Models.Sgpa.Afiliado>();
            this.OnAfiliadosRead(ref items);

            return items;
        }

        partial void OnAfiliadosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Afiliado> items);

        partial void OnAfiliadoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Afiliado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Afiliados(CI={CI})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Afiliado> GetAfiliado(int key)
        {
            var items = this.context.Afiliados.Where(i => i.CI == key);
            var result = SingleResult.Create(items);

            OnAfiliadoGet(ref result);

            return result;
        }
        partial void OnAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.Afiliado item);

        [HttpDelete("/odata/Sgpa/Afiliados(CI={CI})")]
        public IActionResult DeleteAfiliado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Afiliados
                    .Where(i => i.CI == key)
                    .Include(i => i.AdPreJubs)
                    .Include(i => i.AfiliadoApuntes)
                    .Include(i => i.AfiliadoEspecialidads)
                    .Include(i => i.Certificacions)
                    .Include(i => i.CertificacionProrrogas)
                    .Include(i => i.Prestacions)
                    .Include(i => i.PrimaFallecimientos)
                    .Include(i => i.ReintegroMutuals)
                    .Include(i => i.SubsidioCabezals)
                    .Include(i => i.Trabajas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Afiliado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoDeleted(item);
                this.context.Afiliados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAfiliadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.Afiliado item);

        [HttpPut("/odata/Sgpa/Afiliados(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAfiliado(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Afiliado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Afiliados
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Afiliado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoUpdated(item);
                this.context.Afiliados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Afiliados.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Banco,Mutualistum,RegimenJubilatorio");
                this.OnAfterAfiliadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Afiliados(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAfiliado(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Afiliado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Afiliados
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Afiliado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAfiliadoUpdated(item);
                this.context.Afiliados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Afiliados.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Banco,Mutualistum,RegimenJubilatorio");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoCreated(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoCreated(SgpaNew.Server.Models.Sgpa.Afiliado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Afiliado item)
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

                this.OnAfiliadoCreated(item);
                this.context.Afiliados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Afiliados.Where(i => i.CI == item.CI);

                Request.QueryString = Request.QueryString.Add("$expand", "Banco,Mutualistum,RegimenJubilatorio");

                this.OnAfterAfiliadoCreated(item);

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
