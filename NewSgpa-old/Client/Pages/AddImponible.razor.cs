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
    public partial class AddImponible
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
            imponible = new SgpaNew.Server.Models.Sgpa.Imponible();
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Imponible imponible;

        protected IEnumerable<SgpaNew.Server.Models.Sgpa.Trabaja> trabajasForIdTrabaja;


        protected int trabajasForIdTrabajaCount;
        protected SgpaNew.Server.Models.Sgpa.Trabaja trabajasForIdTrabajaValue;
        protected async Task trabajasForIdTrabajaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await SgpaService.GetTrabajas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(NroFichaEmpresa, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                trabajasForIdTrabaja = result.Value.AsODataEnumerable();
                trabajasForIdTrabajaCount = result.Count;

                if (!object.Equals(imponible.IdTrabaja, null))
                {
                    var valueResult = await SgpaService.GetTrabajas(filter: $"IdTrabaja eq {imponible.IdTrabaja}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        trabajasForIdTrabajaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Trabaja" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.CreateImponible(imponible);
                DialogService.Close(imponible);
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