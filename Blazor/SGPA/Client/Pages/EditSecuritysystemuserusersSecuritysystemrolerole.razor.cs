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
    public partial class EditSecuritysystemuserusersSecuritysystemrolerole
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
        public Guid OID { get; set; }

        protected override async Task OnInitializedAsync()
        {
            securitysystemuserusersSecuritysystemrolerole = await CMUService.GetSecuritysystemuserusersSecuritysystemroleroleByOid(oid:OID);
        }
        protected bool errorVisible;
        protected SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole securitysystemuserusersSecuritysystemrolerole;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemRole> securitySystemRolesForRoles;

        protected IEnumerable<SGPA.Server.Models.CMU.SecuritySystemUser> securitySystemUsersForUsers;


        protected int securitySystemRolesForRolesCount;
        protected SGPA.Server.Models.CMU.SecuritySystemRole securitySystemRolesForRolesValue;
        protected async Task securitySystemRolesForRolesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemRoles(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemRolesForRoles = result.Value.AsODataEnumerable();
                securitySystemRolesForRolesCount = result.Count;

                if (!object.Equals(securitysystemuserusersSecuritysystemrolerole.Roles, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemRoles(filter: $"Oid eq {securitysystemuserusersSecuritysystemrolerole.Roles}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemRolesForRolesValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemRole" });
            }
        }

        protected int securitySystemUsersForUsersCount;
        protected SGPA.Server.Models.CMU.SecuritySystemUser securitySystemUsersForUsersValue;
        protected async Task securitySystemUsersForUsersLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await CMUService.GetSecuritySystemUsers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                securitySystemUsersForUsers = result.Value.AsODataEnumerable();
                securitySystemUsersForUsersCount = result.Count;

                if (!object.Equals(securitysystemuserusersSecuritysystemrolerole.Users, null))
                {
                    var valueResult = await CMUService.GetSecuritySystemUsers(filter: $"Oid eq {securitysystemuserusersSecuritysystemrolerole.Users}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        securitySystemUsersForUsersValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SecuritySystemUser" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await CMUService.UpdateSecuritysystemuserusersSecuritysystemrolerole(oid:OID, securitysystemuserusersSecuritysystemrolerole);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(securitysystemuserusersSecuritysystemrolerole);
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

            securitysystemuserusersSecuritysystemrolerole = await CMUService.GetSecuritysystemuserusersSecuritysystemroleroleByOid(oid:OID);
        }
    }
}