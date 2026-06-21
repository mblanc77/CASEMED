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
    [Route("odata/Sgpa/Patologia")]
    public partial class PatologiaController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public PatologiaController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Patologium> GetPatologia()
        {
            var items = this.context.Patologia.AsQueryable<SgpaNew.Server.Models.Sgpa.Patologium>();
            this.OnPatologiaRead(ref items);

            return items;
        }

        partial void OnPatologiaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Patologium> items);

        partial void OnPatologiumGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.Patologium> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/Patologia(CodPatologia={CodPatologia})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.Patologium> GetPatologium(int key)
        {
            var items = this.context.Patologia.Where(i => i.CodPatologia == key);
            var result = SingleResult.Create(items);

            OnPatologiumGet(ref result);

            return result;
        }
        partial void OnPatologiumDeleted(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumDeleted(SgpaNew.Server.Models.Sgpa.Patologium item);

        [HttpDelete("/odata/Sgpa/Patologia(CodPatologia={CodPatologia})")]
        public IActionResult DeletePatologium(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Patologia
                    .Where(i => i.CodPatologia == key)
                    .Include(i => i.AfeccionGrupos)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Patologium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPatologiumDeleted(item);
                this.context.Patologia.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPatologiumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPatologiumUpdated(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumUpdated(SgpaNew.Server.Models.Sgpa.Patologium item);

        [HttpPut("/odata/Sgpa/Patologia(CodPatologia={CodPatologia})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPatologium(int key, [FromBody]SgpaNew.Server.Models.Sgpa.Patologium item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Patologia
                    .Where(i => i.CodPatologia == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Patologium>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPatologiumUpdated(item);
                this.context.Patologia.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Patologia.Where(i => i.CodPatologia == key);
                ;
                this.OnAfterPatologiumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/Patologia(CodPatologia={CodPatologia})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPatologium(int key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.Patologium> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Patologia
                    .Where(i => i.CodPatologia == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.Patologium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPatologiumUpdated(item);
                this.context.Patologia.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Patologia.Where(i => i.CodPatologia == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPatologiumCreated(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumCreated(SgpaNew.Server.Models.Sgpa.Patologium item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.Patologium item)
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

                this.OnPatologiumCreated(item);
                this.context.Patologia.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Patologia.Where(i => i.CodPatologia == item.CodPatologia);

                ;

                this.OnAfterPatologiumCreated(item);

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
