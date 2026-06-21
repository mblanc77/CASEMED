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
    [Route("odata/Sgpa/Certificadors")]
    public partial class CertificadorsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public CertificadorsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Certificador> GetCertificadors()
        {
            var items = this.context.Certificadors.AsQueryable<SgpaNew.Server.Models.Sgpa.Certificador>();
            this.OnCertificadorsRead(ref items);

            return items;
        }

        partial void OnCertificadorsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificador> items);

        partial void OnCertificadorGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Certificador> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Certificadors(CodCertificador={CodCertificador})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Certificador> GetCertificador(short key)
        {
            var items = this.context.Certificadors.Where(i => i.CodCertificador == key);
            var result = SingleResult.Create(items);

            OnCertificadorGet(ref result);

            return result;
        }
        partial void OnCertificadorDeleted(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorDeleted(SgpaNew.Server.Models.Sgpa.Certificador item);

        [HttpDelete("/odata/Sgpa/Certificadors(CodCertificador={CodCertificador})")]
        public IActionResult DeleteCertificador(short key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Certificadors
                    .Where(i => i.CodCertificador == key)
                    .Include(i => i.Certificacions)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificador>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificadorDeleted(item);
                this.context.Certificadors.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCertificadorDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificadorUpdated(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorUpdated(SgpaNew.Server.Models.Sgpa.Certificador item);

        [HttpPut("/odata/Sgpa/Certificadors(CodCertificador={CodCertificador})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCertificador(short key, [FromBody]SgpaNew.Server.Models.Sgpa.Certificador item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Certificadors
                    .Where(i => i.CodCertificador == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificador>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCertificadorUpdated(item);
                this.context.Certificadors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificadors.Where(i => i.CodCertificador == key);
                ;
                this.OnAfterCertificadorUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Certificadors(CodCertificador={CodCertificador})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCertificador(short key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Certificador> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Certificadors
                    .Where(i => i.CodCertificador == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Certificador>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCertificadorUpdated(item);
                this.context.Certificadors.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificadors.Where(i => i.CodCertificador == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCertificadorCreated(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorCreated(SgpaNew.Server.Models.Sgpa.Certificador item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Certificador item)
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

                this.OnCertificadorCreated(item);
                this.context.Certificadors.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Certificadors.Where(i => i.CodCertificador == item.CodCertificador);

                ;

                this.OnAfterCertificadorCreated(item);

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
