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
    public partial class AddDebito
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
            debito = new SGPA.Server.Models.CMU.Debito();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Debito debito;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> agenteCobranzaDebitosForAgenteDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.Cobro> cobrosForId;


        protected int agenteCobranzaDebitosForAgenteDebitoCount;
        protected SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebitosForAgenteDebitoValue;
        protected async Task agenteCobranzaDebitosForAgenteDebitoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzaDebitos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzaDebitosForAgenteDebito = result.Value.AsODataEnumerable();
                agenteCobranzaDebitosForAgenteDebitoCount = result.Count;

                if (!object.Equals(debito.AgenteDebito, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzaDebitos(filter: $"Id eq {debito.AgenteDebito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzaDebitosForAgenteDebitoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranzaDebito" });
            }
        }

        protected int cobrosForIdCount;
        protected SGPA.Server.Models.CMU.Cobro cobrosForIdValue;
        protected async Task cobrosForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetCobros(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                cobrosForId = result.Value.AsODataEnumerable();
                cobrosForIdCount = result.Count;

                if (!object.Equals(debito.Id, null))
                {
                    var valueResult = await CMUService.GetCobros(filter: $"Id eq {debito.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        cobrosForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Cobro" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateDebito(debito);
                DialogService.Close(debito);
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