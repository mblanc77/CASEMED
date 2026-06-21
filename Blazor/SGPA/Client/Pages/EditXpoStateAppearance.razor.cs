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
    public partial class EditXpoStateAppearance
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
        public Guid Oid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            xpoStateAppearance = await CMUService.GetXpoStateAppearanceByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpoStateAppearance xpoStateAppearance;

        protected IEnumerable<SGPA.Server.Models.CMU.XpoState> xpoStatesForState;


        protected int xpoStatesForStateCount;
        protected SGPA.Server.Models.CMU.XpoState xpoStatesForStateValue;
        protected async Task xpoStatesForStateLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpoStates(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpoStatesForState = result.Value.AsODataEnumerable();
                xpoStatesForStateCount = result.Count;

                if (!object.Equals(xpoStateAppearance.State, null))
                {
                    var valueResult = await CMUService.GetXpoStates(filter: $"Oid eq {xpoStateAppearance.State}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpoStatesForStateValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpoState" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateXpoStateAppearance(oid:Oid, xpoStateAppearance);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(xpoStateAppearance);
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

            xpoStateAppearance = await CMUService.GetXpoStateAppearanceByOid(oid:Oid);
        }
    }
}