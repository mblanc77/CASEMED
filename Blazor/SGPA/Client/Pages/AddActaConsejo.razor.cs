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
    public partial class AddActaConsejo
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
            actaConsejo = new SGPA.Server.Models.CMU.ActaConsejo();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.ActaConsejo actaConsejo;

        protected IEnumerable<SGPA.Server.Models.CMU.FileDatum> fileDataForArchivo;


        protected int fileDataForArchivoCount;
        protected SGPA.Server.Models.CMU.FileDatum fileDataForArchivoValue;
        protected async Task fileDataForArchivoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetFileData(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                fileDataForArchivo = result.Value.AsODataEnumerable();
                fileDataForArchivoCount = result.Count;

                if (!object.Equals(actaConsejo.Archivo, null))
                {
                    var valueResult = await CMUService.GetFileData(filter: $"Oid eq {actaConsejo.Archivo}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        fileDataForArchivoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load FileDatum" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateActaConsejo(actaConsejo);
                DialogService.Close(actaConsejo);
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