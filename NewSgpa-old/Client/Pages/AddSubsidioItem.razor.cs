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
    public partial class AddSubsidioItem
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
            subsidioItem = new SgpaNew.Server.Models.Sgpa.SubsidioItem();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.SubsidioItem subsidioItem;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> subsidioItemCodsForCodSubsidioItemCod;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> subsidioCabezalsForIdSubsidio;


        protected int subsidioItemCodsForCodSubsidioItemCodCount;
        protected SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioItemCodsForCodSubsidioItemCodValue;
        protected async Task subsidioItemCodsForCodSubsidioItemCodLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetSubsidioItemCods(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Descrip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                subsidioItemCodsForCodSubsidioItemCod = result.Value.AsODataEnumerable();
                subsidioItemCodsForCodSubsidioItemCodCount = result.Count;

                if (!object.Equals(subsidioItem.CodSubsidioItemCod, null))
                {
                    var valueResult = await SgpaService.GetSubsidioItemCods(filter: $"CodSubsidioItemCod eq {subsidioItem.CodSubsidioItemCod}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        subsidioItemCodsForCodSubsidioItemCodValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SubsidioItemCod" });
            }
        }

        protected int subsidioCabezalsForIdSubsidioCount;
        protected SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidioCabezalsForIdSubsidioValue;
        protected async Task subsidioCabezalsForIdSubsidioLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetSubsidioCabezals(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(NroCuenta, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                subsidioCabezalsForIdSubsidio = result.Value.AsODataEnumerable();
                subsidioCabezalsForIdSubsidioCount = result.Count;

                if (!object.Equals(subsidioItem.IdSubsidio, null))
                {
                    var valueResult = await SgpaService.GetSubsidioCabezals(filter: $"IdSubsidio eq {subsidioItem.IdSubsidio}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        subsidioCabezalsForIdSubsidioValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SubsidioCabezal" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateSubsidioItem(subsidioItem);
                DialogService.Close(subsidioItem);
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