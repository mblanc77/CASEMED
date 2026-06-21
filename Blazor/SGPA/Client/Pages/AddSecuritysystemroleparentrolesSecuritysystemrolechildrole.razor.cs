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
    public partial class AddSecuritysystemroleparentrolesSecuritysystemrolechildrole
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
            securitysystemroleparentrolesSecuritysystemrolechildrole = new SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole();
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole securitysystemroleparentrolesSecuritysystemrolechildrole;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemRole> securitySystemRolesForChildRoles;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemRole> securitySystemRolesForParentRoles;


        protected int securitySystemRolesForChildRolesCount;
        protected SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRolesForChildRolesValue;
        protected async Task securitySystemRolesForChildRolesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemRoles(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemRolesForChildRoles = result.Value.AsODataEnumerable();
                securitySystemRolesForChildRolesCount = result.Count;

                if (!object.Equals(securitysystemroleparentrolesSecuritysystemrolechildrole.ChildRoles, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemRoles(filter: $"Oid eq {securitysystemroleparentrolesSecuritysystemrolechildrole.ChildRoles}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemRolesForChildRolesValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemRole" });
            }
        }

        protected int securitySystemRolesForParentRolesCount;
        protected SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRolesForParentRolesValue;
        protected async Task securitySystemRolesForParentRolesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemRoles(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemRolesForParentRoles = result.Value.AsODataEnumerable();
                securitySystemRolesForParentRolesCount = result.Count;

                if (!object.Equals(securitysystemroleparentrolesSecuritysystemrolechildrole.ParentRoles, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemRoles(filter: $"Oid eq {securitysystemroleparentrolesSecuritysystemrolechildrole.ParentRoles}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemRolesForParentRolesValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemRole1" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.CreateSecuritysystemroleparentrolesSecuritysystemrolechildrole(securitysystemroleparentrolesSecuritysystemrolechildrole);
                DialogService.Close(securitysystemroleparentrolesSecuritysystemrolechildrole);
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