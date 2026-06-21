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
    [Route("odata/CMU/TmpFechas")]
    public partial class TmpFechasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TmpFechasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TmpFecha> GetTmpFechas()
        {
            var items = this.context.TmpFechas.AsQueryable<SGPA.Server.Models.CMU.TmpFecha>();
            this.OnTmpFechasRead(ref items);

            return items;
        }

        partial void OnTmpFechasRead(ref IQueryable<SGPA.Server.Models.CMU.TmpFecha> items);

        partial void OnTmpFechaGet(ref SingleResult<SGPA.Server.Models.CMU.TmpFecha> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TmpFechas(Fecha={Fecha})")]
        public SingleResult<SGPA.Server.Models.CMU.TmpFecha> GetTmpFecha(DateTime key)
        {
            var items = this.context.TmpFechas.Where(i => i.Fecha == key);
            var result = SingleResult.Create(items);

            OnTmpFechaGet(ref result);

            return result;
        }
        partial void OnTmpFechaDeleted(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaDeleted(SGPA.Server.Models.CMU.TmpFecha item);

        [HttpDelete("/odata/CMU/TmpFechas(Fecha={Fecha})")]
        public IActionResult DeleteTmpFecha(DateTime key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TmpFechas
                    .Where(i => i.Fecha == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpFecha>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpFechaDeleted(item);
                this.context.TmpFechas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTmpFechaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpFechaUpdated(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaUpdated(SGPA.Server.Models.CMU.TmpFecha item);

        [HttpPut("/odata/CMU/TmpFechas(Fecha={Fecha})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTmpFecha(DateTime key, [FromBody]SGPA.Server.Models.CMU.TmpFecha item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpFechas
                    .Where(i => i.Fecha == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpFecha>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpFechaUpdated(item);
                this.context.TmpFechas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpFechas.Where(i => i.Fecha == key);
                ;
                this.OnAfterTmpFechaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TmpFechas(Fecha={Fecha})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTmpFecha(DateTime key, [FromBody]Delta<SGPA.Server.Models.CMU.TmpFecha> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpFechas
                    .Where(i => i.Fecha == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpFecha>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTmpFechaUpdated(item);
                this.context.TmpFechas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpFechas.Where(i => i.Fecha == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpFechaCreated(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaCreated(SGPA.Server.Models.CMU.TmpFecha item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TmpFecha item)
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

                this.OnTmpFechaCreated(item);
                this.context.TmpFechas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpFechas.Where(i => i.Fecha == item.Fecha);

                ;

                this.OnAfterTmpFechaCreated(item);

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
