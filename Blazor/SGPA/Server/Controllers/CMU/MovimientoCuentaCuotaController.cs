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
    [Route("odata/CMU/MovimientoCuentaCuota")]
    public partial class MovimientoCuentaCuotaController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MovimientoCuentaCuotaController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> GetMovimientoCuentaCuota()
        {
            var items = this.context.MovimientoCuentaCuota.AsQueryable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>();
            this.OnMovimientoCuentaCuotaRead(ref items);

            return items;
        }

        partial void OnMovimientoCuentaCuotaRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> items);

        partial void OnMovimientoCuentaCuotumGet(ref SingleResult<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MovimientoCuentaCuota(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> GetMovimientoCuentaCuotum(int key)
        {
            var items = this.context.MovimientoCuentaCuota.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnMovimientoCuentaCuotumGet(ref result);

            return result;
        }
        partial void OnMovimientoCuentaCuotumDeleted(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumDeleted(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        [HttpDelete("/odata/CMU/MovimientoCuentaCuota(Id={Id})")]
        public IActionResult DeleteMovimientoCuentaCuotum(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MovimientoCuentaCuota
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentaCuotumDeleted(item);
                this.context.MovimientoCuentaCuota.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMovimientoCuentaCuotumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentaCuotumUpdated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumUpdated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        [HttpPut("/odata/CMU/MovimientoCuentaCuota(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMovimientoCuentaCuotum(int key, [FromBody]SGPA.Server.Models.CMU.MovimientoCuentaCuotum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuentaCuota
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMovimientoCuentaCuotumUpdated(item);
                this.context.MovimientoCuentaCuota.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaCuota.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,MovimientoCuentum");
                this.OnAfterMovimientoCuentaCuotumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MovimientoCuentaCuota(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMovimientoCuentaCuotum(int key, [FromBody]Delta<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MovimientoCuentaCuota
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMovimientoCuentaCuotumUpdated(item);
                this.context.MovimientoCuentaCuota.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaCuota.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,MovimientoCuentum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMovimientoCuentaCuotumCreated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumCreated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MovimientoCuentaCuotum item)
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

                this.OnMovimientoCuentaCuotumCreated(item);
                this.context.MovimientoCuentaCuota.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MovimientoCuentaCuota.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "CategoriaColegiado,MovimientoCuentum");

                this.OnAfterMovimientoCuentaCuotumCreated(item);

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
