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
    public partial class EditEspecialidad
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
        public int CodEspecialidad { get; set; }

        protected override async Task OnInitializedAsync()
        {
            especialidad = await SgpaService.GetEspecialidadByCodEspecialidad(codEspecialidad:CodEspecialidad);
        }
        protected bool errorVisible;
        protected SgpaNew.Server.Models.Sgpa.Especialidad especialidad;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await SgpaService.UpdateEspecialidad(codEspecialidad:CodEspecialidad, especialidad);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(especialidad);
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

            especialidad = await SgpaService.GetEspecialidadByCodEspecialidad(codEspecialidad:CodEspecialidad);
        }
    }
}