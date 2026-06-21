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
    [Route("odata/Sgpa/Parametros")]
    public partial class ParametrosController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public ParametrosController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Parametro> GetParametros()
        {
            var items = this.context.Parametros.AsQueryable<SgpaNew.Server.Models.Sgpa.Parametro>();
            this.OnParametrosRead(ref items);

            return items;
        }

        partial void OnParametrosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Parametro> items);
    }
}
