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
    public partial class AddTramiteInfoadjuntabase
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
            tramiteInfoadjuntabase = new SGPA.Server.Models.CMU.TramiteInfoadjuntabase();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteInfoadjuntabase;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;

        protected IEnumerable<SGPA.Server.Models.CMU.TramiteCarne> tramiteCarnesForTramite;


        protected int xpObjectTypesForObjectTypeCount;
        protected SGPA.Server.Models.CMU.XpObjectType xpObjectTypesForObjectTypeValue;
        protected async Task xpObjectTypesForObjectTypeLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpObjectTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpObjectTypesForObjectType = result.Value.AsODataEnumerable();
                xpObjectTypesForObjectTypeCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntabase.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {tramiteInfoadjuntabase.ObjectType}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpObjectTypesForObjectTypeValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpobjectType" });
            }
        }

        protected int tramiteCarnesForTramiteCount;
        protected SGPA.Server.Models.CMU.TramiteCarne tramiteCarnesForTramiteValue;
        protected async Task tramiteCarnesForTramiteLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetTramiteCarnes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                tramiteCarnesForTramite = result.Value.AsODataEnumerable();
                tramiteCarnesForTramiteCount = result.Count;

                if (!object.Equals(tramiteInfoadjuntabase.Tramite, null))
                {
                    var valueResult = await CMUService.GetTramiteCarnes(filter: $"OID eq {tramiteInfoadjuntabase.Tramite}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tramiteCarnesForTramiteValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TramiteCarne" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateTramiteInfoadjuntabase(tramiteInfoadjuntabase);
                DialogService.Close(tramiteInfoadjuntabase);
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