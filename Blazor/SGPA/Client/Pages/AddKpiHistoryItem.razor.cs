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
    public partial class AddKpiHistoryItem
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
            kpiHistoryItem = new SGPA.Server.Models.CMU.KpiHistoryItem();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.KpiHistoryItem kpiHistoryItem;

        protected IEnumerable<SGPA.Server.Models.CMU.KpiInstance> kpiInstancesForKpiInstance;


        protected int kpiInstancesForKpiInstanceCount;
        protected SGPA.Server.Models.CMU.KpiInstance kpiInstancesForKpiInstanceValue;
        protected async Task kpiInstancesForKpiInstanceLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetKpiInstances(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                kpiInstancesForKpiInstance = result.Value.AsODataEnumerable();
                kpiInstancesForKpiInstanceCount = result.Count;

                if (!object.Equals(kpiHistoryItem.KpiInstance, null))
                {
                    var valueResult = await CMUService.GetKpiInstances(filter: $"Oid eq {kpiHistoryItem.KpiInstance}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        kpiInstancesForKpiInstanceValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load KpiInstance1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateKpiHistoryItem(kpiHistoryItem);
                DialogService.Close(kpiHistoryItem);
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