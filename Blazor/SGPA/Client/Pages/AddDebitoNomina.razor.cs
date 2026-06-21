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
    public partial class AddDebitoNomina
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
            debitoNomina = new SGPA.Server.Models.CMU.DebitoNomina();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DebitoNomina debitoNomina;

        protected IEnumerable<SGPA.Server.Models.CMU.Debito> debitosForDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.CobroNomina> cobroNominasForId;


        protected int debitosForDebitoCount;
        protected SGPA.Server.Models.CMU.Debito debitosForDebitoValue;
        protected async Task debitosForDebitoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDebitos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                debitosForDebito = result.Value.AsODataEnumerable();
                debitosForDebitoCount = result.Count;

                if (!object.Equals(debitoNomina.Debito, null))
                {
                    var valueResult = await CMUService.GetDebitos(filter: $"Id eq {debitoNomina.Debito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        debitosForDebitoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Debito1" });
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

                if (!object.Equals(debitoNomina.Id, null))
                {
                    var valueResult = await CMUService.GetCobroNominas(filter: $"Id eq {debitoNomina.Id}");
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
                var result = await CMUService.CreateDebitoNomina(debitoNomina);
                DialogService.Close(debitoNomina);
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