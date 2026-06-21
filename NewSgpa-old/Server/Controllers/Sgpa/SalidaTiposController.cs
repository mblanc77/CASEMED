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
    [Route("odata/Sgpa/SalidaTipos")]
    public partial class SalidaTiposController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SalidaTiposController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SalidaTipo> GetSalidaTipos()
        {
            var items = this.context.SalidaTipos.AsQueryable<SgpaNew.Server.Models.Sgpa.SalidaTipo>();
            this.OnSalidaTiposRead(ref items);

            return items;
        }

        partial void OnSalidaTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SalidaTipo> items);

        partial void OnSalidaTipoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SalidaTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SalidaTipos(CodSalidaTipo={CodSalidaTipo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SalidaTipo> GetSalidaTipo(short key)
        {
            var items = this.context.SalidaTipos.Where(i => i.CodSalidaTipo == key);
            var result = SingleResult.Create(items);

            OnSalidaTipoGet(ref result);

            return result;
        }
        partial void OnSalidaTipoDeleted(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoDeleted(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        [HttpDelete("/odata/Sgpa/SalidaTipos(CodSalidaTipo={CodSalidaTipo})")]
        public IActionResult DeleteSalidaTipo(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SalidaTipos
                    .Where(i => i.CodSalidaTipo == key)
                    .Include(i => i.Certificacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SalidaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalidaTipoDeleted(item);
                this.context.SalidaTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSalidaTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalidaTipoUpdated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoUpdated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        [HttpPut("/odata/Sgpa/SalidaTipos(CodSalidaTipo={CodSalidaTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSalidaTipo(short key, [FromBody]SgpaNew.Server.Models.Sgpa.SalidaTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalidaTipos
                    .Where(i => i.CodSalidaTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SalidaTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSalidaTipoUpdated(item);
                this.context.SalidaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalidaTipos.Where(i => i.CodSalidaTipo == key);
                ;
                this.OnAfterSalidaTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SalidaTipos(CodSalidaTipo={CodSalidaTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSalidaTipo(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SalidaTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SalidaTipos
                    .Where(i => i.CodSalidaTipo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SalidaTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSalidaTipoUpdated(item);
                this.context.SalidaTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalidaTipos.Where(i => i.CodSalidaTipo == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSalidaTipoCreated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoCreated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SalidaTipo item)
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

                this.OnSalidaTipoCreated(item);
                this.context.SalidaTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SalidaTipos.Where(i => i.CodSalidaTipo == item.CodSalidaTipo);

                ;

                this.OnAfterSalidaTipoCreated(item);

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
