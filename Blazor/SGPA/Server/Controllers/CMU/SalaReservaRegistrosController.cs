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
    [Route("odata/CMU/SalaReservaRegistros")]
    public partial class SalaReservaRegistrosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SalaReservaRegistrosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SalaReservaRegistro> GetSalaReservaRegistros()
        {
            var items = this.context.SalaReservaRegistros.AsQueryable<SGPA.Server.Models.CMU.SalaReservaRegistro>();
            this.OnSalaReservaRegistrosRead(ref items);

            return items;
        }

        partial void OnSalaReservaRegistrosRead(ref IQueryable<SGPA.Server.Models.CMU.SalaReservaRegistro> items);

        partial void OnSalaReservaRegistroGet(ref SingleResult<SGPA.Server.Models.CMU.SalaReservaRegistro> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SalaReservaRegistros(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.SalaReservaRegistro> GetSalaReservaRegistro(int key)
        {
            var items = this.context.SalaReservaRegistros.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnSalaReservaRegistroGet(ref result);

            return result;
        }
        partial void OnSalaReservaRegistroDeleted(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroDeleted(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        [HttpDelete("/odata/CMU/SalaReservaRegistros(Id={Id})")]
        public IActionResult DeleteSalaReservaRegistro(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SalaReservaRegistros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReservaRegistro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaReservaRegistroDeleted(item);
                this.context.SalaReservaRegistros.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalaReservaRegistroDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaReservaRegistroUpdated(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroUpdated(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        [HttpPut("/odata/CMU/SalaReservaRegistros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalaReservaRegistro(int key, [FromBody]SGPA.Server.Models.CMU.SalaReservaRegistro item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaReservaRegistros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReservaRegistro>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaReservaRegistroUpdated(item);
                this.context.SalaReservaRegistros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservaRegistros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SalaReserva");
                this.OnAfterSalaReservaRegistroUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SalaReservaRegistros(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalaReservaRegistro(int key, [FromBody]Delta<SGPA.Server.Models.CMU.SalaReservaRegistro> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaReservaRegistros
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReservaRegistro>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSalaReservaRegistroUpdated(item);
                this.context.SalaReservaRegistros.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservaRegistros.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "SalaReserva");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaReservaRegistroCreated(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroCreated(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SalaReservaRegistro item)
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

                this.OnSalaReservaRegistroCreated(item);
                this.context.SalaReservaRegistros.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservaRegistros.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "SalaReserva");

                this.OnAfterSalaReservaRegistroCreated(item);

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
