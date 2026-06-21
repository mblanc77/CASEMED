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
    public partial class AddDepositoNominaRedPago
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

        protected override async Task OnInitializedAsync()
        {
            depositoNominaRedPago = new SGPA.Server.Models.CMU.DepositoNominaRedPago();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DepositoNominaRedPago depositoNominaRedPago;

        protected IEnumerable<SGPA.Server.Models.CMU.DepositoNomina> depositoNominasForId;


        protected int depositoNominasForIdCount;
        protected SGPA.Server.Models.CMU.DepositoNomina depositoNominasForIdValue;
        protected async Task depositoNominasForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepositoNominas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                depositoNominasForId = result.Value.AsODataEnumerable();
                depositoNominasForIdCount = result.Count;

                if (!object.Equals(depositoNominaRedPago.Id, null))
                {
                    var valueResult = await CMUService.GetDepositoNominas(filter: $"Id eq {depositoNominaRedPago.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        depositoNominasForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load DepositoNomina" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateDepositoNominaRedPago(depositoNominaRedPago);
                DialogService.Close(depositoNominaRedPago);
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
    }
}