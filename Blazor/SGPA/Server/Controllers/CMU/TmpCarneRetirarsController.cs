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
    [Route("odata/CMU/TmpCarneRetirars")]
    public partial class TmpCarneRetirarsController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TmpCarneRetirarsController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TmpCarneRetirar> GetTmpCarneRetirars()
        {
            var items = this.context.TmpCarneRetirars.AsQueryable<SGPA.Server.Models.CMU.TmpCarneRetirar>();
            this.OnTmpCarneRetirarsRead(ref items);

            return items;
        }

        partial void OnTmpCarneRetirarsRead(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneRetirar> items);

        partial void OnTmpCarneRetirarGet(ref SingleResult<SGPA.Server.Models.CMU.TmpCarneRetirar> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TmpCarneRetirars(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.TmpCarneRetirar> GetTmpCarneRetirar(int key)
        {
            var items = this.context.TmpCarneRetirars.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnTmpCarneRetirarGet(ref result);

            return result;
        }
        partial void OnTmpCarneRetirarDeleted(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarDeleted(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        [HttpDelete("/odata/CMU/TmpCarneRetirars(Documento={Documento})")]
        public IActionResult DeleteTmpCarneRetirar(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TmpCarneRetirars
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneRetirar>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpCarneRetirarDeleted(item);
                this.context.TmpCarneRetirars.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTmpCarneRetirarDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpCarneRetirarUpdated(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarUpdated(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        [HttpPut("/odata/CMU/TmpCarneRetirars(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTmpCarneRetirar(int key, [FromBody]SGPA.Server.Models.CMU.TmpCarneRetirar item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpCarneRetirars
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneRetirar>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpCarneRetirarUpdated(item);
                this.context.TmpCarneRetirars.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneRetirars.Where(i => i.Documento == key);
                ;
                this.OnAfterTmpCarneRetirarUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TmpCarneRetirars(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTmpCarneRetirar(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TmpCarneRetirar> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpCarneRetirars
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneRetirar>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTmpCarneRetirarUpdated(item);
                this.context.TmpCarneRetirars.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneRetirars.Where(i => i.Documento == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpCarneRetirarCreated(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarCreated(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TmpCarneRetirar item)
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

                this.OnTmpCarneRetirarCreated(item);
                this.context.TmpCarneRetirars.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneRetirars.Where(i => i.Documento == item.Documento);

                ;

                this.OnAfterTmpCarneRetirarCreated(item);

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
