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
    public partial class EditMovimientoCuentaManual
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            movimientoCuentaManual = await CMUService.GetMovimientoCuentaManualById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.MovimientoCuentaManual movimientoCuentaManual;

        protected IEnumerable<SGPA.Server.Models.CMU.MovimientoCuentum> movimientoCuentaForId;


        protected int movimientoCuentaForIdCount;
        protected SGPA.Server.Models.CMU.MovimientoCuentum movimientoCuentaForIdValue;
        protected async Task movimientoCuentaForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetMovimientoCuenta(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                movimientoCuentaForId = result.Value.AsODataEnumerable();
                movimientoCuentaForIdCount = result.Count;

                if (!object.Equals(movimientoCuentaManual.Id, null))
                {
                    var valueResult = await CMUService.GetMovimientoCuenta(filter: $"Id eq {movimientoCuentaManual.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        movimientoCuentaForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load MovimientoCuentum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateMovimientoCuentaManual(id:Id, movimientoCuentaManual);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(movimientoCuentaManual);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            movimientoCuentaManual = await CMUService.GetMovimientoCuentaManualById(id:Id);
        }
    }
}