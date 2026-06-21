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
    [Route("odata/Sgpa/Moneda")]
    public partial class MonedaController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public MonedaController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Monedum> GetMoneda()
        {
            var items = this.context.Moneda.AsQueryable<SgpaNew.Server.Models.Sgpa.Monedum>();
            this.OnMonedaRead(ref items);

            return items;
        }

        partial void OnMonedaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Monedum> items);

        partial void OnMonedumGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Monedum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Moneda(Moneda1={Moneda1})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Monedum> GetMonedum(string key)
        {
            var items = this.context.Moneda.Where(i => i.Moneda1 == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnMonedumGet(ref result);

            return result;
        }
        partial void OnMonedumDeleted(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumDeleted(SgpaNew.Server.Models.Sgpa.Monedum item);

        [HttpDelete("/odata/Sgpa/Moneda(Moneda1={Moneda1})")]
        public IActionResult DeleteMonedum(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Moneda
                    .Where(i => i.Moneda1 == Uri.UnescapeDataString(key))
                    .Include(i => i.Prestacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Monedum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMonedumDeleted(item);
                this.context.Moneda.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMonedumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMonedumUpdated(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumUpdated(SgpaNew.Server.Models.Sgpa.Monedum item);

        [HttpPut("/odata/Sgpa/Moneda(Moneda1={Moneda1})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMonedum(string key, [FromBody]SgpaNew.Server.Models.Sgpa.Monedum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Moneda
                    .Where(i => i.Moneda1 == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Monedum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMonedumUpdated(item);
                this.context.Moneda.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Moneda.Where(i => i.Moneda1 == Uri.UnescapeDataString(key));
                ;
                this.OnAfterMonedumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Moneda(Moneda1={Moneda1})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMonedum(string key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Monedum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Moneda
                    .Where(i => i.Moneda1 == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Monedum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMonedumUpdated(item);
                this.context.Moneda.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Moneda.Where(i => i.Moneda1 == Uri.UnescapeDataString(key));
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMonedumCreated(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumCreated(SgpaNew.Server.Models.Sgpa.Monedum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Monedum item)
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

                this.OnMonedumCreated(item);
                this.context.Moneda.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Moneda.Where(i => i.Moneda1 == item.Moneda1);

                ;

                this.OnAfterMonedumCreated(item);

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
