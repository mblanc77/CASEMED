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
    [Route("odata/CMU/ActaConsejos")]
    public partial class ActaConsejosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ActaConsejosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ActaConsejo> GetActaConsejos()
        {
            var items = this.context.ActaConsejos.AsQueryable<SGPA.Server.Models.CMU.ActaConsejo>();
            this.OnActaConsejosRead(ref items);

            return items;
        }

        partial void OnActaConsejosRead(ref IQueryable<SGPA.Server.Models.CMU.ActaConsejo> items);

        partial void OnActaConsejoGet(ref SingleResult<SGPA.Server.Models.CMU.ActaConsejo> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ActaConsejos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ActaConsejo> GetActaConsejo(int key)
        {
            var items = this.context.ActaConsejos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnActaConsejoGet(ref result);

            return result;
        }
        partial void OnActaConsejoDeleted(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoDeleted(SGPA.Server.Models.CMU.ActaConsejo item);

        [HttpDelete("/odata/CMU/ActaConsejos(Id={Id})")]
        public IActionResult DeleteActaConsejo(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ActaConsejos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ActaConsejo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnActaConsejoDeleted(item);
                this.context.ActaConsejos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterActaConsejoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnActaConsejoUpdated(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoUpdated(SGPA.Server.Models.CMU.ActaConsejo item);

        [HttpPut("/odata/CMU/ActaConsejos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutActaConsejo(int key, [FromBody]SGPA.Server.Models.CMU.ActaConsejo item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ActaConsejos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ActaConsejo>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnActaConsejoUpdated(item);
                this.context.ActaConsejos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ActaConsejos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum");
                this.OnAfterActaConsejoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ActaConsejos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchActaConsejo(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ActaConsejo> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ActaConsejos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ActaConsejo>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnActaConsejoUpdated(item);
                this.context.ActaConsejos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ActaConsejos.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnActaConsejoCreated(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoCreated(SGPA.Server.Models.CMU.ActaConsejo item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ActaConsejo item)
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

                this.OnActaConsejoCreated(item);
                this.context.ActaConsejos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ActaConsejos.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "FileDatum");

                this.OnAfterActaConsejoCreated(item);

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
