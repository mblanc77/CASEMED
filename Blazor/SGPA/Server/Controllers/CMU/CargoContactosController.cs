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
    [Route("odata/CMU/CargoContactos")]
    public partial class CargoContactosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CargoContactosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CargoContacto> GetCargoContactos()
        {
            var items = this.context.CargoContactos.AsQueryable<SGPA.Server.Models.CMU.CargoContacto>();
            this.OnCargoContactosRead(ref items);

            return items;
        }

        partial void OnCargoContactosRead(ref IQueryable<SGPA.Server.Models.CMU.CargoContacto> items);

        partial void OnCargoContactoGet(ref SingleResult<SGPA.Server.Models.CMU.CargoContacto> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CargoContactos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.CargoContacto> GetCargoContacto(int key)
        {
            var items = this.context.CargoContactos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCargoContactoGet(ref result);

            return result;
        }
        partial void OnCargoContactoDeleted(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoDeleted(SGPA.Server.Models.CMU.CargoContacto item);

        [HttpDelete("/odata/CMU/CargoContactos(Id={Id})")]
        public IActionResult DeleteCargoContacto(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CargoContactos
                    .Where(i => i.Id == key)
                    .Include(i => i.Contactos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CargoContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCargoContactoDeleted(item);
                this.context.CargoContactos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCargoContactoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCargoContactoUpdated(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoUpdated(SGPA.Server.Models.CMU.CargoContacto item);

        [HttpPut("/odata/CMU/CargoContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCargoContacto(int key, [FromBody]SGPA.Server.Models.CMU.CargoContacto item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CargoContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CargoContacto>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCargoContactoUpdated(item);
                this.context.CargoContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CargoContactos.Where(i => i.Id == key);
                ;
                this.OnAfterCargoContactoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CargoContactos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCargoContacto(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CargoContacto> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CargoContactos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CargoContacto>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCargoContactoUpdated(item);
                this.context.CargoContactos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CargoContactos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCargoContactoCreated(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoCreated(SGPA.Server.Models.CMU.CargoContacto item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CargoContacto item)
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

                this.OnCargoContactoCreated(item);
                this.context.CargoContactos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CargoContactos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterCargoContactoCreated(item);

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
