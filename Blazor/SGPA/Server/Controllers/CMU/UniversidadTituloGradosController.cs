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
    [Route("odata/CMU/UniversidadTituloGrados")]
    public partial class UniversidadTituloGradosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UniversidadTituloGradosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.UniversidadTituloGrado> GetUniversidadTituloGrados()
        {
            var items = this.context.UniversidadTituloGrados.AsQueryable<SGPA.Server.Models.CMU.UniversidadTituloGrado>();
            this.OnUniversidadTituloGradosRead(ref items);

            return items;
        }

        partial void OnUniversidadTituloGradosRead(ref IQueryable<SGPA.Server.Models.CMU.UniversidadTituloGrado> items);

        partial void OnUniversidadTituloGradoGet(ref SingleResult<SGPA.Server.Models.CMU.UniversidadTituloGrado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/UniversidadTituloGrados(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.UniversidadTituloGrado> GetUniversidadTituloGrado(int key)
        {
            var items = this.context.UniversidadTituloGrados.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnUniversidadTituloGradoGet(ref result);

            return result;
        }
        partial void OnUniversidadTituloGradoDeleted(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoDeleted(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        [HttpDelete("/odata/CMU/UniversidadTituloGrados(Id={Id})")]
        public IActionResult DeleteUniversidadTituloGrado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.UniversidadTituloGrados
                    .Where(i => i.Id == key)
                    .Include(i => i.Colegiados)
                    .Include(i => i.RegistroColegiados)
                    .Include(i => i.TramiteInfoadjuntatitulos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UniversidadTituloGrado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUniversidadTituloGradoDeleted(item);
                this.context.UniversidadTituloGrados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUniversidadTituloGradoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUniversidadTituloGradoUpdated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoUpdated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        [HttpPut("/odata/CMU/UniversidadTituloGrados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUniversidadTituloGrado(int key, [FromBody]SGPA.Server.Models.CMU.UniversidadTituloGrado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UniversidadTituloGrados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UniversidadTituloGrado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUniversidadTituloGradoUpdated(item);
                this.context.UniversidadTituloGrados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UniversidadTituloGrados.Where(i => i.Id == key);
                ;
                this.OnAfterUniversidadTituloGradoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/UniversidadTituloGrados(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUniversidadTituloGrado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.UniversidadTituloGrado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.UniversidadTituloGrados
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.UniversidadTituloGrado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUniversidadTituloGradoUpdated(item);
                this.context.UniversidadTituloGrados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UniversidadTituloGrados.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUniversidadTituloGradoCreated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoCreated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.UniversidadTituloGrado item)
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

                this.OnUniversidadTituloGradoCreated(item);
                this.context.UniversidadTituloGrados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UniversidadTituloGrados.Where(i => i.Id == item.Id);

                ;

                this.OnAfterUniversidadTituloGradoCreated(item);

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
