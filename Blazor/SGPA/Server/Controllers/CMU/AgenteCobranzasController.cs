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
    [Route("odata/CMU/AgenteCobranzas")]
    public partial class AgenteCobranzasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public AgenteCobranzasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> GetAgenteCobranzas()
        {
            var items = this.context.AgenteCobranzas.AsQueryable<SGPA.Server.Models.CMU.AgenteCobranza>();
            this.OnAgenteCobranzasRead(ref items);

            return items;
        }

        partial void OnAgenteCobranzasRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranza> items);

        partial void OnAgenteCobranzaGet(ref SingleResult<SGPA.Server.Models.CMU.AgenteCobranza> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/AgenteCobranzas(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.AgenteCobranza> GetAgenteCobranza(int key)
        {
            var items = this.context.AgenteCobranzas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAgenteCobranzaGet(ref result);

            return result;
        }
        partial void OnAgenteCobranzaDeleted(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaDeleted(SGPA.Server.Models.CMU.AgenteCobranza item);

        [HttpDelete("/odata/CMU/AgenteCobranzas(Id={Id})")]
        public IActionResult DeleteAgenteCobranza(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AgenteCobranzas
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzaDebitos)
                    .Include(i => i.Cobros)
                    .Include(i => i.Colegiados)
                    .Include(i => i.Colegiados1)
                    .Include(i => i.Parametros)
                    .Include(i => i.UsuarioInstitucions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranza>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaDeleted(item);
                this.context.AgenteCobranzas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAgenteCobranzaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaUpdated(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaUpdated(SGPA.Server.Models.CMU.AgenteCobranza item);

        [HttpPut("/odata/CMU/AgenteCobranzas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAgenteCobranza(int key, [FromBody]SGPA.Server.Models.CMU.AgenteCobranza item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranza>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAgenteCobranzaUpdated(item);
                this.context.AgenteCobranzas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaTipo,CuentaBancarium,Departamento1,AgenteGrupo,OrigenMovimiento,Region1");
                this.OnAfterAgenteCobranzaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/AgenteCobranzas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAgenteCobranza(int key, [FromBody]Delta<SGPA.Server.Models.CMU.AgenteCobranza> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AgenteCobranzas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.AgenteCobranza>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAgenteCobranzaUpdated(item);
                this.context.AgenteCobranzas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaTipo,CuentaBancarium,Departamento1,AgenteGrupo,OrigenMovimiento,Region1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAgenteCobranzaCreated(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaCreated(SGPA.Server.Models.CMU.AgenteCobranza item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.AgenteCobranza item)
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

                this.OnAgenteCobranzaCreated(item);
                this.context.AgenteCobranzas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AgenteCobranzas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "AgenteCobranzaTipo,CuentaBancarium,Departamento1,AgenteGrupo,OrigenMovimiento,Region1");

                this.OnAfterAgenteCobranzaCreated(item);

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
