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
    [Route("odata/CMU/BajaTemporalMotivos")]
    public partial class BajaTemporalMotivosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public BajaTemporalMotivosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.BajaTemporalMotivo> GetBajaTemporalMotivos()
        {
            var items = this.context.BajaTemporalMotivos.AsQueryable<SGPA.Server.Models.CMU.BajaTemporalMotivo>();
            this.OnBajaTemporalMotivosRead(ref items);

            return items;
        }

        partial void OnBajaTemporalMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.BajaTemporalMotivo> items);

        partial void OnBajaTemporalMotivoGet(ref SingleResult<SGPA.Server.Models.CMU.BajaTemporalMotivo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/BajaTemporalMotivos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.BajaTemporalMotivo> GetBajaTemporalMotivo(int key)
        {
            var items = this.context.BajaTemporalMotivos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnBajaTemporalMotivoGet(ref result);

            return result;
        }
        partial void OnBajaTemporalMotivoDeleted(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoDeleted(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        [HttpDelete("/odata/CMU/BajaTemporalMotivos(Id={Id})")]
        public IActionResult DeleteBajaTemporalMotivo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BajaTemporalMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaTemporalMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBajaTemporalMotivoDeleted(item);
                this.context.BajaTemporalMotivos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBajaTemporalMotivoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBajaTemporalMotivoUpdated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoUpdated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        [HttpPut("/odata/CMU/BajaTemporalMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBajaTemporalMotivo(int key, [FromBody]SGPA.Server.Models.CMU.BajaTemporalMotivo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaTemporalMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaTemporalMotivo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBajaTemporalMotivoUpdated(item);
                this.context.BajaTemporalMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaTemporalMotivos.Where(i => i.Id == key);
                ;
                this.OnAfterBajaTemporalMotivoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/BajaTemporalMotivos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBajaTemporalMotivo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.BajaTemporalMotivo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaTemporalMotivos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.BajaTemporalMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBajaTemporalMotivoUpdated(item);
                this.context.BajaTemporalMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaTemporalMotivos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBajaTemporalMotivoCreated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoCreated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.BajaTemporalMotivo item)
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

                this.OnBajaTemporalMotivoCreated(item);
                this.context.BajaTemporalMotivos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaTemporalMotivos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterBajaTemporalMotivoCreated(item);

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
