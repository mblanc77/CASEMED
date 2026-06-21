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
    [Route("odata/Sgpa/PrimaFallecimientos")]
    public partial class PrimaFallecimientosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public PrimaFallecimientosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> GetPrimaFallecimientos()
        {
            var items = this.context.PrimaFallecimientos.AsQueryable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>();
            this.OnPrimaFallecimientosRead(ref items);

            return items;
        }

        partial void OnPrimaFallecimientosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> items);

        partial void OnPrimaFallecimientoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/PrimaFallecimientos(CI={CI})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> GetPrimaFallecimiento(int key)
        {
            var items = this.context.PrimaFallecimientos.Where(i => i.CI == key);
            var result = SingleResult.Create(items);

            OnPrimaFallecimientoGet(ref result);

            return result;
        }
        partial void OnPrimaFallecimientoDeleted(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoDeleted(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        [HttpDelete("/odata/Sgpa/PrimaFallecimientos(CI={CI})")]
        public IActionResult DeletePrimaFallecimiento(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.PrimaFallecimientos
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrimaFallecimientoDeleted(item);
                this.context.PrimaFallecimientos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPrimaFallecimientoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrimaFallecimientoUpdated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoUpdated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        [HttpPut("/odata/Sgpa/PrimaFallecimientos(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPrimaFallecimiento(int key, [FromBody]SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PrimaFallecimientos
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPrimaFallecimientoUpdated(item);
                this.context.PrimaFallecimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrimaFallecimientos.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                this.OnAfterPrimaFallecimientoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/PrimaFallecimientos(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPrimaFallecimiento(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PrimaFallecimientos
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPrimaFallecimientoUpdated(item);
                this.context.PrimaFallecimientos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrimaFallecimientos.Where(i => i.CI == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPrimaFallecimientoCreated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoCreated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item)
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

                this.OnPrimaFallecimientoCreated(item);
                this.context.PrimaFallecimientos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PrimaFallecimientos.Where(i => i.CI == item.CI);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");

                this.OnAfterPrimaFallecimientoCreated(item);

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
