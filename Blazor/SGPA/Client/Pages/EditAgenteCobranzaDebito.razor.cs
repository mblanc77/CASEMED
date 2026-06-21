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
    public partial class EditAgenteCobranzaDebito
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
            agenteCobranzaDebito = await CMUService.GetAgenteCobranzaDebitoById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.AgenteCobranzaDebito agenteCobranzaDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.AgenteCobranza> agenteCobranzasForId;


        protected int agenteCobranzasForIdCount;
        protected SGPA.Server.Models.CMU.AgenteCobranza agenteCobranzasForIdValue;
        protected async Task agenteCobranzasForIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetAgenteCobranzas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                agenteCobranzasForId = result.Value.AsODataEnumerable();
                agenteCobranzasForIdCount = result.Count;

                if (!object.Equals(agenteCobranzaDebito.Id, null))
                {
                    var valueResult = await CMUService.GetAgenteCobranzas(filter: $"Id eq {agenteCobranzaDebito.Id}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        agenteCobranzasForIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AgenteCobranza" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateAgenteCobranzaDebito(id:Id, agenteCobranzaDebito);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(agenteCobranzaDebito);
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

            agenteCobranzaDebito = await CMUService.GetAgenteCobranzaDebitoById(id:Id);
        }
    }
}