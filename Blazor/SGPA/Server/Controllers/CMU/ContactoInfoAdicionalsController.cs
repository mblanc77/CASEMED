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
    [Route("odata/CMU/ContactoInfoAdicionals")]
    public partial class ContactoInfoAdicionalsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public ContactoInfoAdicionalsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.ContactoInfoAdicional> GetContactoInfoAdicionals()
        {
            var items = this.context.ContactoInfoAdicionals.AsQueryable<SGPA.Server.Models.CMU.ContactoInfoAdicional>();
            this.OnContactoInfoAdicionalsRead(ref items);

            return items;
        }

        partial void OnContactoInfoAdicionalsRead(ref IQueryable<SGPA.Server.Models.CMU.ContactoInfoAdicional> items);

        partial void OnContactoInfoAdicionalGet(ref SingleResult<SGPA.Server.Models.CMU.ContactoInfoAdicional> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/ContactoInfoAdicionals(Id={Id})")]
        public SingleResult<SGPA.Server.Models.CMU.ContactoInfoAdicional> GetContactoInfoAdicional(int key)
        {
            var items = this.context.ContactoInfoAdicionals.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnContactoInfoAdicionalGet(ref result);

            return result;
        }
        partial void OnContactoInfoAdicionalDeleted(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalDeleted(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        [HttpDelete("/odata/CMU/ContactoInfoAdicionals(Id={Id})")]
        public IActionResult DeleteContactoInfoAdicional(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.ContactoInfoAdicionals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ContactoInfoAdicional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactoInfoAdicionalDeleted(item);
                this.context.ContactoInfoAdicionals.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactoInfoAdicionalDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactoInfoAdicionalUpdated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalUpdated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        [HttpPut("/odata/CMU/ContactoInfoAdicionals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContactoInfoAdicional(int key, [FromBody]SGPA.Server.Models.CMU.ContactoInfoAdicional item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ContactoInfoAdicionals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ContactoInfoAdicional>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactoInfoAdicionalUpdated(item);
                this.context.ContactoInfoAdicionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ContactoInfoAdicionals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Contacto1");
                this.OnAfterContactoInfoAdicionalUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/ContactoInfoAdicionals(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContactoInfoAdicional(int key, [FromBody]Delta<SGPA.Server.Models.CMU.ContactoInfoAdicional> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.ContactoInfoAdicionals
                    .Where(i => i.Id == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.ContactoInfoAdicional>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactoInfoAdicionalUpdated(item);
                this.context.ContactoInfoAdicionals.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ContactoInfoAdicionals.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Contacto1");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactoInfoAdicionalCreated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalCreated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.ContactoInfoAdicional item)
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

                this.OnContactoInfoAdicionalCreated(item);
                this.context.ContactoInfoAdicionals.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ContactoInfoAdicionals.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Contacto1");

                this.OnAfterContactoInfoAdicionalCreated(item);

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
