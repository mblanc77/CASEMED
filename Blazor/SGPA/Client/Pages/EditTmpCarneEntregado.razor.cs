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
    public partial class EditTmpCarneEntregado
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
        public int Documento { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tmpCarneEntregado = await CMUService.GetTmpCarneEntregadoByDocumento(documento:Documento);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TmpCarneEntregado tmpCarneEntregado;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateTmpCarneEntregado(documento:Documento, tmpCarneEntregado);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(tmpCarneEntregado);
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

            tmpCarneEntregado = await CMUService.GetTmpCarneEntregadoByDocumento(documento:Documento);
        }
    }
}