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
    [Route("odata/CMU/Departamentos")]
    public partial class DepartamentosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DepartamentosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Departamento> GetDepartamentos()
        {
            var items = this.context.Departamentos.AsQueryable<SGPA.Server.Models.CMU.Departamento>();
            this.OnDepartamentosRead(ref items);

            return items;
        }

        partial void OnDepartamentosRead(ref IQueryable<SGPA.Server.Models.CMU.Departamento> items);

        partial void OnDepartamentoGet(ref SingleResult<SGPA.Server.Models.CMU.Departamento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Departamentos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Departamento> GetDepartamento(int key)
        {
            var items = this.context.Departamentos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDepartamentoGet(ref result);

            return result;
        }
        partial void OnDepartamentoDeleted(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoDeleted(SGPA.Server.Models.CMU.Departamento item);

        [HttpDelete("/odata/CMU/Departamentos(Id={Id})")]
        public IActionResult DeleteDepartamento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Departamentos
                    .Where(i => i.Id == key)
                    .Include(i => i.AgenteCobranzas)
                    .Include(i => i.Colegiados)
                    .Include(i => i.LugarRetiroCarnes)
                    .Include(i => i.RegistroColegiados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Departamento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepartamentoDeleted(item);
                this.context.Departamentos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepartamentoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepartamentoUpdated(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoUpdated(SGPA.Server.Models.CMU.Departamento item);

        [HttpPut("/odata/CMU/Departamentos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepartamento(int key, [FromBody]SGPA.Server.Models.CMU.Departamento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departamentos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Departamento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepartamentoUpdated(item);
                this.context.Departamentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departamentos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Regional1");
                this.OnAfterDepartamentoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Departamentos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepartamento(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Departamento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departamentos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Departamento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepartamentoUpdated(item);
                this.context.Departamentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departamentos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Regional1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepartamentoCreated(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoCreated(SGPA.Server.Models.CMU.Departamento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Departamento item)
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

                this.OnDepartamentoCreated(item);
                this.context.Departamentos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departamentos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Regional1");

                this.OnAfterDepartamentoCreated(item);

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
