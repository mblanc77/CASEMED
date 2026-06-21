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
    public partial class EditBajaMotivo
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
        public int CodBajaMotivo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            bajaMotivo = await SgpaService.GetBajaMotivoByCodBajaMotivo(codBajaMotivo:CodBajaMotivo);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.BajaMotivo bajaMotivo;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateBajaMotivo(codBajaMotivo:CodBajaMotivo, bajaMotivo);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(bajaMotivo);
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

            bajaMotivo = await SgpaService.GetBajaMotivoByCodBajaMotivo(codBajaMotivo:CodBajaMotivo);
        }
    }
}