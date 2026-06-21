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
    [Route("odata/CMU/TramiteInfoadjuntacedulas")]
    public partial class TramiteInfoadjuntacedulasController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TramiteInfoadjuntacedulasController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> GetTramiteInfoadjuntacedulas()
        {
            var items = this.context.TramiteInfoadjuntacedulas.AsQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>();
            this.OnTramiteInfoadjuntacedulasRead(ref items);

            return items;
        }

        partial void OnTramiteInfoadjuntacedulasRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> items);

        partial void OnTramiteInfoadjuntacedulaGet(ref SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TramiteInfoadjuntacedulas(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> GetTramiteInfoadjuntacedula(int key)
        {
            var items = this.context.TramiteInfoadjuntacedulas.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnTramiteInfoadjuntacedulaGet(ref result);

            return result;
        }
        partial void OnTramiteInfoadjuntacedulaDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        [HttpDelete("/odata/CMU/TramiteInfoadjuntacedulas(OID={OID})")]
        public IActionResult DeleteTramiteInfoadjuntacedula(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TramiteInfoadjuntacedulas
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntacedulaDeleted(item);
                this.context.TramiteInfoadjuntacedulas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTramiteInfoadjuntacedulaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntacedulaUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        [HttpPut("/odata/CMU/TramiteInfoadjuntacedulas(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTramiteInfoadjuntacedula(int key, [FromBody]SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntacedulas
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTramiteInfoadjuntacedulaUpdated(item);
                this.context.TramiteInfoadjuntacedulas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntacedulas.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");
                this.OnAfterTramiteInfoadjuntacedulaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TramiteInfoadjuntacedulas(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTramiteInfoadjuntacedula(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TramiteInfoadjuntacedulas
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTramiteInfoadjuntacedulaUpdated(item);
                this.context.TramiteInfoadjuntacedulas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntacedulas.Where(i => i.OID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTramiteInfoadjuntacedulaCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item)
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

                this.OnTramiteInfoadjuntacedulaCreated(item);
                this.context.TramiteInfoadjuntacedulas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TramiteInfoadjuntacedulas.Where(i => i.OID == item.OID);

                Request.QueryString = Request.QueryString.Add("$expand", "TramiteInfoAdjuntaBase");

                this.OnAfterTramiteInfoadjuntacedulaCreated(item);

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
