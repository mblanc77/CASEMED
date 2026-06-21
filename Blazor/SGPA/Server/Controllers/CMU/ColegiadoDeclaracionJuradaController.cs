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
    [Route("odata/CMU/ColegiadoDeclaracionJurada")]
    public partial class ColegiadoDeclaracionJuradaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoDeclaracionJuradaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> GetColegiadoDeclaracionJurada()
        {
            var items = this.context.ColegiadoDeclaracionJurada.AsQueryable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>();
            this.OnColegiadoDeclaracionJuradaRead(ref items);

            return items;
        }

        partial void OnColegiadoDeclaracionJuradaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> items);

        partial void OnColegiadoDeclaracionJuradumGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoDeclaracionJurada(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> GetColegiadoDeclaracionJuradum(int key)
        {
            var items = this.context.ColegiadoDeclaracionJurada.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoDeclaracionJuradumGet(ref result);

            return result;
        }
        partial void OnColegiadoDeclaracionJuradumDeleted(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumDeleted(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        [HttpDelete("/odata/CMU/ColegiadoDeclaracionJurada(Id={Id})")]
        public IActionResult DeleteColegiadoDeclaracionJuradum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoDeclaracionJurada
                    .Where(i => i.Id == key)
                    .Include(i => i.AjusteRetroactivos)
                    .Include(i => i.DeclaracionJuradaAdjuntos)
                    .Include(i => i.SolicitudBajas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoDeclaracionJuradumDeleted(item);
                this.context.ColegiadoDeclaracionJurada.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoDeclaracionJuradumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoDeclaracionJuradumUpdated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumUpdated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        [HttpPut("/odata/CMU/ColegiadoDeclaracionJurada(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoDeclaracionJuradum(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoDeclaracionJurada
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoDeclaracionJuradumUpdated(item);
                this.context.ColegiadoDeclaracionJurada.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDeclaracionJurada.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,DjinactividadMotivo,XpobjectType,DeclaracionJuradaTipo");
                this.OnAfterColegiadoDeclaracionJuradumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoDeclaracionJurada(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoDeclaracionJuradum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoDeclaracionJurada
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoDeclaracionJuradumUpdated(item);
                this.context.ColegiadoDeclaracionJurada.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDeclaracionJurada.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,DjinactividadMotivo,XpobjectType,DeclaracionJuradaTipo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoDeclaracionJuradumCreated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumCreated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item)
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

                this.OnColegiadoDeclaracionJuradumCreated(item);
                this.context.ColegiadoDeclaracionJurada.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoDeclaracionJurada.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,DjinactividadMotivo,XpobjectType,DeclaracionJuradaTipo");

                this.OnAfterColegiadoDeclaracionJuradumCreated(item);

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
