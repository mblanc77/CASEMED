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
    [Route("odata/Sgpa/Seleccions")]
    public partial class SeleccionsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SeleccionsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Seleccion> GetSeleccions()
        {
            var items = this.context.Seleccions.AsQueryable<SgpaNew.Server.Models.Sgpa.Seleccion>();
            this.OnSeleccionsRead(ref items);

            return items;
        }

        partial void OnSeleccionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Seleccion> items);

        partial void OnSeleccionGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Seleccion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Seleccions(SeleccionId={SeleccionId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Seleccion> GetSeleccion(int key)
        {
            var items = this.context.Seleccions.Where(i => i.SeleccionId == key);
            var result = SingleResult.Create(items);

            OnSeleccionGet(ref result);

            return result;
        }
        partial void OnSeleccionDeleted(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionDeleted(SgpaNew.Server.Models.Sgpa.Seleccion item);

        [HttpDelete("/odata/Sgpa/Seleccions(SeleccionId={SeleccionId})")]
        public IActionResult DeleteSeleccion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Seleccions
                    .Where(i => i.SeleccionId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Seleccion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSeleccionDeleted(item);
                this.context.Seleccions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSeleccionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSeleccionUpdated(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionUpdated(SgpaNew.Server.Models.Sgpa.Seleccion item);

        [HttpPut("/odata/Sgpa/Seleccions(SeleccionId={SeleccionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSeleccion(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Seleccion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Seleccions
                    .Where(i => i.SeleccionId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Seleccion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSeleccionUpdated(item);
                this.context.Seleccions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Seleccions.Where(i => i.SeleccionId == key);
                ;
                this.OnAfterSeleccionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Seleccions(SeleccionId={SeleccionId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSeleccion(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Seleccion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Seleccions
                    .Where(i => i.SeleccionId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Seleccion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSeleccionUpdated(item);
                this.context.Seleccions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Seleccions.Where(i => i.SeleccionId == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSeleccionCreated(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionCreated(SgpaNew.Server.Models.Sgpa.Seleccion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Seleccion item)
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

                this.OnSeleccionCreated(item);
                this.context.Seleccions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Seleccions.Where(i => i.SeleccionId == item.SeleccionId);

                ;

                this.OnAfterSeleccionCreated(item);

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
