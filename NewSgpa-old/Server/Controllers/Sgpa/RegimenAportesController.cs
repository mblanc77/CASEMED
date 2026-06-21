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
    [Route("odata/Sgpa/RegimenAportes")]
    public partial class RegimenAportesController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public RegimenAportesController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.RegimenAporte> GetRegimenAportes()
        {
            var items = this.context.RegimenAportes.AsQueryable<SgpaNew.Server.Models.Sgpa.RegimenAporte>();
            this.OnRegimenAportesRead(ref items);

            return items;
        }

        partial void OnRegimenAportesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenAporte> items);

        partial void OnRegimenAporteGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.RegimenAporte> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/RegimenAportes(CodRegimenAporte={CodRegimenAporte})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.RegimenAporte> GetRegimenAporte(short key)
        {
            var items = this.context.RegimenAportes.Where(i => i.CodRegimenAporte == key);
            var result = SingleResult.Create(items);

            OnRegimenAporteGet(ref result);

            return result;
        }
        partial void OnRegimenAporteDeleted(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteDeleted(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        [HttpDelete("/odata/Sgpa/RegimenAportes(CodRegimenAporte={CodRegimenAporte})")]
        public IActionResult DeleteRegimenAporte(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegimenAportes
                    .Where(i => i.CodRegimenAporte == key)
                    .Include(i => i.Empresas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenAporte>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegimenAporteDeleted(item);
                this.context.RegimenAportes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegimenAporteDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegimenAporteUpdated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteUpdated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        [HttpPut("/odata/Sgpa/RegimenAportes(CodRegimenAporte={CodRegimenAporte})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegimenAporte(short key, [FromBody]SgpaNew.Server.Models.Sgpa.RegimenAporte item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegimenAportes
                    .Where(i => i.CodRegimenAporte == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenAporte>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegimenAporteUpdated(item);
                this.context.RegimenAportes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenAportes.Where(i => i.CodRegimenAporte == key);
                ;
                this.OnAfterRegimenAporteUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/RegimenAportes(CodRegimenAporte={CodRegimenAporte})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegimenAporte(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.RegimenAporte> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegimenAportes
                    .Where(i => i.CodRegimenAporte == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenAporte>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegimenAporteUpdated(item);
                this.context.RegimenAportes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenAportes.Where(i => i.CodRegimenAporte == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegimenAporteCreated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteCreated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.RegimenAporte item)
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

                this.OnRegimenAporteCreated(item);
                this.context.RegimenAportes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenAportes.Where(i => i.CodRegimenAporte == item.CodRegimenAporte);

                ;

                this.OnAfterRegimenAporteCreated(item);

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
