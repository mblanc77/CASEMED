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

namespace SgpaNew.Server.Controllers.Sgpa
{
    [Route("odata/Sgpa/Ctasbrous")]
    public partial class CtasbrousController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public CtasbrousController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Ctasbrou> GetCtasbrous()
        {
            var items = this.context.Ctasbrous.AsQueryable<SgpaNew.Server.Models.Sgpa.Ctasbrou>();
            this.OnCtasbrousRead(ref items);

            return items;
        }

        partial void OnCtasbrousRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Ctasbrou> items);

        partial void OnCtasbrouGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Ctasbrou> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Ctasbrous(CI={CI})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Ctasbrou> GetCtasbrou(int key)
        {
            var items = this.context.Ctasbrous.Where(i => i.CI == key);
            var result = SingleResult.Create(items);

            OnCtasbrouGet(ref result);

            return result;
        }
        partial void OnCtasbrouDeleted(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouDeleted(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        [HttpDelete("/odata/Sgpa/Ctasbrous(CI={CI})")]
        public IActionResult DeleteCtasbrou(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Ctasbrous
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Ctasbrou>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCtasbrouDeleted(item);
                this.context.Ctasbrous.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCtasbrouDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCtasbrouUpdated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouUpdated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        [HttpPut("/odata/Sgpa/Ctasbrous(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCtasbrou(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Ctasbrou item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Ctasbrous
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Ctasbrou>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCtasbrouUpdated(item);
                this.context.Ctasbrous.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Ctasbrous.Where(i => i.CI == key);
                ;
                this.OnAfterCtasbrouUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Ctasbrous(CI={CI})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCtasbrou(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Ctasbrou> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Ctasbrous
                    .Where(i => i.CI == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Ctasbrou>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCtasbrouUpdated(item);
                this.context.Ctasbrous.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Ctasbrous.Where(i => i.CI == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCtasbrouCreated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouCreated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Ctasbrou item)
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

                this.OnCtasbrouCreated(item);
                this.context.Ctasbrous.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Ctasbrous.Where(i => i.CI == item.CI);

                ;

                this.OnAfterCtasbrouCreated(item);

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
