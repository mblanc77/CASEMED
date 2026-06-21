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
    [Route("odata/CMU/TmpCarneEntregados")]
    public partial class TmpCarneEntregadosController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public TmpCarneEntregadosController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.TmpCarneEntregado> GetTmpCarneEntregados()
        {
            var items = this.context.TmpCarneEntregados.AsQueryable<SGPA.Server.Models.CMU.TmpCarneEntregado>();
            this.OnTmpCarneEntregadosRead(ref items);

            return items;
        }

        partial void OnTmpCarneEntregadosRead(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneEntregado> items);

        partial void OnTmpCarneEntregadoGet(ref SingleResult<SGPA.Server.Models.CMU.TmpCarneEntregado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/TmpCarneEntregados(Documento={Documento})")]
        public SingleResult<SGPA.Server.Models.CMU.TmpCarneEntregado> GetTmpCarneEntregado(int key)
        {
            var items = this.context.TmpCarneEntregados.Where(i => i.Documento == key);
            var result = SingleResult.Create(items);

            OnTmpCarneEntregadoGet(ref result);

            return result;
        }
        partial void OnTmpCarneEntregadoDeleted(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoDeleted(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        [HttpDelete("/odata/CMU/TmpCarneEntregados(Documento={Documento})")]
        public IActionResult DeleteTmpCarneEntregado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.TmpCarneEntregados
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneEntregado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpCarneEntregadoDeleted(item);
                this.context.TmpCarneEntregados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTmpCarneEntregadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpCarneEntregadoUpdated(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoUpdated(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        [HttpPut("/odata/CMU/TmpCarneEntregados(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTmpCarneEntregado(int key, [FromBody]SGPA.Server.Models.CMU.TmpCarneEntregado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpCarneEntregados
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneEntregado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTmpCarneEntregadoUpdated(item);
                this.context.TmpCarneEntregados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneEntregados.Where(i => i.Documento == key);
                ;
                this.OnAfterTmpCarneEntregadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/TmpCarneEntregados(Documento={Documento})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTmpCarneEntregado(int key, [FromBody]Delta<SGPA.Server.Models.CMU.TmpCarneEntregado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.TmpCarneEntregados
                    .Where(i => i.Documento == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.TmpCarneEntregado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTmpCarneEntregadoUpdated(item);
                this.context.TmpCarneEntregados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneEntregados.Where(i => i.Documento == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTmpCarneEntregadoCreated(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoCreated(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.TmpCarneEntregado item)
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

                this.OnTmpCarneEntregadoCreated(item);
                this.context.TmpCarneEntregados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TmpCarneEntregados.Where(i => i.Documento == item.Documento);

                ;

                this.OnAfterTmpCarneEntregadoCreated(item);

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
