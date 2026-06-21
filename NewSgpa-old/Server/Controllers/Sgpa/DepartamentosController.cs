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
    [Route("odata/Sgpa/Departamentos")]
    public partial class DepartamentosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public DepartamentosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Departamento> GetDepartamentos()
        {
            var items = this.context.Departamentos.AsQueryable<SgpaNew.Server.Models.Sgpa.Departamento>();
            this.OnDepartamentosRead(ref items);

            return items;
        }

        partial void OnDepartamentosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Departamento> items);

        partial void OnDepartamentoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Departamento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Departamentos(CodDepartamento={CodDepartamento})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Departamento> GetDepartamento(string key)
        {
            var items = this.context.Departamentos.Where(i => i.CodDepartamento == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnDepartamentoGet(ref result);

            return result;
        }
        partial void OnDepartamentoDeleted(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoDeleted(SgpaNew.Server.Models.Sgpa.Departamento item);

        [HttpDelete("/odata/Sgpa/Departamentos(CodDepartamento={CodDepartamento})")]
        public IActionResult DeleteDepartamento(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Departamentos
                    .Where(i => i.CodDepartamento == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Departamento>(Request, items);

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

        partial void OnDepartamentoUpdated(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoUpdated(SgpaNew.Server.Models.Sgpa.Departamento item);

        [HttpPut("/odata/Sgpa/Departamentos(CodDepartamento={CodDepartamento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepartamento(string key, [FromBody]SgpaNew.Server.Models.Sgpa.Departamento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departamentos
                    .Where(i => i.CodDepartamento == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Departamento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDepartamentoUpdated(item);
                this.context.Departamentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departamentos.Where(i => i.CodDepartamento == Uri.UnescapeDataString(key));
                ;
                this.OnAfterDepartamentoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Departamentos(CodDepartamento={CodDepartamento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepartamento(string key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Departamento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Departamentos
                    .Where(i => i.CodDepartamento == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Departamento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDepartamentoUpdated(item);
                this.context.Departamentos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Departamentos.Where(i => i.CodDepartamento == Uri.UnescapeDataString(key));
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepartamentoCreated(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoCreated(SgpaNew.Server.Models.Sgpa.Departamento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Departamento item)
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

                var itemToReturn = this.context.Departamentos.Where(i => i.CodDepartamento == item.CodDepartamento);

                ;

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
