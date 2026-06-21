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
    [Route("odata/Sgpa/XUsrParams")]
    public partial class XUsrParamsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public XUsrParamsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.XUsrParam> GetXUsrParams()
        {
            var items = this.context.XUsrParams.AsQueryable<SgpaNew.Server.Models.Sgpa.XUsrParam>();
            this.OnXUsrParamsRead(ref items);

            return items;
        }

        partial void OnXUsrParamsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.XUsrParam> items);

        partial void OnXUsrParamGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.XUsrParam> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/XUsrParams(IdUsuario={IdUsuario})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.XUsrParam> GetXUsrParam(int key)
        {
            var items = this.context.XUsrParams.Where(i => i.IdUsuario == key);
            var result = SingleResult.Create(items);

            OnXUsrParamGet(ref result);

            return result;
        }
        partial void OnXUsrParamDeleted(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamDeleted(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        [HttpDelete("/odata/Sgpa/XUsrParams(IdUsuario={IdUsuario})")]
        public IActionResult DeleteXUsrParam(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.XUsrParams
                    .Where(i => i.IdUsuario == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.XUsrParam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXUsrParamDeleted(item);
                this.context.XUsrParams.Remove(item);
                this.context.SaveChanges();
                this.OnAfterXUsrParamDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXUsrParamUpdated(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamUpdated(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        [HttpPut("/odata/Sgpa/XUsrParams(IdUsuario={IdUsuario})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutXUsrParam(int key, [FromBody]SgpaNew.Server.Models.Sgpa.XUsrParam item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XUsrParams
                    .Where(i => i.IdUsuario == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.XUsrParam>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnXUsrParamUpdated(item);
                this.context.XUsrParams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XUsrParams.Where(i => i.IdUsuario == key);
                ;
                this.OnAfterXUsrParamUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/XUsrParams(IdUsuario={IdUsuario})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchXUsrParam(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.XUsrParam> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.XUsrParams
                    .Where(i => i.IdUsuario == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.XUsrParam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnXUsrParamUpdated(item);
                this.context.XUsrParams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XUsrParams.Where(i => i.IdUsuario == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnXUsrParamCreated(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamCreated(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.XUsrParam item)
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

                this.OnXUsrParamCreated(item);
                this.context.XUsrParams.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.XUsrParams.Where(i => i.IdUsuario == item.IdUsuario);

                ;

                this.OnAfterXUsrParamCreated(item);

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
