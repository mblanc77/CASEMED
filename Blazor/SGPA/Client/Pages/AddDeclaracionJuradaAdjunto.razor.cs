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
    public partial class AddDeclaracionJuradaAdjunto
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
            declaracionJuradaAdjunto = new SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto declaracionJuradaAdjunto;

        protected IEnumerable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> colegiadoDeclaracionJuradaForDeclaracion;

        protected IEnumerable<SGPA.Server.Models.CMU.FileDatum> fileDataForFileData;


        protected int colegiadoDeclaracionJuradaForDeclaracionCount;
        protected SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadoDeclaracionJuradaForDeclaracionValue;
        protected async Task colegiadoDeclaracionJuradaForDeclaracionLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetColegiadoDeclaracionJurada(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                colegiadoDeclaracionJuradaForDeclaracion = result.Value.AsODataEnumerable();
                colegiadoDeclaracionJuradaForDeclaracionCount = result.Count;

                if (!object.Equals(declaracionJuradaAdjunto.Declaracion, null))
                {
                    var valueResult = await CMUService.GetColegiadoDeclaracionJurada(filter: $"Id eq {declaracionJuradaAdjunto.Declaracion}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        colegiadoDeclaracionJuradaForDeclaracionValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load ColegiadoDeclaracionJuradum" });
            }
        }

        protected int fileDataForFileDataCount;
        protected SGPA.Server.Models.CMU.FileDatum fileDataForFileDataValue;
        protected async Task fileDataForFileDataLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetFileData(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                fileDataForFileData = result.Value.AsODataEnumerable();
                fileDataForFileDataCount = result.Count;

                if (!object.Equals(declaracionJuradaAdjunto.FileData, null))
                {
                    var valueResult = await CMUService.GetFileData(filter: $"Oid eq {declaracionJuradaAdjunto.FileData}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        fileDataForFileDataValue = firstItem;
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
                var result = await CMUService.CreateDeclaracionJuradaAdjunto(declaracionJuradaAdjunto);
                DialogService.Close(declaracionJuradaAdjunto);
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