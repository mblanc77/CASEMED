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
    [Route("odata/Sgpa/AporteTipos")]
    public partial class AporteTiposController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AporteTiposController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AporteTipo> GetAporteTipos()
        {
            var items = this.context.AporteTipos.AsQueryable<SgpaNew.Server.Models.Sgpa.AporteTipo>();
            this.OnAporteTiposRead(ref items);

            return items;
        }

        partial void OnAporteTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AporteTipo> items);

        partial void OnAporteTipoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AporteTipo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AporteTipos(CodAporteTipo={CodAporteTipo})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AporteTipo> GetAporteTipo(string key)
        {
            var items = this.context.AporteTipos.Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAporteTipoGet(ref result);

            return result;
        }
        partial void OnAporteTipoDeleted(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoDeleted(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        [HttpDelete("/odata/Sgpa/AporteTipos(CodAporteTipo={CodAporteTipo})")]
        public IActionResult DeleteAporteTipo(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AporteTipos
                    .Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AporteTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAporteTipoDeleted(item);
                this.context.AporteTipos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAporteTipoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAporteTipoUpdated(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoUpdated(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        [HttpPut("/odata/Sgpa/AporteTipos(CodAporteTipo={CodAporteTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAporteTipo(string key, [FromBody]SgpaNew.Server.Models.Sgpa.AporteTipo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AporteTipos
                    .Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AporteTipo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAporteTipoUpdated(item);
                this.context.AporteTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AporteTipos.Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key));
                ;
                this.OnAfterAporteTipoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AporteTipos(CodAporteTipo={CodAporteTipo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAporteTipo(string key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AporteTipo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AporteTipos
                    .Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AporteTipo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAporteTipoUpdated(item);
                this.context.AporteTipos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AporteTipos.Where(i => i.CodAporteTipo == Uri.UnescapeDataString(key));
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAporteTipoCreated(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoCreated(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AporteTipo item)
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

                this.OnAporteTipoCreated(item);
                this.context.AporteTipos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AporteTipos.Where(i => i.CodAporteTipo == item.CodAporteTipo);

                ;

                this.OnAfterAporteTipoCreated(item);

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
