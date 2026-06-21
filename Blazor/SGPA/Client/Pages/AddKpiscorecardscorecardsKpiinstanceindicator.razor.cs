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
    public partial class AddKpiscorecardscorecardsKpiinstanceindicator
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
            kpiscorecardscorecardsKpiinstanceindicator = new SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator kpiscorecardscorecardsKpiinstanceindicator;

        protected IEnumerable<SGPA.Server.Models.CMU.KpiInstance> kpiInstancesForIndicators;

        protected IEnumerable<SGPA.Server.Models.CMU.KpiScorecard> kpiScorecardsForScorecards;


        protected int kpiInstancesForIndicatorsCount;
        protected SGPA.Server.Models.CMU.KpiInstance kpiInstancesForIndicatorsValue;
        protected async Task kpiInstancesForIndicatorsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetKpiInstances(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                kpiInstancesForIndicators = result.Value.AsODataEnumerable();
                kpiInstancesForIndicatorsCount = result.Count;

                if (!object.Equals(kpiscorecardscorecardsKpiinstanceindicator.Indicators, null))
                {
                    var valueResult = await CMUService.GetKpiInstances(filter: $"Oid eq {kpiscorecardscorecardsKpiinstanceindicator.Indicators}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        kpiInstancesForIndicatorsValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load KpiInstance" });
            }
        }

        protected int kpiScorecardsForScorecardsCount;
        protected SGPA.Server.Models.CMU.KpiScorecard kpiScorecardsForScorecardsValue;
        protected async Task kpiScorecardsForScorecardsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetKpiScorecards(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                kpiScorecardsForScorecards = result.Value.AsODataEnumerable();
                kpiScorecardsForScorecardsCount = result.Count;

                if (!object.Equals(kpiscorecardscorecardsKpiinstanceindicator.Scorecards, null))
                {
                    var valueResult = await CMUService.GetKpiScorecards(filter: $"Oid eq {kpiscorecardscorecardsKpiinstanceindicator.Scorecards}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        kpiScorecardsForScorecardsValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load KpiScorecard" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateKpiscorecardscorecardsKpiinstanceindicator(kpiscorecardscorecardsKpiinstanceindicator);
                DialogService.Close(kpiscorecardscorecardsKpiinstanceindicator);
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