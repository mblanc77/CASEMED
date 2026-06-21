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
    [Route("odata/CMU/Pais")]
    public partial class PaisController : ODataController
    {
        private SGPA.Server.Data.CMUContext context;

        public PaisController(SGPA.Server.Data.CMUContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SGPA.Server.Models.CMU.Pai> GetPais()
        {
            var items = this.context.Pais.AsQueryable<SGPA.Server.Models.CMU.Pai>();
            this.OnPaisRead(ref items);

            return items;
        }

        partial void OnPaisRead(ref IQueryable<SGPA.Server.Models.CMU.Pai> items);

        partial void OnPaiGet(ref SingleResult<SGPA.Server.Models.CMU.Pai> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/CMU/Pais(Codigo={Codigo})")]
        public SingleResult<SGPA.Server.Models.CMU.Pai> GetPai(int key)
        {
            var items = this.context.Pais.Where(i => i.Codigo == key);
            var result = SingleResult.Create(items);

            OnPaiGet(ref result);

            return result;
        }
        partial void OnPaiDeleted(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiDeleted(SGPA.Server.Models.CMU.Pai item);

        [HttpDelete("/odata/CMU/Pais(Codigo={Codigo})")]
        public IActionResult DeletePai(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Pais
                    .Where(i => i.Codigo == key)
                    .Include(i => i.Colegiados)
                    .Include(i => i.RegistroColegiados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Pai>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPaiDeleted(item);
                this.context.Pais.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPaiDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPaiUpdated(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiUpdated(SGPA.Server.Models.CMU.Pai item);

        [HttpPut("/odata/CMU/Pais(Codigo={Codigo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPai(int key, [FromBody]SGPA.Server.Models.CMU.Pai item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Pais
                    .Where(i => i.Codigo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Pai>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPaiUpdated(item);
                this.context.Pais.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Pais.Where(i => i.Codigo == key);
                ;
                this.OnAfterPaiUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/CMU/Pais(Codigo={Codigo})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPai(int key, [FromBody]Delta<SGPA.Server.Models.CMU.Pai> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Pais
                    .Where(i => i.Codigo == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SGPA.Server.Models.CMU.Pai>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPaiUpdated(item);
                this.context.Pais.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Pais.Where(i => i.Codigo == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPaiCreated(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiCreated(SGPA.Server.Models.CMU.Pai item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SGPA.Server.Models.CMU.Pai item)
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

                this.OnPaiCreated(item);
                this.context.Pais.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Pais.Where(i => i.Codigo == item.Codigo);

                ;

                this.OnAfterPaiCreated(item);

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
