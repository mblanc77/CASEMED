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
    [Route("odata/CMU/SalaReservas")]
    public partial class SalaReservasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SalaReservasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SalaReserva> GetSalaReservas()
        {
            var items = this.context.SalaReservas.AsQueryable<SGPA.Server.Models.CMU.SalaReserva>();
            this.OnSalaReservasRead(ref items);

            return items;
        }

        partial void OnSalaReservasRead(ref IQueryable<SGPA.Server.Models.CMU.SalaReserva> items);

        partial void OnSalaReservaGet(ref SingleResult<SGPA.Server.Models.CMU.SalaReserva> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SalaReservas(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.SalaReserva> GetSalaReserva(int key)
        {
            var items = this.context.SalaReservas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnSalaReservaGet(ref result);

            return result;
        }
        partial void OnSalaReservaDeleted(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaDeleted(SGPA.Server.Models.CMU.SalaReserva item);

        [HttpDelete("/odata/CMU/SalaReservas(Id={Id})")]
        public IActionResult DeleteSalaReserva(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SalaReservas
                    .Where(i => i.Id == key)
                    .Include(i => i.SalaReservaRegistros)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReserva>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaReservaDeleted(item);
                this.context.SalaReservas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalaReservaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaReservaUpdated(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaUpdated(SGPA.Server.Models.CMU.SalaReserva item);

        [HttpPut("/odata/CMU/SalaReservas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalaReserva(int key, [FromBody]SGPA.Server.Models.CMU.SalaReserva item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaReservas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReserva>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaReservaUpdated(item);
                this.context.SalaReservas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MyFileDatum,SalaOrganizador,SalaCmu");
                this.OnAfterSalaReservaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SalaReservas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalaReserva(int key, [FromBody]Delta<SGPA.Server.Models.CMU.SalaReserva> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaReservas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaReserva>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSalaReservaUpdated(item);
                this.context.SalaReservas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "MyFileDatum,SalaOrganizador,SalaCmu");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaReservaCreated(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaCreated(SGPA.Server.Models.CMU.SalaReserva item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SalaReserva item)
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

                this.OnSalaReservaCreated(item);
                this.context.SalaReservas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaReservas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "MyFileDatum,SalaOrganizador,SalaCmu");

                this.OnAfterSalaReservaCreated(item);

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
