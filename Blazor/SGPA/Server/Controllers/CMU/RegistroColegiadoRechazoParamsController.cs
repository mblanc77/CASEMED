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
    [Route("odata/CMU/RegistroColegiadoRechazoParams")]
    public partial class RegistroColegiadoRechazoParamsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public RegistroColegiadoRechazoParamsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> GetRegistroColegiadoRechazoParams()
        {
            var items = this.context.RegistroColegiadoRechazoParams.AsQueryable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>();
            this.OnRegistroColegiadoRechazoParamsRead(ref items);

            return items;
        }

        partial void OnRegistroColegiadoRechazoParamsRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> items);

        partial void OnRegistroColegiadoRechazoParamGet(ref SingleResult<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/RegistroColegiadoRechazoParams(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> GetRegistroColegiadoRechazoParam(int key)
        {
            var items = this.context.RegistroColegiadoRechazoParams.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnRegistroColegiadoRechazoParamGet(ref result);

            return result;
        }
        partial void OnRegistroColegiadoRechazoParamDeleted(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamDeleted(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        [HttpDelete("/odata/CMU/RegistroColegiadoRechazoParams(OID={OID})")]
        public IActionResult DeleteRegistroColegiadoRechazoParam(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegistroColegiadoRechazoParams
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoRechazoParamDeleted(item);
                this.context.RegistroColegiadoRechazoParams.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegistroColegiadoRechazoParamDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoRechazoParamUpdated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamUpdated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        [HttpPut("/odata/CMU/RegistroColegiadoRechazoParams(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegistroColegiadoRechazoParam(int key, [FromBody]SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiadoRechazoParams
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegistroColegiadoRechazoParamUpdated(item);
                this.context.RegistroColegiadoRechazoParams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoRechazoParams.Where(i => i.OID == key);
                ;
                this.OnAfterRegistroColegiadoRechazoParamUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/RegistroColegiadoRechazoParams(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegistroColegiadoRechazoParam(int key, [FromBody]Delta<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegistroColegiadoRechazoParams
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegistroColegiadoRechazoParamUpdated(item);
                this.context.RegistroColegiadoRechazoParams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoRechazoParams.Where(i => i.OID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegistroColegiadoRechazoParamCreated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamCreated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item)
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

                this.OnRegistroColegiadoRechazoParamCreated(item);
                this.context.RegistroColegiadoRechazoParams.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegistroColegiadoRechazoParams.Where(i => i.OID == item.OID);

                ;

                this.OnAfterRegistroColegiadoRechazoParamCreated(item);

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
