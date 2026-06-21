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
    public partial class AddRegional
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
            regional = new SGPA.Server.Models.CMU.Regional();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.Regional regional;

        protected IEnumerable<SGPA.Server.Models.CMU.Region> regionsForRegion;


        protected int regionsForRegionCount;
        protected SGPA.Server.Models.CMU.Region regionsForRegionValue;
        protected async Task regionsForRegionLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetRegions(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                regionsForRegion = result.Value.AsODataEnumerable();
                regionsForRegionCount = result.Count;

                if (!object.Equals(regional.Region, null))
                {
                    var valueResult = await CMUService.GetRegions(filter: $"Id eq {regional.Region}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        regionsForRegionValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Region1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateRegional(regional);
                DialogService.Close(regional);
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