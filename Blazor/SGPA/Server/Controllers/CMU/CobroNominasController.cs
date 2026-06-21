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
    [Route("odata/CMU/CobroNominas")]
    public partial class CobroNominasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public CobroNominasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.CobroNomina> GetCobroNominas()
        {
            var items = this.context.CobroNominas.AsQueryable<SGPA.Server.Models.CMU.CobroNomina>();
            this.OnCobroNominasRead(ref items);

            return items;
        }

        partial void OnCobroNominasRead(ref IQueryable<SGPA.Server.Models.CMU.CobroNomina> items);

        partial void OnCobroNominaGet(ref SingleResult<SGPA.Server.Models.CMU.CobroNomina> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/CobroNominas(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.CobroNomina> GetCobroNomina(int key)
        {
            var items = this.context.CobroNominas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnCobroNominaGet(ref result);

            return result;
        }
        partial void OnCobroNominaDeleted(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaDeleted(SGPA.Server.Models.CMU.CobroNomina item);

        [HttpDelete("/odata/CMU/CobroNominas(Id={Id})")]
        public IActionResult DeleteCobroNomina(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CobroNominas
                    .Where(i => i.Id == key)
                    .Include(i => i.DebitoNominas)
                    .Include(i => i.DepositoNominas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CobroNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCobroNominaDeleted(item);
                this.context.CobroNominas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCobroNominaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCobroNominaUpdated(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaUpdated(SGPA.Server.Models.CMU.CobroNomina item);

        [HttpPut("/odata/CMU/CobroNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCobroNomina(int key, [FromBody]SGPA.Server.Models.CMU.CobroNomina item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CobroNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CobroNomina>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCobroNominaUpdated(item);
                this.context.CobroNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CobroNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Cobro1,Colegiado1,Convenio1,MovimientoCuentum,XpobjectType");
                this.OnAfterCobroNominaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/CobroNominas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCobroNomina(int key, [FromBody]Delta<SGPA.Server.Models.CMU.CobroNomina> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CobroNominas
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.CobroNomina>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCobroNominaUpdated(item);
                this.context.CobroNominas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CobroNominas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Cobro1,Colegiado1,Convenio1,MovimientoCuentum,XpobjectType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCobroNominaCreated(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaCreated(SGPA.Server.Models.CMU.CobroNomina item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.CobroNomina item)
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

                this.OnCobroNominaCreated(item);
                this.context.CobroNominas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CobroNominas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Cobro1,Colegiado1,Convenio1,MovimientoCuentum,XpobjectType");

                this.OnAfterCobroNominaCreated(item);

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
