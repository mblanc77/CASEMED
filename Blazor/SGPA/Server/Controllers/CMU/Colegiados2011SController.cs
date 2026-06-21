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
    [Route("odata/CMU/Colegiados2011S")]
    public partial class Colegiados2011SController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public Colegiados2011SController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Colegiados2011> GetColegiados2011S()
        {
            var items = this.context.Colegiados2011S.AsQueryable<SGPA.Server.Models.CMU.Colegiados2011>();
            this.OnColegiados2011SRead(ref items);

            return items;
        }

        partial void OnColegiados2011SRead(ref IQueryable<SGPA.Server.Models.CMU.Colegiados2011> items);

        partial void OnColegiados2011Get(ref SingleResult<SGPA.Server.Models.CMU.Colegiados2011> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Colegiados2011S(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.Colegiados2011> GetColegiados2011(int key)
        {
            var items = this.context.Colegiados2011S.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnColegiados2011Get(ref result);

            return result;
        }
        partial void OnColegiados2011Deleted(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Deleted(SGPA.Server.Models.CMU.Colegiados2011 item);

        [HttpDelete("/odata/CMU/Colegiados2011S(Documento={Documento})")]
        public IActionResult DeleteColegiados2011(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Colegiados2011S
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiados2011>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiados2011Deleted(item);
                this.context.Colegiados2011S.Remove(item);
                this.context.SaveChanges();
                this.OnAfterColegiados2011Deleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiados2011Updated(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Updated(SGPA.Server.Models.CMU.Colegiados2011 item);

        [HttpPut("/odata/CMU/Colegiados2011S(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutColegiados2011(int key, [FromBody]SGPA.Server.Models.CMU.Colegiados2011 item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Colegiados2011S
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiados2011>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnColegiados2011Updated(item);
                this.context.Colegiados2011S.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados2011S.Where(i => i.Documento == key);
                ;
                this.OnAfterColegiados2011Updated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Colegiados2011S(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchColegiados2011(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Colegiados2011> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Colegiados2011S
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Colegiados2011>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnColegiados2011Updated(item);
                this.context.Colegiados2011S.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados2011S.Where(i => i.Documento == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnColegiados2011Created(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Created(SGPA.Server.Models.CMU.Colegiados2011 item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Colegiados2011 item)
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

                this.OnColegiados2011Created(item);
                this.context.Colegiados2011S.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Colegiados2011S.Where(i => i.Documento == item.Documento);

                ;

                this.OnAfterColegiados2011Created(item);

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
