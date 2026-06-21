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
    public partial class AddXpoStateMachine
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
            xpoStateMachine = new SGPA.Server.Models.CMU.XpoStateMachine();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpoStateMachine xpoStateMachine;

        protected IEnumerable<SGPA.Server.Models.CMU.XpoState> xpoStatesForStartState;


        protected int xpoStatesForStartStateCount;
        protected SGPA.Server.Models.CMU.XpoState xpoStatesForStartStateValue;
        protected async Task xpoStatesForStartStateLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpoStates(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpoStatesForStartState = result.Value.AsODataEnumerable();
                xpoStatesForStartStateCount = result.Count;

                if (!object.Equals(xpoStateMachine.StartState, null))
                {
                    var valueResult = await CMUService.GetXpoStates(filter: $"Oid eq {xpoStateMachine.StartState}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpoStatesForStartStateValue = firstItem;
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
                var result = await CMUService.CreateXpoStateMachine(xpoStateMachine);
                DialogService.Close(xpoStateMachine);
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