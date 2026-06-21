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
    public partial class EditTramiteInfoadjuntacedula
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
            tramiteInfoadjuntacedula = await CMUService.GetTramiteInfoadjuntacedulaByOid(oid:OID);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteInfoadjuntacedula;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> tramiteInfoadjuntabasesForOID;


        protected int tramiteInfoadjuntabasesForOIDCount;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteInfoadjuntabasesForOIDValue;
        protected async Task tramiteInfoadjuntabasesForOIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteInfoadjuntabases(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteInfoadjuntabasesForOID = result.Value.AsODataEnumerable();
                tramiteInfoadjuntabasesForOIDCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntacedula.OID, null))
                {
                    var valueResult = await CMUService.GetTramiteInfoadjuntabases(filter: $"OID eq {tramiteInfoadjuntacedula.OID}");
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
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateTramiteInfoadjuntacedula(oid:OID, tramiteInfoadjuntacedula);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(tramiteInfoadjuntacedula);
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

            tramiteInfoadjuntacedula = await CMUService.GetTramiteInfoadjuntacedulaByOid(oid:OID);
        }
    }
}