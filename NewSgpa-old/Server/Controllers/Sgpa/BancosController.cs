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
    [Route("odata/Sgpa/Bancos")]
    public partial class BancosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public BancosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Banco> GetBancos()
        {
            var items = this.context.Bancos.AsQueryable<SgpaNew.Server.Models.Sgpa.Banco>();
            this.OnBancosRead(ref items);

            return items;
        }

        partial void OnBancosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Banco> items);

        partial void OnBancoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Banco> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Bancos(CodBanco={CodBanco})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Banco> GetBanco(int key)
        {
            var items = this.context.Bancos.Where(i => i.CodBanco == key);
            var result = SingleResult.Create(items);

            OnBancoGet(ref result);

            return result;
        }
        partial void OnBancoDeleted(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoDeleted(SgpaNew.Server.Models.Sgpa.Banco item);

        [HttpDelete("/odata/Sgpa/Bancos(CodBanco={CodBanco})")]
        public IActionResult DeleteBanco(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Bancos
                    .Where(i => i.CodBanco == key)
                    .Include(i => i.Afiliados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Banco>(Request, items);

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

        partial void OnBancoUpdated(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoUpdated(SgpaNew.Server.Models.Sgpa.Banco item);

        [HttpPut("/odata/Sgpa/Bancos(CodBanco={CodBanco})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBanco(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Banco item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Bancos
                    .Where(i => i.CodBanco == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Banco>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBancoUpdated(item);
                this.context.Bancos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Bancos.Where(i => i.CodBanco == key);
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

        [HttpPatch("/odata/Sgpa/Bancos(CodBanco={CodBanco})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBanco(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Banco> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Bancos
                    .Where(i => i.CodBanco == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Banco>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBancoUpdated(item);
                this.context.Bancos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Bancos.Where(i => i.CodBanco == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBancoCreated(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoCreated(SgpaNew.Server.Models.Sgpa.Banco item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Banco item)
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

                var itemToReturn = this.context.Bancos.Where(i => i.CodBanco == item.CodBanco);

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
