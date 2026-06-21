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
    [Route("odata/CMU/MyFileData")]
    public partial class MyFileDataController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public MyFileDataController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.MyFileDatum> GetMyFileData()
        {
            var items = this.context.MyFileData.AsQueryable<SGPA.Server.Models.CMU.MyFileDatum>();
            this.OnMyFileDataRead(ref items);

            return items;
        }

        partial void OnMyFileDataRead(ref IQueryable<SGPA.Server.Models.CMU.MyFileDatum> items);

        partial void OnMyFileDatumGet(ref SingleResult<SGPA.Server.Models.CMU.MyFileDatum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/MyFileData(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.MyFileDatum> GetMyFileDatum(Guid key)
        {
            var items = this.context.MyFileData.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnMyFileDatumGet(ref result);

            return result;
        }
        partial void OnMyFileDatumDeleted(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumDeleted(SGPA.Server.Models.CMU.MyFileDatum item);

        [HttpDelete("/odata/CMU/MyFileData(Oid={Oid})")]
        public IActionResult DeleteMyFileDatum(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.MyFileData
                    .Where(i => i.Oid == key)
                    .Include(i => i.SalaReservas)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MyFileDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMyFileDatumDeleted(item);
                this.context.MyFileData.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMyFileDatumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMyFileDatumUpdated(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumUpdated(SGPA.Server.Models.CMU.MyFileDatum item);

        [HttpPut("/odata/CMU/MyFileData(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMyFileDatum(Guid key, [FromBody]SGPA.Server.Models.CMU.MyFileDatum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MyFileData
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MyFileDatum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMyFileDatumUpdated(item);
                this.context.MyFileData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MyFileData.Where(i => i.Oid == key);
                ;
                this.OnAfterMyFileDatumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/MyFileData(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMyFileDatum(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.MyFileDatum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.MyFileData
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.MyFileDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMyFileDatumUpdated(item);
                this.context.MyFileData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MyFileData.Where(i => i.Oid == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMyFileDatumCreated(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumCreated(SGPA.Server.Models.CMU.MyFileDatum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.MyFileDatum item)
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

                this.OnMyFileDatumCreated(item);
                this.context.MyFileData.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MyFileData.Where(i => i.Oid == item.Oid);

                ;

                this.OnAfterMyFileDatumCreated(item);

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
