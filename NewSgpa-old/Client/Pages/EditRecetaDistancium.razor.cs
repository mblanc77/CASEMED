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
    public partial class EditRecetaDistancium
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
        public string CodRecetaDistancia { get; set; }

        protected override async Task OnInitializedAsync()
        {
            recetaDistancium = await SgpaService.GetRecetaDistanciumByCodRecetaDistancia(codRecetaDistancia:CodRecetaDistancia);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.RecetaDistancium recetaDistancium;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateRecetaDistancium(codRecetaDistancia:CodRecetaDistancia, recetaDistancium);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(recetaDistancium);
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

            recetaDistancium = await SgpaService.GetRecetaDistanciumByCodRecetaDistancia(codRecetaDistancia:CodRecetaDistancia);
        }
    }
}