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
    public partial class AddMutualistum
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

        protected override async Task OnInitializedAsync()
        {
            mutualistum = new SgpaNew.Server.Models.Sgpa.Mutualistum();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Mutualistum mutualistum;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.FormaPago> formaPagosForCodFormaPago;


        protected int formaPagosForCodFormaPagoCount;
        protected SgpaNew.Server.Models.Sgpa.FormaPago formaPagosForCodFormaPagoValue;
        protected async Task formaPagosForCodFormaPagoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetFormaPagos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                formaPagosForCodFormaPago = result.Value.AsODataEnumerable();
                formaPagosForCodFormaPagoCount = result.Count;

                if (!object.Equals(mutualistum.CodFormaPago, null))
                {
                    var valueResult = await SgpaService.GetFormaPagos(filter: $"CodFormaPago eq {mutualistum.CodFormaPago}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        formaPagosForCodFormaPagoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load FormaPago" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateMutualistum(mutualistum);
                DialogService.Close(mutualistum);
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
    }
}