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
    public partial class EditSubsidioItemCod
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
        public short CodSubsidioItemCod { get; set; }

        protected override async Task OnInitializedAsync()
        {
            subsidioItemCod = await SgpaService.GetSubsidioItemCodByCodSubsidioItemCod(codSubsidioItemCod:CodSubsidioItemCod);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioItemCod;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateSubsidioItemCod(codSubsidioItemCod:CodSubsidioItemCod, subsidioItemCod);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(subsidioItemCod);
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

            subsidioItemCod = await SgpaService.GetSubsidioItemCodByCodSubsidioItemCod(codSubsidioItemCod:CodSubsidioItemCod);
        }
    }
}