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
    [Route("odata/Sgpa/SubsidioImponibles")]
    public partial class SubsidioImponiblesController : ODataController
    {
        private SgpaNew.Server.Data.SgpaContext context;

        public SubsidioImponiblesController(SgpaNew.Server.Data.SgpaContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioImponible> GetSubsidioImponibles()
        {
            var items = this.context.SubsidioImponibles.AsQueryable<SgpaNew.Server.Models.Sgpa.SubsidioImponible>();
            this.OnSubsidioImponiblesRead(ref items);

            return items;
        }

        partial void OnSubsidioImponiblesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioImponible> items);
    }
}
