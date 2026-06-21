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
    [Route("odata/CMU/DebitoNominas")]
    public partial class DebitoNominasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public DebitoNominasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.DebitoNomina> GetDebitoNominas()
        {
            var items = this.context.DebitoNominas.AsQueryable<SGPA.Server.Models.CMU.DebitoNomina>();
            this.OnDebitoNominasRead(ref items);

            return items;
        }

        partial void OnDebitoNominasRead(ref IQueryable<SGPA.Server.Models.CMU.DebitoNomina> items);

        partial void OnDebitoNominaGet(ref SingleResult<SGPA.Server.Models.CMU.DebitoNomina> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/DebitoNominas(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.DebitoNomina> GetDebitoNomina(int key)
        {
            var items = this.context.DebitoNominas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnDebitoNominaGet(ref result);

            return result;
        }
        partial void OnDebitoNominaDeleted(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaDeleted(SGPA.Server.Models.CMU.DebitoNomina item);

        [HttpDelete("/odata/CMU/DebitoNominas(Id={Id})")]
        public IActionResult DeleteDebitoNomina(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.DebitoNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoNominaDeleted(item);
                this.context.DebitoNominas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDebitoNominaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoNominaUpdated(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaUpdated(SGPA.Server.Models.CMU.DebitoNomina item);

        [HttpPut("/odata/CMU/DebitoNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDebitoNomina(int key, [FromBody]SGPA.Server.Models.CMU.DebitoNomina item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DebitoNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoNomina>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDebitoNominaUpdated(item);
                this.context.DebitoNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,CobroNomina");
                this.OnAfterDebitoNominaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/DebitoNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDebitoNomina(int key, [FromBody]Delta<SGPA.Server.Models.CMU.DebitoNomina> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.DebitoNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.DebitoNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDebitoNominaUpdated(item);
                this.context.DebitoNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,CobroNomina");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDebitoNominaCreated(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaCreated(SGPA.Server.Models.CMU.DebitoNomina item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.DebitoNomina item)
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

                this.OnDebitoNominaCreated(item);
                this.context.DebitoNominas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DebitoNominas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Debito1,CobroNomina");

                this.OnAfterDebitoNominaCreated(item);

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
