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
    [Route("odata/CMU/Bancos")]
    public partial class BancosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public BancosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Banco> GetBancos()
        {
            var items = this.context.Bancos.AsQueryable<SGPA.Server.Models.CMU.Banco>();
            this.OnBancosRead(ref items);

            return items;
        }

        partial void OnBancosRead(ref IQueryable<SGPA.Server.Models.CMU.Banco> items);

        partial void OnBancoGet(ref SingleResult<SGPA.Server.Models.CMU.Banco> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Bancos(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.Banco> GetBanco(int key)
        {
            var items = this.context.Bancos.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnBancoGet(ref result);

            return result;
        }
        partial void OnBancoDeleted(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoDeleted(SGPA.Server.Models.CMU.Banco item);

        [HttpDelete("/odata/CMU/Bancos(Id={Id})")]
        public IActionResult DeleteBanco(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Bancos
                    .Where(i => i.Id == key)
                    .Include(i => i.CuentaBancaria)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Banco>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBancoDeleted(item);
                this.context.Bancos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBancoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBancoUpdated(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoUpdated(SGPA.Server.Models.CMU.Banco item);

        [HttpPut("/odata/CMU/Bancos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBanco(int key, [FromBody]SGPA.Server.Models.CMU.Banco item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Bancos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Banco>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBancoUpdated(item);
                this.context.Bancos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Bancos.Where(i => i.Id == key);
                ;
                this.OnAfterBancoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Bancos(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBanco(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Banco> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Bancos
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Banco>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBancoUpdated(item);
                this.context.Bancos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Bancos.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBancoCreated(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoCreated(SGPA.Server.Models.CMU.Banco item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Banco item)
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

                this.OnBancoCreated(item);
                this.context.Bancos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Bancos.Where(i => i.Id == item.Id);

                ;

                this.OnAfterBancoCreated(item);

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
