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
    [Route("odata/Sgpa/BajaMotivos")]
    public partial class BajaMotivosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public BajaMotivosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.BajaMotivo> GetBajaMotivos()
        {
            var items = this.context.BajaMotivos.AsQueryable<SgpaNew.Server.Models.Sgpa.BajaMotivo>();
            this.OnBajaMotivosRead(ref items);

            return items;
        }

        partial void OnBajaMotivosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.BajaMotivo> items);

        partial void OnBajaMotivoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.BajaMotivo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/BajaMotivos(CodBajaMotivo={CodBajaMotivo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.BajaMotivo> GetBajaMotivo(int key)
        {
            var items = this.context.BajaMotivos.Where(i => i.CodBajaMotivo == key);
            var result = SingleResult.Create(items);

            OnBajaMotivoGet(ref result);

            return result;
        }
        partial void OnBajaMotivoDeleted(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoDeleted(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        [HttpDelete("/odata/Sgpa/BajaMotivos(CodBajaMotivo={CodBajaMotivo})")]
        public IActionResult DeleteBajaMotivo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BajaMotivos
                    .Where(i => i.CodBajaMotivo == key)
                    .Include(i => i.Trabajas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.BajaMotivo>(Request, items);

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

        partial void OnBajaMotivoUpdated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoUpdated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        [HttpPut("/odata/Sgpa/BajaMotivos(CodBajaMotivo={CodBajaMotivo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBajaMotivo(int key, [FromBody]SgpaNew.Server.Models.Sgpa.BajaMotivo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaMotivos
                    .Where(i => i.CodBajaMotivo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.BajaMotivo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBajaMotivoUpdated(item);
                this.context.BajaMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaMotivos.Where(i => i.CodBajaMotivo == key);
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

        [HttpPatch("/odata/Sgpa/BajaMotivos(CodBajaMotivo={CodBajaMotivo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBajaMotivo(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.BajaMotivo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BajaMotivos
                    .Where(i => i.CodBajaMotivo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.BajaMotivo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBajaMotivoUpdated(item);
                this.context.BajaMotivos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BajaMotivos.Where(i => i.CodBajaMotivo == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBajaMotivoCreated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoCreated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.BajaMotivo item)
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

                var itemToReturn = this.context.BajaMotivos.Where(i => i.CodBajaMotivo == item.CodBajaMotivo);

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
