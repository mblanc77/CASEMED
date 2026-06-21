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
    [Route("odata/Sgpa/CertificacionProrrogas")]
    public partial class CertificacionProrrogasController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public CertificacionProrrogasController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> GetCertificacionProrrogas()
        {
            var items = this.context.CertificacionProrrogas.AsQueryable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>();
            this.OnCertificacionProrrogasRead(ref items);

            return items;
        }

        partial void OnCertificacionProrrogasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> items);

        partial void OnCertificacionProrrogaGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/CertificacionProrrogas(CertificacionProrrogaId={CertificacionProrrogaId})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> GetCertificacionProrroga(int key)
        {
            var items = this.context.CertificacionProrrogas.Where(i => i.CertificacionProrrogaId == key);
            var result = SingleResult.Create(items);

            OnCertificacionProrrogaGet(ref result);

            return result;
        }
        partial void OnCertificacionProrrogaDeleted(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaDeleted(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        [HttpDelete("/odata/Sgpa/CertificacionProrrogas(CertificacionProrrogaId={CertificacionProrrogaId})")]
        public IActionResult DeleteCertificacionProrroga(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.CertificacionProrrogas
                    .Where(i => i.CertificacionProrrogaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificacionProrrogaDeleted(item);
                this.context.CertificacionProrrogas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCertificacionProrrogaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificacionProrrogaUpdated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaUpdated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        [HttpPut("/odata/Sgpa/CertificacionProrrogas(CertificacionProrrogaId={CertificacionProrrogaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCertificacionProrroga(int key, [FromBody]SgpaNew.Server.Models.Sgpa.CertificacionProrroga item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CertificacionProrrogas
                    .Where(i => i.CertificacionProrrogaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificacionProrrogaUpdated(item);
                this.context.CertificacionProrrogas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CertificacionProrrogas.Where(i => i.CertificacionProrrogaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                this.OnAfterCertificacionProrrogaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/CertificacionProrrogas(CertificacionProrrogaId={CertificacionProrrogaId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCertificacionProrroga(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.CertificacionProrrogas
                    .Where(i => i.CertificacionProrrogaId == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCertificacionProrrogaUpdated(item);
                this.context.CertificacionProrrogas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CertificacionProrrogas.Where(i => i.CertificacionProrrogaId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificacionProrrogaCreated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaCreated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.CertificacionProrroga item)
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

                this.OnCertificacionProrrogaCreated(item);
                this.context.CertificacionProrrogas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.CertificacionProrrogas.Where(i => i.CertificacionProrrogaId == item.CertificacionProrrogaId);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado");

                this.OnAfterCertificacionProrrogaCreated(item);

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
