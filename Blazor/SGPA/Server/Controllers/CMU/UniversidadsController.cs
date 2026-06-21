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
    [Route("odata/CMU/Universidads")]
    public partial class UniversidadsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public UniversidadsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Universidad> GetUniversidads()
        {
            var items = this.context.Universidads.AsQueryable<SGPA.Server.Models.CMU.Universidad>();
            this.OnUniversidadsRead(ref items);

            return items;
        }

        partial void OnUniversidadsRead(ref IQueryable<SGPA.Server.Models.CMU.Universidad> items);

        partial void OnUniversidadGet(ref SingleResult<SGPA.Server.Models.CMU.Universidad> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Universidads(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Universidad> GetUniversidad(int key)
        {
            var items = this.context.Universidads.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnUniversidadGet(ref result);

            return result;
        }
        partial void OnUniversidadDeleted(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadDeleted(SGPA.Server.Models.CMU.Universidad item);

        [HttpDelete("/odata/CMU/Universidads(Id={Id})")]
        public IActionResult DeleteUniversidad(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Universidads
                    .Where(i => i.Id == key)
                    .Include(i => i.RegistroColegiados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Universidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUniversidadDeleted(item);
                this.context.Universidads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUniversidadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUniversidadUpdated(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadUpdated(SGPA.Server.Models.CMU.Universidad item);

        [HttpPut("/odata/CMU/Universidads(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUniversidad(int key, [FromBody]SGPA.Server.Models.CMU.Universidad item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Universidads
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Universidad>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnUniversidadUpdated(item);
                this.context.Universidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Universidads.Where(i => i.Id == key);
                ;
                this.OnAfterUniversidadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Universidads(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUniversidad(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Universidad> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Universidads
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Universidad>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnUniversidadUpdated(item);
                this.context.Universidads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Universidads.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUniversidadCreated(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadCreated(SGPA.Server.Models.CMU.Universidad item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Universidad item)
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

                this.OnUniversidadCreated(item);
                this.context.Universidads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Universidads.Where(i => i.Id == item.Id);

                ;

                this.OnAfterUniversidadCreated(item);

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
