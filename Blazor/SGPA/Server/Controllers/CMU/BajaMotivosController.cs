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
    [Route("odata/CMU/BajaMotivos")]
    public partial class BajaMotivosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public BajaMotivosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.BajaMotivo> GetBajaMotivos()
        {
            var items = this.context.BajaMotivos.AsQueryable<SGPA.Server.Models.CMU.BajaMotivo>();
            this.OnBajaMotivosRead(ref items);

            return items;
        }

        partial void OnBajaMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.BajaMotivo> items);

        partial void OnBajaMotivoGet(ref SingleResult<SGPA.Server.Models.CMU.BajaMotivo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/BajaMotivos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.BajaMotivo> GetBajaMotivo(int key)
        {
            var items = this.context.BajaMotivos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnBajaMotivoGet(ref result);

            return result;
        }
        partial void OnBajaMotivoDeleted(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoDeleted(SGPA.Server.Models.CMU.BajaMotivo item);

        [HttpDelete("/odata/CMU/BajaMotivos(Id={Id})")]
        public IActionResult DeleteBajaMotivo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BajaMotivos
                    .Where(i => i.Id == key)
                    .Include(i => i.Colegiados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBajaMotivoDeleted(item);
                this.context.BajaMotivos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBajaMotivoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBajaMotivoUpdated(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoUpdated(SGPA.Server.Models.CMU.BajaMotivo item);

        [HttpPut("/odata/CMU/BajaMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBajaMotivo(int key, [FromBody]SGPA.Server.Models.CMU.BajaMotivo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaMotivo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBajaMotivoUpdated(item);
                this.context.BajaMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaMotivos.Where(i => i.Id == key);
                ;
                this.OnAfterBajaMotivoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/BajaMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBajaMotivo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.BajaMotivo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBajaMotivoUpdated(item);
                this.context.BajaMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaMotivos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBajaMotivoCreated(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoCreated(SGPA.Server.Models.CMU.BajaMotivo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.BajaMotivo item)
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

                this.OnBajaMotivoCreated(item);
                this.context.BajaMotivos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaMotivos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterBajaMotivoCreated(item);

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
