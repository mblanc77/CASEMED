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
    public partial class EditTramiteInfoadjuntaespecialidad
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
        public int OID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tramiteInfoadjuntaespecialidad = await CMUService.GetTramiteInfoadjuntaespecialidadByOid(oid:OID);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad tramiteInfoadjuntaespecialidad;

        protected IEnumerable<SGPA.Server.Models.CMU.Especialidad> especialidadsForEspecialidad;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> tramiteInfoadjuntatitulosForOID;


        protected int especialidadsForEspecialidadCount;
        protected SGPA.Server.Models.CMU.Especialidad especialidadsForEspecialidadValue;
        protected async Task especialidadsForEspecialidadLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetEspecialidads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                especialidadsForEspecialidad = result.Value.AsODataEnumerable();
                especialidadsForEspecialidadCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntaespecialidad.Especialidad, null))
                {
                    var valueResult = await CMUService.GetEspecialidads(filter: $"Id eq {tramiteInfoadjuntaespecialidad.Especialidad}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        especialidadsForEspecialidadValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Especialidad1" });
            }
        }

        protected int tramiteInfoadjuntatitulosForOIDCount;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteInfoadjuntatitulosForOIDValue;
        protected async Task tramiteInfoadjuntatitulosForOIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteInfoadjuntatitulos(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteInfoadjuntatitulosForOID = result.Value.AsODataEnumerable();
                tramiteInfoadjuntatitulosForOIDCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntaespecialidad.OID, null))
                {
                    var valueResult = await CMUService.GetTramiteInfoadjuntatitulos(filter: $"OID eq {tramiteInfoadjuntaespecialidad.OID}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteInfoadjuntatitulosForOIDValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteInfoAdjuntaTitulo" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateTramiteInfoadjuntaespecialidad(oid:OID, tramiteInfoadjuntaespecialidad);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(tramiteInfoadjuntaespecialidad);
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

            tramiteInfoadjuntaespecialidad = await CMUService.GetTramiteInfoadjuntaespecialidadByOid(oid:OID);
        }
    }
}