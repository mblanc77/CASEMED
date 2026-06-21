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
    [Route("odata/Sgpa/Certificacions")]
    public partial class CertificacionsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public CertificacionsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Certificacion> GetCertificacions()
        {
            var items = this.context.Certificacions.AsQueryable<SgpaNew.Server.Models.Sgpa.Certificacion>();
            this.OnCertificacionsRead(ref items);

            return items;
        }

        partial void OnCertificacionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificacion> items);

        partial void OnCertificacionGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Certificacion> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Certificacions(NroLlamado={NroLlamado})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Certificacion> GetCertificacion(int key)
        {
            var items = this.context.Certificacions.Where(i => i.NroLlamado == key);
            var result = SingleResult.Create(items);

            OnCertificacionGet(ref result);

            return result;
        }
        partial void OnCertificacionDeleted(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionDeleted(SgpaNew.Server.Models.Sgpa.Certificacion item);

        [HttpDelete("/odata/Sgpa/Certificacions(NroLlamado={NroLlamado})")]
        public IActionResult DeleteCertificacion(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Certificacions
                    .Where(i => i.NroLlamado == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificacionDeleted(item);
                this.context.Certificacions.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCertificacionDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificacionUpdated(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionUpdated(SgpaNew.Server.Models.Sgpa.Certificacion item);

        [HttpPut("/odata/Sgpa/Certificacions(NroLlamado={NroLlamado})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCertificacion(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Certificacion item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Certificacions
                    .Where(i => i.NroLlamado == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificacion>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificacionUpdated(item);
                this.context.Certificacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificacions.Where(i => i.NroLlamado == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,AfeccionTipo,Certificador,SalidaTipo");
                this.OnAfterCertificacionUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Certificacions(NroLlamado={NroLlamado})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCertificacion(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Certificacion> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Certificacions
                    .Where(i => i.NroLlamado == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificacion>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCertificacionUpdated(item);
                this.context.Certificacions.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificacions.Where(i => i.NroLlamado == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,AfeccionTipo,Certificador,SalidaTipo");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificacionCreated(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionCreated(SgpaNew.Server.Models.Sgpa.Certificacion item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Certificacion item)
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

                this.OnCertificacionCreated(item);
                this.context.Certificacions.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificacions.Where(i => i.NroLlamado == item.NroLlamado);

                Request.QueryString = Request.QueryString.Add("$expand", "Afiliado,AfeccionTipo,Certificador,SalidaTipo");

                this.OnAfterCertificacionCreated(item);

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
