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
    [Route("odata/Sgpa/Imponibles")]
    public partial class ImponiblesController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public ImponiblesController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Imponible> GetImponibles()
        {
            var items = this.context.Imponibles.AsQueryable<SgpaNew.Server.Models.Sgpa.Imponible>();
            this.OnImponiblesRead(ref items);

            return items;
        }

        partial void OnImponiblesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Imponible> items);

        partial void OnImponibleGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Imponible> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Imponibles(ImponibleId={ImponibleId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Imponible> GetImponible(int key)
        {
            var items = this.context.Imponibles.Where(i => i.ImponibleId == key);
            var result = SingleResult.Create(items);

            OnImponibleGet(ref result);

            return result;
        }
        partial void OnImponibleDeleted(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleDeleted(SgpaNew.Server.Models.Sgpa.Imponible item);

        [HttpDelete("/odata/Sgpa/Imponibles(ImponibleId={ImponibleId})")]
        public IActionResult DeleteImponible(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Imponibles
                    .Where(i => i.ImponibleId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Imponible>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnImponibleDeleted(item);
                this.context.Imponibles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterImponibleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnImponibleUpdated(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleUpdated(SgpaNew.Server.Models.Sgpa.Imponible item);

        [HttpPut("/odata/Sgpa/Imponibles(ImponibleId={ImponibleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutImponible(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Imponible item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Imponibles
                    .Where(i => i.ImponibleId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Imponible>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnImponibleUpdated(item);
                this.context.Imponibles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Imponibles.Where(i => i.ImponibleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Trabaja");
                this.OnAfterImponibleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Imponibles(ImponibleId={ImponibleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchImponible(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Imponible> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Imponibles
                    .Where(i => i.ImponibleId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Imponible>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnImponibleUpdated(item);
                this.context.Imponibles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Imponibles.Where(i => i.ImponibleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Trabaja");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnImponibleCreated(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleCreated(SgpaNew.Server.Models.Sgpa.Imponible item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Imponible item)
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

                this.OnImponibleCreated(item);
                this.context.Imponibles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Imponibles.Where(i => i.ImponibleId == item.ImponibleId);

                Request.QueryString = Request.QueryString.Add("$expand", "Trabaja");

                this.OnAfterImponibleCreated(item);

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
