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
    public partial class EditSolicitudBajaFileAttachment
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
        public Guid Guid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            solicitudBajaFileAttachment = await CMUService.GetSolicitudBajaFileAttachmentByGuid(guid:Guid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SolicitudBajaFileAttachment solicitudBajaFileAttachment;

        protected IEnumerable<SGPA.Server.Models.CMU.FileDatum> fileDataForFile;

        protected IEnumerable<SGPA.Server.Models.CMU.SolicitudBaja> solicitudBajasForSolicitud;


        protected int fileDataForFileCount;
        protected SGPA.Server.Models.CMU.FileDatum fileDataForFileValue;
        protected async Task fileDataForFileLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetFileData(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                fileDataForFile = result.Value.AsODataEnumerable();
                fileDataForFileCount = result.Count;

                if (!object.Equals(solicitudBajaFileAttachment.File, null))
                {
                    var valueResult = await CMUService.GetFileData(filter: $"Oid eq {solicitudBajaFileAttachment.File}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        fileDataForFileValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load FileDatum" });
            }
        }

        protected int solicitudBajasForSolicitudCount;
        protected SGPA.Server.Models.CMU.SolicitudBaja solicitudBajasForSolicitudValue;
        protected async Task solicitudBajasForSolicitudLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSolicitudBajas(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                solicitudBajasForSolicitud = result.Value.AsODataEnumerable();
                solicitudBajasForSolicitudCount = result.Count;

                if (!object.Equals(solicitudBajaFileAttachment.Solicitud, null))
                {
                    var valueResult = await CMUService.GetSolicitudBajas(filter: $"OID eq {solicitudBajaFileAttachment.Solicitud}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        solicitudBajasForSolicitudValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SolicitudBaja" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateSolicitudBajaFileAttachment(guid:Guid, solicitudBajaFileAttachment);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(solicitudBajaFileAttachment);
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

            solicitudBajaFileAttachment = await CMUService.GetSolicitudBajaFileAttachmentByGuid(guid:Guid);
        }
    }
}