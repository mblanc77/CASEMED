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
    [Route("odata/CMU/TramiteCarnes")]
    public partial class TramiteCarnesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteCarnesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteCarne> GetTramiteCarnes()
        {
            var items = this.context.TramiteCarnes.AsQueryable<SGPA.Server.Models.CMU.TramiteCarne>();
            this.OnTramiteCarnesRead(ref items);

            return items;
        }

        partial void OnTramiteCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarne> items);

        partial void OnTramiteCarneGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteCarne> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteCarnes(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteCarne> GetTramiteCarne(int key)
        {
            var items = this.context.TramiteCarnes.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteCarneGet(ref result);

            return result;
        }
        partial void OnTramiteCarneDeleted(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneDeleted(SGPA.Server.Models.CMU.TramiteCarne item);

        [HttpDelete("/odata/CMU/TramiteCarnes(OID={OID})")]
        public IActionResult DeleteTramiteCarne(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteCarnes
                    .Where(i => i.OID == key)
                    .Include(i => i.TramiteInfoadjuntabases)
                    .Include(i => i.TramiteCarneEstados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneDeleted(item);
                this.context.TramiteCarnes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteCarneDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneUpdated(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneUpdated(SGPA.Server.Models.CMU.TramiteCarne item);

        [HttpPut("/odata/CMU/TramiteCarnes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteCarne(int key, [FromBody]SGPA.Server.Models.CMU.TramiteCarne item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarnes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarne>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteCarneUpdated(item);
                this.context.TramiteCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarnes.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,LugarRetiroCarne");
                this.OnAfterTramiteCarneUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteCarnes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteCarne(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteCarne> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteCarnes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteCarne>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteCarneUpdated(item);
                this.context.TramiteCarnes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarnes.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,LugarRetiroCarne");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteCarneCreated(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneCreated(SGPA.Server.Models.CMU.TramiteCarne item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteCarne item)
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

                this.OnTramiteCarneCreated(item);
                this.context.TramiteCarnes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteCarnes.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "Colegiado1,LugarRetiroCarne");

                this.OnAfterTramiteCarneCreated(item);

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
