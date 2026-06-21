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
    public partial class EditSecuritySystemMemberPermissionsObject
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
            securitySystemMemberPermissionsObject = await CMUService.GetSecuritySystemMemberPermissionsObjectByOid(oid:Oid);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject securitySystemMemberPermissionsObject;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> securitySystemTypePermissionsObjectsForOwner;


        protected int securitySystemTypePermissionsObjectsForOwnerCount;
        protected SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject securitySystemTypePermissionsObjectsForOwnerValue;
        protected async Task securitySystemTypePermissionsObjectsForOwnerLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemTypePermissionsObjects(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemTypePermissionsObjectsForOwner = result.Value.AsODataEnumerable();
                securitySystemTypePermissionsObjectsForOwnerCount = result.Count;

                if (!object.Equals(securitySystemMemberPermissionsObject.Owner, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemTypePermissionsObjects(filter: $"Oid eq {securitySystemMemberPermissionsObject.Owner}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemTypePermissionsObjectsForOwnerValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemTypePermissionsObject" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateSecuritySystemMemberPermissionsObject(oid:Oid, securitySystemMemberPermissionsObject);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(securitySystemMemberPermissionsObject);
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

            securitySystemMemberPermissionsObject = await CMUService.GetSecuritySystemMemberPermissionsObjectByOid(oid:Oid);
        }
    }
}