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
    [Route("odata/Sgpa/Imps")]
    public partial class ImpsController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public ImpsController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.Imp> GetImps()
        {
            var items = this.context.Imps.AsQueryable<SgpaNew.Server.Models.Sgpa.Imp>();
            this.OnImpsRead(ref items);

            return items;
        }

        partial void OnImpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Imp> items);
    }
}
