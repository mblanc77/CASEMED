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
    [Route("odata/CMU/TramiteInfoadjuntatitulos")]
    public partial class TramiteInfoadjuntatitulosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteInfoadjuntatitulosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> GetTramiteInfoadjuntatitulos()
        {
            var items = this.context.TramiteInfoadjuntatitulos.AsQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>();
            this.OnTramiteInfoadjuntatitulosRead(ref items);

            return items;
        }

        partial void OnTramiteInfoadjuntatitulosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> items);

        partial void OnTramiteInfoadjuntatituloGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteInfoadjuntatitulos(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> GetTramiteInfoadjuntatitulo(int key)
        {
            var items = this.context.TramiteInfoadjuntatitulos.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteInfoadjuntatituloGet(ref result);

            return result;
        }
        partial void OnTramiteInfoadjuntatituloDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        [HttpDelete("/odata/CMU/TramiteInfoadjuntatitulos(OID={OID})")]
        public IActionResult DeleteTramiteInfoadjuntatitulo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteInfoadjuntatitulos
                    .Where(i => i.OID == key)
                    .Include(i => i.TramiteInfoadjuntaespecialidads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntatituloDeleted(item);
                this.context.TramiteInfoadjuntatitulos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteInfoadjuntatituloDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntatituloUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        [HttpPut("/odata/CMU/TramiteInfoadjuntatitulos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteInfoadjuntatitulo(int key, [FromBody]SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntatitulos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntatituloUpdated(item);
                this.context.TramiteInfoadjuntatitulos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntatitulos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase,UniversidadTituloGrado");
                this.OnAfterTramiteInfoadjuntatituloUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteInfoadjuntatitulos(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteInfoadjuntatitulo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntatitulos
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteInfoadjuntatituloUpdated(item);
                this.context.TramiteInfoadjuntatitulos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntatitulos.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase,UniversidadTituloGrado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntatituloCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item)
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

                this.OnTramiteInfoadjuntatituloCreated(item);
                this.context.TramiteInfoadjuntatitulos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntatitulos.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase,UniversidadTituloGrado");

                this.OnAfterTramiteInfoadjuntatituloCreated(item);

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
