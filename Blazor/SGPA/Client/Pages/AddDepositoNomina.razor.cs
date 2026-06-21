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
    public partial class AddDepositoNomina
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
            depositoNomina = new SGPA.Server.Models.CMU.DepositoNomina();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DepositoNomina depositoNomina;

        protected IEnumerable<SGPA.Server.Models.CMU.Deposito> depositosForDeposito;

        protected IEnumerable<SGPA.Server.Models.CMU.CobroNomina> cobroNominasForId;


        protected int depositosForDepositoCount;
        protected SGPA.Server.Models.CMU.Deposito depositosForDepositoValue;
        protected async Task depositosForDepositoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDepositos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                depositosForDeposito = result.Value.AsODataEnumerable();
                depositosForDepositoCount = result.Count;

                if (!object.Equals(depositoNomina.Deposito, null))
                {
                    var valueResult = await CMUService.GetDepositos(filter: $"Id eq {depositoNomina.Deposito}");
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

        protected int cobroNominasForIdCount;
        protected SGPA.Server.Models.CMU.CobroNomina cobroNominasForIdValue;
        protected async Task cobroNominasForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCobroNominas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cobroNominasForId = result.Value.AsODataEnumerable();
                cobroNominasForIdCount = result.Count;

                if (!object.Equals(depositoNomina.Id, null))
                {
                    var valueResult = await CMUService.GetCobroNominas(filter: $"Id eq {depositoNomina.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cobroNominasForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load CobroNomina" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateDepositoNomina(depositoNomina);
                DialogService.Close(depositoNomina);
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