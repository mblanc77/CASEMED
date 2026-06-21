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
    public partial class EditTmpMese
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
        public int Mes { get; set; }

        [Parameter]
        public int Año { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tmpMese = await CMUService.GetTmpMeseByMesAndAño(mes:Mes, año:Año);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TmpMese tmpMese;

        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateTmpMese(mes:Mes, año:Año, tmpMese);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(tmpMese);
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

            tmpMese = await CMUService.GetTmpMeseByMesAndAño(mes:Mes, año:Año);
        }
    }
}