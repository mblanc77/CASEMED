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
    public partial class EditTramiteInfoadjuntatitulo
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
            tramiteInfoadjuntatitulo = await CMUService.GetTramiteInfoadjuntatituloByOid(oid:OID);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteInfoadjuntatitulo;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> tramiteInfoadjuntabasesForOID;

        protected IEnumerable<SGPA.Server.Models.CMU.UniversidadTituloGrado> universidadTituloGradosForUniversidad;


        protected int tramiteInfoadjuntabasesForOIDCount;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteInfoadjuntabasesForOIDValue;
        protected async Task tramiteInfoadjuntabasesForOIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteInfoadjuntabases(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteInfoadjuntabasesForOID = result.Value.AsODataEnumerable();
                tramiteInfoadjuntabasesForOIDCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntatitulo.OID, null))
                {
                    var valueResult = await CMUService.GetTramiteInfoadjuntabases(filter: $"OID eq {tramiteInfoadjuntatitulo.OID}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteInfoadjuntabasesForOIDValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteInfoAdjuntaBase" });
            }
        }

        protected int universidadTituloGradosForUniversidadCount;
        protected SGPA.Server.Models.CMU.UniversidadTituloGrado universidadTituloGradosForUniversidadValue;
        protected async Task universidadTituloGradosForUniversidadLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetUniversidadTituloGrados(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                universidadTituloGradosForUniversidad = result.Value.AsODataEnumerable();
                universidadTituloGradosForUniversidadCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntatitulo.Universidad, null))
                {
                    var valueResult = await CMUService.GetUniversidadTituloGrados(filter: $"Id eq {tramiteInfoadjuntatitulo.Universidad}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        universidadTituloGradosForUniversidadValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load UniversidadTituloGrado" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateTramiteInfoadjuntatitulo(oid:OID, tramiteInfoadjuntatitulo);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(tramiteInfoadjuntatitulo);
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

            tramiteInfoadjuntatitulo = await CMUService.GetTramiteInfoadjuntatituloByOid(oid:OID);
        }
    }
}