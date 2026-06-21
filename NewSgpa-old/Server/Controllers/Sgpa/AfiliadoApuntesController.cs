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
    [Route("odata/Sgpa/AfiliadoApuntes")]
    public partial class AfiliadoApuntesController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public AfiliadoApuntesController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> GetAfiliadoApuntes()
        {
            var items = this.context.AfiliadoApuntes.AsQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>();
            this.OnAfiliadoApuntesRead(ref items);

            return items;
        }

        partial void OnAfiliadoApuntesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> items);

        partial void OnAfiliadoApunteGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/AfiliadoApuntes(AfiliadoApunteId={AfiliadoApunteId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> GetAfiliadoApunte(int key)
        {
            var items = this.context.AfiliadoApuntes.Where(i => i.AfiliadoApunteId == key);
            var result = SingleResult.Create(items);

            OnAfiliadoApunteGet(ref result);

            return result;
        }
        partial void OnAfiliadoApunteDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        [HttpDelete("/odata/Sgpa/AfiliadoApuntes(AfiliadoApunteId={AfiliadoApunteId})")]
        public IActionResult DeleteAfiliadoApunte(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.AfiliadoApuntes
                    .Where(i => i.AfiliadoApunteId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoApunteDeleted(item);
                this.context.AfiliadoApuntes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAfiliadoApunteDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoApunteUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        [HttpPut("/odata/Sgpa/AfiliadoApuntes(AfiliadoApunteId={AfiliadoApunteId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAfiliadoApunte(int key, [FromBody]SgpaNew.Server.Models.Sgpa.AfiliadoApunte item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfiliadoApuntes
                    .Where(i => i.AfiliadoApunteId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAfiliadoApunteUpdated(item);
                this.context.AfiliadoApuntes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoApuntes.Where(i => i.AfiliadoApunteId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                this.OnAfterAfiliadoApunteUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/AfiliadoApuntes(AfiliadoApunteId={AfiliadoApunteId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAfiliadoApunte(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.AfiliadoApuntes
                    .Where(i => i.AfiliadoApunteId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAfiliadoApunteUpdated(item);
                this.context.AfiliadoApuntes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoApuntes.Where(i => i.AfiliadoApunteId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAfiliadoApunteCreated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteCreated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.AfiliadoApunte item)
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

                this.OnAfiliadoApunteCreated(item);
                this.context.AfiliadoApuntes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AfiliadoApuntes.Where(i => i.AfiliadoApunteId == item.AfiliadoApunteId);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");

                this.OnAfterAfiliadoApunteCreated(item);

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
