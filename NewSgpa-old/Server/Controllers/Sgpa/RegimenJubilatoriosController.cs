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
    [Route("odata/Sgpa/RegimenJubilatorios")]
    public partial class RegimenJubilatoriosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public RegimenJubilatoriosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> GetRegimenJubilatorios()
        {
            var items = this.context.RegimenJubilatorios.AsQueryable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>();
            this.OnRegimenJubilatoriosRead(ref items);

            return items;
        }

        partial void OnRegimenJubilatoriosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> items);

        partial void OnRegimenJubilatorioGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/RegimenJubilatorios(CodRegimenJubilatorio={CodRegimenJubilatorio})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> GetRegimenJubilatorio(byte key)
        {
            var items = this.context.RegimenJubilatorios.Where(i => i.CodRegimenJubilatorio == key);
            var result = SingleResult.Create(items);

            OnRegimenJubilatorioGet(ref result);

            return result;
        }
        partial void OnRegimenJubilatorioDeleted(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioDeleted(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        [HttpDelete("/odata/Sgpa/RegimenJubilatorios(CodRegimenJubilatorio={CodRegimenJubilatorio})")]
        public IActionResult DeleteRegimenJubilatorio(byte key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RegimenJubilatorios
                    .Where(i => i.CodRegimenJubilatorio == key)
                    .Include(i => i.Afiliados)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegimenJubilatorioDeleted(item);
                this.context.RegimenJubilatorios.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRegimenJubilatorioDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegimenJubilatorioUpdated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioUpdated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        [HttpPut("/odata/Sgpa/RegimenJubilatorios(CodRegimenJubilatorio={CodRegimenJubilatorio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRegimenJubilatorio(byte key, [FromBody]SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegimenJubilatorios
                    .Where(i => i.CodRegimenJubilatorio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRegimenJubilatorioUpdated(item);
                this.context.RegimenJubilatorios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenJubilatorios.Where(i => i.CodRegimenJubilatorio == key);
                ;
                this.OnAfterRegimenJubilatorioUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/RegimenJubilatorios(CodRegimenJubilatorio={CodRegimenJubilatorio})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRegimenJubilatorio(byte key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RegimenJubilatorios
                    .Where(i => i.CodRegimenJubilatorio == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRegimenJubilatorioUpdated(item);
                this.context.RegimenJubilatorios.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenJubilatorios.Where(i => i.CodRegimenJubilatorio == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRegimenJubilatorioCreated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioCreated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item)
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

                this.OnRegimenJubilatorioCreated(item);
                this.context.RegimenJubilatorios.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RegimenJubilatorios.Where(i => i.CodRegimenJubilatorio == item.CodRegimenJubilatorio);

                ;

                this.OnAfterRegimenJubilatorioCreated(item);

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
