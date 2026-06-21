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
    [Route("odata/CMU/XpObjectTypes")]
    public partial class XpObjectTypesController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public XpObjectTypesController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.XpObjectType> GetXpObjectTypes()
        {
            var items = this.context.XpObjectTypes.AsQueryable<SGPA.Server.Models.CMU.XpObjectType>();
            this.OnXpObjectTypesRead(ref items);

            return items;
        }

        partial void OnXpObjectTypesRead(ref IQueryable<SGPA.Server.Models.CMU.XpObjectType> items);

        partial void OnXpObjectTypeGet(ref SingleResult<SGPA.Server.Models.CMU.XpObjectType> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/XpObjectTypes(OID={OID})")]
        public SingleResult<SGPA.Server.Models.CMU.XpObjectType> GetXpObjectType(int key)
        {
            var items = this.context.XpObjectTypes.Where(i => i.OID == key);
            var result = SingleResult.Create(items);

            OnXpObjectTypeGet(ref result);

            return result;
        }
        partial void OnXpObjectTypeDeleted(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeDeleted(SGPA.Server.Models.CMU.XpObjectType item);

        [HttpDelete("/odata/CMU/XpObjectTypes(OID={OID})")]
        public IActionResult DeleteXpObjectType(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XpObjectTypes
                    .Where(i => i.OID == key)
                    .Include(i => i.Cobros)
                    .Include(i => i.CobroNominas)
                    .Include(i => i.ColegiadoBitacoras)
                    .Include(i => i.ColegiadoDeclaracionJurada)
                    .Include(i => i.Convenios)
                    .Include(i => i.MovimientoCuenta)
                    .Include(i => i.OrigenMovimientos)
                    .Include(i => i.SecuritySystemRoles)
                    .Include(i => i.SecuritySystemTypePermissionsObjects)
                    .Include(i => i.SecuritySystemUsers)
                    .Include(i => i.TramiteInfoadjuntabases)
                    .Include(i => i.XpObjectModifieds)
                    .Include(i => i.XpWeakReferences)
                    .Include(i => i.XpWeakReferences1)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpObjectTypeDeleted(item);
                this.context.XpObjectTypes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXpObjectTypeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpObjectTypeUpdated(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeUpdated(SGPA.Server.Models.CMU.XpObjectType item);

        [HttpPut("/odata/CMU/XpObjectTypes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXpObjectType(int key, [FromBody]SGPA.Server.Models.CMU.XpObjectType item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpObjectTypes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectType>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXpObjectTypeUpdated(item);
                this.context.XpObjectTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectTypes.Where(i => i.OID == key);
                ;
                this.OnAfterXpObjectTypeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/XpObjectTypes(OID={OID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXpObjectType(int key, [FromBody]Delta<SGPA.Server.Models.CMU.XpObjectType> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XpObjectTypes
                    .Where(i => i.OID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.XpObjectType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXpObjectTypeUpdated(item);
                this.context.XpObjectTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectTypes.Where(i => i.OID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXpObjectTypeCreated(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeCreated(SGPA.Server.Models.CMU.XpObjectType item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.XpObjectType item)
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

                this.OnXpObjectTypeCreated(item);
                this.context.XpObjectTypes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XpObjectTypes.Where(i => i.OID == item.OID);

                ;

                this.OnAfterXpObjectTypeCreated(item);

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
