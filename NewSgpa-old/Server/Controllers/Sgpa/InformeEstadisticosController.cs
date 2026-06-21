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
    [Route("odata/Sgpa/InformeEstadisticos")]
    public partial class InformeEstadisticosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public InformeEstadisticosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.InformeEstadistico> GetInformeEstadisticos()
        {
            var items = this.context.InformeEstadisticos.AsQueryable<SgpaNew.Server.Models.Sgpa.InformeEstadistico>();
            this.OnInformeEstadisticosRead(ref items);

            return items;
        }

        partial void OnInformeEstadisticosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.InformeEstadistico> items);

        partial void OnInformeEstadisticoGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.InformeEstadistico> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/InformeEstadisticos(IdRpt={IdRpt})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.InformeEstadistico> GetInformeEstadistico(int key)
        {
            var items = this.context.InformeEstadisticos.Where(i => i.IdRpt == key);
            var result = SingleResult.Create(items);

            OnInformeEstadisticoGet(ref result);

            return result;
        }
        partial void OnInformeEstadisticoDeleted(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoDeleted(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        [HttpDelete("/odata/Sgpa/InformeEstadisticos(IdRpt={IdRpt})")]
        public IActionResult DeleteInformeEstadistico(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.InformeEstadisticos
                    .Where(i => i.IdRpt == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.InformeEstadistico>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnInformeEstadisticoDeleted(item);
                this.context.InformeEstadisticos.Remove(item);
                this.context.SaveChanges();
                this.OnAfterInformeEstadisticoDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnInformeEstadisticoUpdated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoUpdated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        [HttpPut("/odata/Sgpa/InformeEstadisticos(IdRpt={IdRpt})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutInformeEstadistico(int key, [FromBody]SgpaNew.Server.Models.Sgpa.InformeEstadistico item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.InformeEstadisticos
                    .Where(i => i.IdRpt == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.InformeEstadistico>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnInformeEstadisticoUpdated(item);
                this.context.InformeEstadisticos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.InformeEstadisticos.Where(i => i.IdRpt == key);
                ;
                this.OnAfterInformeEstadisticoUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/InformeEstadisticos(IdRpt={IdRpt})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchInformeEstadistico(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.InformeEstadistico> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.InformeEstadisticos
                    .Where(i => i.IdRpt == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.InformeEstadistico>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnInformeEstadisticoUpdated(item);
                this.context.InformeEstadisticos.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.InformeEstadisticos.Where(i => i.IdRpt == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnInformeEstadisticoCreated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoCreated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.InformeEstadistico item)
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

                this.OnInformeEstadisticoCreated(item);
                this.context.InformeEstadisticos.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.InformeEstadisticos.Where(i => i.IdRpt == item.IdRpt);

                ;

                this.OnAfterInformeEstadisticoCreated(item);

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
