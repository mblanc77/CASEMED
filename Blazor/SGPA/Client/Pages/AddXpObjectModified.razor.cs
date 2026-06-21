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
    public partial class AddXpObjectModified
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
            xpObjectModified = new SGPA.Server.Models.CMU.XpObjectModified();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpObjectModified xpObjectModified;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForXPObjectType;


        protected int xpObjectTypesForXPObjectTypeCount;
        protected SGPA.Server.Models.CMU.XpObjectType xpObjectTypesForXPObjectTypeValue;
        protected async Task xpObjectTypesForXPObjectTypeLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpObjectTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpObjectTypesForXPObjectType = result.Value.AsODataEnumerable();
                xpObjectTypesForXPObjectTypeCount = result.Count;

                if (!object.Equals(xpObjectModified.XPObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {xpObjectModified.XPObjectType}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpObjectTypesForXPObjectTypeValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpobjectType" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateXpObjectModified(xpObjectModified);
                DialogService.Close(xpObjectModified);
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