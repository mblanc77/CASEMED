using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace SGPA.Client.Pages
{
    public partial class TramiteInfoadjuntacedulas
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public CMUService CMUService { get; set; }

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> tramiteInfoadjuntacedulas;

        protected RadzenDataGrid<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteInfoadjuntacedulas(filter: $"{args.Filter}", expand: "TramiteInfoAdjuntaBase", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                tramiteInfoadjuntacedulas = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteInfoadjuntacedulas" });
            }
        }    

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddTramiteInfoadjuntacedula>("Add TramiteInfoadjuntacedula", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> args)
        {
            await DialogService.OpenAsync<EditTramiteInfoadjuntacedula>("Edit TramiteInfoadjuntacedula", new Dictionary<string, object> { {"OID", args.Data.OID} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteInfoadjuntacedula)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await CMUService.DeleteTramiteInfoadjuntacedula(oid:tramiteInfoadjuntacedula.OID);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete TramiteInfoadjuntacedula" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await CMUService.ExportTramiteInfoadjuntacedulasToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "TramiteInfoAdjuntaBase", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "TramiteInfoadjuntacedulas");
            }

            if (args == null || args.Value == "xlsx")
            {
                await CMUService.ExportTramiteInfoadjuntacedulasToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "TramiteInfoAdjuntaBase", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "TramiteInfoadjuntacedulas");
            }
        }
    }
}