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
    [Route("odata/CMU/FileData")]
    public partial class FileDataController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public FileDataController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.FileDatum> GetFileData()
        {
            var items = this.context.FileData.AsQueryable<SGPA.Server.Models.CMU.FileDatum>();
            this.OnFileDataRead(ref items);

            return items;
        }

        partial void OnFileDataRead(ref IQueryable<SGPA.Server.Models.CMU.FileDatum> items);

        partial void OnFileDatumGet(ref SingleResult<SGPA.Server.Models.CMU.FileDatum> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/FileData(Oid={Oid})")]
        public SingleResult<SGPA.Server.Models.CMU.FileDatum> GetFileDatum(Guid key)
        {
            var items = this.context.FileData.Where(i => i.Oid == key);
            var result = SingleResult.Create(items);

            OnFileDatumGet(ref result);

            return result;
        }
        partial void OnFileDatumDeleted(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumDeleted(SGPA.Server.Models.CMU.FileDatum item);

        [HttpDelete("/odata/CMU/FileData(Oid={Oid})")]
        public IActionResult DeleteFileDatum(Guid key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.FileData
                    .Where(i => i.Oid == key)
                    .Include(i => i.ActaConsejos)
                    .Include(i => i.DebitoAdjuntos)
                    .Include(i => i.DeclaracionJuradaAdjuntos)
                    .Include(i => i.SolicitudBajaFileAttachments)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FileDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFileDatumDeleted(item);
                this.context.FileData.Remove(item);
                this.context.SaveChanges();
                this.OnAfterFileDatumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFileDatumUpdated(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumUpdated(SGPA.Server.Models.CMU.FileDatum item);

        [HttpPut("/odata/CMU/FileData(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutFileDatum(Guid key, [FromBody]SGPA.Server.Models.CMU.FileDatum item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FileData
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FileDatum>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnFileDatumUpdated(item);
                this.context.FileData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FileData.Where(i => i.Oid == key);
                ;
                this.OnAfterFileDatumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/FileData(Oid={Oid})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchFileDatum(Guid key, [FromBody]Delta<SGPA.Server.Models.CMU.FileDatum> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.FileData
                    .Where(i => i.Oid == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.FileDatum>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnFileDatumUpdated(item);
                this.context.FileData.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FileData.Where(i => i.Oid == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFileDatumCreated(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumCreated(SGPA.Server.Models.CMU.FileDatum item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.FileDatum item)
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

                this.OnFileDatumCreated(item);
                this.context.FileData.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FileData.Where(i => i.Oid == item.Oid);

                ;

                this.OnAfterFileDatumCreated(item);

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
