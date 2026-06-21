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
    [Route("odata/CMU/ColegiadoBitacoras")]
    public partial class ColegiadoBitacorasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ColegiadoBitacorasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ColegiadoBitacora> GetColegiadoBitacoras()
        {
            var items = this.context.ColegiadoBitacoras.AsQueryable<SGPA.Server.Models.CMU.ColegiadoBitacora>();
            this.OnColegiadoBitacorasRead(ref items);

            return items;
        }

        partial void OnColegiadoBitacorasRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacora> items);

        partial void OnColegiadoBitacoraGet(ref SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacora> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ColegiadoBitacoras(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ColegiadoBitacora> GetColegiadoBitacora(int key)
        {
            var items = this.context.ColegiadoBitacoras.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnColegiadoBitacoraGet(ref result);

            return result;
        }
        partial void OnColegiadoBitacoraDeleted(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraDeleted(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        [HttpDelete("/odata/CMU/ColegiadoBitacoras(Id={Id})")]
        public IActionResult DeleteColegiadoBitacora(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ColegiadoBitacoras
                    .Where(i => i.Id == key)
                    .Include(i => i.ColegiadoBitacoraNota)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacora>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraDeleted(item);
                this.context.ColegiadoBitacoras.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiadoBitacoraDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraUpdated(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraUpdated(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        [HttpPut("/odata/CMU/ColegiadoBitacoras(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiadoBitacora(int key, [FromBody]SGPA.Server.Models.CMU.ColegiadoBitacora item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoras
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacora>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiadoBitacoraUpdated(item);
                this.context.ColegiadoBitacoras.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoras.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");
                this.OnAfterColegiadoBitacoraUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ColegiadoBitacoras(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiadoBitacora(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ColegiadoBitacora> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ColegiadoBitacoras
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ColegiadoBitacora>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiadoBitacoraUpdated(item);
                this.context.ColegiadoBitacoras.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoras.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiadoBitacoraCreated(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraCreated(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ColegiadoBitacora item)
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

                this.OnColegiadoBitacoraCreated(item);
                this.context.ColegiadoBitacoras.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ColegiadoBitacoras.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");

                this.OnAfterColegiadoBitacoraCreated(item);

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
