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
    public partial class AddKpiInstance
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
            kpiInstance = new SGPA.Server.Models.CMU.KpiInstance();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.KpiInstance kpiInstance;

        protected IEnumerable<SGPA.Server.Models.CMU.KpiDefinition> kpiDefinitionsForKpiDefinition;


        protected int kpiDefinitionsForKpiDefinitionCount;
        protected SGPA.Server.Models.CMU.KpiDefinition kpiDefinitionsForKpiDefinitionValue;
        protected async Task kpiDefinitionsForKpiDefinitionLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetKpiDefinitions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                kpiDefinitionsForKpiDefinition = result.Value.AsODataEnumerable();
                kpiDefinitionsForKpiDefinitionCount = result.Count;

                if (!object.Equals(kpiInstance.KpiDefinition, null))
                {
                    var valueResult = await CMUService.GetKpiDefinitions(filter: $"Oid eq {kpiInstance.KpiDefinition}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        kpiDefinitionsForKpiDefinitionValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load KpiDefinition1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateKpiInstance(kpiInstance);
                DialogService.Close(kpiInstance);
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