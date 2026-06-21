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
    [Route("odata/CMU/TmpMeses")]
    public partial class TmpMesesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TmpMesesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TmpMese> GetTmpMeses()
        {
            var items = this.context.TmpMeses.AsQueryable<SGPA.Server.Models.CMU.TmpMese>();
            this.OnTmpMesesRead(ref items);

            return items;
        }

        partial void OnTmpMesesRead(ref IQueryable<SGPA.Server.Models.CMU.TmpMese> items);

        partial void OnTmpMeseGet(ref SingleResult<SGPA.Server.Models.CMU.TmpMese> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TmpMeses(Mes={keyMes},Año={keyAño})")]
        public SingleResult<SGPA.Server.Models.CMU.TmpMese> GetTmpMese([FromODataUri] int keyMes, [FromODataUri] int keyAño)
        {
            var items = this.context.TmpMeses.Where(i => i.Mes == keyMes && i.Año == keyAño);
            var result = SingleResult.Create(items);

            OnTmpMeseGet(ref result);

            return result;
        }
        partial void OnTmpMeseDeleted(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseDeleted(SGPA.Server.Models.CMU.TmpMese item);

        [HttpDelete("/odata/CMU/TmpMeses(Mes={keyMes},Año={keyAño})")]
        public IActionResult DeleteTmpMese([FromODataUri] int keyMes, [FromODataUri] int keyAño)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TmpMeses
                    .Where(i => i.Mes == keyMes && i.Año == keyAño)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpMese>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpMeseDeleted(item);
                this.context.TmpMeses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTmpMeseDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpMeseUpdated(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseUpdated(SGPA.Server.Models.CMU.TmpMese item);

        [HttpPut("/odata/CMU/TmpMeses(Mes={keyMes},Año={keyAño})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTmpMese([FromODataUri] int keyMes, [FromODataUri] int keyAño, [FromBody]SGPA.Server.Models.CMU.TmpMese item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpMeses
                    .Where(i => i.Mes == keyMes && i.Año == keyAño)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpMese>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpMeseUpdated(item);
                this.context.TmpMeses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpMeses.Where(i => i.Mes == keyMes && i.Año == keyAño);
                ;
                this.OnAfterTmpMeseUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TmpMeses(Mes={keyMes},Año={keyAño})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTmpMese([FromODataUri] int keyMes, [FromODataUri] int keyAño, [FromBody]Delta<SGPA.Server.Models.CMU.TmpMese> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpMeses
                    .Where(i => i.Mes == keyMes && i.Año == keyAño)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpMese>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTmpMeseUpdated(item);
                this.context.TmpMeses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpMeses.Where(i => i.Mes == keyMes && i.Año == keyAño);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpMeseCreated(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseCreated(SGPA.Server.Models.CMU.TmpMese item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TmpMese item)
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

                this.OnTmpMeseCreated(item);
                this.context.TmpMeses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpMeses.Where(i => i.Mes == item.Mes && i.Año == item.Año);

                ;

                this.OnAfterTmpMeseCreated(item);

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
