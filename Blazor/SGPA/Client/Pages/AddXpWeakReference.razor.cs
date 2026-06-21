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
    public partial class AddXpWeakReference
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
            xpWeakReference = new SGPA.Server.Models.CMU.XpWeakReference();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.XpWeakReference xpWeakReference;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForObjectType;

        protected IEnumerable<SGPA.Server.Models.CMU.XpObjectType> xpObjectTypesForTargetType;


        protected int xpObjectTypesForObjectTypeCount;
        protected SGPA.Server.Models.CMU.XpObjectType xpObjectTypesForObjectTypeValue;
        protected async Task xpObjectTypesForObjectTypeLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpObjectTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpObjectTypesForObjectType = result.Value.AsODataEnumerable();
                xpObjectTypesForObjectTypeCount = result.Count;

                if (!object.Equals(xpWeakReference.ObjectType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {xpWeakReference.ObjectType}");
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

        protected int xpObjectTypesForTargetTypeCount;
        protected SGPA.Server.Models.CMU.XpObjectType xpObjectTypesForTargetTypeValue;
        protected async Task xpObjectTypesForTargetTypeLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetXpObjectTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                xpObjectTypesForTargetType = result.Value.AsODataEnumerable();
                xpObjectTypesForTargetTypeCount = result.Count;

                if (!object.Equals(xpWeakReference.TargetType, null))
                {
                    var valueResult = await CMUService.GetXpObjectTypes(filter: $"OID eq {xpWeakReference.TargetType}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        xpObjectTypesForTargetTypeValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load XpobjectType1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateXpWeakReference(xpWeakReference);
                DialogService.Close(xpWeakReference);
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