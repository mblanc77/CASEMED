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
    public partial class EditDebitoAdjunto
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
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            debitoAdjunto = await CMUService.GetDebitoAdjuntoById(id:Id);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.DebitoAdjunto debitoAdjunto;

        protected IEnumerable<SGPA.Server.Models.CMU.Debito> debitosForDebito;

        protected IEnumerable<SGPA.Server.Models.CMU.FileDatum> fileDataForFileData;


        protected int debitosForDebitoCount;
        protected SGPA.Server.Models.CMU.Debito debitosForDebitoValue;
        protected async Task debitosForDebitoLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetDebitos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                debitosForDebito = result.Value.AsODataEnumerable();
                debitosForDebitoCount = result.Count;

                if (!object.Equals(debitoAdjunto.Debito, null))
                {
                    var valueResult = await CMUService.GetDebitos(filter: $"Id eq {debitoAdjunto.Debito}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        debitosForDebitoValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Debito1" });
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

                if (!object.Equals(debitoAdjunto.FileData, null))
                {
                    var valueResult = await CMUService.GetFileData(filter: $"Oid eq {debitoAdjunto.FileData}");
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
                var result = await CMUService.UpdateDebitoAdjunto(id:Id, debitoAdjunto);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(debitoAdjunto);
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

            debitoAdjunto = await CMUService.GetDebitoAdjuntoById(id:Id);
        }
    }
}