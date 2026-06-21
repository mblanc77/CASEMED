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
    [Route("odata/CMU/TramiteInfoadjuntabases")]
    public partial class TramiteInfoadjuntabasesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteInfoadjuntabasesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> GetTramiteInfoadjuntabases()
        {
            var items = this.context.TramiteInfoadjuntabases.AsQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>();
            this.OnTramiteInfoadjuntabasesRead(ref items);

            return items;
        }

        partial void OnTramiteInfoadjuntabasesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> items);

        partial void OnTramiteInfoadjuntabaseGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteInfoadjuntabases(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> GetTramiteInfoadjuntabase(int key)
        {
            var items = this.context.TramiteInfoadjuntabases.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteInfoadjuntabaseGet(ref result);

            return result;
        }
        partial void OnTramiteInfoadjuntabaseDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        [HttpDelete("/odata/CMU/TramiteInfoadjuntabases(OID={OID})")]
        public IActionResult DeleteTramiteInfoadjuntabase(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteInfoadjuntabases
                    .Where(i => i.OID == key)
                    .Include(i => i.TramiteInfoadjuntacedulas)
                    .Include(i => i.TramiteInfoadjuntafotocarnes)
                    .Include(i => i.TramiteInfoadjuntatitulos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntabaseDeleted(item);
                this.context.TramiteInfoadjuntabases.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteInfoadjuntabaseDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntabaseUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        [HttpPut("/odata/CMU/TramiteInfoadjuntabases(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteInfoadjuntabase(int key, [FromBody]SGPA.Server.Models.CMU.TramiteInfoadjuntabase item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntabases
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntabaseUpdated(item);
                this.context.TramiteInfoadjuntabases.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntabases.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,TramiteCarne");
                this.OnAfterTramiteInfoadjuntabaseUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteInfoadjuntabases(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteInfoadjuntabase(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntabases
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteInfoadjuntabaseUpdated(item);
                this.context.TramiteInfoadjuntabases.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntabases.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,TramiteCarne");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntabaseCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteInfoadjuntabase item)
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

                this.OnTramiteInfoadjuntabaseCreated(item);
                this.context.TramiteInfoadjuntabases.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntabases.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "XpobjectType,TramiteCarne");

                this.OnAfterTramiteInfoadjuntabaseCreated(item);

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
