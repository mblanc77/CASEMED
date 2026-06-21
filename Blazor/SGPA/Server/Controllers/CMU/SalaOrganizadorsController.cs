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
    [Route("odata/CMU/SalaOrganizadors")]
    public partial class SalaOrganizadorsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public SalaOrganizadorsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.SalaOrganizador> GetSalaOrganizadors()
        {
            var items = this.context.SalaOrganizadors.AsQueryable<SGPA.Server.Models.CMU.SalaOrganizador>();
            this.OnSalaOrganizadorsRead(ref items);

            return items;
        }

        partial void OnSalaOrganizadorsRead(ref IQueryable<SGPA.Server.Models.CMU.SalaOrganizador> items);

        partial void OnSalaOrganizadorGet(ref SingleResult<SGPA.Server.Models.CMU.SalaOrganizador> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/SalaOrganizadors(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.SalaOrganizador> GetSalaOrganizador(int key)
        {
            var items = this.context.SalaOrganizadors.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnSalaOrganizadorGet(ref result);

            return result;
        }
        partial void OnSalaOrganizadorDeleted(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorDeleted(SGPA.Server.Models.CMU.SalaOrganizador item);

        [HttpDelete("/odata/CMU/SalaOrganizadors(Id={Id})")]
        public IActionResult DeleteSalaOrganizador(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SalaOrganizadors
                    .Where(i => i.Id == key)
                    .Include(i => i.SalaReservas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaOrganizador>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaOrganizadorDeleted(item);
                this.context.SalaOrganizadors.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalaOrganizadorDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaOrganizadorUpdated(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorUpdated(SGPA.Server.Models.CMU.SalaOrganizador item);

        [HttpPut("/odata/CMU/SalaOrganizadors(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalaOrganizador(int key, [FromBody]SGPA.Server.Models.CMU.SalaOrganizador item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaOrganizadors
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaOrganizador>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalaOrganizadorUpdated(item);
                this.context.SalaOrganizadors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaOrganizadors.Where(i => i.Id == key);
                ;
                this.OnAfterSalaOrganizadorUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/SalaOrganizadors(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalaOrganizador(int key, [FromBody]Delta<SGPA.Server.Models.CMU.SalaOrganizador> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalaOrganizadors
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.SalaOrganizador>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSalaOrganizadorUpdated(item);
                this.context.SalaOrganizadors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaOrganizadors.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalaOrganizadorCreated(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorCreated(SGPA.Server.Models.CMU.SalaOrganizador item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.SalaOrganizador item)
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

                this.OnSalaOrganizadorCreated(item);
                this.context.SalaOrganizadors.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalaOrganizadors.Where(i => i.Id == item.Id);

                ;

                this.OnAfterSalaOrganizadorCreated(item);

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
