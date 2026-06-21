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
    [Route("odata/CMU/TramiteInfoadjuntafotocarnes")]
    public partial class TramiteInfoadjuntafotocarnesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteInfoadjuntafotocarnesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> GetTramiteInfoadjuntafotocarnes()
        {
            var items = this.context.TramiteInfoadjuntafotocarnes.AsQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>();
            this.OnTramiteInfoadjuntafotocarnesRead(ref items);

            return items;
        }

        partial void OnTramiteInfoadjuntafotocarnesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> items);

        partial void OnTramiteInfoadjuntafotocarneGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteInfoadjuntafotocarnes(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> GetTramiteInfoadjuntafotocarne(int key)
        {
            var items = this.context.TramiteInfoadjuntafotocarnes.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteInfoadjuntafotocarneGet(ref result);

            return result;
        }
        partial void OnTramiteInfoadjuntafotocarneDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        [HttpDelete("/odata/CMU/TramiteInfoadjuntafotocarnes(OID={OID})")]
        public IActionResult DeleteTramiteInfoadjuntafotocarne(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteInfoadjuntafotocarnes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntafotocarneDeleted(item);
                this.context.TramiteInfoadjuntafotocarnes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteInfoadjuntafotocarneDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntafotocarneUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        [HttpPut("/odata/CMU/TramiteInfoadjuntafotocarnes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteInfoadjuntafotocarne(int key, [FromBody]SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntafotocarnes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntafotocarneUpdated(item);
                this.context.TramiteInfoadjuntafotocarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntafotocarnes.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");
                this.OnAfterTramiteInfoadjuntafotocarneUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteInfoadjuntafotocarnes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteInfoadjuntafotocarne(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntafotocarnes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteInfoadjuntafotocarneUpdated(item);
                this.context.TramiteInfoadjuntafotocarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntafotocarnes.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntafotocarneCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item)
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

                this.OnTramiteInfoadjuntafotocarneCreated(item);
                this.context.TramiteInfoadjuntafotocarnes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntafotocarnes.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");

                this.OnAfterTramiteInfoadjuntafotocarneCreated(item);

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
