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
    [Route("odata/CMU/FacultadTitulos")]
    public partial class FacultadTitulosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public FacultadTitulosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.FacultadTitulo> GetFacultadTitulos()
        {
            var items = this.context.FacultadTitulos.AsQueryable<SGPA.Server.Models.CMU.FacultadTitulo>();
            this.OnFacultadTitulosRead(ref items);

            return items;
        }

        partial void OnFacultadTitulosRead(ref IQueryable<SGPA.Server.Models.CMU.FacultadTitulo> items);

        partial void OnFacultadTituloGet(ref SingleResult<SGPA.Server.Models.CMU.FacultadTitulo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/FacultadTitulos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.FacultadTitulo> GetFacultadTitulo(int key)
        {
            var items = this.context.FacultadTitulos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnFacultadTituloGet(ref result);

            return result;
        }
        partial void OnFacultadTituloDeleted(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloDeleted(SGPA.Server.Models.CMU.FacultadTitulo item);

        [HttpDelete("/odata/CMU/FacultadTitulos(Id={Id})")]
        public IActionResult DeleteFacultadTitulo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.FacultadTitulos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FacultadTitulo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFacultadTituloDeleted(item);
                this.context.FacultadTitulos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterFacultadTituloDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFacultadTituloUpdated(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloUpdated(SGPA.Server.Models.CMU.FacultadTitulo item);

        [HttpPut("/odata/CMU/FacultadTitulos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutFacultadTitulo(int key, [FromBody]SGPA.Server.Models.CMU.FacultadTitulo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FacultadTitulos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FacultadTitulo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFacultadTituloUpdated(item);
                this.context.FacultadTitulos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FacultadTitulos.Where(i => i.Id == key);
                ;
                this.OnAfterFacultadTituloUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/FacultadTitulos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchFacultadTitulo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.FacultadTitulo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FacultadTitulos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FacultadTitulo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnFacultadTituloUpdated(item);
                this.context.FacultadTitulos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FacultadTitulos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFacultadTituloCreated(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloCreated(SGPA.Server.Models.CMU.FacultadTitulo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.FacultadTitulo item)
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

                this.OnFacultadTituloCreated(item);
                this.context.FacultadTitulos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FacultadTitulos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterFacultadTituloCreated(item);

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
