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
    [Route("odata/Sgpa/RecetaDistancia")]
    public partial class RecetaDistanciaController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public RecetaDistanciaController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.RecetaDistancium> GetRecetaDistancia()
        {
            var items = this.context.RecetaDistancia.AsQueryable<SgpaNew.Server.Models.Sgpa.RecetaDistancium>();
            this.OnRecetaDistanciaRead(ref items);

            return items;
        }

        partial void OnRecetaDistanciaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RecetaDistancium> items);

        partial void OnRecetaDistanciumGet(ref SingleResult<SgpaNew.Server.Models.Sgpa.RecetaDistancium> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/Sgpa/RecetaDistancia(CodRecetaDistancia={CodRecetaDistancia})")]
        public SingleResult<SgpaNew.Server.Models.Sgpa.RecetaDistancium> GetRecetaDistancium(string key)
        {
            var items = this.context.RecetaDistancia.Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnRecetaDistanciumGet(ref result);

            return result;
        }
        partial void OnRecetaDistanciumDeleted(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumDeleted(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        [HttpDelete("/odata/Sgpa/RecetaDistancia(CodRecetaDistancia={CodRecetaDistancia})")]
        public IActionResult DeleteRecetaDistancium(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RecetaDistancia
                    .Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key))
                    .Include(i => i.Receta)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RecetaDistancium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRecetaDistanciumDeleted(item);
                this.context.RecetaDistancia.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRecetaDistanciumDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRecetaDistanciumUpdated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumUpdated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        [HttpPut("/odata/Sgpa/RecetaDistancia(CodRecetaDistancia={CodRecetaDistancia})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRecetaDistancium(string key, [FromBody]SgpaNew.Server.Models.Sgpa.RecetaDistancium item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RecetaDistancia
                    .Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RecetaDistancium>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRecetaDistanciumUpdated(item);
                this.context.RecetaDistancia.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RecetaDistancia.Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key));
                ;
                this.OnAfterRecetaDistanciumUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/Sgpa/RecetaDistancia(CodRecetaDistancia={CodRecetaDistancia})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRecetaDistancium(string key, [FromBody]Delta<SgpaNew.Server.Models.Sgpa.RecetaDistancium> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RecetaDistancia
                    .Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<SgpaNew.Server.Models.Sgpa.RecetaDistancium>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRecetaDistanciumUpdated(item);
                this.context.RecetaDistancia.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RecetaDistancia.Where(i => i.CodRecetaDistancia == Uri.UnescapeDataString(key));
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRecetaDistanciumCreated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumCreated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] SgpaNew.Server.Models.Sgpa.RecetaDistancium item)
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

                this.OnRecetaDistanciumCreated(item);
                this.context.RecetaDistancia.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RecetaDistancia.Where(i => i.CodRecetaDistancia == item.CodRecetaDistancia);

                ;

                this.OnAfterRecetaDistanciumCreated(item);

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
