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
    [Route("odata/CMU/RegistroColegiados")]
    public partial class RegistroColegiadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RegistroColegiadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.RegistroColegiado> GetRegistroColegiados()
        {
            var items = this.context.RegistroColegiados.AsQueryable<SGPA.Server.Models.CMU.RegistroColegiado>();
            this.OnRegistroColegiadosRead(ref items);

            return items;
        }

        partial void OnRegistroColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiado> items);

        partial void OnRegistroColegiadoGet(ref SingleResult<SGPA.Server.Models.CMU.RegistroColegiado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/RegistroColegiados(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.RegistroColegiado> GetRegistroColegiado(int key)
        {
            var items = this.context.RegistroColegiados.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnRegistroColegiadoGet(ref result);

            return result;
        }
        partial void OnRegistroColegiadoDeleted(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoDeleted(SGPA.Server.Models.CMU.RegistroColegiado item);

        [HttpDelete("/odata/CMU/RegistroColegiados(OID={OID})")]
        public IActionResult DeleteRegistroColegiado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegistroColegiados
                    .Where(i => i.OID == key)
                    .Include(i => i.RegistroColegiadoNotificacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoDeleted(item);
                this.context.RegistroColegiados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegistroColegiadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoUpdated(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoUpdated(SGPA.Server.Models.CMU.RegistroColegiado item);

        [HttpPut("/odata/CMU/RegistroColegiados(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegistroColegiado(int key, [FromBody]SGPA.Server.Models.CMU.RegistroColegiado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiados
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoUpdated(item);
                this.context.RegistroColegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiados.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,Pai,Universidad,UniversidadTituloGrado1");
                this.OnAfterRegistroColegiadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/RegistroColegiados(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegistroColegiado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.RegistroColegiado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiados
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegistroColegiadoUpdated(item);
                this.context.RegistroColegiados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiados.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,Pai,Universidad,UniversidadTituloGrado1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoCreated(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoCreated(SGPA.Server.Models.CMU.RegistroColegiado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.RegistroColegiado item)
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

                this.OnRegistroColegiadoCreated(item);
                this.context.RegistroColegiados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiados.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "Departamento1,Pai,Universidad,UniversidadTituloGrado1");

                this.OnAfterRegistroColegiadoCreated(item);

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
