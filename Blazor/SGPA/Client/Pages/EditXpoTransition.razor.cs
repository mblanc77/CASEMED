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
    public partial class EditXpoTransition
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
            xpoTransition = await CMUService.GetXpoTransitionByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpoTransition xpoTransition;

        protected IEnumerable<SGPA.Server.Models.CMU.XpoState> xpoStatesForSourceState;

        protected IEnumerable<SGPA.Server.Models.CMU.XpoState> xpoStatesForTargetState;


        protected int xpoStatesForSourceStateCount;
        protected SGPA.Server.Models.CMU.XpoState xpoStatesForSourceStateValue;
        protected async Task xpoStatesForSourceStateLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpoStates(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpoStatesForSourceState = result.Value.AsODataEnumerable();
                xpoStatesForSourceStateCount = result.Count;

                if (!object.Equals(xpoTransition.SourceState, null))
                {
                    var valueResult = await CMUService.GetXpoStates(filter: $"Oid eq {xpoTransition.SourceState}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpoStatesForSourceStateValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpoState" });
            }
        }

        protected int xpoStatesForTargetStateCount;
        protected SGPA.Server.Models.CMU.XpoState xpoStatesForTargetStateValue;
        protected async Task xpoStatesForTargetStateLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpoStates(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpoStatesForTargetState = result.Value.AsODataEnumerable();
                xpoStatesForTargetStateCount = result.Count;

                if (!object.Equals(xpoTransition.TargetState, null))
                {
                    var valueResult = await CMUService.GetXpoStates(filter: $"Oid eq {xpoTransition.TargetState}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpoStatesForTargetStateValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpoState1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateXpoTransition(oid:Oid, xpoTransition);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(xpoTransition);
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

            xpoTransition = await CMUService.GetXpoTransitionByOid(oid:Oid);
        }
    }
}