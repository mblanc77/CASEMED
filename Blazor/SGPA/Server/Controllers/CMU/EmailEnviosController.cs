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
    [Route("odata/CMU/EmailEnvios")]
    public partial class EmailEnviosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public EmailEnviosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.EmailEnvio> GetEmailEnvios()
        {
            var items = this.context.EmailEnvios.AsQueryable<SGPA.Server.Models.CMU.EmailEnvio>();
            this.OnEmailEnviosRead(ref items);

            return items;
        }

        partial void OnEmailEnviosRead(ref IQueryable<SGPA.Server.Models.CMU.EmailEnvio> items);

        partial void OnEmailEnvioGet(ref SingleResult<SGPA.Server.Models.CMU.EmailEnvio> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/EmailEnvios(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.EmailEnvio> GetEmailEnvio(int key)
        {
            var items = this.context.EmailEnvios.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnEmailEnvioGet(ref result);

            return result;
        }
        partial void OnEmailEnvioDeleted(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioDeleted(SGPA.Server.Models.CMU.EmailEnvio item);

        [HttpDelete("/odata/CMU/EmailEnvios(Id={Id})")]
        public IActionResult DeleteEmailEnvio(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.EmailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.EmailEnvio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEnvioDeleted(item);
                this.context.EmailEnvios.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmailEnvioDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEnvioUpdated(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioUpdated(SGPA.Server.Models.CMU.EmailEnvio item);

        [HttpPut("/odata/CMU/EmailEnvios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmailEnvio(int key, [FromBody]SGPA.Server.Models.CMU.EmailEnvio item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.EmailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.EmailEnvio>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEnvioUpdated(item);
                this.context.EmailEnvios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmailEnvios.Where(i => i.Id == key);
                ;
                this.OnAfterEmailEnvioUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/EmailEnvios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmailEnvio(int key, [FromBody]Delta<SGPA.Server.Models.CMU.EmailEnvio> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.EmailEnvios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.EmailEnvio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmailEnvioUpdated(item);
                this.context.EmailEnvios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmailEnvios.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEnvioCreated(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioCreated(SGPA.Server.Models.CMU.EmailEnvio item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.EmailEnvio item)
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

                this.OnEmailEnvioCreated(item);
                this.context.EmailEnvios.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.EmailEnvios.Where(i => i.Id == item.Id);

                ;

                this.OnAfterEmailEnvioCreated(item);

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
