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
    public partial class AddGrupoLugarRetiroCarne
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

        protected override async Task OnInitializedAsync()
        {
            grupoLugarRetiroCarne = new SGPA.Server.Models.CMU.GrupoLugarRetiroCarne();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupoLugarRetiroCarne;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateGrupoLugarRetiroCarne(grupoLugarRetiroCarne);
                DialogService.Close(grupoLugarRetiroCarne);
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
    }
}