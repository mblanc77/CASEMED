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
    public partial class AddXpoState
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
            xpoState = new SGPA.Server.Models.CMU.XpoState();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpoState xpoState;

        protected IEnumerable<SGPA.Server.Models.CMU.XpoStateMachine> xpoStateMachinesForStateMachine;


        protected int xpoStateMachinesForStateMachineCount;
        protected SGPA.Server.Models.CMU.XpoStateMachine xpoStateMachinesForStateMachineValue;
        protected async Task xpoStateMachinesForStateMachineLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpoStateMachines(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpoStateMachinesForStateMachine = result.Value.AsODataEnumerable();
                xpoStateMachinesForStateMachineCount = result.Count;

                if (!object.Equals(xpoState.StateMachine, null))
                {
                    var valueResult = await CMUService.GetXpoStateMachines(filter: $"Oid eq {xpoState.StateMachine}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpoStateMachinesForStateMachineValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpoStateMachine" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateXpoState(xpoState);
                DialogService.Close(xpoState);
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