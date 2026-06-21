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
    [Route("odata/CMU/Convenios")]
    public partial class ConveniosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ConveniosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Convenio> GetConvenios()
        {
            var items = this.context.Convenios.AsQueryable<SGPA.Server.Models.CMU.Convenio>();
            this.OnConveniosRead(ref items);

            return items;
        }

        partial void OnConveniosRead(ref IQueryable<SGPA.Server.Models.CMU.Convenio> items);

        partial void OnConvenioGet(ref SingleResult<SGPA.Server.Models.CMU.Convenio> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Convenios(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Convenio> GetConvenio(int key)
        {
            var items = this.context.Convenios.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnConvenioGet(ref result);

            return result;
        }
        partial void OnConvenioDeleted(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioDeleted(SGPA.Server.Models.CMU.Convenio item);

        [HttpDelete("/odata/CMU/Convenios(Id={Id})")]
        public IActionResult DeleteConvenio(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Convenios
                    .Where(i => i.Id == key)
                    .Include(i => i.CobroNominas)
                    .Include(i => i.ConvenioFinanciacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Convenio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnConvenioDeleted(item);
                this.context.Convenios.Remove(item);
                this.context.SaveChanges();
                this.OnAfterConvenioDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnConvenioUpdated(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioUpdated(SGPA.Server.Models.CMU.Convenio item);

        [HttpPut("/odata/CMU/Convenios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutConvenio(int key, [FromBody]SGPA.Server.Models.CMU.Convenio item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Convenios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Convenio>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnConvenioUpdated(item);
                this.context.Convenios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Convenios.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");
                this.OnAfterConvenioUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Convenios(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchConvenio(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Convenio> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Convenios
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Convenio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnConvenioUpdated(item);
                this.context.Convenios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Convenios.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnConvenioCreated(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioCreated(SGPA.Server.Models.CMU.Convenio item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Convenio item)
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

                this.OnConvenioCreated(item);
                this.context.Convenios.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Convenios.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,XpobjectType");

                this.OnAfterConvenioCreated(item);

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
