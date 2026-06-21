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
    [Route("odata/Sgpa/SubsidioitemcodAfiliados")]
    public partial class SubsidioitemcodAfiliadosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioitemcodAfiliadosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> GetSubsidioitemcodAfiliados()
        {
            var items = this.context.SubsidioitemcodAfiliados.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>();
            this.OnSubsidioitemcodAfiliadosRead(ref items);

            return items;
        }

        partial void OnSubsidioitemcodAfiliadosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> items);

        partial void OnSubsidioitemcodAfiliadoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/SubsidioitemcodAfiliados(SubItmCodAfiId={SubItmCodAfiId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> GetSubsidioitemcodAfiliado(int key)
        {
            var items = this.context.SubsidioitemcodAfiliados.Where(i => i.SubItmCodAfiId == key);
            var result = SingleResult.Create(items);

            OnSubsidioitemcodAfiliadoGet(ref result);

            return result;
        }
        partial void OnSubsidioitemcodAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        [HttpDelete("/odata/Sgpa/SubsidioitemcodAfiliados(SubItmCodAfiId={SubItmCodAfiId})")]
        public IActionResult DeleteSubsidioitemcodAfiliado(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.SubsidioitemcodAfiliados
                    .Where(i => i.SubItmCodAfiId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioitemcodAfiliadoDeleted(item);
                this.context.SubsidioitemcodAfiliados.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSubsidioitemcodAfiliadoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioitemcodAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        [HttpPut("/odata/Sgpa/SubsidioitemcodAfiliados(SubItmCodAfiId={SubItmCodAfiId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSubsidioitemcodAfiliado(int key, [FromBody]SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioitemcodAfiliados
                    .Where(i => i.SubItmCodAfiId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSubsidioitemcodAfiliadoUpdated(item);
                this.context.SubsidioitemcodAfiliados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioitemcodAfiliados.Where(i => i.SubItmCodAfiId == key);
                ;
                this.OnAfterSubsidioitemcodAfiliadoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/SubsidioitemcodAfiliados(SubItmCodAfiId={SubItmCodAfiId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSubsidioitemcodAfiliado(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.SubsidioitemcodAfiliados
                    .Where(i => i.SubItmCodAfiId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSubsidioitemcodAfiliadoUpdated(item);
                this.context.SubsidioitemcodAfiliados.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioitemcodAfiliados.Where(i => i.SubItmCodAfiId == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSubsidioitemcodAfiliadoCreated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoCreated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item)
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

                this.OnSubsidioitemcodAfiliadoCreated(item);
                this.context.SubsidioitemcodAfiliados.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.SubsidioitemcodAfiliados.Where(i => i.SubItmCodAfiId == item.SubItmCodAfiId);

                ;

                this.OnAfterSubsidioitemcodAfiliadoCreated(item);

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
