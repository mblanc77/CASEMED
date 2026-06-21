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
    public partial class AddDepositoNominaNoIdentificadum
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
            depositoNominaNoIdentificadum = new SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum depositoNominaNoIdentificadum;

        protected IEnumerable<SGPA.Server.Models.CMU.Deposito> depositosForDeposito;


        protected int depositosForDepositoCount;
        protected SGPA.Server.Models.CMU.Deposito depositosForDepositoValue;
        protected async Task depositosForDepositoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepositos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                depositosForDeposito = result.Value.AsODataEnumerable();
                depositosForDepositoCount = result.Count;

                if (!object.Equals(depositoNominaNoIdentificadum.Deposito, null))
                {
                    var valueResult = await CMUService.GetDepositos(filter: $"Id eq {depositoNominaNoIdentificadum.Deposito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        depositosForDepositoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Deposito1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateDepositoNominaNoIdentificadum(depositoNominaNoIdentificadum);
                DialogService.Close(depositoNominaNoIdentificadum);
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