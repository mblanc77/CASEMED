using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace SgpaNew.Client.Pages
{
    public partial class EditAdPreJubPago
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
        public SgpaService SgpaService { get; set; }

        [Parameter]
        public int AdPreJubPagoId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            adPreJubPago = await SgpaService.GetAdPreJubPagoByAdPreJubPagoId(adPreJubPagoId:AdPreJubPagoId);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.AdPreJubPago adPreJubPago;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.AdPreJub> adPreJubsForCI;


        protected int adPreJubsForCICount;
        protected SgpaNew.Server.Models.Sgpa.AdPreJub adPreJubsForCIValue;
        protected async Task adPreJubsForCILoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetAdPreJubs(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Observaciones, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                adPreJubsForCI = result.Value.AsODataEnumerable();
                adPreJubsForCICount = result.Count;

                if (!object.Equals(adPreJubPago.CI, null))
                {
                    var valueResult = await SgpaService.GetAdPreJubs(filter: $"CI eq {adPreJubPago.CI}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        adPreJubsForCIValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AdPreJub" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateAdPreJubPago(adPreJubPagoId:AdPreJubPagoId, adPreJubPago);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(adPreJubPago);
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

        [Inject]
        protected SecurityService Security { get; set; }


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            adPreJubPago = await SgpaService.GetAdPreJubPagoByAdPreJubPagoId(adPreJubPagoId:AdPreJubPagoId);
        }
    }
}